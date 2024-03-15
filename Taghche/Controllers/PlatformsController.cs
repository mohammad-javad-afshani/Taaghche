using CacheService.Data;
using CacheService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CacheService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepo _repository;

        public BooksController(IBookRepo repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            return Ok(_repository.GetAllBooks());
        }

        [HttpGet("{id}", Name="GetBookById")]
        public ActionResult<IEnumerable<Book>> GetBookById(string id)
        {
            
            var Book = _repository.GetBookById(id);
            
            if (Book != null)
            {
                return Ok(Book);
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult <Book> CreateBook(Book Book)
        {
            _repository.CreateBook(Book);

            return CreatedAtRoute(nameof(GetBookById), new {Id = Book.Id}, Book);
        }
    }
}
