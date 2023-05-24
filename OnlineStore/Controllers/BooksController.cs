
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;
using OnlineStore.Rsponses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Castle.Core.Resource;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;
using OnlineStore.Requests;

namespace OnlineStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookstoresdbContext DB;
        public static IWebHostEnvironment _environment;
        public BooksController(BookstoresdbContext context , IWebHostEnvironment environment)
        {
            DB = context;
            _environment = environment;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
          if (DB.Books == null)
          {
              return NotFound();
          }
            return await DB.Books.Include(a=>a.Pub).ToListAsync();
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
          if (DB.Books == null)
          {
              return NotFound();
          }
            
            var book = await DB.Books.Include(a => a.Pub).Where(a => a.BookId == id).FirstAsync();
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }
        
        // POST: api/Books/UploadImg
        [HttpPost("UploadImg")]
        public async Task<ActionResult<Book>> UploadBookImage([FromForm] FileUploadRequest objFile)
        {
            Book book = await DB.Books.FindAsync(objFile.BookId);
            try
            {
                string FileName = "";

                if (objFile.ImageType == 1)
                {
                    FileName = "\\Upload\\front_" + objFile.Files.FileName;
                    book.FrontCover = FileName;
                }
                else
                {
                    FileName = "\\Upload\\back_" + objFile.Files.FileName;
                    book.BackCover = FileName;
                }

                if (objFile.Files.Length > 0)
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\Upload"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
                    }
                    using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + FileName))
                    {
                        objFile.Files.CopyTo(filestream);
                        filestream.Flush();
                        DB.Books.Update(book);
                        DB.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Ok(book);
        }
        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.BookId)
            {
                return BadRequest();
            }

            DB.Entry(book).State = EntityState.Modified;

            try
            {
                await DB.SaveChangesAsync();
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

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
          if (DB.Books == null)
          {
              return Problem("Entity set 'BookstoresdbContext.Books'  is null.");
          }
            DB.Books.Add(book);
            await DB.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.BookId }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (DB.Books == null)
            {
                return NotFound();
            }   
            var book = await DB.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            DB.Books.Remove(book);
            await DB.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return (DB.Books?.Any(e => e.BookId == id)).GetValueOrDefault();
        }
    }
}
