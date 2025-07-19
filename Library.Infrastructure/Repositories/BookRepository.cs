using Library.Application.Interfaces;
using Library.Domain.Models;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for book data access operations
    /// </summary>
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;

        /// <summary>
        /// Initializes a new instance of the BookRepository class
        /// </summary>
        /// <param name="context">The database context</param>
        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Book>> SearchBooksByTitleAsync(string title)
        {
            return await _context.Books
                .Where(b => b.Title.Contains(title))
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Book>> SearchBooksByAuthorAsync(string author)
        {
            return await _context.Books
                .Where(b => b.FirstName.Contains(author) || b.LastName.Contains(author))
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Book>> SearchBooksByISBNAsync(string isbn)
        {
            return await _context.Books
                .Where(b => b.ISBN.Contains(isbn))
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Book>> SearchBooksByOwnershipStatusAsync(Book.OwnershipStatus status)
        {
            return await _context.Books
                .Where(b => b.Status == status)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Book> AddBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        /// <inheritdoc/>
        public async Task<Book> UpdateBookAsync(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return book;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}