using LawMatics.SDK.Configuration;
using LawMatics.SDK.Exceptions;
using LawMatics.SDK.Models;
using Microsoft.Extensions.Logging;

namespace LawMatics.SDK.Clients
{
    /// <summary>
    /// Client for managing events (appointments) in the Lawmatics API
    /// </summary>
    public class EventsClient : BaseClient
    {
        public EventsClient(HttpClient httpClient, LawMaticsClientOptions options, ILogger? logger = null)
            : base(httpClient, options, logger)
        {
        }

        /// <summary>
        /// Get all events with optional filtering and pagination
        /// </summary>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>Paginated list of events</returns>
        public async Task<ApiResponse<List<Event>>> GetEventsAsync(FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Event>>($"/events?{queryParams}");
            return response;
        }

        /// <summary>
        /// Get a specific event by ID
        /// </summary>
        /// <param name="id">Event ID</param>
        /// <returns>Event details</returns>
        public async Task<Event> GetEventAsync(int id)
        {
            var response = await GetAsync<Event>($"/events/{id}");
            return response.Data;
        }

        /// <summary>
        /// Create a new event
        /// </summary>
        /// <param name="request">Event creation request</param>
        /// <returns>Created event</returns>
        public async Task<Event> CreateEventAsync(CreateEventRequest request)
        {
            var response = await PostAsync<Event>("/events", request);
            return response.Data;
        }

        /// <summary>
        /// Update an existing event
        /// </summary>
        /// <param name="id">Event ID</param>
        /// <param name="request">Event update request</param>
        /// <returns>Updated event</returns>
        public async Task<Event> UpdateEventAsync(int id, CreateEventRequest request)
        {
            var response = await PutAsync<Event>($"/events/{id}", request);
            return response.Data;
        }

        /// <summary>
        /// Delete an event
        /// </summary>
        /// <param name="id">Event ID</param>
        public async Task DeleteEventAsync(int id)
        {
            await DeleteAsync($"/events/{id}");
        }

        /// <summary>
        /// Get events by date range
        /// </summary>
        /// <param name="startDate">Start date filter</param>
        /// <param name="endDate">End date filter</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of events within date range</returns>
        public async Task<ApiResponse<List<Event>>> GetEventsByDateRangeAsync(DateTime startDate, DateTime endDate, FilterParameters? parameters = null)
        {
            var filterParams = parameters ?? new FilterParameters();
            filterParams.StartDate = startDate;
            filterParams.EndDate = endDate;
            
            var queryParams = BuildQueryString(filterParams);
            var response = await GetAsync<List<Event>>($"/events?{queryParams}");
            return response;
        }

        /// <summary>
        /// Get events for a specific date
        /// </summary>
        /// <param name="date">Date to filter by</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of events for specified date</returns>
        public async Task<ApiResponse<List<Event>>> GetEventsByDateAsync(DateTime date, FilterParameters? parameters = null)
        {
            return await GetEventsByDateRangeAsync(date.Date, date.Date.AddDays(1).AddTicks(-1), parameters);
        }

