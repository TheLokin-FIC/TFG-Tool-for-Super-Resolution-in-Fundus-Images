using Business.Exceptions;
using Business.Utils;
using DataTransfer.Output.Collections;
using DataTransfer.Output.MachineLearning;
using DataTransfer.Output.MachineLearning.SuperResolution;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using Repository.DAOs.Collections;
using Repository.DAOs.MachineLearningModelDAO;
using Repository.DAOs.SuperResolutionModelDAO;
using Repository.Exceptions;
using Repository.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Business.Services.MachineLearningService
{
    public class MachineLearningService : IMachineLearningService
    {
        private const int NearestNeighbor = -3;
        private const int Bilinear = -2;
        private const int Bicubic = -1;

        private readonly IMachineLearningModelDAO machineLearningModelDAO;
        private readonly ISuperResolutionModelDAO superResolutionModelDAO;
        private readonly IDictionary<Tuple<int, byte>, InferenceSession> sessions;

        public MachineLearningService(IMachineLearningModelDAO machineLearningModelDAO, ISuperResolutionModelDAO superResolutionModelDAO)
        {
            this.machineLearningModelDAO = machineLearningModelDAO;
            this.superResolutionModelDAO = superResolutionModelDAO;
            sessions = new Dictionary<Tuple<int, byte>, InferenceSession>();
        }

        public Page<ModelDetails> ModelPage(int pageSize, int pageIndex, string searchTerm)
        {
            try
            {
                PageList<MachineLearningModel> pageList = machineLearningModelDAO.FindPageByTerm(pageSize, pageIndex - 1, searchTerm);

                Page<ModelDetails> page = new()
                {
                    PageSize = pageList.PageSize,
                    PageIndex = pageList.PageIndex + 1,
                    TotalPages = pageList.TotalPages,
                    Items = new List<ModelDetails>(),
                    HasPreviousPage = pageList.HasPreviousPage,
                    HasNextPage = pageList.HasNextPage
                };

                foreach (MachineLearningModel machineLearningModel in pageList.Items)
                {
                    page.Items.Add(new ModelDetails()
                    {
                        Id = machineLearningModel.Id,
                        Category = machineLearningModel.Name,
                        Title = $"{machineLearningModel.Architecture} {machineLearningModel.Loss}",
                        Description = machineLearningModel.ShortDescription,
                        Date = machineLearningModel.CreationDate
                    });
                }

                return page;
            }
            catch (Exception e) when (e is PageSizeException || e is PageIndexException)
            {
                throw new PageException(e);
            }
        }

        public ResolutionModelDetails ResolutionModelDetails(int modelId)
        {
            try
            {
                MachineLearningModel machineLearningModel = machineLearningModelDAO.Find(modelId);

                IList<byte> upscaleFactors = GetUpscaleFactors(modelId);
                if (upscaleFactors.Any())
                {
                    ResolutionModelDetails resolutionDetails = new()
                    {
                        Id = machineLearningModel.Id,
                        Category = machineLearningModel.Name,
                        Title = $"{machineLearningModel.Architecture} {machineLearningModel.Loss}",
                        Description = machineLearningModel.LongDescription,
                        UpscaleFactors = upscaleFactors,
                        RelatedResolutionModels = new List<RelatedResolutionModel>()
                    };

                    foreach (MachineLearningModel model in machineLearningModelDAO.FindByName("Super Resolution"))
                    {
                        if (model.Id != modelId)
                        {
                            resolutionDetails.RelatedResolutionModels.Add(new RelatedResolutionModel()
                            {
                                Id = model.Id,
                                Title = $"{model.Architecture} {model.Loss}",
                                UpscaleFactors = GetUpscaleFactors(model.Id)
                            });
                        }
                    }

                    return resolutionDetails;
                }
                else
                {
                    throw new NotFoundException(modelId);
                }
            }
            catch (InstanceNotFoundException)
            {
                throw new NotFoundException(modelId);
            }
        }

        public byte[] UpscaleImage(int modelId, byte upscaleFactor, byte[] image)
        {
            try
            {
                Bitmap input = Converter.ConvertByteArrayToBitmap(image);
                Bitmap output = Resize(modelId, upscaleFactor, input);

                return Converter.ConvertBitmapToByteArray(output);
            }
            catch (InstanceNotFoundException)
            {
                throw new NotFoundException(modelId, upscaleFactor);
            }
            catch (OnnxRuntimeException e)
            {
                throw new InternalErrorException(e);
            }
        }

        public DatasetMetrics GenerateDatasetMetrics(int modelId, byte upscaleFactor, IList<byte[]> dataset)
        {
            try
            {
                if (dataset.Any())
                {
                    DatasetMetrics datasetMetrics = new()
                    {
                        ImageMetrics = new List<ImageMetrics>()
                    };
                    switch (modelId)
                    {
                        case NearestNeighbor:
                            datasetMetrics = new()
                            {
                                Id = NearestNeighbor,
                                Title = "Nearest Neighbor",
                                ImageMetrics = new List<ImageMetrics>()
                            };
                            break;

                        case Bilinear:
                            datasetMetrics = new()
                            {
                                Id = Bilinear,
                                Title = "Bilinear",
                                ImageMetrics = new List<ImageMetrics>()
                            };
                            break;

                        case Bicubic:
                            datasetMetrics = new()
                            {
                                Id = Bicubic,
                                Title = "Bicubic",
                                ImageMetrics = new List<ImageMetrics>()
                            };
                            break;

                        default:
                            MachineLearningModel machineLearningModel = machineLearningModelDAO.Find(modelId);
                            datasetMetrics = new()
                            {
                                Id = machineLearningModel.Id,
                                Title = $"{machineLearningModel.Architecture} {machineLearningModel.Loss}",
                                ImageMetrics = new List<ImageMetrics>()
                            };
                            break;
                    }

                    foreach (byte[] image in dataset)
                    {
                        Bitmap reference = Converter.ConvertByteArrayToBitmap(image);
                        Bitmap input = ResizeBitmap(reference, reference.Width / upscaleFactor, reference.Height / upscaleFactor, InterpolationMode.Bicubic);
                        Bitmap compare = Resize(modelId, upscaleFactor, input);

                        datasetMetrics.ImageMetrics.Add(
                            new ImageMetrics()
                            {
                                Image = Converter.ConvertBitmapToByteArray(compare),
                                MSE = Math.Round(Metric.MSE(reference, compare), 4),
                                PSNR = Math.Round(Metric.PSNR(reference, compare), 4),
                                SSIM = Math.Round(Metric.SSIM(reference, compare), 4)
                            }
                        );
                    }
                    datasetMetrics.MSE = Math.Round(datasetMetrics.ImageMetrics.Average(x => x.MSE), 4);
                    datasetMetrics.PSNR = Math.Round(datasetMetrics.ImageMetrics.Average(x => x.PSNR), 4);
                    datasetMetrics.SSIM = Math.Round(datasetMetrics.ImageMetrics.Average(x => x.SSIM), 4);

                    return datasetMetrics;
                }
                else
                {
                    throw new ArgumentException("Dataset can not be empty");
                }
            }
            catch (InstanceNotFoundException)
            {
                throw new NotFoundException(modelId, upscaleFactor);
            }
            catch (OnnxRuntimeException e)
            {
                throw new InternalErrorException(e);
            }
        }

        private IList<byte> GetUpscaleFactors(int modelId)
        {
            IList<byte> upscaleFactors = new List<byte>();
            foreach (SuperResolutionModel superResolutionModel in superResolutionModelDAO.FindByModelId(modelId))
            {
                upscaleFactors.Add(superResolutionModel.UpscaleFactor);
            }

            return upscaleFactors;
        }

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="OnnxRuntimeException"/>
        private Bitmap Resize(int modelId, byte upscaleFactor, Bitmap bitmap)
        {
            switch (modelId)
            {
                case NearestNeighbor:
                    return ResizeBitmap(bitmap, bitmap.Width * upscaleFactor, bitmap.Height * upscaleFactor, InterpolationMode.NearestNeighbor);

                case Bilinear:
                    return ResizeBitmap(bitmap, bitmap.Width * upscaleFactor, bitmap.Height * upscaleFactor, InterpolationMode.Bilinear);

                case Bicubic:
                    return ResizeBitmap(bitmap, bitmap.Width * upscaleFactor, bitmap.Height * upscaleFactor, InterpolationMode.Bicubic);

                default:
                    Tensor<float> tensor = Converter.ConvertBitmapToFloatTensor(bitmap);
                    IReadOnlyCollection<NamedOnnxValue> input = new List<NamedOnnxValue>
                    {
                        NamedOnnxValue.CreateFromTensor("input", tensor)
                    };

                    InferenceSession session = GetSession(modelId, upscaleFactor);

                    using (IDisposableReadOnlyCollection<DisposableNamedOnnxValue> results = session.Run(input))
                    {
                        Tensor<float> output = results.First().AsTensor<float>();

                        return Converter.ConvertFloatTensorToBitmap(output);
                    }
            }
        }

        /// <exception cref="InstanceNotFoundException"/>
        private InferenceSession GetSession(int modelId, byte upscaleFactor)
        {
            if (sessions.TryGetValue(Tuple.Create(modelId, upscaleFactor), out InferenceSession session))
            {
                return session;
            }
            else
            {
                SuperResolutionModel superResolutionModel = superResolutionModelDAO.Find(modelId, upscaleFactor);
                session = new InferenceSession(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), superResolutionModel.Path));
                sessions.TryAdd(Tuple.Create(modelId, upscaleFactor), session);

                return session;
            }
        }

        private static Bitmap ResizeBitmap(Bitmap input, int width, int height, InterpolationMode interpolation)
        {
            Bitmap output = new(width, height, PixelFormat.Format24bppRgb);
            using (Graphics graphics = Graphics.FromImage(output))
            {
                graphics.InterpolationMode = interpolation;
                graphics.DrawImage(input, 0, 0, width + 1, height + 1);
            }

            return output;
        }
    }
}