using System.Collections.Generic;

namespace DataTransfer.Input.Dataset
{
    public class NewDataset
    {
        public string Title { get; set; }
        public bool Public { get; set; }
        public IList<NewImage> Images { get; set; }
    }
}