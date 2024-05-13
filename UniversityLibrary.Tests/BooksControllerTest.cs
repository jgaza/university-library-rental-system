using UniversityLibrary.Controllers;
using Moq;
using Microsoft.AspNetCore.Mvc;
using UniversityLibrary.Data.Repositories;
using UniversityLibrary.Models;

namespace UniversityLibrary.Tests;

public class BooksControllerTest
{
	[Fact]
	public async Task Index_ReturnsAViewResult()
	{
		var repositoryMock = new Mock<IBooksRepository>();
		var controller = new BooksController(repositoryMock.Object, null);

		var result = await controller.Index();

		Assert.IsType<ViewResult>(result);
	}

	[Fact]
	public async Task Index_ReturnsAViewResult_WithAListOfAllBooks()
	{
		var repositoryMock = new Mock<IBooksRepository>();
		repositoryMock.Setup(repo => repo.GetAllBooksAsync()).ReturnsAsync(InstantiateTestBooks());
		var controller = new BooksController(repositoryMock.Object, null);

		var result = await controller.Index() as ViewResult;
		var model = result?.ViewData?.Model as IEnumerable<Book>;

		Assert.Equal(InstantiateTestBooks().Count, model?.Count());
	}

	private static List<Book> InstantiateTestBooks()
	{
		var books = new List<Book>
		{
			new Book()
			{
				ID = 1,
				Title = "Test Title 2",
				Isbn = "1234",
				AuthorID = 1,
				Publisher = "Test Publisher 1"
			},
			new Book()
			{
				ID = 2,
				Title = "Test Title 1",
				Isbn = "5678",
				AuthorID = 2,
				Publisher = "Test Publisher 2"
			}
		};

		return books;
	}
}
