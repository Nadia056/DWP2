﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DWP2.Models
{
    public class Product_Categories
    {
        [Key]
        [JsonPropertyName("category_id")]
        public int CATEGORY_ID { get; set; }
        [JsonPropertyName("category_name")]
        public string? CATEGORY_NAME { get; set; }

    }
}
