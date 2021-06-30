using System.Collections.Generic;

namespace SharedData.SuperResolution
{
    public class ResolutionDetails
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public IList<byte> UpscaleFactors { get; set; }
    }
}