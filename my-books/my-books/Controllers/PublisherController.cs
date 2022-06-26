using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using my_books.Data.Services;
using my_books.Data.ViewModels;
using my_books.Exceptions;
using System;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private PublishersService _publishersService;
        private readonly ILogger<PublisherController> _logger;
        public PublisherController(PublishersService publishersService, ILogger<PublisherController> logger)
        {
            _publishersService = publishersService;
            _logger = logger;
        }

        [HttpGet("get-all-publishers")]
        public IActionResult GetAllPublishers(string sortBy, string searchString, int pageNumber)
        {
            try
            {
                var res = _publishersService.GetAllPublishers(sortBy, searchString, pageNumber);
                return Ok(res);
            }
            catch (Exception)
            {
                return BadRequest("Sorry, we could not load the publishers");
            }
        }

        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            try
            {
                var res = _publishersService.AddPublisher(publisher);
                return Created(nameof(AddPublisher), res);
            }
            catch (PublisherNameException ex)
            {
                return BadRequest($"{ex.Message}, Publisher Name: {ex.PublisherName}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-publisher-by-id/{publisherId}")]
        public IActionResult GetPublisherById(int publisherId)
        {
            try
            {
                var res = _publishersService.GetPublisherById(publisherId);

                if (res != null)
                {
                    return Ok(res);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-publisher-books-with-authors/{publisherId}")]
        public IActionResult GetPublisherData(int publisherId)
        {
            try
            {
                var res = _publishersService.GetPublisherData(publisherId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-publisher-by-id/{publisherId}")]
        public IActionResult DeletePublisherById(int publisherId)
        {
            try
            {
                _publishersService.DeletePublisherById(publisherId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
