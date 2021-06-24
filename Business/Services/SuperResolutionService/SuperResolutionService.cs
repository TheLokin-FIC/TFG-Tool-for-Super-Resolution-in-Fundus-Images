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

        public ModelDetails GetDetails(int modelId)
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
                    return new ModelDetails()
                    {
                        Name = machineLearningModel.Name,
                        Info = $"{machineLearningModel.Architecture} {machineLearningModel.Loss}",
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

        public byte[] UpscaleImage(ResolutionData resolutionData)
        {
            try
            {
                Tensor<float> tensor = Converter.ConvertByteArrayToFloatTensor(resolutionData.Image);
                IReadOnlyCollection<NamedOnnxValue> input = new List<NamedOnnxValue>
                {
                    NamedOnnxValue.CreateFromTensor("input", tensor)
                };

                InferenceSession session = GetSession(resolutionData.ModelId, resolutionData.UpscaleFactor);
                using IDisposableReadOnlyCollection<DisposableNamedOnnxValue> results = session.Run(input);

                Tensor<float> output = results.First().AsTensor<float>();

                return Converter.ConvertFloatTensorToBitmap(output);
            }
            catch (OnnxRuntimeException e)
            {
                throw new InternalErrorException(e);
            }
        }

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