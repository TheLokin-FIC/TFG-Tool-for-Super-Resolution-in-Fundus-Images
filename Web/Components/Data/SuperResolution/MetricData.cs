using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Web.Components.Validation;

namespace Web.Components.Data.SuperResolution
{
    public class MetricData
    {
        public IList<UpscaleFactorItem> UpscaleFactors { get; set; }

        [Required]
        public byte UpscaleFactor { get; set; }

        [NotEmpty(ErrorMessage = "Please choose an image.")]
        public IList<byte[]> Images { get; set; }

        public IList<RelatedResolutionModelItem> RelatedResolutionModels { get; set; }

        public readonly string[] Metrics = { "MSE", "PSNR", "SSIM" };
        public string SelectedMetric { get; set; }
    }
}