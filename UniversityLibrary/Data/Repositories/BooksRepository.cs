using Microsoft.EntityFrameworkCore;
using UniversityLibrary.Models;

namespace UniversityLibrary.Data.Repositories
{
	public class BooksRepository(UniversityLibraryContext context) : IBooksRepository
	{
		private readonly UniversityLibraryContext _context = context;

		public async Task<int> AddBookAsync(Book book)
		{
			_context.Add(book);
			return await _context.SaveChangesAsync();
		}

		public bool BookExists(int id)
		{
			return _context.Book.Any(e => e.ID == id);
		}

		public async Task<Book?> FindBookByIDAsync(int? id) => await _context.Book.FindAsync(id);

		public async Task<List<Book>> GetAllBooksAsync() => await _context.Book.Include(b => b.Authors).Include(b => b.Publisher).ToListAsync();

		public async Task<Book?> GetBookByIDAsync(int? id) => await _context.Book.Include(b => b.Authors).Include(b => b.Publisher).FirstOrDefaultAsync(m => m.ID == id);

		public async Task<int> RemoveBookByIDAsync(int id)
		{
			var book = await _context.Book.FindAsync(id);
			if (book != null)
			{
				_context.Book.Remove(book);
			}

			return await _context.SaveChangesAsync();
		}

		public async Task<int> UpdateBookAsync(Book book)
		{
			_context.Update(book);
			return await _context.SaveChangesAsync();
		}
	}
}
