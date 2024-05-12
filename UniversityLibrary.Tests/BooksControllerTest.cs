using UniversityLibrary.Controllers;
using Moq;
using Microsoft.AspNetCore.Mvc;
using UniversityLibrary.Data.Repositories;

namespace UniversityLibrary.Tests;

public class BooksControllerTest
{
	[Fact]
	public async Task Index_ReturnsAViewResult()
	{
		var repositoryMock = new Mock<IBooksRepository>();
		var controller = new BooksController(repositoryMock.Object);

		var result = await controller.Index();

		Assert.IsType<ViewResult>(result);
	}
}
