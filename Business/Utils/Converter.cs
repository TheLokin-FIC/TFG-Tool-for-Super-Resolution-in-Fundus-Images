using Microsoft.ML.OnnxRuntime.Tensors;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Business.Utils
{
    public static class Converter
    {
        public static Bitmap ConvertByteArrayToBitmap(byte[] bytes)
        {
            // Converts the byte array to a bitmap
            using MemoryStream stream = new(bytes);

            return new Bitmap(stream);
        }

        public static byte[] ConvertBitmapToByteArray(Bitmap bitmap)
        {
            // Converts the bitmap to a byte array
            using MemoryStream stream = new();
            bitmap.Save(stream, ImageFormat.Jpeg);

            return stream.ToArray();
        }

        public static Tensor<float> ConvertBitmapToFloatTensor(Bitmap bitmap)
        {
            // Create a tensor with the appropiate dimensions
            Tensor<float> tensor = new DenseTensor<float>(new[] { 1, 3, bitmap.Height, bitmap.Width });

            // Iterate over the bitmap and copy each pixel to the tensor
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color color = bitmap.GetPixel(x, y);

                    tensor[0, 0, y, x] = color.R / (float)255.0;
                    tensor[0, 1, y, x] = color.G / (float)255.0;
                    tensor[0, 2, y, x] = color.B / (float)255.0;
                }
            }

            return tensor;
        }

        public static Bitmap ConvertFloatTensorToBitmap(Tensor<float> tensor)
        {
            int[] dimensions = tensor.Dimensions.ToArray();
            int height = dimensions[2];
            int width = dimensions[3];

            // Create a bitmap with the appropiate dimensions
            Bitmap bitmap = new(width, height, PixelFormat.Format24bppRgb);

            // Iterate over the tensor and copy each pixel to the bitmap
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color c = Color.FromArgb((int)(tensor[0, 0, y, x] * 255.0), (int)(tensor[0, 1, y, x] * 255.0), (int)(tensor[0, 2, y, x] * 255.0));

                    bitmap.SetPixel(x, y, c);
                }
            }

            return bitmap;
        }
    }
}