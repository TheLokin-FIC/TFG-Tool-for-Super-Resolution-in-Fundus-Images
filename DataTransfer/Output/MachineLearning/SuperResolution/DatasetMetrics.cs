using System.Collections.Generic;

namespace DataTransfer.Output.MachineLearning.SuperResolution
{
    public class DatasetMetrics
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double MSE { get; set; }
        public double PSNR { get; set; }
        public double SSIM { get; set; }
        public IList<ImageMetrics> ImageMetrics { get; set; }
    }
}