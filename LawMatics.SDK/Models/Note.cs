using System.Text.Json.Serialization;

namespace LawMatics.SDK.Models
{
    /// <summary>
    /// Represents a note in Lawmatics
    /// </summary>
    public class Note
    {
        /// <summary>
        /// Unique identifier for the note
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Note title
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        /// <summary>
        /// Note content
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Note type (general, phone_call, email, meeting, document)
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Note category
        /// </summary>
        [JsonPropertyName("category")]
        public string? Category { get; set; }

        /// <summary>
        /// Note priority (low, medium, high)
        /// </summary>
        [JsonPropertyName("priority")]
        public string? Priority { get; set; }

        /// <summary>
        /// Note status (active, archived)
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Author of the note
        /// </summary>
        [JsonPropertyName("author")]
        public User? Author { get; set; }

        /// <summary>
        /// Associated contact
        /// </summary>
        [JsonPropertyName("contact")]
        public Contact? Contact { get; set; }

        /// <summary>
        /// Associated company
        /// </summary>
        [JsonPropertyName("company")]
        public Company? Company { get; set; }

        /// <summary>
        /// Associated matter
        /// </summary>
        [JsonPropertyName("matter")]
        public Matter? Matter { get; set; }

        /// <summary>
        /// Associated event
        /// </summary>
        [JsonPropertyName("event")]
        public Event? Event { get; set; }

        /// <summary>
        /// Associated document
        /// </summary>
        [JsonPropertyName("document")]
        public Document? Document { get; set; }

        /// <summary>
        /// Whether the note is pinned
        /// </summary>
        [JsonPropertyName("pinned")]
        public bool Pinned { get; set; }

        /// <summary>
        /// Whether the note is private
        /// </summary>
        [JsonPropertyName("private")]
        public bool Private { get; set; }

        /// <summary>
        /// Note tags
        /// </summary>
        [JsonPropertyName("tags")]
        public List<string>? Tags { get; set; }

        /// <summary>
        /// Note attachments
        /// </summary>
        [JsonPropertyName("attachments")]
        public List<NoteAttachment>? Attachments { get; set; }

        /// <summary>
        /// Mentioned users
        /// </summary>
        [JsonPropertyName("mentions")]
        public List<User>? Mentions { get; set; }

        /// <summary>
        /// Date when note was created
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date when note was last updated
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Custom fields for the note
        /// </summary>
        [JsonPropertyName("custom_fields")]
        public Dictionary<string, object>? CustomFields { get; set; }
    }

    /// <summary>
    /// Note attachment information
    /// </summary>
    public class NoteAttachment
    {
        /// <summary>
        /// Attachment ID
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Attachment name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// File URL
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }

        /// <summary>
        /// Download URL
        /// </summary>
        [JsonPropertyName("download_url")]
        public string? DownloadUrl { get; set; }

        /// <summary>
        /// File size in bytes
        /// </summary>
        [JsonPropertyName("size")]
        public long Size { get; set; }

        /// <summary>
        /// MIME type
        /// </summary>
        [JsonPropertyName("mime_type")]
        public string? MimeType { get; set; }

        /// <summary>
        /// File extension
        /// </summary>
        [JsonPropertyName("extension")]
        public string? Extension { get; set; }
    }

    /// <summary>
    /// Request model for creating a new note
    /// </summary>
    public class CreateNoteRequest
    {
        /// <summary>
        /// Note title
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        /// <summary>
        /// Note content (required)
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Note type
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Note category
        /// </summary>
        [JsonPropertyName("category")]
        public string? Category { get; set; }

        /// <summary>
        /// Note priority
        /// </summary>
        [JsonPropertyName("priority")]
        public string? Priority { get; set; }

        /// <summary>
        /// Contact ID
        /// </summary>
        [JsonPropertyName("contact_id")]
        public int? ContactId { get; set; }

        /// <summary>
        /// Company ID
        /// </summary>
        [JsonPropertyName("company_id")]
        public int? CompanyId { get; set; }

        /// <summary>
        /// Matter ID
        /// </summary>
        [JsonPropertyName("matter_id")]
        public int? MatterId { get; set; }

        /// <summary>
        /// Event ID
        /// </summary>
        [JsonPropertyName("event_id")]
        public int? EventId { get; set; }

        /// <summary>
        /// Document ID
        /// </summary>
        [JsonPropertyName("document_id")]
        public int? DocumentId { get; set; }

        /// <summary>
        /// Whether the note is pinned
        /// </summary>
        [JsonPropertyName("pinned")]
        public bool Pinned { get; set; }

        /// <summary>
        /// Whether the note is private
        /// </summary>
        [JsonPropertyName("private")]
        public bool Private { get; set; }

        /// <summary>
        /// Note tags
        /// </summary>
        [JsonPropertyName("tags")]
        public List<string>? Tags { get; set; }

        /// <summary>
        /// Mentioned user IDs
        /// </summary>
        [JsonPropertyName("mention_ids")]
        public List<int>? MentionIds { get; set; }

        /// <summary>
        /// Custom fields for the note
        /// </summary>
        [JsonPropertyName("custom_fields")]
        public Dictionary<string, object>? CustomFields { get; set; }
    }

    /// <summary>
    /// Request model for updating an existing note
    /// </summary>
    public class UpdateNoteRequest
    {
        /// <summary>
        /// Note title
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        /// <summary>
        /// Note content
        /// </summary>
        [JsonPropertyName("content")]
        public string? Content { get; set; }

        /// <summary>
        /// Note type
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Note category
        /// </summary>
        [JsonPropertyName("category")]
        public string? Category { get; set; }

        /// <summary>
        /// Note priority
        /// </summary>
        [JsonPropertyName("priority")]
        public string? Priority { get; set; }

        /// <summary>
        /// Note status
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Contact ID
        /// </summary>
        [JsonPropertyName("contact_id")]
        public int? ContactId { get; set; }

        /// <summary>
        /// Company ID
        /// </summary>
        [JsonPropertyName("company_id")]
        public int? CompanyId { get; set; }

        /// <summary>
        /// Matter ID
        /// </summary>
        [JsonPropertyName("matter_id")]
        public int? MatterId { get; set; }

        /// <summary>
        /// Event ID
        /// </summary>
        [JsonPropertyName("event_id")]
        public int? EventId { get; set; }

        /// <summary>
        /// Document ID
        /// </summary>
        [JsonPropertyName("document_id")]
        public int? DocumentId { get; set; }

        /// <summary>
        /// Whether the note is pinned
        /// </summary>
        [JsonPropertyName("pinned")]
        public bool? Pinned { get; set; }

        /// <summary>
        /// Whether the note is private
        /// </summary>
        [JsonPropertyName("private")]
        public bool? Private { get; set; }

        /// <summary>
        /// Note tags
        /// </summary>
        [JsonPropertyName("tags")]
        public List<string>? Tags { get; set; }

        /// <summary>
        /// Mentioned user IDs
        /// </summary>
        [JsonPropertyName("mention_ids")]
        public List<int>? MentionIds { get; set; }

        /// <summary>
        /// Custom fields for the note
        /// </summary>
        [JsonPropertyName("custom_fields")]
        public Dictionary<string, object>? CustomFields { get; set; }
    }
}