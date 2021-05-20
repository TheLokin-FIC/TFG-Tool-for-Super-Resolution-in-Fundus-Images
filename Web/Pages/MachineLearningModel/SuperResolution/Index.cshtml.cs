using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.ML.OnnxRuntime;
using Model.Services.SuperResolutionService;
using Model.Services.SuperResolutionService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Web.Pages.MachineLearningModel.SuperResolution
{
    public class IndexModel : PageModel
    {
        private readonly ISuperResolutionService superResolutionService;

        public IList<SuperResolutionModelDetails> SuperResolutionModelDetails { get; private set; }

        [BindProperty]
        public int ModelId { get; set; }

        [BindProperty, Required(ErrorMessage = "Please choose an image.")]
        public IFormFile InputFile { get; set; }

        public string ImageByte64 { get; set; }

        public IndexModel(ISuperResolutionService superResolutionService)
        {
            this.superResolutionService = superResolutionService;
            SuperResolutionModelDetails = superResolutionService.FindUpscaleFactors("Super Resolution SRResNet SSIM");
            ModelId = SuperResolutionModelDetails[0].Id;
        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    InputFile.CopyTo(stream);
                    using (Image image = Image.FromStream(stream))
                    {
                        try
                        {
                            Bitmap input = new Bitmap(image);
                            Bitmap output = superResolutionService.UpscaleImage(ModelId, input);

                            ImageByte64 = string.Format("data:image/jpeg;base64,{0}", Convert.ToBase64String(ConvertBitmapToBytes(output)));
                        }
                        catch (OnnxRuntimeException)
                        {
                            ModelState.AddModelError("InputFile", "An error occurred on the server while processing the image.");
                        }
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