using BookApi.Models;
using Microsoft.AspNetCore.Mvc;
using Nest; //we want to use MVC approach using Models at some point of time
namespace BookApi.Controllers;

[Route("api/[Controller]")] //--> gives https://localhost:port/api/books
[ApiController] //helps us with some core functionalities e.g. errors on the server
public class BooksController : ControllerBase
{
                //The old code -- works but not ES
        // private Book[] _books = new Book[]
        // {
        //         new Book{Id = 1, Author = "Author One", Title = "Book One"},
        //         new Book{Id = 2, Author = "Author Two", Title = "Book Two"},
        //         new Book{Id = 3, Author = "Author Three", Title = "Book Three"},
        // };
        //
        // [HttpGet]
        // public ActionResult<IEnumerable<Book>> GetBooks()
        // {
        //         return Ok(_books);
        // }
        
        private readonly IElasticClient _elasticClient;

        public BooksController(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }
        
        //POST: add a book
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            var response = await _elasticClient.IndexDocumentAsync(book);
            return Ok(response);
        }
        
        //GET : Search for books by title or get all
        [HttpGet]
        public async Task<IActionResult> GetBooks([FromQuery] string? title = null)
        {
            var response = await _elasticClient.SearchAsync<Book>(s => s
                .Query(q => q.Match(m => m
                    .Field(f=> f.Title)  // Search on the `Title` field
                    .Query(title ?? "")))); // The search term (from query string) -> it's mandatory to have it eventho you're not looking for a specific term
            return Ok(response);
        }
        
        
        //PUT : Update a book (by Id)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(string id, [FromBody] Book updatedbook)
        {
            var response = await _elasticClient.UpdateAsync<Book>(id, u => u.Doc(updatedbook));
            return Ok(response);
        }
        
        //DELETE : Remove a book
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(string id)
        {
            var response = await _elasticClient.DeleteAsync<Book>(id);
            return Ok(response);
        }
}