        /// <summary>
        /// Get events by status
        /// </summary>
        /// <param name="status">Status to filter by</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of events with specified status</returns>
        public async Task<ApiResponse<List<Event>>> GetEventsByStatusAsync(string status, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Event>>($"/events?status={status}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get events by priority
        /// </summary>
        /// <param name="priority">Priority to filter by</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of events with specified priority</returns>
        public async Task<ApiResponse<List<Event>>> GetEventsByPriorityAsync(string priority, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Event>>($"/events?priority={priority}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get events by location
        /// </summary>
        /// <param name="locationId">Location ID to filter by</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of events at specified location</returns>
        public async Task<ApiResponse<List<Event>>> GetEventsByLocationAsync(int locationId, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Event>>($"/events?location_id={locationId}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get events by type
        /// </summary>
        /// <param name="eventTypeId">Event type ID to filter by</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of events of specified type</returns>
        public async Task<ApiResponse<List<Event>>> GetEventsByTypeAsync(int eventTypeId, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Event>>($"/events?event_type_id={eventTypeId}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get events assigned to a specific user
        /// </summary>
        /// <param name="assignedToId">User ID to filter by</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of events assigned to specified user</returns>
        public async Task<ApiResponse<List<Event>>> GetEventsByAssignedUserAsync(int assignedToId, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Event>>($"/events?assigned_to_id={assignedToId}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get events for a specific contact
        /// </summary>
        /// <param name="contactId">Contact ID to filter by</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of events for specified contact</returns>
        public async Task<ApiResponse<List<Event>>> GetEventsByContactAsync(int contactId, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Event>>($"/events?contact_id={contactId}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get events for a specific matter
        /// </summary>
        /// <param name="matterId">Matter ID to filter by</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of events for specified matter</returns>
        public async Task<ApiResponse<List<Event>>> GetEventsByMatterAsync(int matterId, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Event>>($"/events?matter_id={matterId}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get upcoming events
        /// </summary>
        /// <param name="days">Number of days ahead to look</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of upcoming events</returns>
        public async Task<ApiResponse<List<Event>>> GetUpcomingEventsAsync(int days = 7, FilterParameters? parameters = null)
        {
            var startDate = DateTime.Now;
            var endDate = startDate.AddDays(days);
            return await GetEventsByDateRangeAsync(startDate, endDate, parameters);
        }

        /// <summary>
        /// Get past events
        /// </summary>
        /// <param name="days">Number of days back to look</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of past events</returns>
        public async Task<ApiResponse<List<Event>>> GetPastEventsAsync(int days = 7, FilterParameters? parameters = null)
        {
            var endDate = DateTime.Now;
            var startDate = endDate.AddDays(-days);
            return await GetEventsByDateRangeAsync(startDate, endDate, parameters);
        }

        #region Event Location Management

        /// <summary>
        /// Get all event locations
        /// </summary>
        /// <returns>List of event locations</returns>
        public async Task<ApiResponse<List<EventLocation>>> GetEventLocationsAsync()
        {
            var response = await GetAsync<List<EventLocation>>("/event-locations");
            return response;
        }

        /// <summary>
        /// Get a specific event location by ID
        /// </summary>
        /// <param name="id">Location ID</param>
        /// <returns>Location details</returns>
        public async Task<EventLocation> GetEventLocationAsync(int id)
        {
            var response = await GetAsync<EventLocation>($"/event-locations/{id}");
            return response.Data;
        }

        /// <summary>
        /// Create a new event location
        /// </summary>
        /// <param name="request">Location creation request</param>
        /// <returns>Created location</returns>
        public async Task<EventLocation> CreateEventLocationAsync(EventLocation request)
        {
            var response = await PostAsync<EventLocation>("/event-locations", request);
            return response.Data;
        }

        /// <summary>
        /// Update an existing event location
        /// </summary>
        /// <param name="id">Location ID</param>
        /// <param name="request">Location update request</param>
        /// <returns>Updated location</returns>
        public async Task<EventLocation> UpdateEventLocationAsync(int id, EventLocation request)
        {
            var response = await PutAsync<EventLocation>($"/event-locations/{id}", request);
            return response.Data;
        }

        /// <summary>
        /// Delete an event location
        /// </summary>
        /// <param name="id">Location ID</param>
        public async Task DeleteEventLocationAsync(int id)
        {
            await DeleteAsync($"/event-locations/{id}");
        }

        #endregion

        #region Event Type Management

        /// <summary>
        /// Get all event types
        /// </summary>
        /// <returns>List of event types</returns>
        public async Task<ApiResponse<List<EventType>>> GetEventTypesAsync()
        {
            var response = await GetAsync<List<EventType>>("/event-types");
            return response;
        }

        /// <summary>
        /// Get a specific event type by ID
        /// </summary>
        /// <param name="id">Event type ID</param>
        /// <returns>Event type details</returns>
        public async Task<EventType> GetEventTypeAsync(int id)
        {
            var response = await GetAsync<EventType>($"/event-types/{id}");
            return response.Data;
        }

        /// <summary>
        /// Create a new event type
        /// </summary>
        /// <param name="request">Event type creation request</param>
        /// <returns>Created event type</returns>
        public async Task<EventType> CreateEventTypeAsync(EventType request)
        {
            var response = await PostAsync<EventType>("/event-types", request);
            return response.Data;
        }

        /// <summary>
        /// Update an existing event type
        /// </summary>
        /// <param name="id">Event type ID</param>
        /// <param name="request">Event type update request</param>
        /// <returns>Updated event type</returns>
        public async Task<EventType> UpdateEventTypeAsync(int id, EventType request)
        {
            var response = await PutAsync<EventType>($"/event-types/{id}", request);
            return response.Data;
        }

        /// <summary>
        /// Delete an event type
        /// </summary>
        /// <param name="id">Event type ID</param>
        public async Task DeleteEventTypeAsync(int id)
        {
            await DeleteAsync($"/event-types/{id}");
        }

        #endregion

        /// <summary>
        /// Bulk create multiple events
        /// </summary>
        /// <param name="requests">List of event creation requests</param>
        /// <returns>List of created events</returns>
        public async Task<List<Event>> BulkCreateEventsAsync(List<CreateEventRequest> requests)
        {
            var response = await PostAsync<List<Event>>("/events/bulk", new { events = requests });
            return response.Data;
        }

        /// <summary>
        /// Bulk update multiple events
        /// </summary>
        /// <param name="updates">Dictionary of event IDs to update requests</param>
        /// <returns>List of updated events</returns>
        public async Task<List<Event>> BulkUpdateEventsAsync(Dictionary<int, CreateEventRequest> updates)
        {
            var response = await PutAsync<List<Event>>("/events/bulk", new { updates });
            return response.Data;
        }

        /// <summary>
        /// Get event calendar for a date range
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <param name="assignedToId">Optional user filter</param>
        /// <returns>Calendar events</returns>
        public async Task<ApiResponse<List<Event>>> GetEventCalendarAsync(DateTime startDate, DateTime endDate, int? assignedToId = null)
        {
            var queryString = $"start_date={startDate:yyyy-MM-dd}&end_date={endDate:yyyy-MM-dd}";
            if (assignedToId.HasValue)
            {
                queryString += $"&assigned_to_id={assignedToId.Value}";
            }
            
            var response = await GetAsync<List<Event>>($"/events/calendar?{queryString}");
            return response;
        }
    }
}