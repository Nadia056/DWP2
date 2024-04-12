using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DWP2.Models
{
    public class Regions
    {
        [Key]
        [JsonPropertyName("region_id")]
        public int REGION_ID { get; set; }
        [JsonPropertyName("region_name")]
        public string? REGION_NAME { get; set; }
    }
}
