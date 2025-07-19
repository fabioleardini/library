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
            return await _context.Books.AsNoTracking().ToListAsync();
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
                .AsNoTracking()
                .Where(b => b.Title.ToLower().Contains(title.ToLower()))
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Book>> SearchBooksByAuthorAsync(string author)
        {
            var lowerAuthor = author.ToLower();
            return await _context.Books
                .AsNoTracking()
                .Where(b => b.FirstName.ToLower().Contains(lowerAuthor) || b.LastName.ToLower().Contains(lowerAuthor))
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Book>> SearchBooksByISBNAsync(string isbn)
        {
            return await _context.Books
                .AsNoTracking()
                .Where(b => b.ISBN.ToLower().Contains(isbn.ToLower()))
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Book>> SearchBooksByOwnershipStatusAsync(Book.OwnershipStatus status)
        {
            return await _context.Books
                .AsNoTracking()
                .Where(b => b.Status == status)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<PagedResult<Book>> GetAllBooksPagedAsync(int page = 1, int pageSize = 20)
        {
            var query = _context.Books.AsNoTracking();
            var totalCount = await query.CountAsync();
            var books = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            
            return new PagedResult<Book>(books, totalCount, page, pageSize);
        }

        /// <inheritdoc/>
        public async Task<PagedResult<Book>> SearchBooksByTitlePagedAsync(string title, int page = 1, int pageSize = 20)
        {
            var query = _context.Books
                .AsNoTracking()
                .Where(b => b.Title.ToLower().Contains(title.ToLower()));
            
            var totalCount = await query.CountAsync();
            var books = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            
            return new PagedResult<Book>(books, totalCount, page, pageSize);
        }

        /// <inheritdoc/>
        public async Task<PagedResult<Book>> SearchBooksByAuthorPagedAsync(string author, int page = 1, int pageSize = 20)
        {
            var lowerAuthor = author.ToLower();
            var query = _context.Books
                .AsNoTracking()
                .Where(b => b.FirstName.ToLower().Contains(lowerAuthor) || b.LastName.ToLower().Contains(lowerAuthor));
            
            var totalCount = await query.CountAsync();
            var books = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            
            return new PagedResult<Book>(books, totalCount, page, pageSize);
        }

        /// <inheritdoc/>
        public async Task<PagedResult<Book>> SearchBooksByISBNPagedAsync(string isbn, int page = 1, int pageSize = 20)
        {
            var query = _context.Books
                .AsNoTracking()
                .Where(b => b.ISBN.ToLower().Contains(isbn.ToLower()));
            
            var totalCount = await query.CountAsync();
            var books = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            
            return new PagedResult<Book>(books, totalCount, page, pageSize);
        }

        /// <inheritdoc/>
        public async Task<PagedResult<Book>> SearchBooksByOwnershipStatusPagedAsync(Book.OwnershipStatus status, int page = 1, int pageSize = 20)
        {
            var query = _context.Books
                .AsNoTracking()
                .Where(b => b.Status == status);
            
            var totalCount = await query.CountAsync();
            var books = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            
            return new PagedResult<Book>(books, totalCount, page, pageSize);
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