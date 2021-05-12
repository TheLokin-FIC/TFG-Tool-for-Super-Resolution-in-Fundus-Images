using System.Drawing;

namespace Model.Services.SuperResolutionService
{
    public interface ISuperResolutionService
    {
        Bitmap Upscale(Bitmap input);
    }
}