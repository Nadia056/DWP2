using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DWP2.Models
{
    public class Inventories
    {
        [Key] // Esto indica que esta propiedad será parte de la clave primaria
        [Column(Order = 1)] // Orden de la columna en la clave primaria compuesta
        [ForeignKey("Products")]
        [JsonPropertyName("product_id")]
        public int PRODUCT_ID { get; set; }
        public Products? Products { get; set; }

        [Key] // Esto indica que esta propiedad será parte de la clave primaria
        [Column(Order = 2)] // Orden de la columna en la clave primaria compuesta
        [ForeignKey("Warehouses")]
        [JsonPropertyName("warehouse_id")]
        public int WAREHOUSE_ID { get; set; }
        public Warehouses? Warehouses { get; set; }

        [JsonPropertyName("quantity")]
        public int? QUANTITY { get; set; }
    }
}
