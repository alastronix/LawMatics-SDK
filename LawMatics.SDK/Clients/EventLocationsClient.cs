using LawMatics.SDK.Configuration;
using LawMatics.SDK.Models;
using Microsoft.Extensions.Logging;

namespace LawMatics.SDK.Clients
{
    /// <summary>
    /// Client for managing event locations in the LawMatics API.
    /// </summary>
    public class EventLocationsClient : BaseClient
    {
        public EventLocationsClient(HttpClient httpClient, LawMaticsClientOptions options, ILogger? logger)
            : base(httpClient, options, logger)
        {
        }

        /// <summary>
        /// Gets all event locations with optional pagination and filtering.
        /// </summary>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="name">Filter by location name.</param>
        /// <param name="address">Filter by address.</param>
        /// <param name="capacityFrom">Filter by minimum capacity.</param>
        /// <param name="capacityTo">Filter by maximum capacity.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of event locations.</returns>
        public async Task<PagedResponse<EventLocation>> GetEventLocationsAsync(
            int page = 1,
            int pageSize = 20,
            string? name = null,
            string? address = null,
            int? capacityFrom = null,
            int? capacityTo = null,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["page"] = page.ToString(),
                ["page_size"] = pageSize.ToString()
            };

            if (!string.IsNullOrEmpty(name))
                parameters["name"] = name;

            if (!string.IsNullOrEmpty(address))
                parameters["address"] = address;

            if (capacityFrom.HasValue)
                parameters["capacity_from"] = capacityFrom.Value.ToString();

            if (capacityTo.HasValue)
                parameters["capacity_to"] = capacityTo.Value.ToString();

