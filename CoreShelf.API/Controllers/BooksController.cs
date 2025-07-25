using CoreShelf.Core.Entities;
using CoreShelf.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreShelf.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController(CoreShelfDbContext context) : ControllerBase
    {
        [HttpGet] //api/books
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await context.Books.ToListAsync();
        }

        [HttpGet("{id:int}")] //api/books/2
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(Book book)
        {
            context.Books.Add(book);

            await context.SaveChangesAsync();

            return book;
        }
    }
}
