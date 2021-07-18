using System.Collections.Generic;

namespace DataTransfer.Output.MachineLearning.SuperResolution
{
    public class ResolutionModelDetails
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IList<byte> UpscaleFactors { get; set; }
        public IList<RelatedResolutionModel> RelatedResolutionModels { get; set; }
    }
}