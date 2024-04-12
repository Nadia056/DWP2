using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace DWP2.Models
{
    public class Locations
    {
        [Key]
        [JsonPropertyName("location_id")]
        public int LOCATION_ID { get; set; }
        
        [JsonPropertyName("address")]
        public required string ADDRESS { get; set; }
        
        [JsonPropertyName("postal_code")]
        public int? POSTAL_CODE { get; set; }
        
        [JsonPropertyName("city")]
        public  required string CITY { get; set; }
        
        [JsonPropertyName("state")]
        public string? STATE { get; set; }

        [ForeignKey("countries")]
        [JsonPropertyName("country_id")]
        public int COUNTRY_ID { get; set; }


        public Countries? Countries { get; set; }
    }
}
