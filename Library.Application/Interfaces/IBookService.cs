using Library.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Application.Interfaces
{
    /// <summary>
    /// Service for managing books in the library
    /// </summary>
    public interface IBookService
    {
        /// <summary>
        /// Gets all books in the library
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
        /// Searches for books based on specified criteria
        /// </summary>
        /// <param name="searchBy">The field to search by (Title, Author, ISBN, Status)</param>
        /// <param name="searchValue">The value to search for</param>
        /// <returns>A collection of books matching the search criteria</returns>
        Task<IEnumerable<Book>> SearchBooksAsync(string searchBy, string searchValue);
        
        /// <summary>
        /// Gets all books in the library with pagination
        /// </summary>
        /// <param name="page">The page number (1-based)</param>
        /// <param name="pageSize">The number of items per page</param>
        /// <returns>A paged result of books</returns>
        Task<PagedResult<Book>> GetAllBooksPagedAsync(int page = 1, int pageSize = 20);
        
        /// <summary>
        /// Searches for books based on specified criteria with pagination
        /// </summary>
        /// <param name="searchBy">The field to search by (Title, Author, ISBN, Status)</param>
        /// <param name="searchValue">The value to search for</param>
        /// <param name="page">The page number (1-based)</param>
        /// <param name="pageSize">The number of items per page</param>
        /// <returns>A paged result of books matching the search criteria</returns>
        Task<PagedResult<Book>> SearchBooksPagedAsync(string searchBy, string searchValue, int page = 1, int pageSize = 20);
        
        /// <summary>
        /// Adds a new book to the library
        /// </summary>
        /// <param name="book">The book information to add</param>
        /// <returns>The newly created book</returns>
        Task<Book> AddBookAsync(Book book);
        
        /// <summary>
        /// Updates an existing book in the library
        /// </summary>
        /// <param name="book">The updated book information</param>
        /// <returns>The updated book</returns>
        Task<Book> UpdateBookAsync(Book book);
        
        /// <summary>
        /// Deletes a book from the library
        /// </summary>
        /// <param name="id">The ID of the book to delete</param>
        /// <returns>True if the book was successfully deleted, otherwise false</returns>
        Task<bool> DeleteBookAsync(int id);
    }
}