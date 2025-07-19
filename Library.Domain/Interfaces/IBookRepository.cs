using Library.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        /// Searches for books by ownership status
        /// </summary>
        /// <param name="status">The ownership status to search for</param>
        /// <returns>A collection of books matching the ownership status</returns>
        Task<IEnumerable<Book>> SearchBooksByOwnershipStatusAsync(Book.OwnershipStatus status);
        
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