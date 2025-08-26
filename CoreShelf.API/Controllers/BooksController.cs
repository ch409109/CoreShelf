using CoreShelf.Core.Entities;
using CoreShelf.Core.Interfaces;
using CoreShelf.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreShelf.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController(IGenericRepository<Book> repo) : ControllerBase
    {
        [HttpGet] //api/books
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return Ok(await repo.GetAllAsync());
        }

        [HttpGet("{id:int}")] //api/books/2
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await repo.GetByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(Book book)
        {
            repo.Add(book);

            if (await repo.SaveChangesAsync())
            {
                return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
            }

            return BadRequest("Cannot create this book");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateBook(int id, Book book)
        {
            if (book.Id != id || !BookExists(id))
            {
                return BadRequest("Cannot update this book");
            }

            repo.Update(book);

            if (await repo.SaveChangesAsync())
            {
                return NoContent();
            }

            return BadRequest("Cannot update this book");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var book = await repo.GetByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            repo.Delete(book);

            if (await repo.SaveChangesAsync())
            {
                return NoContent();
            }

            return BadRequest("Cannot delete this book");
        }

        private bool BookExists(int id)
        {
            return repo.Exists(id);
        }
    }
}
