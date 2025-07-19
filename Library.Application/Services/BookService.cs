using Library.Application.Interfaces;
using Library.Domain.Models;

namespace Library.Application.Services
{
    /// <summary>
    /// Implementation of the book service for managing books in the library
    /// </summary>
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        /// <summary>
        /// Initializes a new instance of the BookService class
        /// </summary>
        /// <param name="bookRepository">The book repository</param>
        /// <exception cref="ArgumentNullException">Thrown when bookRepository is null</exception>
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllBooksAsync();
        }

        /// <inheritdoc/>
        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _bookRepository.GetBookByIdAsync(id);
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Supports searching by title, author, and ISBN.
        /// If searchValue is empty, returns all books.
        /// If searchBy is not recognized, returns all books.
        /// </remarks>
        public async Task<IEnumerable<Book>> SearchBooksAsync(string searchBy, string searchValue)
        {
            if (string.IsNullOrWhiteSpace(searchValue))
                return await GetAllBooksAsync();

            return searchBy.ToLowerInvariant() switch
            {
                "title" => await _bookRepository.SearchBooksByTitleAsync(searchValue),
                "author" => await _bookRepository.SearchBooksByAuthorAsync(searchValue),
                "isbn" => await _bookRepository.SearchBooksByISBNAsync(searchValue),
                "status" => Enum.TryParse<OwnershipStatus>(searchValue, true, out var status) 
                    ? await _bookRepository.SearchBooksByOwnershipStatusAsync(status)
                    : throw new ArgumentException("Invalid ownership status"),
                _ => throw new ArgumentException("Invalid search criteria")
            };
        }

        /// <inheritdoc/>
        public async Task<Book> AddBookAsync(Book book)
        {
            return await _bookRepository.AddBookAsync(book);
        }

        /// <inheritdoc/>
        public async Task<Book> UpdateBookAsync(Book book)
        {
            return await _bookRepository.UpdateBookAsync(book);
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteBookAsync(int id)
        {
            return await _bookRepository.DeleteBookAsync(id);
        }

        /// <inheritdoc/>
        public async Task<PagedResult<Book>> GetAllBooksPagedAsync(int page = 1, int pageSize = 20)
        {
            return await _bookRepository.GetAllBooksPagedAsync(page, pageSize);
        }

        /// <inheritdoc/>
        public async Task<PagedResult<Book>> SearchBooksPagedAsync(string searchBy, string searchValue, int page = 1, int pageSize = 20)
        {
            if (string.IsNullOrWhiteSpace(searchValue))
                return await GetAllBooksPagedAsync(page, pageSize);

            return searchBy.ToLowerInvariant() switch
            {
                "title" => await _bookRepository.SearchBooksByTitlePagedAsync(searchValue, page, pageSize),
                "author" => await _bookRepository.SearchBooksByAuthorPagedAsync(searchValue, page, pageSize),
                "isbn" => await _bookRepository.SearchBooksByISBNPagedAsync(searchValue, page, pageSize),
                "status" => Enum.TryParse<OwnershipStatus>(searchValue, true, out var status) 
                    ? await _bookRepository.SearchBooksByOwnershipStatusPagedAsync(status, page, pageSize)
                    : throw new ArgumentException("Invalid ownership status"),
                _ => throw new ArgumentException("Invalid search criteria")
            };
        }
    }
}