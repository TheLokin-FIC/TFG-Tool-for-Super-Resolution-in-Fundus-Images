using System.Collections.Generic;

namespace DataTransfer.Output.MachineLearning.SuperResolution
{
    public class RelatedResolutionModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IList<byte> UpscaleFactors { get; set; } = new List<byte>();
    }
}