using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Books_Crud_API.Models;

namespace Books_Crud_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BooksContext _context;

        public BooksController(BooksContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all books
        /// </summary>
        /// <returns>A list of all books</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        // GET: api/Books/5
        /// <summary>
        /// Get a specific book by id
        /// </summary>
        /// <param name="id">Identifier for the book</param>
        /// <returns>Requested book</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        /// <summary>
        /// Update a book
        /// </summary>
        /// <param name="id">Identifier for the book</param>
        /// <param name="book">Book with updated information</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            book.UpdatedDate = DateTime.UtcNow;
            _context.Entry(book).State = EntityState.Modified;

            try
            {
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Add a new book
        /// </summary>
        /// <param name="book">New book with info to be added.</param>
        /// <returns>Newly created book</returns>
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            // update timestamps
            book.CreatedDate = DateTime.UtcNow;
            book.UpdatedDate = DateTime.UtcNow;

            // Create and save to DB
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        /// <summary>
        /// Delete a book by id
        /// </summary>
        /// <param name="id">Identifier for the book</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
