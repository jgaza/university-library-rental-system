using Microsoft.EntityFrameworkCore;
using UniversityLibrary.Models;

namespace UniversityLibrary.Data
{
	public class UniversityLibraryContext : DbContext
	{
		public UniversityLibraryContext(DbContextOptions<UniversityLibraryContext> options)
			: base(options)
		{
		}

		public DbSet<Book> Book { get; set; } = default!;
	}
}
