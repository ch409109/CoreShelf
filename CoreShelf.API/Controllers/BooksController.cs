using CoreShelf.Core.Entities;
using CoreShelf.Core.Interfaces;
using CoreShelf.Core.Specifications;
using CoreShelf.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreShelf.API.Controllers
{
    public class BooksController(IGenericRepository<Book> repo) : BaseApiController
    {
        [HttpGet] //api/books
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks([FromQuery] BookSpecParams specParams)
        {
            var spec = new BookSpecification(specParams);

            return await CreatePagedResult(repo, spec, specParams.PageIndex, specParams.PageSize);
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
                return CreatedAtAction("GetBook", new { id = book.Id }, book);
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

        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetCategories()
        {
            var spec = new CategoryListSpecification();

            return Ok(await repo.ListAsync(spec));
        }

        private bool BookExists(int id)
        {
            return repo.Exists(id);
        }
    }
}
