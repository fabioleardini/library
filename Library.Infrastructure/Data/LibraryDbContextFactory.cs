using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Library.Infrastructure.Data
{
    /// <summary>
    /// Design-time factory for LibraryDbContext to support EF Core migrations
    /// </summary>
    public class LibraryDbContextFactory : IDesignTimeDbContextFactory<LibraryDbContext>
    {
        public LibraryDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LibraryDbContext>();
            
            // Use SQLite for migrations (you can change this to your preferred database)
            optionsBuilder.UseSqlite("Data Source=library.db");
            
            return new LibraryDbContext(optionsBuilder.Options);
        }
    }
}