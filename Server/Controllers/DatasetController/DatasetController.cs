using Business.Exceptions;
using Business.Services.DatasetService;
using DataTransfer.Input.Dataset;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Server.Controllers.DatasetController
{
    [ApiController]
    [Route("api/datasets")]
    public class DatasetController : Controller
    {
        private readonly IDatasetService datasetService;

        public DatasetController(IDatasetService datasetService)
        {
            this.datasetService = datasetService;
        }

        [HttpGet]
        [Route("page")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetDatasetPage([FromQuery] int pageSize, [FromQuery] int pageIndex, [FromQuery] string searchTerm)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, datasetService.GetDatasetPage(pageSize, pageIndex, searchTerm, null));
            }
            catch (PageException)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        [HttpGet]
        [Route("page/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetUserDatasetPage([FromRoute] long userId, [FromQuery] int pageSize, [FromQuery] int pageIndex, [FromQuery] string searchTerm)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, datasetService.GetDatasetPage(pageSize, pageIndex, searchTerm, userId));
            }
            catch (PageException)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        [HttpPost]
        [Route("create/{userId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult CreateDataset([FromRoute] long userId, [FromBody] NewDataset dataset)
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created, datasetService.CreateDataset(userId, dataset));
            }
            catch (AuthenticationException)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            catch (ArgumentException)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        [HttpGet]
        [Route("details/{datasetId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetDataset([FromRoute] long datasetId)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, datasetService.GetDataset(datasetId, null));
            }
            catch (NotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (AuthenticationException)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }

        [HttpGet]
        [Route("details/{datasetId}/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetDataset([FromRoute] long datasetId, [FromRoute] long userId)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, datasetService.GetDataset(datasetId, userId));
            }
            catch (NotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (AuthenticationException)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }

        [HttpGet]
        [Route("images/{datasetId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetImagesDataset([FromRoute] long datasetId)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, datasetService.GetImagesDataset(datasetId, null));
            }
            catch (NotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (AuthenticationException)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }

        [HttpGet]
        [Route("images/{datasetId}/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetImagesDataset([FromRoute] long datasetId, [FromRoute] long userId)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, datasetService.GetImagesDataset(datasetId, userId));
            }
            catch (NotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (AuthenticationException)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }

        [HttpPut]
        [Route("update/{datasetId}/{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult UpdateDataset([FromRoute] long datasetId, [FromRoute] long userId, [FromBody] NewDataset dataset)
        {
            try
            {
                datasetService.UpdateDataset(datasetId, userId, dataset);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (AuthenticationException)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            catch (NotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpDelete]
        [Route("delete/{datasetId}/{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult DeleteDataset([FromRoute] long datasetId, [FromRoute] long userId)
        {
            try
            {
                datasetService.DeleteDataset(datasetId, userId);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (AuthenticationException)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            catch (NotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }
    }
}