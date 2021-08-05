using System.Collections.Generic;

namespace Web.Components.Data
{
    public class FileData
    {
        public string Name { get; set; }
        public double Size { get; set; }
        public int Percentage { get; set; }

        public byte[] File
        {
            get
            {
                return fileBytes.ToArray();
            }
            set
            {
                fileBytes.AddRange(value);
            }
        }

        private readonly List<byte> fileBytes = new();
    }
}