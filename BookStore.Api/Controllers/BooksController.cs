using BookStore.Api.Models;
using BookStore.Api.Repositry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepositry _bookRepository;

        public BooksController(IBookRepositry bookRepository)
        {
            _bookRepository = bookRepository;
        }

        //Get All books
        [HttpGet("")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return Ok(books);
        }


        //Get Book by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById([FromRoute] int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            //error handling
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        //Add book to database
        [HttpPost("")]
        public async Task<IActionResult> AddNewBook([FromBody] BookModel book)
        {
            var id = await _bookRepository.AddBookAsync(book);
            return CreatedAtAction(nameof(GetBookById), new { id = id, controller = "books" }, id);
        }

        //Update book on database
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromBody] BookModel book, [FromRoute] int id)
        {

            await _bookRepository.UpdateBookAsync(id, book);
            return Ok();

        }

        //Update one item about book on database
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateBookPatch([FromBody] JsonPatchDocument book, [FromRoute] int id)
        {

            await _bookRepository.UpdateBookPatchAsync(id, book);
            return Ok();

        }

        //Delete item from database
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
          await  _bookRepository.DeleteBookAsync(id);
            return Ok();
        }

    }
}
