using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Model.Services.SuperResolutionService;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Web.Pages.MachineLearningModel.SuperResolution
{
    public class IndexModel : PageModel
    {
        private readonly ISuperResolutionService superResolutionService;

        public byte[] ImageBytes;

        public IndexModel(ISuperResolutionService superResolutionService)
        {
            this.superResolutionService = superResolutionService;
        }

        public void OnPost(IFormFile file, string upscaleFactor)
        {
            if (file != null)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    using (Image image = Image.FromStream(stream))
                    {
                        Bitmap input = new Bitmap(image);
                        Bitmap output = superResolutionService.Upscale(input);

                        ImageBytes = ConvertBitmapToBytes(output);
                    }
                }
            }
        }

        private static byte[] ConvertBitmapToBytes(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Jpeg);

                return stream.ToArray();
            }
        }
    }
}