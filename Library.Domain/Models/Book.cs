using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Library.Domain.Models
{
    public enum OwnershipStatus
    {
        Own,
        Love,
        WantToRead
    }

    /// <summary>
    /// Represents a book in the Royal Library
    /// </summary>
    public class Book
    {
        /// <summary>
        /// The unique identifier for the book
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The title of the book
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// The author's first name
        /// </summary>
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// The author's last name
        /// </summary>
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// The total number of copies owned by the library
        /// </summary>
        [Range(0, int.MaxValue)]
        public int TotalCopies { get; set; }

        /// <summary>
        /// The number of copies currently checked out
        /// </summary>
        [Range(0, int.MaxValue)]
        public int CopiesInUse { get; set; }

        /// <summary>
        /// The type of book (Hardcover, Paperback, E-Book, Audiobook)
        /// </summary>
        [StringLength(20)]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// The ISBN (International Standard Book Number)
        /// </summary>
        [StringLength(20)]
        public string ISBN { get; set; } = string.Empty;

        /// <summary>
        /// The category or genre of the book
        /// </summary>
        [StringLength(50)]
        public string Category { get; set; } = string.Empty;
        
        public OwnershipStatus Status { get; set; }
        
        /// <summary>
        /// The full name of the author (derived from FirstName and LastName)
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Author => $"{FirstName} {LastName}".Trim();
        
        /// <summary>
        /// The number of copies available for checkout (derived from TotalCopies - CopiesInUse)
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int AvailableCopies => TotalCopies - CopiesInUse;
        

    }
}