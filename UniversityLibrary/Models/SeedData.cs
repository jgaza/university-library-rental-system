using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UniversityLibrary.Data;
using System;
using System.Linq;

namespace UniversityLibrary.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new UniversityLibraryContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<UniversityLibraryContext>>()))
        {
            // Look for any Books.
            if (context.Book.Any())
            {
                return;   // DB has been seeded
            }
            context.Book.AddRange(
                new Book
                {
                    Title = "Some Title",
                    Isbn = "1234",
                    AuthorID = 1,
                    Publisher = "Some Publisher"
                },
                new Book
                {
                    Title = "Some Other Title",
                    Isbn = "5678",
                    AuthorID = 2,
                    Publisher = "Some Other Publisher"
                },
                new Book
                {
                    Title = "Yet Another Title",
                    Isbn = "1357",
                    AuthorID = 1,
                    Publisher = "Surprise, The First Publisher"
                },
                new Book
                {
                    Title = "One Last Title",
                    Isbn = "2468",
                    AuthorID = 3,
                    Publisher = "Yet Another Publisher"
                }
            );
            context.SaveChanges();
        }
    }
}
