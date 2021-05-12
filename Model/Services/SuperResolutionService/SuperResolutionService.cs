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
        public Bitmap Upscale(Bitmap image)
        {
            // Converts the bitmap to a tensor
            Tensor<float> tensor = Converter.ConvertBitmapToFloatTensor(image);

            // Generate the input for the model
            IReadOnlyCollection<NamedOnnxValue> input = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("input", tensor)
            };

            // Create a session with the model and run it
            // Run the session with the model
            using (InferenceSession session = new InferenceSession(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Assets", "super_resolution_2x.onnx"), SessionOptions.MakeSessionOptionWithCudaProvider(0)))
            {
                IDisposableReadOnlyCollection<DisposableNamedOnnxValue> results = session.Run(input);

                // Get the output from the model
                Tensor<float> output = results.First().AsTensor<float>();

                // Converts the tensor to a bitmap and returns it
                return Converter.ConvertFloatTensorToBitmap(output);
            }
        }
    }
}