            return await GetAsync<PagedResponse<EventLocation>>("event-locations", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets a specific event location by ID.
        /// </summary>
        /// <param name="id">The event location ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The event location details.</returns>
        public async Task<EventLocation?> GetEventLocationAsync(int id, CancellationToken cancellationToken = default)
        {
            return await GetAsync<EventLocation>($"event-locations/{id}", cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Creates a new event location.
        /// </summary>
        /// <param name="request">The event location creation request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created event location.</returns>
        public async Task<EventLocation?> CreateEventLocationAsync(CreateEventLocationRequest request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<EventLocation>("event-locations", request, cancellationToken);
        }

        /// <summary>
        /// Updates an existing event location.
        /// </summary>
        /// <param name="id">The event location ID.</param>
        /// <param name="request">The event location update request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated event location.</returns>
        public async Task<EventLocation?> UpdateEventLocationAsync(int id, UpdateEventLocationRequest request, CancellationToken cancellationToken = default)
        {
            return await PutAsync<EventLocation>($"event-locations/{id}", request, cancellationToken);
        }

        /// <summary>
        /// Deletes an event location.
        /// </summary>
        /// <param name="id">The event location ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if deletion was successful.</returns>
        public async Task<bool> DeleteEventLocationAsync(int id, CancellationToken cancellationToken = default)
        {
            return await DeleteAsync($"event-locations/{id}", cancellationToken);
        }

        /// <summary>
        /// Gets event locations that can accommodate a specific capacity.
        /// </summary>
        /// <param name="requiredCapacity">The required capacity.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of event locations with sufficient capacity.</returns>
        public async Task<PagedResponse<EventLocation>> GetEventLocationsByCapacityAsync(
            int requiredCapacity,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            return await GetEventLocationsAsync(page, pageSize, capacityFrom: requiredCapacity, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Searches event locations by name or address.
        /// </summary>
        /// <param name="query">Search query.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of event locations matching the search query.</returns>
        public async Task<PagedResponse<EventLocation>> SearchEventLocationsAsync(
            string query,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["page"] = page.ToString(),
                ["page_size"] = pageSize.ToString(),
                ["search"] = query
            };

            return await GetAsync<PagedResponse<EventLocation>>("event-locations", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets events scheduled at a specific location.
        /// </summary>
        /// <param name="locationId">The event location ID.</param>
        /// <param name="dateFrom">Filter by start date (optional).</param>
        /// <param name="dateTo">Filter by end date (optional).</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of events at the location.</returns>
        public async Task<PagedResponse<Event>> GetEventsByLocationAsync(
            int locationId,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["page"] = page.ToString(),
                ["page_size"] = pageSize.ToString()
            };

            if (dateFrom.HasValue)
                parameters["date_from"] = dateFrom.Value.ToString("yyyy-MM-dd");

            if (dateTo.HasValue)
                parameters["date_to"] = dateTo.Value.ToString("yyyy-MM-dd");

            return await GetAsync<PagedResponse<Event>>($"event-locations/{locationId}/events", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets availability for an event location.
        /// </summary>
        /// <param name="locationId">The event location ID.</param>
        /// <param name="dateFrom">Start date.</param>
        /// <param name="dateTo">End date.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Location availability information.</returns>
        public async Task<LocationAvailability?> GetLocationAvailabilityAsync(
            int locationId,
            DateTime dateFrom,
            DateTime dateTo,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["date_from"] = dateFrom.ToString("yyyy-MM-dd"),
                ["date_to"] = dateTo.ToString("yyyy-MM-dd")
            };

            return await GetAsync<LocationAvailability>($"event-locations/{locationId}/availability", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets location utilization statistics.
        /// </summary>
        /// <param name="locationId">The event location ID.</param>
        /// <param name="dateFrom">Start date (optional).</param>
        /// <param name="dateTo">End date (optional).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Location utilization statistics.</returns>
        public async Task<LocationUtilization?> GetLocationUtilizationAsync(
            int locationId,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>();

            if (dateFrom.HasValue)
                parameters["date_from"] = dateFrom.Value.ToString("yyyy-MM-dd");

            if (dateTo.HasValue)
                parameters["date_to"] = dateTo.Value.ToString("yyyy-MM-dd");

            return await GetAsync<LocationUtilization>($"event-locations/{locationId}/utilization", parameters, cancellationToken);
        }

        /// <summary>
        /// Duplicates an event location.
        /// </summary>
        /// <param name="id">The event location ID to duplicate.</param>
        /// <param name="newName">The name for the duplicated location.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The duplicated event location.</returns>
        public async Task<EventLocation?> DuplicateEventLocationAsync(
            int id,
            string newName,
            CancellationToken cancellationToken = default)
        {
            var request = new { name = newName };
            return await PostAsync<EventLocation>($"event-locations/{id}/duplicate", request, cancellationToken);
        }

        /// <summary>
        /// Validates event location data.
        /// </summary>
        /// <param name="request">The event location request to validate.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Validation result.</returns>
        public async Task<LocationValidationResult?> ValidateEventLocationAsync(
            CreateEventLocationRequest request,
            CancellationToken cancellationToken = default)
        {
            return await PostAsync<LocationValidationResult>("event-locations/validate", request, cancellationToken);
        }

        /// <summary>
        /// Gets location capacity utilization over time.
        /// </summary>
        /// <param name="locationId">The event location ID.</param>
        /// <param name="dateFrom">Start date.</param>
        /// <param name="dateTo">End date.</param>
        /// <param name="groupBy">How to group the data (day, week, month).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Capacity utilization data.</returns>
        public async Task<CapacityUtilizationReport?> GetCapacityUtilizationReportAsync(
            int locationId,
            DateTime dateFrom,
            DateTime dateTo,
            string groupBy = "day",
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["date_from"] = dateFrom.ToString("yyyy-MM-dd"),
                ["date_to"] = dateTo.ToString("yyyy-MM-dd"),
                ["group_by"] = groupBy
            };

            return await GetAsync<CapacityUtilizationReport>($"event-locations/{locationId}/capacity-report", parameters, cancellationToken);
        }

        /// <summary>
        /// Exports event locations to CSV.
        /// </summary>
        /// <param name="name">Filter by name (optional).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The CSV file content.</returns>
        public async Task<byte[]> ExportEventLocationsToCsvAsync(
            string? name = null,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(name))
                parameters["name"] = name;

            parameters["format"] = "csv";

            return await GetFileAsync("event-locations/export", parameters, cancellationToken);
        }
    }

    /// <summary>
    /// Represents location availability information.
    /// </summary>
    public class LocationAvailability
    {
        /// <summary>
        /// Gets or sets the location ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("location_id")]
        public int LocationId { get; set; }

        /// <summary>
        /// Gets or sets the location name.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("location_name")]
        public string LocationName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the location capacity.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("capacity")]
        public int Capacity { get; set; }

        /// <summary>
        /// Gets or sets available time slots.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("available_slots")]
        public List<TimeSlot> AvailableSlots { get; set; } = new();

        /// <summary>
        /// Gets or sets busy time slots.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("busy_slots")]
        public List<TimeSlot> BusySlots { get; set; } = new();

        /// <summary>
        /// Gets or sets the total available hours.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("available_hours")]
        public decimal AvailableHours { get; set; }
    }

    /// <summary>
    /// Represents a time slot.
    /// </summary>
    public class TimeSlot
    {
        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("start_time")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("end_time")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Gets or sets the event name (for busy slots).
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("event_name")]
        public string? EventName { get; set; }

        /// <summary>
        /// Gets or sets the event ID (for busy slots).
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("event_id")]
        public int? EventId { get; set; }
    }

    /// <summary>
    /// Represents location utilization statistics.
    /// </summary>
    public class LocationUtilization
    {
        /// <summary>
        /// Gets or sets the location ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("location_id")]
        public int LocationId { get; set; }

        /// <summary>
        /// Gets or sets the location name.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("location_name")]
        public string LocationName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the utilization percentage.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("utilization_percentage")]
        public decimal UtilizationPercentage { get; set; }

        /// <summary>
        /// Gets or sets the total events.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("total_events")]
        public int TotalEvents { get; set; }

        /// <summary>
        /// Gets or sets the total attendees.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("total_attendees")]
        public int TotalAttendees { get; set; }

        /// <summary>
        /// Gets or sets the average attendance.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("average_attendance")]
        public decimal AverageAttendance { get; set; }

        /// <summary>
        /// Gets or sets the utilization by day.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("daily_utilization")]
        public List<DailyUtilization> DailyUtilization { get; set; } = new();
    }

    /// <summary>
    /// Represents daily utilization data.
    /// </summary>
    public class DailyUtilization
    {
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("date")]
        public string Date { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the number of events.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("events")]
        public int Events { get; set; }

        /// <summary>
        /// Gets or sets the total attendees.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("attendees")]
        public int Attendees { get; set; }

        /// <summary>
        /// Gets or sets the utilization percentage.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("utilization_percentage")]
        public decimal UtilizationPercentage { get; set; }
    }

