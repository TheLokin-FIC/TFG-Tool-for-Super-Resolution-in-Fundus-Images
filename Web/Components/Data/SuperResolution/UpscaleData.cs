using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Web.Components.Validation;

namespace Web.Components.Data.SuperResolution
{
    public class UpscaleData
    {
        public IList<UpscaleFactorItem> UpscaleFactors { get; set; }

        [Required]
        public byte UpscaleFactor { get; set; }

        [Required(ErrorMessage = "Please choose an image.")]
        [FileProcessed(ErrorMessage = "Image is loading.")]
        public FileData Image { get; set; }
    }
}