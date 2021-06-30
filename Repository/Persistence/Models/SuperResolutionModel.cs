namespace Repository.Persistence.Models
{
    public class SuperResolutionModel
    {
        public int ModelId { get; set; }
        public byte UpscaleFactor { get; set; }
        public string Path { get; set; }

        public MachineLearningModel MachineLearningModel { get; set; }
    }
}