    /// <summary>
    /// Represents location validation result.
    /// </summary>
    public class LocationValidationResult
    {
        /// <summary>
        /// Gets or sets whether the location is valid.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("is_valid")]
        public bool IsValid { get; set; }

        /// <summary>
        /// Gets or sets validation errors.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("errors")]
        public List<string> Errors { get; set; } = new();

        /// <summary>
        /// Gets or sets validation warnings.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("warnings")]
        public List<string> Warnings { get; set; } = new();
    }

    /// <summary>
    /// Represents capacity utilization report.
    /// </summary>
    public class CapacityUtilizationReport
    {
        /// <summary>
        /// Gets or sets the location ID.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("location_id")]
        public int LocationId { get; set; }

        /// <summary>
        /// Gets or sets the report period.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("period")]
        public string Period { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the utilization data points.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("data_points")]
        public List<CapacityDataPoint> DataPoints { get; set; } = new();

        /// <summary>
        /// Gets or sets the average utilization.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("average_utilization")]
        public decimal AverageUtilization { get; set; }

        /// <summary>
        /// Gets or sets the peak utilization.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("peak_utilization")]
        public decimal PeakUtilization { get; set; }
    }

    /// <summary>
    /// Represents a capacity data point.
    /// </summary>
    public class CapacityDataPoint
    {
        /// <summary>
        /// Gets or sets the date or period.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("date")]
        public string Date { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the utilization percentage.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("utilization_percentage")]
        public decimal UtilizationPercentage { get; set; }

        /// <summary>
        /// Gets or sets the number of events.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("events")]
        public int Events { get; set; }

        /// <summary>
        /// Gets or sets the total attendees.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("attendees")]
        public int Attendees { get; set; }
    }
}