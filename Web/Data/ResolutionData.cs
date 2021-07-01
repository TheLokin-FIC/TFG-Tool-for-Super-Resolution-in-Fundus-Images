using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Data
{
    public class ResolutionData
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public IList<byte> UpscaleFactors { get; set; }

        [Required]
        public byte UpscaleFactor { get; set; }

        [Required(ErrorMessage = "Please choose an image.")]
        public byte[] InputFile { get; set; }

        public byte[] OutputFile { get; set; }

        public ResolutionData()
        {
            UpscaleFactors = new List<byte>();
        }
    }
}