using Library.Application.Interfaces;
using Library.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Library.API.Controllers
{
    /// <summary>
    /// Controller for managing books in the Royal Library
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [SwaggerTag("Book Management API")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        }

        /// <summary>
        /// Gets all books in the library
        /// </summary>
        /// <returns>A collection of all books</returns>
        /// <response code="200">Returns the list of books</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(
            Summary = "Gets all books",
            Description = "Retrieves a collection of all books in the library",
            OperationId = "GetAllBooks",
            Tags = new[] { "Books" })]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        /// <summary>
        /// Gets a specific book by its ID
        /// </summary>
        /// <param name="id">The ID of the book to retrieve</param>
        /// <returns>The requested book</returns>
        /// <response code="200">Returns the requested book</response>
        /// <response code="404">If the book is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Gets a book by ID",
            Description = "Retrieves a specific book by its unique identifier",
            OperationId = "GetBookById",
            Tags = new[] { "Books" })]
        public async Task<ActionResult<Book>> GetBookById([SwaggerParameter(Description = "The unique identifier of the book", Required = true)] int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        /// <summary>
        /// Searches for books based on specified criteria
        /// </summary>
        /// <param name="searchBy">The field to search by (Title, Author, ISBN, Status)</param>
        /// <param name="searchValue">The value to search for</param>
        /// <returns>A collection of books matching the search criteria</returns>
        /// <response code="200">Returns the matching books</response>
        /// <response code="400">If the search parameters are invalid</response>
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Searches for books",
            Description = "Searches for books based on specified criteria (Title, Author, ISBN, Status)",
            OperationId = "SearchBooks",
            Tags = new[] { "Books" })]
        public async Task<ActionResult<IEnumerable<Book>>> SearchBooks(
            [FromQuery, Required] 
            [SwaggerParameter(Description = "Field to search by (Title, Author, ISBN, Status)", Required = true)]
            string searchBy, 
            [FromQuery, Required] 
            [SwaggerParameter(Description = "Value to search for", Required = true)]
            string searchValue)
        {
            if (string.IsNullOrWhiteSpace(searchBy) || string.IsNullOrWhiteSpace(searchValue))
                return BadRequest("Search parameters cannot be empty");

            var books = await _bookService.SearchBooksAsync(searchBy, searchValue);
            return Ok(books);
        }

        /// <summary>
        /// Gets all books in the library with pagination
        /// </summary>
        /// <param name="page">The page number (1-based, default: 1)</param>
        /// <param name="pageSize">The number of items per page (default: 20, max: 100)</param>
        /// <returns>A paged result of books</returns>
        [HttpGet("paged")]
        public async Task<ActionResult<PagedResult<Book>>> GetAllBooksPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            try
            {
                // Validate pagination parameters
                if (page < 1) page = 1;
                if (pageSize < 1) pageSize = 20;
                if (pageSize > 100) pageSize = 100; // Limit max page size

                var pagedBooks = await _bookService.GetAllBooksPagedAsync(page, pageSize);
                return Ok(pagedBooks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Searches for books based on specified criteria with pagination
        /// </summary>
        /// <param name="searchBy">The field to search by (Title, Author, ISBN, Status)</param>
        /// <param name="searchValue">The value to search for</param>
        /// <param name="page">The page number (1-based, default: 1)</param>
        /// <param name="pageSize">The number of items per page (default: 20, max: 100)</param>
        /// <returns>A paged result of books matching the search criteria</returns>
        [HttpGet("search/paged")]
        public async Task<ActionResult<PagedResult<Book>>> SearchBooksPaged([FromQuery] string searchBy, [FromQuery] string searchValue, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            try
            {
                // Validate pagination parameters
                if (page < 1) page = 1;
                if (pageSize < 1) pageSize = 20;
                if (pageSize > 100) pageSize = 100; // Limit max page size

                var pagedBooks = await _bookService.SearchBooksPagedAsync(searchBy, searchValue, page, pageSize);
                return Ok(pagedBooks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Adds a new book to the library
        /// </summary>
        /// <param name="book">The book information to add</param>
        /// <returns>The newly created book</returns>
        /// <response code="201">Returns the newly created book</response>
        /// <response code="400">If the book data is invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Adds a new book",
            Description = "Adds a new book to the library collection",
            OperationId = "AddBook",
            Tags = new[] { "Books" })]
        public async Task<ActionResult<Book>> AddBook([FromBody] Book book)
        {
            if (book == null)
                return BadRequest("Book cannot be null");

            var createdBook = await _bookService.AddBookAsync(book);
            return CreatedAtAction(nameof(GetBookById), new { id = createdBook.Id }, createdBook);
        }

        /// <summary>
        /// Updates an existing book in the library
        /// </summary>
        /// <param name="id">The ID of the book to update</param>
        /// <param name="book">The updated book information</param>
        /// <returns>The updated book</returns>
        /// <response code="200">Returns the updated book</response>
        /// <response code="400">If the book data is invalid</response>
        /// <response code="404">If the book is not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Updates an existing book",
            Description = "Updates an existing book's information in the library",
            OperationId = "UpdateBook",
            Tags = new[] { "Books" })]
        public async Task<ActionResult<Book>> UpdateBook(int id, [FromBody] Book book)
        {
            if (book == null || id != book.Id)
                return BadRequest("Invalid book data");

            var existingBook = await _bookService.GetBookByIdAsync(id);
            if (existingBook == null)
                return NotFound();

            var updatedBook = await _bookService.UpdateBookAsync(book);
            return Ok(updatedBook);
        }

        /// <summary>
        /// Deletes a book from the library
        /// </summary>
        /// <param name="id">The ID of the book to delete</param>
        /// <returns>No content if successful</returns>
        /// <response code="204">If the book was successfully deleted</response>
        /// <response code="404">If the book is not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Deletes a book",
            Description = "Removes a book from the library collection",
            OperationId = "DeleteBook",
            Tags = new[] { "Books" })]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBookAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}