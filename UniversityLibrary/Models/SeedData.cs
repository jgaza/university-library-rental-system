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
        if (context.BookLoan.Any())
        {
            return;   // DB has been seeded
        }

        Address commonStreet = new()
        {
            Street = "Common Street",
            HouseNumber = 123,
            Postcode = 55555,
            City = "Munich"
        };

        Address laufendeStrasse = new()
        {
            Street = "Laufende Strasse",
            HouseNumber = 456,
            Postcode = 12345,
            City = "Munich"
        };

        context.Address.AddRange(commonStreet, laufendeStrasse);

        Student albertEinstein = new()
        {
            FirstName = "Albert",
            LastName = "Einstein",
            Address = laufendeStrasse
        };

        Student napoleonBonaparte = new()
        {
            FirstName = "Napoleon",
            LastName = "Bonaparte",
            Address = commonStreet
        };

        context.Student.AddRange(albertEinstein, napoleonBonaparte);

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

        Publisher publisher = new()
        {
            Name = "Publisher 1"
        };

        Publisher publisher2 = new()
        {
            Name = "Publisher 2"
        };

        context.Publisher.AddRange(publisher, publisher2);

        Book someTitle = new()
        {
            Title = "Some Title",
            Isbn = "1234",
            Authors = [johnDoe],
            Publisher = publisher
        };
        Book someOtherTitle = new()
        {
            Title = "Some Other Title",
            Isbn = "5678",
            Authors = [janeDoe],
            Publisher = publisher
        };

        Book yetAnotherTitle = new()
        {
            Title = "Yet Another Title",
            Isbn = "1357",
            Authors = [johnDoe, johnSmith],
            Publisher = publisher2
        };

        Book oneLastTitle = new()
        {
            Title = "One Last Title",
            Isbn = "2468",
            Authors = [johnDoe, janeDoe],
            Publisher = publisher
        };

        context.Book.AddRange(someTitle, someOtherTitle, yetAnotherTitle, oneLastTitle);

        BookLoan albertLoan = new()
        {
            Student = albertEinstein,
            Books = [someTitle, someOtherTitle],
            StartDate = DateOnly.MinValue,
            EndDate = DateOnly.MaxValue
        };

        BookLoan napoleonLoan = new()
        {
            Student = napoleonBonaparte,
            Books = [yetAnotherTitle, oneLastTitle],
            StartDate = DateOnly.MinValue,
            EndDate = DateOnly.MaxValue
        };

        context.BookLoan.AddRange(albertLoan, napoleonLoan);

        context.SaveChanges();
    }
}
