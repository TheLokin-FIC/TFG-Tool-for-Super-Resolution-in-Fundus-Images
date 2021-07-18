namespace DataTransfer.Output.MachineLearning.SuperResolution
{
    public class ImageMetrics
    {
        public double MSE { get; set; }
        public double PSNR { get; set; }
        public double SSIM { get; set; }
        public byte[] Image { get; set; }
    }
}