using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace pharmacy.Core.Entities;
public class Product 
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public int Price { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    public Category Category { get; set; }

    [ForeignKey("Brand")]
    public int BrandId { get; set; }
    public Brand Brand { get; set; }
    public Discount Discount { get; set; }
    public int? DiscountId { get; set; }
    private string _imageUrlsJson;
    private string _imagePublicIdsJson;

    public string ImageUrlsJson
    {
        get => JsonSerializer.Serialize(ImageUrls);
        set
        {
            _imageUrlsJson = value;
            ImageUrls = DeserializeJson(value);
        }
    }

    public string ImagePublicIdsJson
    {
        get => JsonSerializer.Serialize(ImagePublicIds);
        set
        {
            _imagePublicIdsJson = value;
            ImagePublicIds = DeserializeJson(value);
        }
    }

    [NotMapped]
    public List<string> ImageUrls { get; set; } = new List<string>();

    [NotMapped]
    public List<string> ImagePublicIds { get; set; } = new List<string>();

    private List<string> DeserializeJson(string json)
    {
        if (string.IsNullOrEmpty(json))
        {
            return new List<string>();
        }

        try
        {
            return JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
        }
        catch (JsonException ex)
        {
            Console.WriteLine("Error deserializing JSON: " + ex.Message);
            return new List<string>();
        }
    }
}