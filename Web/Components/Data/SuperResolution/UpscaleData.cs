using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Components.Data.SuperResolution
{
    public class UpscaleData
    {
        public IList<UpscaleFactorItem> UpscaleFactors { get; set; }

        [Required]
        public byte UpscaleFactor { get; set; }

        [Required(ErrorMessage = "Please choose an image.")]
        public byte[] Image { get; set; }
    }
}