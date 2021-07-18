using System.Collections.Generic;

namespace Web.Components.Data.SuperResolution
{
    public class RelatedResolutionModelItem
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public bool Checked { get; set; }
        public IList<byte> UpscaleFactors { get; set; }
    }
}