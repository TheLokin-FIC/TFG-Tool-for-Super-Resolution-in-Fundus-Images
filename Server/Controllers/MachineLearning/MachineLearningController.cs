using Business.Exceptions;
using Business.Services.MachineLearningService;
using Business.Services.SuperResolutionService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers.MachineLearning
{
    [ApiController]
    [Route("api/machine-learning-model")]
    public class MachineLearningController : Controller
    {
        private readonly IMachineLearningService machineLearningService;
        private readonly ISuperResolutionService superResolutionService;

        public MachineLearningController(IMachineLearningService machineLearningService, ISuperResolutionService superResolutionService)
        {
            this.machineLearningService = machineLearningService;
            this.superResolutionService = superResolutionService;
        }

        [HttpGet]
        [Route("{model}/super-resolution/details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Details(int model)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, superResolutionService.GetDetails(model));
            }
            catch (ModelNotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpPost]
        [Route("{model}/super-resolution/upscale/{factor}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Upscale(int model, byte factor, byte[] image)
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created, superResolutionService.UpscaleImage(model, factor, image));
            }
            catch (SuperResolutionModelNotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (InternalErrorException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("page/{size}/{index}/{search?}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Page(int size, int index, string search = "")
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, machineLearningService.GetPage(size, index, search));
            }
            catch (PageException)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }
    }
}