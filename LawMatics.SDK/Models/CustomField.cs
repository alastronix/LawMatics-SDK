using System.Text.Json.Serialization;

namespace LawMatics.SDK.Models
{
    /// <summary>
    /// Represents a custom field in Lawmatics
    /// </summary>
    public class CustomField
    {
        /// <summary>
        /// Unique identifier for the custom field
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Field name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Field label
        /// </summary>
        [JsonPropertyName("label")]
        public string Label { get; set; } = string.Empty;

        /// <summary>
        /// Field description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Field type (text, number, date, boolean, dropdown, checkbox, textarea)
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Field category (contact, company, matter, event, document)
        /// </summary>
        [JsonPropertyName("category")]
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// Whether the field is required
        /// </summary>
        [JsonPropertyName("required")]
        public bool Required { get; set; }

        /// <summary>
        /// Whether the field is read-only
        /// </summary>
        [JsonPropertyName("read_only")]
        public bool ReadOnly { get; set; }

        /// <summary>
        /// Whether the field is visible
        /// </summary>
        [JsonPropertyName("visible")]
        public bool Visible { get; set; }

        /// <summary>
        /// Field order
        /// </summary>
        [JsonPropertyName("order")]
        public int Order { get; set; }

        /// <summary>
        /// Default value
        /// </summary>
        [JsonPropertyName("default_value")]
        public object? DefaultValue { get; set; }

        /// <summary>
        /// Field options (for dropdown, checkbox types)
        /// </summary>
        [JsonPropertyName("options")]
        public List<CustomFieldOption>? Options { get; set; }

        /// <summary>
        /// Validation rules
        /// </summary>
        [JsonPropertyName("validation")]
        public CustomFieldValidation? Validation { get; set; }

        /// <summary>
        /// Field settings
        /// </summary>
        [JsonPropertyName("settings")]
        public Dictionary<string, object>? Settings { get; set; }

        /// <summary>
        /// Date when field was created
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date when field was last updated
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

    /// <summary>
    /// Custom field option for dropdown/checkbox fields
    /// </summary>
    public class CustomFieldOption
    {
        /// <summary>
        /// Option ID
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Option value
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// Option label
        /// </summary>
        [JsonPropertyName("label")]
        public string Label { get; set; } = string.Empty;

        /// <summary>
        /// Option order
        /// </summary>
        [JsonPropertyName("order")]
        public int Order { get; set; }

        /// <summary>
        /// Whether the option is selected by default
        /// </summary>
        [JsonPropertyName("default")]
        public bool Default { get; set; }
    }

    /// <summary>
    /// Custom field validation rules
    /// </summary>
    public class CustomFieldValidation
    {
        /// <summary>
        /// Minimum length (for text fields)
        /// </summary>
        [JsonPropertyName("min_length")]
        public int? MinLength { get; set; }

        /// <summary>
        /// Maximum length (for text fields)
        /// </summary>
        [JsonPropertyName("max_length")]
        public int? MaxLength { get; set; }

        /// <summary>
        /// Minimum value (for number fields)
        /// </summary>
        [JsonPropertyName("min_value")]
        public decimal? MinValue { get; set; }

        /// <summary>
        /// Maximum value (for number fields)
        /// </summary>
        [JsonPropertyName("max_value")]
        public decimal? MaxValue { get; set; }

        /// <summary>
        /// Regular expression pattern
        /// </summary>
        [JsonPropertyName("pattern")]
        public string? Pattern { get; set; }

        /// <summary>
        /// Error message for validation failures
        /// </summary>
        [JsonPropertyName("error_message")]
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Whether the field must be unique
        /// </summary>
        [JsonPropertyName("unique")]
        public bool Unique { get; set; }
    }

    /// <summary>
    /// Represents a custom form in Lawmatics
    /// </summary>
    public class CustomForm
    {
        /// <summary>
        /// Unique identifier for the form
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Form name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Form title
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Form description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Form type (contact, company, matter, event, document)
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Form status (active, inactive, draft)
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Form fields
        /// </summary>
        [JsonPropertyName("fields")]
        public List<CustomField> Fields { get; set; } = new List<CustomField>();

        /// <summary>
        /// Form settings
        /// </summary>
        [JsonPropertyName("settings")]
        public FormSettings? Settings { get; set; }

        /// <summary>
        /// Form URL (for web forms)
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }

        /// <summary>
        /// Form submission URL
        /// </summary>
        [JsonPropertyName("submission_url")]
        public string? SubmissionUrl { get; set; }

