using System.Text.Json.Serialization;

namespace LawMatics.SDK.Models
{
    /// <summary>
    /// Represents a file in Lawmatics
    /// </summary>
    public class Document
    {
        /// <summary>
        /// Unique identifier for the document
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Document name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Document description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Document type/category
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// File extension
        /// </summary>
        [JsonPropertyName("extension")]
        public string? Extension { get; set; }

        /// <summary>
        /// MIME type
        /// </summary>
        [JsonPropertyName("mime_type")]
        public string? MimeType { get; set; }

        /// <summary>
        /// File size in bytes
        /// </summary>
        [JsonPropertyName("size")]
        public long Size { get; set; }

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
        /// Thumbnail URL
        /// </summary>
        [JsonPropertyName("thumbnail_url")]
        public string? ThumbnailUrl { get; set; }

        /// <summary>
        /// Associated folder
        /// </summary>
        [JsonPropertyName("folder")]
        public Folder? Folder { get; set; }

        /// <summary>
        /// Associated matter
        /// </summary>
        [JsonPropertyName("matter")]
        public Matter? Matter { get; set; }

        /// <summary>
        /// Associated contact
        /// </summary>
        [JsonPropertyName("contact")]
        public Contact? Contact { get; set; }

        /// <summary>
        /// Document status
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Whether the document is publicly accessible
        /// </summary>
        [JsonPropertyName("public")]
        public bool Public { get; set; }

        /// <summary>
        /// Document tags
        /// </summary>
        [JsonPropertyName("tags")]
        public List<string>? Tags { get; set; }

        /// <summary>
        /// Date when document was created
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date when document was last updated
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Custom fields for the document
        /// </summary>
        [JsonPropertyName("custom_fields")]
        public Dictionary<string, object>? CustomFields { get; set; }
    }

    /// <summary>
    /// Represents a folder in Lawmatics
    /// </summary>
    public class Folder
    {
        /// <summary>
        /// Unique identifier for the folder
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Folder name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Folder description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Parent folder ID
        /// </summary>
        [JsonPropertyName("parent_id")]
        public int? ParentId { get; set; }

        /// <summary>
        /// Parent folder
        /// </summary>
        [JsonPropertyName("parent")]
        public Folder? Parent { get; set; }

        /// <summary>
        /// Child folders
        /// </summary>
        [JsonPropertyName("children")]
        public List<Folder>? Children { get; set; }

        /// <summary>
        /// Documents in this folder
        /// </summary>
        [JsonPropertyName("documents")]
        public List<Document>? Documents { get; set; }

        /// <summary>
        /// Folder path
        /// </summary>
        [JsonPropertyName("path")]
        public string? Path { get; set; }

        /// <summary>
        /// Folder color
        /// </summary>
        [JsonPropertyName("color")]
        public string? Color { get; set; }

        /// <summary>
        /// Whether the folder is publicly accessible
        /// </summary>
        [JsonPropertyName("public")]
        public bool Public { get; set; }

        /// <summary>
        /// Folder permissions
        /// </summary>
        [JsonPropertyName("permissions")]
        public FolderPermissions? Permissions { get; set; }

        /// <summary>
        /// Date when folder was created
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date when folder was last updated
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Custom fields for the folder
        /// </summary>
        [JsonPropertyName("custom_fields")]
        public Dictionary<string, object>? CustomFields { get; set; }
    }

    /// <summary>
    /// Folder permissions information
    /// </summary>
    public class FolderPermissions
    {
        /// <summary>
        /// Whether users can read the folder
        /// </summary>
        [JsonPropertyName("can_read")]
        public bool CanRead { get; set; }

        /// <summary>
        /// Whether users can write to the folder
        /// </summary>
        [JsonPropertyName("can_write")]
        public bool CanWrite { get; set; }

        /// <summary>
        /// Whether users can delete from the folder
        /// </summary>
        [JsonPropertyName("can_delete")]
        public bool CanDelete { get; set; }

        /// <summary>
        /// Whether users can share the folder
        /// </summary>
        [JsonPropertyName("can_share")]
        public bool CanShare { get; set; }
    }

    /// <summary>
    /// Request model for uploading a document
    /// </summary>
    public class UploadDocumentRequest
    {
        /// <summary>
        /// Document name (required)
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Document description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Document type/category
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Folder ID to upload to
        /// </summary>
        [JsonPropertyName("folder_id")]
        public int? FolderId { get; set; }

        /// <summary>
        /// Associated matter ID
        /// </summary>
        [JsonPropertyName("matter_id")]
        public int? MatterId { get; set; }

        /// <summary>
        /// Associated contact ID
        /// </summary>
        [JsonPropertyName("contact_id")]
        public int? ContactId { get; set; }

        /// <summary>
        /// Whether the document is publicly accessible
        /// </summary>
        [JsonPropertyName("public")]
        public bool Public { get; set; }

        /// <summary>
        /// Document tags
        /// </summary>
        [JsonPropertyName("tags")]
        public List<string>? Tags { get; set; }

        /// <summary>
        /// Custom fields for the document
        /// </summary>
        [JsonPropertyName("custom_fields")]
        public Dictionary<string, object>? CustomFields { get; set; }

        /// <summary>
        /// File content (for direct upload)
        /// </summary>
        [JsonIgnore]
        public byte[]? FileContent { get; set; }

        /// <summary>
        /// File stream (for streaming upload)
        /// </summary>
        [JsonIgnore]
        public Stream? FileStream { get; set; }

        /// <summary>
        /// File name (if different from document name)
        /// </summary>
        [JsonIgnore]
        public string? FileName { get; set; }
    }

    /// <summary>
    /// Request model for creating a new folder
    /// </summary>
    public class CreateFolderRequest
    {
        /// <summary>
        /// Folder name (required)
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Folder description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Parent folder ID
        /// </summary>
        [JsonPropertyName("parent_id")]
        public int? ParentId { get; set; }

        /// <summary>
        /// Folder color
        /// </summary>
        [JsonPropertyName("color")]
        public string? Color { get; set; }

        /// <summary>
        /// Whether the folder is publicly accessible
        /// </summary>
        [JsonPropertyName("public")]
        public bool Public { get; set; }

        /// <summary>
        /// Folder permissions
        /// </summary>
        [JsonPropertyName("permissions")]
        public FolderPermissions? Permissions { get; set; }

        /// <summary>
        /// Custom fields for the folder
        /// </summary>
        [JsonPropertyName("custom_fields")]
        public Dictionary<string, object>? CustomFields { get; set; }
    }

    /// <summary>
    /// Request model for updating an existing folder
    /// </summary>
    public class UpdateFolderRequest
    {
        /// <summary>
        /// Folder name
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Folder description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Parent folder ID
        /// </summary>
        [JsonPropertyName("parent_id")]
        public int? ParentId { get; set; }

        /// <summary>
        /// Folder color
        /// </summary>
        [JsonPropertyName("color")]
        public string? Color { get; set; }

        /// <summary>
        /// Whether the folder is publicly accessible
        /// </summary>
        [JsonPropertyName("public")]
        public bool? Public { get; set; }

        /// <summary>
        /// Folder permissions
        /// </summary>
        [JsonPropertyName("permissions")]
        public FolderPermissions? Permissions { get; set; }

        /// <summary>
        /// Custom fields for the folder
        /// </summary>
        [JsonPropertyName("custom_fields")]
        public Dictionary<string, object>? CustomFields { get; set; }
    }
}