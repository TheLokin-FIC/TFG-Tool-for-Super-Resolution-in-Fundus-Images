using System;
using System.Collections.Generic;

namespace Repository.Persistence.Models
{
    public class MachineLearningModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Architecture { get; set; }
        public string Loss { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public DateTime CreationDate { get; set; }

        public ICollection<SuperResolutionModel> SuperResolutionModels { get; set; }
    }
}