using LawMatics.SDK.Configuration;
using LawMatics.SDK.Exceptions;
using LawMatics.SDK.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace LawMatics.SDK.Clients
{
    /// <summary>
    /// Client for managing contacts in the Lawmatics API
    /// </summary>
    public class ContactsClient : BaseClient
    {
        public ContactsClient(HttpClient httpClient, LawMaticsClientOptions options, ILogger? logger = null)
            : base(httpClient, options, logger)
        {
        }

        /// <summary>
        /// Get all contacts with optional filtering and pagination
        /// </summary>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>Paginated list of contacts</returns>
        public async Task<ApiResponse<List<Contact>>> GetContactsAsync(GetContactsParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new GetContactsParameters());
            var response = await GetAsync<List<Contact>>($"/contacts?{queryParams}");
            return response;
        }

        /// <summary>
        /// Get a specific contact by ID
        /// </summary>
        /// <param name="id">Contact ID</param>
        /// <returns>Contact details</returns>
        public async Task<Contact> GetContactAsync(int id)
        {
            var response = await GetAsync<Contact>($"/contacts/{id}");
            return response.Data;
        }

        /// <summary>
        /// Create a new contact
        /// </summary>
        /// <param name="request">Contact creation request</param>
        /// <returns>Created contact</returns>
        public async Task<Contact> CreateContactAsync(CreateContactRequest request)
        {
            var response = await PostAsync<Contact>("/contacts", request);
            return response.Data;
        }

        /// <summary>
        /// Update an existing contact
        /// </summary>
        /// <param name="id">Contact ID</param>
        /// <param name="request">Contact update request</param>
        /// <returns>Updated contact</returns>
        public async Task<Contact> UpdateContactAsync(int id, UpdateContactRequest request)
        {
            var response = await PutAsync<Contact>($"/contacts/{id}", request);
            return response.Data;
        }

        /// <summary>
        /// Delete a contact
        /// </summary>
        /// <param name="id">Contact ID</param>
        public async Task DeleteContactAsync(int id)
        {
            await DeleteAsync($"/contacts/{id}");
        }

        /// <summary>
        /// Find contacts by name
        /// </summary>
        /// <param name="name">Contact name to search for</param>
        /// <returns>List of matching contacts</returns>
        public async Task<ApiResponse<List<Contact>>> FindContactsByNameAsync(string name)
        {
            var parameters = new GetContactsParameters { Search = name };
            return await GetContactsAsync(parameters);
        }

        /// <summary>
        /// Find contacts by email
        /// </summary>
        /// <param name="email">Email address to search for</param>
        /// <returns>List of matching contacts</returns>
        public async Task<ApiResponse<List<Contact>>> FindContactsByEmailAsync(string email)
        {
            var parameters = new GetContactsParameters { Search = email };
            return await GetContactsAsync(parameters);
        }

        /// <summary>
        /// Find contacts by phone number
        /// </summary>
        /// <param name="phone">Phone number to search for</param>
        /// <returns>List of matching contacts</returns>
        public async Task<ApiResponse<List<Contact>>> FindContactsByPhoneAsync(string phone)
        {
            var parameters = new GetContactsParameters { Search = phone };
            return await GetContactsAsync(parameters);
        }

        /// <summary>
        /// Get contacts by type
        /// </summary>
        /// <param name="contactType">Contact type filter</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of contacts of specified type</returns>
        public async Task<ApiResponse<List<Contact>>> GetContactsByTypeAsync(string contactType, GetContactsParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new GetContactsParameters());
            var response = await GetAsync<List<Contact>>($"/contacts?type={contactType}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get contacts by lead status
        /// </summary>
        /// <param name="leadStatus">Lead status filter</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of contacts with specified lead status</returns>
        public async Task<ApiResponse<List<Contact>>> GetContactsByLeadStatusAsync(string leadStatus, GetContactsParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new GetContactsParameters());
            var response = await GetAsync<List<Contact>>($"/contacts?lead_status={leadStatus}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get contacts assigned to a specific user
        /// </summary>
        /// <param name="assignedToId">User ID to filter by</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of contacts assigned to specified user</returns>
        public async Task<ApiResponse<List<Contact>>> GetContactsByAssignedUserAsync(int assignedToId, GetContactsParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new GetContactsParameters());
            var response = await GetAsync<List<Contact>>($"/contacts?assigned_to_id={assignedToId}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get contacts created within a date range
        /// </summary>
        /// <param name="startDate">Start date filter</param>
        /// <param name="endDate">End date filter</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of contacts created within date range</returns>
        public async Task<ApiResponse<List<Contact>>> GetContactsByDateRangeAsync(DateTime startDate, DateTime endDate, GetContactsParameters? parameters = null)
        {
            var filterParams = parameters ?? new GetContactsParameters();
            filterParams.CreatedAfter = startDate;
            filterParams.CreatedBefore = endDate;
            
            var queryParams = BuildQueryString(filterParams);
            var response = await GetAsync<List<Contact>>($"/contacts?{queryParams}");
            return response;
        }

        /// <summary>
        /// Get contacts with a specific tag
        /// </summary>
        /// <param name="tag">Tag to filter by</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of contacts with specified tag</returns>
        public async Task<ApiResponse<List<Contact>>> GetContactsByTagAsync(string tag, GetContactsParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new GetContactsParameters());
            var response = await GetAsync<List<Contact>>($"/contacts?tag={tag}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Bulk create multiple contacts
        /// </summary>
        /// <param name="requests">List of contact creation requests</param>
        /// <returns>List of created contacts</returns>
        public async Task<List<Contact>> BulkCreateContactsAsync(List<CreateContactRequest> requests)
        {
            var response = await PostAsync<List<Contact>>("/contacts/bulk", new { contacts = requests });
            return response.Data;
        }

        /// <summary>
        /// Bulk update multiple contacts
        /// </summary>
        /// <param name="updates">Dictionary of contact IDs to update requests</param>
        /// <returns>List of updated contacts</returns>
        public async Task<List<Contact>> BulkUpdateContactsAsync(Dictionary<int, UpdateContactRequest> updates)
        {
            var response = await PutAsync<List<Contact>>("/contacts/bulk", new { updates });
            return response.Data;
        }

        /// <summary>
        /// Merge two contacts
        /// </summary>
        /// <param name="primaryContactId">ID of the contact to keep</param>
        /// <param name="secondaryContactId">ID of the contact to merge</param>
        /// <returns>Merged contact</returns>
        public async Task<Contact> MergeContactsAsync(int primaryContactId, int secondaryContactId)
        {
            var response = await PostAsync<Contact>($"/contacts/{primaryContactId}/merge", new 
            { 
                secondary_contact_id = secondaryContactId 
            });
            return response.Data;
        }

        /// <summary>
        /// Get contact activities/events
        /// </summary>
        /// <param name="contactId">Contact ID</param>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of contact activities</returns>
        public async Task<ApiResponse<List<object>>> GetContactActivitiesAsync(int contactId, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<object>>($"/contacts/{contactId}/activities?{queryParams}");
            return response;
        }

        /// <summary>
        /// Add tag to contact
        /// </summary>
        /// <param name="contactId">Contact ID</param>
        /// <param name="tag">Tag to add</param>
        public async Task AddTagToContactAsync(int contactId, string tag)
        {
            await PostAsync<object>($"/contacts/{contactId}/tags", new { tag });
        }

        /// <summary>
        /// Remove tag from contact
        /// </summary>
        /// <param name="contactId">Contact ID</param>
        /// <param name="tag">Tag to remove</param>
        public async Task RemoveTagFromContactAsync(int contactId, string tag)
        {
            await DeleteAsync($"/contacts/{contactId}/tags/{tag}");
        }

        /// <summary>
        /// Get contact files
        /// </summary>
        /// <param name="contactId">Contact ID</param>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of contact files</returns>
        public async Task<ApiResponse<List<LawMaticsFile>>> GetContactFilesAsync(int contactId, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<LawMaticsFile>>($"/contacts/{contactId}/files?{queryParams}");
            return response;
        }

        /// <summary>
        /// Get contact matters
        /// </summary>
        /// <param name="contactId">Contact ID</param>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of contact matters</returns>
        public async Task<ApiResponse<List<Matter>>> GetContactMattersAsync(int contactId, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Matter>>($"/contacts/{contactId}/matters?{queryParams}");
            return response;
        }

        /// <summary>
        /// Get contact notes
        /// </summary>
        /// <param name="contactId">Contact ID</param>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of contact notes</returns>
        public async Task<ApiResponse<List<Note>>> GetContactNotesAsync(int contactId, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Note>>($"/contacts/{contactId}/notes?{queryParams}");
            return response;
        }

        /// <summary>
        /// Get contact payment history
        /// </summary>
        /// <param name="contactId">Contact ID</param>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of contact payments</returns>
        public async Task<ApiResponse<List<Payment>>> GetContactPaymentsAsync(int contactId, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Payment>>($"/contacts/{contactId}/payments?{queryParams}");
            return response;
        }
    }
}