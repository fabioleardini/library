using Library.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Application.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int id);
        Task<IEnumerable<Book>> SearchBooksByTitleAsync(string title);
        Task<IEnumerable<Book>> SearchBooksByAuthorAsync(string author);
        Task<IEnumerable<Book>> SearchBooksByISBNAsync(string isbn);
        Task<IEnumerable<Book>> SearchBooksByOwnershipStatusAsync(Book.OwnershipStatus status);
        Task<Book> AddBookAsync(Book book);
        Task<Book> UpdateBookAsync(Book book);
        Task<bool> DeleteBookAsync(int id);
    }
}