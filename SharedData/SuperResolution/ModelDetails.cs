using System.Collections.Generic;

namespace SharedData.SuperResolution
{
    public class ModelDetails
    {
        public string Name { get; set; }
        public string Info { get; set; }
        public IList<byte> UpscaleFactors { get; set; }
    }
}