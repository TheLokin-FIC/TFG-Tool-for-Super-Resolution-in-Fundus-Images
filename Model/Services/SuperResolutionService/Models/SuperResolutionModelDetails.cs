using System;

namespace Model.Services.SuperResolutionService.Models
{
    [Serializable]
    public class SuperResolutionModelDetails
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public byte UpscaleFactor { get; private set; }

        public SuperResolutionModelDetails(int id, string name, byte upscaleFactor)
        {
            Id = id;
            Name = name;
            UpscaleFactor = upscaleFactor;
        }
    }
}