using System.ComponentModel.DataAnnotations;

namespace CacheService.Models
{
    public class Book
    {
        [Required]
        public string Id { get; set; } = $"Book:{Guid.NewGuid().ToString()}";
        
        [Required]
        public string Name { get; set; } = String.Empty;
    }
}