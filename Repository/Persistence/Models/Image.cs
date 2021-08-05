namespace Repository.Persistence.Models
{
    public class Image
    {
        public long DatasetId { get; set; }
        public string Name { get; set; }
        public double Size { get; set; }
        public byte[] File { get; set; }

        public Dataset Dataset { get; set; }
    }
}