using System;

namespace DataTransfer.Output.MachineLearning
{
    public class ModelInfo
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}