using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using Model.Utils;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Model.Services.SuperResolutionService
{
    public class SuperResolutionService : ISuperResolutionService
    {
        private readonly InferenceSession session;

        public SuperResolutionService()
        {
            // Configure the model
            SessionOptions options = new SessionOptions();
            options.AppendExecutionProvider_CUDA(0);

            // Create a session with the model
            session = new InferenceSession(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Assets", "super_resolution_2x.onnx"), options);
        }

        public Bitmap Upscale(Bitmap image)
        {
            // Converts the bitmap to a tensor
            Tensor<float> tensor = Converter.ConvertBitmapToFloatTensor(image);

            // Generate the input for the model
            IReadOnlyCollection<NamedOnnxValue> input = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("input", tensor)
            };

            // Run the model
            using (IDisposableReadOnlyCollection<DisposableNamedOnnxValue> results = session.Run(input))
            {
                // Get the output from the model
                Tensor<float> output = results.First().AsTensor<float>();

                // Converts the tensor to a bitmap and returns it
                return Converter.ConvertFloatTensorToBitmap(output);
            }
        }
    }
}