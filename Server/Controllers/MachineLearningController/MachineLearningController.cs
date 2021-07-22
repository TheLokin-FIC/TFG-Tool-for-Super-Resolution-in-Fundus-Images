using Business.Exceptions;
using Business.Services.MachineLearningService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Server.Controllers.MachineLearningController
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
        [Route("models")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ModelPage([FromQuery] int pageSize, [FromQuery] int pageIndex, [FromQuery] string searchTerm)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, machineLearningService.ModelPage(pageSize, pageIndex, searchTerm));
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
        public IActionResult ResolutionModelDetails([FromRoute] int model)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, machineLearningService.ResolutionModelDetails(model));
            }
            catch (NotFoundException)
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
            catch (NotFoundException)
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
            catch (ArgumentException)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (NotFoundException)
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