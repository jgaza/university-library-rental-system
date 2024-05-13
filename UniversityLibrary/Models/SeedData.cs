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
        using var context = new UniversityLibraryContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<UniversityLibraryContext>>());
        // Look for any Books.
        if (context.Book.Any())
        {
            return;   // DB has been seeded
        }

        Author johnDoe = new()
        {
            FirstName = "John",
            LastName = "Doe"
        };

        Author janeDoe = new()
        {
            FirstName = "Jane",
            LastName = "Doe"
        };

        Author johnSmith = new()
        {
            FirstName = "John",
            LastName = "Smith"
        };

        context.Author.AddRange(johnDoe, janeDoe, johnSmith);

        context.Book.AddRange(
            new Book
            {
                Title = "Some Title",
                Isbn = "1234",
                Authors = [johnDoe],
                Publisher = "Some Publisher"
            },
            new Book
            {
                Title = "Some Other Title",
                Isbn = "5678",
                Authors = [janeDoe],
                Publisher = "Some Other Publisher"
            },
            new Book
            {
                Title = "Yet Another Title",
                Isbn = "1357",
                Authors = [johnDoe, johnSmith],
                Publisher = "Surprise, The First Publisher"
            },
            new Book
            {
                Title = "One Last Title",
                Isbn = "2468",
                Authors = [johnDoe, janeDoe],
                Publisher = "Yet Another Publisher"
            }
        );
        context.SaveChanges();
    }
}
