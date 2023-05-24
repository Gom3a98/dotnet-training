using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models;

namespace OnlineStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AuthorController : ControllerBase
    {
        private readonly BookstoresdbContext _context;

        public AuthorController(BookstoresdbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable<Author> Get()
        {
            return _context.Authors.ToList();
        }

    }
}
