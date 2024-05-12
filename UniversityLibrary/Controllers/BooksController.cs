using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityLibrary.Data.Repositories;
using UniversityLibrary.Models;

namespace UniversityLibrary.Controllers
{
	public class BooksController : Controller
	{
		private readonly IBooksRepository _repository;

		public BooksController(IBooksRepository repository)
		{
			_repository = repository;
		}

		// GET: Books
		public async Task<IActionResult> Index()
		{
			return View(await _repository.GetAllBooksAsync());
		}

		// GET: Books/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var book = await _repository.GetBookByIDAsync(id);

			if (book == null)
			{
				return NotFound();
			}

			return View(book);
		}

		// GET: Books/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Books/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("ID,Title,Isbn,AuthorID,Publisher")] Book book)
		{
			if (ModelState.IsValid)
			{
				await _repository.AddBookAsync(book);
				return RedirectToAction(nameof(Index));
			}
			return View(book);
		}

		// GET: Books/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var book = await _repository.FindBookByIDAsync(id);

			if (book == null)
			{
				return NotFound();
			}
			return View(book);
		}

		// POST: Books/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Isbn,AuthorID,Publisher")] Book book)
		{
			if (id != book.ID)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					await _repository.UpdateBookAsync(book);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!BookExists(book.ID))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(book);
		}

		// GET: Books/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var book = await _repository.GetBookByIDAsync(id);

			if (book == null)
			{
				return NotFound();
			}

			return View(book);
		}

		// POST: Books/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _repository.RemoveBookByIDAsync(id);
			return RedirectToAction(nameof(Index));
		}

		private bool BookExists(int id)
		{
			return _repository.BookExists(id);
		}
	}
}
