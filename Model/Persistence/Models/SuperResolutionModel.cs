using Model.Persistence.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Model.Persistence.Models
{
    public class SuperResolutionModel : MachineLearningModel
    {
        [Required]
        public UpscaleFactor UpscaleFactor { get; set; }

        [Required]
        public string Path { get; set; }
    }
}