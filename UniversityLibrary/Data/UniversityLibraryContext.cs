using Microsoft.EntityFrameworkCore;
using UniversityLibrary.Models;

namespace UniversityLibrary.Data
{
    public class UniversityLibraryContext(DbContextOptions<UniversityLibraryContext> options) : DbContext(options)
    {
        public DbSet<Book> Book { get; set; } = default!;
        public DbSet<UniversityLibrary.Models.Author> Author { get; set; } = default!;
        public DbSet<UniversityLibrary.Models.Publisher> Publisher { get; set; } = default!;
    }
}