        /// <summary>
        /// Whether the form requires authentication
        /// </summary>
        [JsonPropertyName("requires_auth")]
        public bool RequiresAuth { get; set; }

        /// <summary>
        /// Form submissions count
        /// </summary>
        [JsonPropertyName("submissions_count")]
        public int SubmissionsCount { get; set; }

        /// <summary>
        /// Date when form was created
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date when form was last updated
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

    /// <summary>
    /// Form settings
    /// </summary>
    public class FormSettings
    {
        /// <summary>
        /// Whether to show progress bar
        /// </summary>
        [JsonPropertyName("show_progress")]
        public bool ShowProgress { get; set; }

        /// <summary>
        /// Whether to allow saving drafts
        /// </summary>
        [JsonPropertyName("allow_drafts")]
        public bool AllowDrafts { get; set; }

        /// <summary>
        /// Whether to send confirmation email
        /// </summary>
        [JsonPropertyName("send_confirmation")]
        public bool SendConfirmation { get; set; }

        /// <summary>
        /// Confirmation email template
        /// </summary>
        [JsonPropertyName("confirmation_template")]
        public string? ConfirmationTemplate { get; set; }

        /// <summary>
        /// Redirect URL after submission
        /// </summary>
        [JsonPropertyName("redirect_url")]
        public string? RedirectUrl { get; set; }

        /// <summary>
        /// Success message
        /// </summary>
        [JsonPropertyName("success_message")]
        public string? SuccessMessage { get; set; }

        /// <summary>
        /// CSS styles
        /// </summary>
        [JsonPropertyName("styles")]
        public Dictionary<string, object>? Styles { get; set; }
    }

    /// <summary>
    /// Form submission data
    /// </summary>
    public class FormSubmission
    {
        /// <summary>
        /// Submission ID
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Form ID
        /// </summary>
        [JsonPropertyName("form_id")]
        public int FormId { get; set; }

        /// <summary>
        /// Submitted data
        /// </summary>
        [JsonPropertyName("data")]
        public Dictionary<string, object> Data { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// Submission status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Submitted by user
        /// </summary>
        [JsonPropertyName("submitted_by")]
        public User? SubmittedBy { get; set; }

        /// <summary>
        /// Associated contact (if created)
        /// </summary>
        [JsonPropertyName("contact")]
        public Contact? Contact { get; set; }

        /// <summary>
        /// Associated company (if created)
        /// </summary>
        [JsonPropertyName("company")]
        public Company? Company { get; set; }

        /// <summary>
        /// Associated matter (if created)
        /// </summary>
        [JsonPropertyName("matter")]
        public Matter? Matter { get; set; }

        /// <summary>
        /// Date when submission was created
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date when submission was last updated
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

    /// <summary>
    /// Request model for creating a custom field
    /// </summary>
    public class CreateCustomFieldRequest
    {
        /// <summary>
        /// Field name (required)
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Field label (required)
        /// </summary>
        [JsonPropertyName("label")]
        public string Label { get; set; } = string.Empty;

        /// <summary>
        /// Field description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Field type (required)
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Field category (required)
        /// </summary>
        [JsonPropertyName("category")]
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// Whether the field is required
        /// </summary>
        [JsonPropertyName("required")]
        public bool Required { get; set; }

        /// <summary>
        /// Whether the field is read-only
        /// </summary>
        [JsonPropertyName("read_only")]
        public bool ReadOnly { get; set; }

        /// <summary>
        /// Field order
        /// </summary>
        [JsonPropertyName("order")]
        public int Order { get; set; }

        /// <summary>
        /// Default value
        /// </summary>
        [JsonPropertyName("default_value")]
        public object? DefaultValue { get; set; }

        /// <summary>
        /// Field options (for dropdown, checkbox types)
        /// </summary>
        [JsonPropertyName("options")]
        public List<CustomFieldOption>? Options { get; set; }

        /// <summary>
        /// Validation rules
        /// </summary>
        [JsonPropertyName("validation")]
        public CustomFieldValidation? Validation { get; set; }
    }

    /// <summary>
    /// Request model for creating a custom form
    /// </summary>
    public class CreateCustomFormRequest
    {
        /// <summary>
        /// Form name (required)
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Form title (required)
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Form description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Form type (required)
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Form status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = "active";

        /// <summary>
        /// Form field IDs
        /// </summary>
        [JsonPropertyName("field_ids")]
        public List<int> FieldIds { get; set; } = new List<int>();

        /// <summary>
        /// Form settings
        /// </summary>
        [JsonPropertyName("settings")]
        public FormSettings? Settings { get; set; }
    }
}