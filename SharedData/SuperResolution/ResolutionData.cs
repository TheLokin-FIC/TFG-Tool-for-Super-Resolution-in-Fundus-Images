namespace SharedData.SuperResolution
{
    public class ResolutionData
    {
        public int ModelId { get; set; }
        public byte UpscaleFactor { get; set; }
        public byte[] Image { get; set; }
    }
}