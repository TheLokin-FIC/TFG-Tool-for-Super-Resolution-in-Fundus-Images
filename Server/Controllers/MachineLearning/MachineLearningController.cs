using Business.Exceptions;
using Business.Services.MachineLearningService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Server.Controllers.MachineLearning
{
    [ApiController]
    [Route("api/machine-learning-model")]
    public class MachineLearningController : Controller
    {
        private readonly IMachineLearningService machineLearningService;

        public MachineLearningController(IMachineLearningService machineLearningService)
        {
            this.machineLearningService = machineLearningService;
        }

        [HttpGet]
        [Route("models/{size}/{index}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetModelPage([FromRoute] int size, [FromRoute] int index, [FromQuery] string searchTerm)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, machineLearningService.GetModelPage(size, index, searchTerm));
            }
            catch (PageException)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        [HttpGet]
        [Route("super-resolution/details/{model}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetResolutionModelDetails([FromRoute] int model)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, machineLearningService.GetResolutionModelDetails(model));
            }
            catch (ModelNotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpPost]
        [Route("super-resolution/upscale/{model}/{factor}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpscaleImage([FromRoute] int model, [FromRoute] byte factor, [FromBody] byte[] image)
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created, machineLearningService.UpscaleImage(model, factor, image));
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

        [HttpPost]
        [Route("super-resolution/metrics/{model}/{factor}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GenerateDatasetMetrics([FromRoute] int model, [FromRoute] byte factor, [FromBody] IList<byte[]> dataset)
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created, machineLearningService.GenerateDatasetMetrics(model, factor, dataset));
            }
            catch (EmptyDatasetException)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
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
    }
}