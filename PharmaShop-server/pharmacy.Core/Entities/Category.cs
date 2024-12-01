using System.Text.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace pharmacy.Core.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

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

        public ICollection<Product> Products { get; set; }

        private List<string> DeserializeJson(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return new List<string>();
            }

            try
            {
                // محاولة التحويل إلى List<string> بأمان
                return JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
            }
            catch (JsonException ex)
            {
                // في حالة حدوث خطأ أثناء التحويل، قم بإرجاع قائمة فارغة
                Console.WriteLine("Error deserializing JSON: " + ex.Message);
                return new List<string>();
            }
        }
    }
}
