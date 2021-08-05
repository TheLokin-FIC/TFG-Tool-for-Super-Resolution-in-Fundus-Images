using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Web.Components.Validation;

namespace Web.Components.Data.Dataset
{
    public class DatasetData
    {
        [Required(ErrorMessage = "Please enter a title for your dataset.")]
        [MinStringTrimLength(4, ErrorMessage = "Dataset title too short.")]
        [MaxStringTrimLength(24, ErrorMessage = "Dataset title too long.")]
        public string Title { get; set; }

        [Required]
        public bool Public { get; set; }

        [NotEmpty(ErrorMessage = "Please choose an image.")]
        [FileProcessed(ErrorMessage = "Images are loading.")]
        public IList<FileData> Images { get; set; }
    }
}