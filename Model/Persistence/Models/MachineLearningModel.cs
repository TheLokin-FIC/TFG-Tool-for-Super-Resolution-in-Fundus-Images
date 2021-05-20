using System.ComponentModel.DataAnnotations;

namespace Model.Persistence.Models
{
    public abstract class MachineLearningModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}