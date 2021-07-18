using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSharp.Quality;
using System.Drawing;

namespace Business.Utils
{
    public static class Metric
    {
        public static double MSE(Bitmap reference, Bitmap compare)
        {
            InputArray referenceArray = InputArray.Create(BitmapConverter.ToMat(reference));
            InputArray compareArray = InputArray.Create(BitmapConverter.ToMat(compare));
            Scalar scalar = QualityMSE.Compute(referenceArray, compareArray, null);

            return (scalar[0] + scalar[1] + scalar[2]) / 3;
        }

        public static double PSNR(Bitmap reference, Bitmap compare)
        {
            Scalar scalar = QualityPSNR.Compute(InputArray.Create(BitmapConverter.ToMat(reference)), InputArray.Create(BitmapConverter.ToMat(compare)), null);

            return (scalar[0] + scalar[1] + scalar[2]) / 3;
        }

        public static double SSIM(Bitmap reference, Bitmap compare)
        {
            Scalar scalar = QualitySSIM.Compute(InputArray.Create(BitmapConverter.ToMat(reference)), InputArray.Create(BitmapConverter.ToMat(compare)), null);

            return (scalar[0] + scalar[1] + scalar[2]) / 3;
        }
    }
}