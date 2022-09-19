using Class3_Homework2.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Class3_Homework2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        [HttpGet]
        public ActionResult<List<Book>> GetAll()
        {
            try
            {
                return Ok(StaticDb.Books);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        [HttpGet("getById")]
        public ActionResult<Book> GetById(int id)
        {
            try
            {
                if (id < 0)
                {
                    return BadRequest("The index can not be negative!");
                }

                var book = StaticDb.Books.FirstOrDefault(book => book.Id == id);

                if (book == null)
                {
                    return NotFound($"A Book with id: {id} does not exist!");
                }

                return Ok(book);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        [HttpGet("getBook")]
        public ActionResult<Book> GetByTitleandAuthor(string? author, string? title)
        {
            try
            {
                if (author == null || title == null)
                {
                    return BadRequest("Title nad Author are required!");
                }

                var book = StaticDb.Books.FirstOrDefault(book => book.Author == author && book.Title == title);

                if (book == null)
                {
                    return NotFound($"A Book with Author: {author} and Title: {title} does not exist!");
                }

                return Ok(book);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        [HttpPost("addBook")]
        public ActionResult<string> Post([FromBody] BookDto bookDto)
        {
            try
            {
                var bookDb = StaticDb.Books.FirstOrDefault(x => x.Author == bookDto.Author && x.Title == bookDto.Title);

                if (bookDb != null)
                {
                    return NotFound($"A Book with Author: {bookDto.Author} and Title: {bookDto.Title} already exsists!");
                }

                int newBookId = 1;
                Book lastBook = StaticDb.Books.LastOrDefault();
                if(lastBook != null)
                {
                    newBookId = lastBook.Id + 1;
                }

                Book newBook = new Book()
                {
                    Id = newBookId,
                    Author = bookDto.Author,
                    Title = bookDto.Title,
                };

                StaticDb.Books.Add(newBook);

                return StatusCode(StatusCodes.Status201Created, "Book Added");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        [HttpPost("getBookTitles")]
        public ActionResult<List<string>> GetBookTitles([FromBody] List<BookDto> bookDto)
        {
            try
            {
                List<string> bookTitles = new List<string>();

                bookDto.ForEach(book =>
                {
                    bookTitles.Add(book.Title);
                });

                return Ok(bookTitles);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }


    }
}
