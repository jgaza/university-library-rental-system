using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Protocol;
using UniversityLibrary.Data.Repositories;
using UniversityLibrary.Models;

namespace UniversityLibrary.Controllers
{
    public class BooksController(IBooksRepository repository, ILogger<BooksController>? logger) : Controller
    {
        private readonly IBooksRepository _repository = repository;
        private readonly ILogger<BooksController>? _logger = logger;

        // GET: Books
        public async Task<IActionResult> Index()
        {
            _logger?.LogDebug("Retrieving all book data...");
            return View(await _repository.GetAllBooksAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                _logger?.LogWarning("Provided ID is null!");
                return NotFound();
            }

            _logger?.LogDebug("Attempting to retrieve stored book data for ID '{ID}'...", id);
            var book = await _repository.GetBookByIDAsync(id);

            if (book == null)
            {
                _logger?.LogWarning("No book data found for provided ID: {ID}!", id);
                return NotFound();
            }

            _logger?.LogDebug("Displaying book data for ID '{ID}':{NewLine}{BookData}", id, Environment.NewLine, book.ToJson(Formatting.Indented));
            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            _logger?.LogDebug("Generating 'Create' View...");
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
                _logger?.LogDebug("Attempting to add book data:{NewLine}{BookData}", Environment.NewLine, book.ToJson(Formatting.Indented));
                await _repository.AddBookAsync(book);
                _logger?.LogDebug("Redirecting to 'Index' page...");
                return RedirectToAction(nameof(Index));
            }

            _logger?.LogDebug("Some field values failed validation.");
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                _logger?.LogWarning("Provided ID is null!");
                return NotFound();
            }

            _logger?.LogDebug("Attempting to find stored book data for ID '{ID}'...", id);
            var book = await _repository.FindBookByIDAsync(id);

            if (book == null)
            {
                _logger?.LogWarning("No book data found for provided ID: {ID}!", id);
                return NotFound();
            }

            _logger?.LogDebug("Generating 'Edit' view with retrieved data:{NewLine}{BookData}", Environment.NewLine, book.ToJson());
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
                _logger?.LogWarning("Book ID does not match record!{NewLine}Book ID: {BookID}; Expected: {ID}", Environment.NewLine, book.ID, id);
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _logger?.LogDebug("Attempting to update stored book information...");
                    await _repository.UpdateBookAsync(book);
                }
                catch (DbUpdateConcurrencyException concurrencyException)
                {
                    if (!_repository.BookExists(book.ID))
                    {
                        _logger?.LogWarning(concurrencyException, "Failed to update book information for ID '{ID}': No Book data stored with provided ID!", book.ID);
                        return NotFound();
                    }
                    else
                    {
                        _logger?.LogError(concurrencyException, "Failed to update book information for ID '{ID}'!", book.ID);
                        throw;
                    }
                }

                _logger?.LogDebug("Update Successful! Redirecting to 'Index' page...");
                return RedirectToAction(nameof(Index));
            }

            _logger?.LogDebug("Provided field values failed validation.");
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                _logger?.LogWarning("Provided ID is null!");
                return NotFound();
            }

            _logger?.LogDebug("Attempting to retrieve stored book data for ID '{ID}'...", id);
            var book = await _repository.GetBookByIDAsync(id);

            if (book == null)
            {
                _logger?.LogWarning("No book data found for provided ID: {ID}!", id);
                return NotFound();
            }

            _logger?.LogDebug("Displaying book data for ID '{ID}':{NewLine}{BookData}", id, Environment.NewLine, book.ToJson(Formatting.Indented));
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _logger?.LogDebug("Attempting to delete stored book data for ID '{ID}'...", id);
            await _repository.RemoveBookByIDAsync(id);
            _logger?.LogDebug("Book Data for ID '{ID}' successfully deleted. Redirecting to 'Index' page...", id);
            return RedirectToAction(nameof(Index));
        }
    }
}
