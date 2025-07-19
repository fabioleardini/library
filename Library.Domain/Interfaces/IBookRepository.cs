using Library.Domain.Models;

namespace Library.Domain.Interfaces
{
    /// <summary>
    /// Repository interface for book data access operations
    /// </summary>
    public interface IBookRepository
    {
        /// <summary>
        /// Gets all books from the repository
        /// </summary>
        /// <returns>A collection of all books</returns>
        Task<IEnumerable<Book>> GetAllBooksAsync();
        
        /// <summary>
        /// Gets a specific book by its ID
        /// </summary>
        /// <param name="id">The ID of the book to retrieve</param>
        /// <returns>The requested book or null if not found</returns>
        Task<Book?> GetBookByIdAsync(int id);
        
        /// <summary>
        /// Searches for books by title
        /// </summary>
        /// <param name="title">The title to search for</param>
        /// <returns>A collection of books matching the title</returns>
        Task<IEnumerable<Book>> SearchBooksByTitleAsync(string title);
        
        /// <summary>
        /// Searches for books by author name
        /// </summary>
        /// <param name="author">The author name to search for</param>
        /// <returns>A collection of books matching the author</returns>
        Task<IEnumerable<Book>> SearchBooksByAuthorAsync(string author);
        
        /// <summary>
        /// Searches for books by ISBN
        /// </summary>
        /// <param name="isbn">The ISBN to search for</param>
        /// <returns>A collection of books matching the ISBN</returns>
        Task<IEnumerable<Book>> SearchBooksByISBNAsync(string isbn);
        

        
        /// <summary>
        /// Gets all books with pagination
        /// </summary>
        /// <param name="page">The page number (1-based)</param>
        /// <param name="pageSize">The number of items per page</param>
        /// <returns>A paginated result of books</returns>
        Task<PagedResult<Book>> GetAllBooksPagedAsync(int page = 1, int pageSize = 20);
        
        /// <summary>
        /// Searches for books by title with pagination
        /// </summary>
        /// <param name="title">The title to search for</param>
        /// <param name="page">The page number (1-based)</param>
        /// <param name="pageSize">The number of items per page</param>
        /// <returns>A paginated result of books matching the title</returns>
        Task<PagedResult<Book>> SearchBooksByTitlePagedAsync(string title, int page = 1, int pageSize = 20);
        
        /// <summary>
        /// Searches for books by author name with pagination
        /// </summary>
        /// <param name="author">The author name to search for</param>
        /// <param name="page">The page number (1-based)</param>
        /// <param name="pageSize">The number of items per page</param>
        /// <returns>A paginated result of books matching the author</returns>
        Task<PagedResult<Book>> SearchBooksByAuthorPagedAsync(string author, int page = 1, int pageSize = 20);
        
        /// <summary>
        /// Searches for books by ISBN with pagination
        /// </summary>
        /// <param name="isbn">The ISBN to search for</param>
        /// <param name="page">The page number (1-based)</param>
        /// <param name="pageSize">The number of items per page</param>
        /// <returns>A paginated result of books matching the ISBN</returns>
        Task<PagedResult<Book>> SearchBooksByISBNPagedAsync(string isbn, int page = 1, int pageSize = 20);
        
        /// <summary>
        /// Searches for books by ownership status
        /// </summary>
        /// <param name="status">The ownership status to search for</param>
        /// <returns>A collection of books matching the ownership status</returns>
        Task<IEnumerable<Book>> SearchBooksByOwnershipStatusAsync(OwnershipStatus status);
        
        /// <summary>
        /// Searches for books by ownership status with pagination
        /// </summary>
        /// <param name="status">The ownership status to search for</param>
        /// <param name="page">The page number (1-based)</param>
        /// <param name="pageSize">The number of items per page</param>
        /// <returns>A paginated result of books matching the ownership status</returns>
        Task<PagedResult<Book>> SearchBooksByOwnershipStatusPagedAsync(OwnershipStatus status, int page = 1, int pageSize = 20);
        

        
        /// <summary>
        /// Adds a new book to the repository
        /// </summary>
        /// <param name="book">The book to add</param>
        /// <returns>The added book with generated ID</returns>
        Task<Book> AddBookAsync(Book book);
        
        /// <summary>
        /// Updates an existing book in the repository
        /// </summary>
        /// <param name="book">The book to update</param>
        /// <returns>The updated book</returns>
        Task<Book> UpdateBookAsync(Book book);
        
        /// <summary>
        /// Deletes a book from the repository
        /// </summary>
        /// <param name="id">The ID of the book to delete</param>
        /// <returns>True if the book was successfully deleted, otherwise false</returns>
        Task<bool> DeleteBookAsync(int id);
    }
}