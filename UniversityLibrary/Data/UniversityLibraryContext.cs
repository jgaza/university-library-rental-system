using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniversityLibrary.Models;

namespace UniversityLibrary.Data
{
    public class UniversityLibraryContext : DbContext
    {
        public UniversityLibraryContext (DbContextOptions<UniversityLibraryContext> options)
            : base(options)
        {
        }

        public DbSet<UniversityLibrary.Models.Book> Book { get; set; } = default!;
    }
}
