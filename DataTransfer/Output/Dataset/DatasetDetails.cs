using System;
using System.Collections.Generic;

namespace DataTransfer.Output.Dataset
{
    public class DatasetDetails
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public bool Public { get; set; }
        public double Size { get; set; }
        public IList<ImageDetails> Images { get; set; }
    }
}