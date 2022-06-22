using Microsoft.AspNetCore.Mvc;
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
        public PublisherController(PublishersService publishersService)
        {
            _publishersService = publishersService;
        }

        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            try
            {
                var res = _publishersService.AddPublisher(publisher);
                return Created(nameof(AddPublisher), res);
            }
            catch(PublisherNameException ex)
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
