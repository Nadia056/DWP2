using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DWP2.Models
{
    public class Employees
    {
        [Key]
        [JsonPropertyName("employee_id")]
        public int EMPLOYEE_ID { get; set; }

        [JsonPropertyName("first_name")]
        public string? FIRST_NAME { get; set; }

        [JsonPropertyName("last_name")]
        public string? LAST_NAME { get; set; }

        [JsonPropertyName("email")]
        public string? EMAIL { get; set; }

        [JsonPropertyName("phone")]
        public string? PHONE { get; set; }

        [JsonPropertyName("hire_date")]
        public DateTime? HIRE_DATE { get; set; }

        [JsonPropertyName("manager_id")]
        public int? MANAGER_ID { get; set; }

        [JsonPropertyName("job_title")]
        public string? JOB_TITLE { get; set; }
    }
}
 