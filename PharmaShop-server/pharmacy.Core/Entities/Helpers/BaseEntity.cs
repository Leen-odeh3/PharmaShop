
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
namespace pharmacy.Core.Entities.Helpers;
public class BaseEntity
{
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
