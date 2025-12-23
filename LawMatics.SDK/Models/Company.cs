using System.Text.Json.Serialization;

namespace LawMatics.SDK.Models
{
    /// <summary>
    /// Represents a company in Lawmatics
    /// </summary>
    public class Company
    {
        /// <summary>
        /// Unique identifier for the company
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Company name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Company description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Company website
        /// </summary>
        [JsonPropertyName("website")]
        public string? Website { get; set; }

        /// <summary>
        /// Company phone number
        /// </summary>
        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        /// <summary>
        /// Company email
        /// </summary>
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        /// <summary>
        /// Company industry
        /// </summary>
        [JsonPropertyName("industry")]
        public string? Industry { get; set; }

        /// <summary>
        /// Company size
        /// </summary>
        [JsonPropertyName("size")]
        public string? Size { get; set; }

        /// <summary>
        /// Company address
        /// </summary>
        [JsonPropertyName("address")]
        public Address? Address { get; set; }

        /// <summary>
        /// Company status
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Date when company was created
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date when company was last updated
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Custom fields for the company
        /// </summary>
        [JsonPropertyName("custom_fields")]
        public Dictionary<string, object>? CustomFields { get; set; }

        /// <summary>
        /// Associated contacts
        /// </summary>
        [JsonPropertyName("contacts")]
        public List<Contact>? Contacts { get; set; }
    }

    /// <summary>
    /// Request model for creating a new company
    /// </summary>
    public class CreateCompanyRequest
    {
        /// <summary>
        /// Company name (required)
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Company description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Company website
        /// </summary>
        [JsonPropertyName("website")]
        public string? Website { get; set; }

        /// <summary>
        /// Company phone number
        /// </summary>
        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        /// <summary>
        /// Company email
        /// </summary>
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        /// <summary>
        /// Company industry
        /// </summary>
        [JsonPropertyName("industry")]
        public string? Industry { get; set; }

        /// <summary>
        /// Company size
        /// </summary>
        [JsonPropertyName("size")]
        public string? Size { get; set; }

        /// <summary>
        /// Company address
        /// </summary>
        [JsonPropertyName("address")]
        public Address? Address { get; set; }

        /// <summary>
        /// Custom fields for the company
        /// </summary>
        [JsonPropertyName("custom_fields")]
        public Dictionary<string, object>? CustomFields { get; set; }
    }

    /// <summary>
    /// Request model for updating an existing company
    /// </summary>
    public class UpdateCompanyRequest
    {
        /// <summary>
        /// Company name
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Company description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Company website
        /// </summary>
        [JsonPropertyName("website")]
        public string? Website { get; set; }

        /// <summary>
        /// Company phone number
        /// </summary>
        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        /// <summary>
        /// Company email
        /// </summary>
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        /// <summary>
        /// Company industry
        /// </summary>
        [JsonPropertyName("industry")]
        public string? Industry { get; set; }

        /// <summary>
        /// Company size
        /// </summary>
        [JsonPropertyName("size")]
        public string? Size { get; set; }

        /// <summary>
        /// Company address
        /// </summary>
        [JsonPropertyName("address")]
        public Address? Address { get; set; }

        /// <summary>
        /// Company status
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Custom fields for the company
        /// </summary>
        [JsonPropertyName("custom_fields")]
        public Dictionary<string, object>? CustomFields { get; set; }
    }
}