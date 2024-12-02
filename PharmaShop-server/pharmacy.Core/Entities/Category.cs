using System.Text.Json;
using System.ComponentModel.DataAnnotations.Schema;
using pharmacy.Core.Entities.Helpers;

namespace pharmacy.Core.Entities;
public class Category : BaseEntity
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string CategoryDescription { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public ICollection<Product> Products { get; set; }
}
