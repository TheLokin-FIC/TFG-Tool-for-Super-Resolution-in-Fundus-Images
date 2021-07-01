using Business.Exceptions;
using Business.Utils;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using Repository.DAOs.MachineLearningModelDAO;
using Repository.DAOs.SuperResolutionModelDAO;
using Repository.Exceptions;
using Repository.Persistence.Models;
using SharedData.SuperResolution;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Business.Services.SuperResolutionService
{
    public class SuperResolutionService : ISuperResolutionService
    {
        private readonly IMachineLearningModelDAO machineLearningModelDAO;
        private readonly ISuperResolutionModelDAO superResolutionModelDAO;
        private readonly IDictionary<Tuple<int, byte>, InferenceSession> sessions;

        public SuperResolutionService(IMachineLearningModelDAO machineLearningModelDAO, ISuperResolutionModelDAO superResolutionModelDAO)
        {
            this.machineLearningModelDAO = machineLearningModelDAO;
            this.superResolutionModelDAO = superResolutionModelDAO;
            sessions = new Dictionary<Tuple<int, byte>, InferenceSession>();
        }

        public ResolutionDetails GetDetails(int modelId)
        {
            try
            {
                MachineLearningModel machineLearningModel = machineLearningModelDAO.Find(modelId);

                IList<byte> upscaleFactors = new List<byte>();
                foreach (SuperResolutionModel superResolutionModel in superResolutionModelDAO.FindByModelId(modelId))
                {
                    upscaleFactors.Add(superResolutionModel.UpscaleFactor);
                }

                if (upscaleFactors.Any())
                {
                    return new ResolutionDetails()
                    {
                        Title = machineLearningModel.Name,
                        Subtitle = $"{machineLearningModel.Architecture} {machineLearningModel.Loss}",
                        Description = machineLearningModel.LongDescription,
                        UpscaleFactors = upscaleFactors
                    };
                }
                else
                {
                    throw new ModelNotFoundException(modelId);
                }
            }
            catch (InstanceNotFoundException)
            {
                throw new ModelNotFoundException(modelId);
            }
        }

        public byte[] UpscaleImage(int modelId, byte upscaleFactor, byte[] image)
        {
            try
            {
                Tensor<float> tensor = Converter.ConvertByteArrayToFloatTensor(image);
                IReadOnlyCollection<NamedOnnxValue> input = new List<NamedOnnxValue>
                {
                    NamedOnnxValue.CreateFromTensor("input", tensor)
                };

                InferenceSession session = GetSession(modelId, upscaleFactor);
                using IDisposableReadOnlyCollection<DisposableNamedOnnxValue> results = session.Run(input);

                Tensor<float> output = results.First().AsTensor<float>();

                return Converter.ConvertFloatTensorToBitmap(output);
            }
            catch (InstanceNotFoundException)
            {
                throw new SuperResolutionModelNotFoundException(modelId, upscaleFactor);
            }
            catch (OnnxRuntimeException e)
            {
                throw new InternalErrorException(e);
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
    }
}