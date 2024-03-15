using CacheService.Models;

namespace CacheService.Data
{
    public interface IBookRepo
    {
        void CreateBook(Book plat);
        Book? GetBookById(string id);
        IEnumerable<Book?>? GetAllBooks();
    }
}