using Library.Domain.Models;

namespace Library.Application.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int id);
        Task<IEnumerable<Book>> SearchBooksByTitleAsync(string title);
        Task<IEnumerable<Book>> SearchBooksByAuthorAsync(string author);
        Task<IEnumerable<Book>> SearchBooksByISBNAsync(string isbn);
    
        
        // Pagination methods
        Task<PagedResult<Book>> GetAllBooksPagedAsync(int page = 1, int pageSize = 20);
        Task<PagedResult<Book>> SearchBooksByTitlePagedAsync(string title, int page = 1, int pageSize = 20);
        Task<PagedResult<Book>> SearchBooksByAuthorPagedAsync(string author, int page = 1, int pageSize = 20);
        Task<PagedResult<Book>> SearchBooksByISBNPagedAsync(string isbn, int page = 1, int pageSize = 20);
        Task<IEnumerable<Book>> SearchBooksByOwnershipStatusAsync(OwnershipStatus status);
        Task<PagedResult<Book>> SearchBooksByOwnershipStatusPagedAsync(OwnershipStatus status, int page, int pageSize);
    
        
        Task<Book> AddBookAsync(Book book);
        Task<Book> UpdateBookAsync(Book book);
        Task<bool> DeleteBookAsync(int id);
    }
}