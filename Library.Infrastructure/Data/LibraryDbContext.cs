using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Data
{
    /// <summary>
    /// Database context for the Royal Library application
    /// </summary>
    public class LibraryDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the LibraryDbContext class
        /// </summary>
        /// <param name="options">The options to be used by the context</param>
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the books in the library
        /// </summary>
        public DbSet<Book> Books { get; set; } = null!;

        /// <summary>
        /// Configures the model that was discovered by convention from the entity types
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Book entity
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.TotalCopies).IsRequired().HasDefaultValue(0);
                entity.Property(e => e.CopiesInUse).IsRequired().HasDefaultValue(0);
                entity.Property(e => e.Type).HasMaxLength(50);
                entity.Property(e => e.ISBN).HasMaxLength(20);
                entity.Property(e => e.Category).HasMaxLength(50);
                
                // Performance indexes for search operations
                entity.HasIndex(e => e.Title).HasDatabaseName("IX_Books_Title");
                entity.HasIndex(e => e.FirstName).HasDatabaseName("IX_Books_FirstName");
                entity.HasIndex(e => e.LastName).HasDatabaseName("IX_Books_LastName");
                entity.HasIndex(e => e.ISBN).IsUnique().HasDatabaseName("IX_Books_ISBN_Unique");
                entity.HasIndex(e => e.Status).HasDatabaseName("IX_Books_Status");
                entity.HasIndex(e => e.Category).HasDatabaseName("IX_Books_Category");
                
                // Composite indexes for common search patterns
                entity.HasIndex(e => new { e.FirstName, e.LastName }).HasDatabaseName("IX_Books_Author");
                entity.HasIndex(e => new { e.Category, e.Status }).HasDatabaseName("IX_Books_Category_Status");
                
                // Ignore derived properties
                entity.Ignore(e => e.Author);
                entity.Ignore(e => e.AvailableCopies);
            });

            // Seed initial data
            SeedData(modelBuilder);
        }

        /// <summary>
        /// Seeds the database with initial book data
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context</param>
        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "Pride and Prejudice",
                    FirstName = "Jane",
                    LastName = "Austen",
                    TotalCopies = 100,
                    CopiesInUse = 80,
                    Type = "Hardcover",
                    ISBN = "123456789",
                    Category = "Fiction",
                    Status = Book.OwnershipStatus.Own
                },
                new Book
                {
                    Id = 2,
                    Title = "To Kill a Mockingbird",
                    FirstName = "Harper",
                    LastName = "Lee",
                    TotalCopies = 75,
                    CopiesInUse = 65,
                    Type = "Paperback",
                    ISBN = "123456782",
                    Category = "Fiction",
                    Status = Book.OwnershipStatus.Love
                },
                new Book
                {
                    Id = 3,
                    Title = "The Catcher in the Rye",
                    FirstName = "J.D.",
                    LastName = "Salinger",
                    TotalCopies = 50,
                    CopiesInUse = 45,
                    Type = "Hardcover",
                    ISBN = "123456783",
                    Category = "Fiction",
                    Status = Book.OwnershipStatus.WantToRead
                },
                new Book
                {
                    Id = 4,
                    Title = "The Great Gatsby",
                    FirstName = "F. Scott",
                    LastName = "Fitzgerald",
                    TotalCopies = 30,
                    CopiesInUse = 22,
                    Type = "Hardcover",
                    ISBN = "123456784",
                    Category = "Non-Fiction",
                    Status = Book.OwnershipStatus.Own
                },
                new Book
                {
                    Id = 5,
                    Title = "The Alchemist",
                    FirstName = "Paulo",
                    LastName = "Coelho",
                    TotalCopies = 50,
                    CopiesInUse = 35,
                    Type = "Hardcover",
                    ISBN = "123456785",
                    Category = "Biography",
                    Status = Book.OwnershipStatus.Love
                },
                new Book
                {
                    Id = 6,
                    Title = "The Book Thief",
                    FirstName = "Markus",
                    LastName = "Zusak",
                    TotalCopies = 75,
                    CopiesInUse = 11,
                    Type = "Hardcover",
                    ISBN = "123456786",
                    Category = "Mystery",
                    Status = Book.OwnershipStatus.WantToRead
                }
            );
        }
    }
}