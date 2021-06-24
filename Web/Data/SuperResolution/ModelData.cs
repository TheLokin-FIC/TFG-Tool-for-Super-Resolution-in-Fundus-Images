using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Data.SuperResolution
{
    public class ModelData
    {
        public string Name { get; set; }
        public string Info { get; set; }
        public IList<byte> UpscaleFactors { get; set; }

        [Required]
        public byte UpscaleFactor { get; set; }

        [Required(ErrorMessage = "Please choose an image.")]
        public byte[] InputFile { get; set; }

        public byte[] OutputFile { get; set; }

        public ModelData()
        {
            UpscaleFactors = new List<byte>();
        }
    }
}