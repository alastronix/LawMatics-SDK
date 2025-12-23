using System.Text.Json.Serialization;

namespace LawMatics.SDK.Models
{
    /// <summary>
    /// Represents an event/appointment in Lawmatics
    /// </summary>
    public class Event
    {
        /// <summary>
        /// Unique identifier for the event
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Event title
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Event description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Event type
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Event status
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Event location
        /// </summary>
        [JsonPropertyName("location")]
        public string? Location { get; set; }

        /// <summary>
        /// Event start date and time
        /// </summary>
        [JsonPropertyName("start_time")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Event end date and time
        /// </summary>
        [JsonPropertyName("end_time")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Event timezone
        /// </summary>
        [JsonPropertyName("timezone")]
        public string? Timezone { get; set; }

        /// <summary>
        /// Whether this is an all-day event
        /// </summary>
        [JsonPropertyName("all_day")]
        public bool AllDay { get; set; }

        /// <summary>
        /// Event reminder settings
        /// </summary>
        [JsonPropertyName("reminders")]
        public List<EventReminder>? Reminders { get; set; }

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
        /// Event organizer/creator
        /// </summary>
        [JsonPropertyName("organizer")]
        public User? Organizer { get; set; }

        /// <summary>
        /// Event attendees
        /// </summary>
        [JsonPropertyName("attendees")]
        public List<EventAttendee>? Attendees { get; set; }

        /// <summary>
        /// Whether the event is recurring
        /// </summary>
        [JsonPropertyName("recurring")]
        public bool Recurring { get; set; }

        /// <summary>
        /// Recurrence pattern
        /// </summary>
        [JsonPropertyName("recurrence_pattern")]
        public string? RecurrencePattern { get; set; }

        /// <summary>
        /// Date when event was created
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date when event was last updated
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Custom fields for the event
        /// </summary>
        [JsonPropertyName("custom_fields")]
        public Dictionary<string, object>? CustomFields { get; set; }
    }

    /// <summary>
    /// Event reminder information
    /// </summary>
    public class EventReminder
    {
        /// <summary>
        /// Reminder type (email, sms, popup)
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Minutes before the event to send reminder
        /// </summary>
        [JsonPropertyName("minutes_before")]
        public int MinutesBefore { get; set; }

        /// <summary>
        /// Whether the reminder is enabled
        /// </summary>
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }
    }

    /// <summary>
    /// Event attendee information
    /// </summary>
    public class EventAttendee
    {
        /// <summary>
        /// Attendee ID
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Attendee email
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Attendee name
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Attendance status (accepted, declined, tentative)
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Whether the attendee is required
        /// </summary>
        [JsonPropertyName("required")]
        public bool Required { get; set; }
    }

    /// <summary>
    /// Request model for creating a new event
    /// </summary>
    public class CreateEventRequest
    {
        /// <summary>
        /// Event title (required)
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Event description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Event type
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Event location
        /// </summary>
        [JsonPropertyName("location")]
        public string? Location { get; set; }

        /// <summary>
        /// Event start date and time (required)
        /// </summary>
        [JsonPropertyName("start_time")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Event end date and time (required)
        /// </summary>
        [JsonPropertyName("end_time")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Event timezone
        /// </summary>
        [JsonPropertyName("timezone")]
        public string? Timezone { get; set; }

        /// <summary>
        /// Whether this is an all-day event
        /// </summary>
        [JsonPropertyName("all_day")]
        public bool AllDay { get; set; }

        /// <summary>
        /// Event reminder settings
        /// </summary>
        [JsonPropertyName("reminders")]
        public List<EventReminder>? Reminders { get; set; }

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
        /// Event attendees
        /// </summary>
        [JsonPropertyName("attendees")]
        public List<EventAttendee>? Attendees { get; set; }

        /// <summary>
        /// Custom fields for the event
        /// </summary>
        [JsonPropertyName("custom_fields")]
        public Dictionary<string, object>? CustomFields { get; set; }
    }

    /// <summary>
    /// Request model for updating an existing event
    /// </summary>
    public class UpdateEventRequest
    {
        /// <summary>
        /// Event title
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        /// <summary>
        /// Event description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Event type
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Event status
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Event location
        /// </summary>
        [JsonPropertyName("location")]
        public string? Location { get; set; }

        /// <summary>
        /// Event start date and time
        /// </summary>
        [JsonPropertyName("start_time")]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// Event end date and time
        /// </summary>
        [JsonPropertyName("end_time")]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// Event timezone
        /// </summary>
        [JsonPropertyName("timezone")]
        public string? Timezone { get; set; }

        /// <summary>
        /// Whether this is an all-day event
        /// </summary>
        [JsonPropertyName("all_day")]
        public bool? AllDay { get; set; }

        /// <summary>
        /// Event reminder settings
        /// </summary>
        [JsonPropertyName("reminders")]
        public List<EventReminder>? Reminders { get; set; }

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
        /// Event attendees
        /// </summary>
        [JsonPropertyName("attendees")]
        public List<EventAttendee>? Attendees { get; set; }

        /// <summary>
        /// Custom fields for the event
        /// </summary>
        [JsonPropertyName("custom_fields")]
        public Dictionary<string, object>? CustomFields { get; set; }
    }
}