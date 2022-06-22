using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;
using my_books.Data.ViewModels;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private AuthorsService _authorsService;
        public AuthorController(AuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        [HttpPost("add-author")]
        public IActionResult AddAuthor([FromBody] AuthorVM author)
        {
            _authorsService.AddAuthor(author);
            return Ok();
        }

        [HttpGet("get-authors-with-books-by-id/{bookId}")]
        public IActionResult GetAuthorsWithBooks(int bookId)
        {
            var res = _authorsService.GetAuthorWithBooksVM(bookId);
            return Ok(res);
        }
    }
}
