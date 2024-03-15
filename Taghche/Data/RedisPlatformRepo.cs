using System.Text.Json;
using CacheService.Models;
using StackExchange.Redis;

namespace CacheService.Data
{
    public class RedisBookRepo : IBookRepo
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisBookRepo(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public void CreateBook(Book plat)
        {
            if (plat == null)
            {
                throw new ArgumentOutOfRangeException(nameof(plat));
            }

            var db = _redis.GetDatabase();

            var serialPlat = JsonSerializer.Serialize(plat);

            //db.StringSet(plat.Id, serialPlat);
            db.HashSet($"hashBook", new HashEntry[] 
                {new HashEntry(plat.Id, serialPlat)});
        }

        public Book? GetBookById(string id)
        {
            var db = _redis.GetDatabase();

            //var plat = db.StringGet(id);

            var plat = db.HashGet("hashBook", id);

            if (!string.IsNullOrEmpty(plat))
            {
                return JsonSerializer.Deserialize<Book>(plat);
            }
            return null;
        }

        public IEnumerable<Book?>? GetAllBooks()
        {
            var db = _redis.GetDatabase();

            var completeSet = db.HashGetAll("hashBook");
            
            if (completeSet.Length > 0)
            {
                var obj = Array.ConvertAll(completeSet, val => 
                    JsonSerializer.Deserialize<Book>(val.Value)).ToList();
                return obj;   
            }
            
            return null;
        }
    }
}
