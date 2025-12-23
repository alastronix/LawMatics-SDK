using System.Text.Json.Serialization;

namespace LawMatics.SDK.Models
{
    /// <summary>
    /// Represents a matter/prospect in Lawmatics
    /// </summary>
    public class Matter
    {
        /// <summary>
        /// Unique identifier for the matter
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Matter name/title
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Matter description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Matter type
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Matter status
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Matter priority
        /// </summary>
        [JsonPropertyName("priority")]
        public string? Priority { get; set; }

        /// <summary>
        /// Primary contact for the matter
        /// </summary>
        [JsonPropertyName("contact")]
        public Contact? Contact { get; set; }

        /// <summary>
        /// Associated company
        /// </summary>
        [JsonPropertyName("company")]
        public Company? Company { get; set; }

        /// <summary>
        /// Assigned attorney/user
        /// </summary>
        [JsonPropertyName("assigned_to")]
        public User? AssignedTo { get; set; }

        /// <summary>
        /// Matter value/amount
        /// </summary>
        [JsonPropertyName("value")]
        public decimal? Value { get; set; }

        /// <summary>
        /// Expected resolution date
        /// </summary>
        [JsonPropertyName("expected_resolution_date")]
        public DateTime? ExpectedResolutionDate { get; set; }

        /// <summary>
        /// Actual resolution date
        /// </summary>
        [JsonPropertyName("resolution_date")]
        public DateTime? ResolutionDate { get; set; }

        /// <summary>
        /// Date when matter was created
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date when matter was last updated
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Custom fields for the matter
        /// </summary>
        [JsonPropertyName("custom_fields")]
        public Dictionary<string, object>? CustomFields { get; set; }

        /// <summary>
        /// Associated documents
        /// </summary>
        [JsonPropertyName("documents")]
        public List<Document>? Documents { get; set; }

        /// <summary>
        /// Associated events
        /// </summary>
        [JsonPropertyName("events")]
        public List<Event>? Events { get; set; }

        /// <summary>
        /// Associated notes
        /// </summary>
        [JsonPropertyName("notes")]
        public List<Note>? Notes { get; set; }
    }

    /// <summary>
    /// Request model for creating a new matter
    /// </summary>
    public class CreateMatterRequest
    {
        /// <summary>
        /// Matter name/title (required)
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Matter description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Matter type
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Matter status
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Matter priority
        /// </summary>
        [JsonPropertyName("priority")]
        public string? Priority { get; set; }

        /// <summary>
        /// Primary contact ID
        /// </summary>
        [JsonPropertyName("contact_id")]
        public int? ContactId { get; set; }

        /// <summary>
        /// Company ID
        /// </summary>
        [JsonPropertyName("company_id")]
        public int? CompanyId { get; set; }

        /// <summary>
        /// Assigned user ID
        /// </summary>
        [JsonPropertyName("assigned_to_id")]
        public int? AssignedToId { get; set; }

        /// <summary>
        /// Matter value/amount
        /// </summary>
        [JsonPropertyName("value")]
        public decimal? Value { get; set; }

        /// <summary>
        /// Expected resolution date
        /// </summary>
        [JsonPropertyName("expected_resolution_date")]
        public DateTime? ExpectedResolutionDate { get; set; }

        /// <summary>
        /// Custom fields for the matter
        /// </summary>
        [JsonPropertyName("custom_fields")]
        public Dictionary<string, object>? CustomFields { get; set; }
    }

    /// <summary>
    /// Request model for updating an existing matter
    /// </summary>
    public class UpdateMatterRequest
    {
        /// <summary>
        /// Matter name/title
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Matter description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Matter type
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Matter status
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Matter priority
        /// </summary>
        [JsonPropertyName("priority")]
        public string? Priority { get; set; }

        /// <summary>
        /// Primary contact ID
        /// </summary>
        [JsonPropertyName("contact_id")]
        public int? ContactId { get; set; }

        /// <summary>
        /// Company ID
        /// </summary>
        [JsonPropertyName("company_id")]
        public int? CompanyId { get; set; }

        /// <summary>
        /// Assigned user ID
        /// </summary>
        [JsonPropertyName("assigned_to_id")]
        public int? AssignedToId { get; set; }

        /// <summary>
        /// Matter value/amount
        /// </summary>
        [JsonPropertyName("value")]
        public decimal? Value { get; set; }

        /// <summary>
        /// Expected resolution date
        /// </summary>
        [JsonPropertyName("expected_resolution_date")]
        public DateTime? ExpectedResolutionDate { get; set; }

        /// <summary>
        /// Actual resolution date
        /// </summary>
        [JsonPropertyName("resolution_date")]
        public DateTime? ResolutionDate { get; set; }

        /// <summary>
        /// Custom fields for the matter
        /// </summary>
        [JsonPropertyName("custom_fields")]
        public Dictionary<string, object>? CustomFields { get; set; }
    }
}