using Library.Application.Interfaces;
using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        /// Supports searching by title, author, ISBN, and ownership status.
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
                "status" => Enum.TryParse<Book.OwnershipStatus>(searchValue, true, out var status) 
                    ? await _bookRepository.SearchBooksByOwnershipStatusAsync(status)
                    : new List<Book>(),
                _ => await GetAllBooksAsync()
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
    }
}