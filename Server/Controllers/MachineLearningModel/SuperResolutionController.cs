using Business.Exceptions;
using Business.Services.SuperResolutionService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedData.SuperResolution;

namespace Server.Controllers.MachineLearningModel
{
    [ApiController]
    [Route("api/machine-learning-model/super-resolution/[action]")]
    public class SuperResolutionController : ControllerBase
    {
        private readonly ISuperResolutionService superResolutionService;

        public SuperResolutionController(ISuperResolutionService superResolutionService)
        {
            this.superResolutionService = superResolutionService;
        }

        [ActionName("details")]
        [HttpGet("{modelId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Details(int modelId)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, superResolutionService.GetDetails(modelId));
            }
            catch (ModelNotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [ActionName("upscale-image")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpscaleImage(ResolutionData resolutionData)
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created, superResolutionService.UpscaleImage(resolutionData));
            }
            catch (InternalErrorException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}