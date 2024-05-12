using UniversityLibrary.Models;

namespace UniversityLibrary.Data.Repositories
{
	public interface IBooksRepository

	{
		public Task<List<Book>> GetAllBooksAsync();

		public Task<Book?> GetBookByIDAsync(int? id);

		public Task<Book?> FindBookByIDAsync(int? id);

		public Task<int> AddBookAsync(Book book);

		public Task<int> UpdateBookAsync(Book book);

		public Task<int> RemoveBookByIDAsync(int id);

		public bool BookExists(int id);
	}
}
