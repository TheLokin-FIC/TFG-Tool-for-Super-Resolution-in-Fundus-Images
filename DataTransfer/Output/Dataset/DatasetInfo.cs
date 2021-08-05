using System;

namespace DataTransfer.Output.Dataset
{
    public class DatasetInfo
    {
        public long Id { get; set; }
        public byte[] Cover { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public bool Public { get; set; }
        public double Size { get; set; }
    }
}