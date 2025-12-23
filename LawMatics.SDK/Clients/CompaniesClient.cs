using LawMatics.SDK.Configuration;
using LawMatics.SDK.Exceptions;
using LawMatics.SDK.Models;
using Microsoft.Extensions.Logging;

namespace LawMatics.SDK.Clients
{
    /// <summary>
    /// Client for managing companies in the Lawmatics API
    /// </summary>
    public class CompaniesClient : BaseClient
    {
        public CompaniesClient(HttpClient httpClient, LawMaticsClientOptions options, ILogger? logger = null)
            : base(httpClient, options, logger)
        {
        }

        /// <summary>
        /// Get all companies with optional filtering and pagination
        /// </summary>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>Paginated list of companies</returns>
        public async Task<ApiResponse<List<Company>>> GetCompaniesAsync(FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Company>>($"/companies?{queryParams}");
            return response;
        }

        /// <summary>
        /// Get a specific company by ID
        /// </summary>
        /// <param name="id">Company ID</param>
        /// <returns>Company details</returns>
        public async Task<Company> GetCompanyAsync(int id)
        {
            var response = await GetAsync<Company>($"/companies/{id}");
            return response.Data;
        }

        /// <summary>
        /// Create a new company
        /// </summary>
        /// <param name="request">Company creation request</param>
        /// <returns>Created company</returns>
        public async Task<Company> CreateCompanyAsync(CreateCompanyRequest request)
        {
            var response = await PostAsync<Company>("/companies", request);
            return response.Data;
        }

        /// <summary>
        /// Update an existing company
        /// </summary>
        /// <param name="id">Company ID</param>
        /// <param name="request">Company update request</param>
        /// <returns>Updated company</returns>
        public async Task<Company> UpdateCompanyAsync(int id, UpdateCompanyRequest request)
        {
            var response = await PutAsync<Company>($"/companies/{id}", request);
            return response.Data;
        }

        /// <summary>
        /// Delete a company
        /// </summary>
        /// <param name="id">Company ID</param>
        public async Task DeleteCompanyAsync(int id)
        {
            await DeleteAsync($"/companies/{id}");
        }

        /// <summary>
        /// Find company by name
        /// </summary>
        /// <param name="name">Company name to search for</param>
        /// <returns>Matching company</returns>
        public async Task<Company?> FindCompanyByNameAsync(string name)
        {
            var response = await GetAsync<Company>($"/companies/find/by-name?name={Uri.EscapeDataString(name)}");
            return response.Data;
        }

        /// <summary>
        /// Find company by email
        /// </summary>
        /// <param name="email">Email address to search for</param>
        /// <returns>Matching company</returns>
        public async Task<Company?> FindCompanyByEmailAsync(string email)
        {
            var response = await GetAsync<Company>($"/companies/find/by-email?email={Uri.EscapeDataString(email)}");
            return response.Data;
        }

        /// <summary>
        /// Find company by phone
        /// </summary>
        /// <param name="phone">Phone number to search for</param>
        /// <returns>Matching company</returns>
        public async Task<Company?> FindCompanyByPhoneAsync(string phone)
        {
            var response = await GetAsync<Company>($"/companies/find/by-phone?phone={Uri.EscapeDataString(phone)}");
            return response.Data;
        }

        /// <summary>
        /// Get companies by industry
        /// </summary>
        /// <param name="industry">Industry to filter by</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of companies in specified industry</returns>
        public async Task<ApiResponse<List<Company>>> GetCompaniesByIndustryAsync(string industry, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Company>>($"/companies?industry={industry}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get companies by company size
        /// </summary>
        /// <param name="companySize">Company size to filter by</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of companies with specified size</returns>
        public async Task<ApiResponse<List<Company>>> GetCompaniesBySizeAsync(string companySize, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Company>>($"/companies?company_size={companySize}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get companies assigned to a specific user
        /// </summary>
        /// <param name="assignedToId">User ID to filter by</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of companies assigned to specified user</returns>
        public async Task<ApiResponse<List<Company>>> GetCompaniesByAssignedUserAsync(int assignedToId, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Company>>($"/companies?assigned_to_id={assignedToId}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get company contacts
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of company contacts</returns>
        public async Task<ApiResponse<List<Contact>>> GetCompanyContactsAsync(int companyId, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Contact>>($"/companies/{companyId}/contacts?{queryParams}");
            return response;
        }

        /// <summary>
        /// Get company matters
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of company matters</returns>
        public async Task<ApiResponse<List<Matter>>> GetCompanyMattersAsync(int companyId, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Matter>>($"/companies/{companyId}/matters?{queryParams}");
            return response;
        }

        /// <summary>
        /// Get company files
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of company files</returns>
        public async Task<ApiResponse<List<LawMaticsFile>>> GetCompanyFilesAsync(int companyId, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<LawMaticsFile>>($"/companies/{companyId}/files?{queryParams}");
            return response;
        }

        /// <summary>
        /// Get company notes
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of company notes</returns>
        public async Task<ApiResponse<List<Note>>> GetCompanyNotesAsync(int companyId, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Note>>($"/companies/{companyId}/notes?{queryParams}");
            return response;
        }

        /// <summary>
        /// Add tag to company
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="tag">Tag to add</param>
        public async Task AddTagToCompanyAsync(int companyId, string tag)
        {
            await PostAsync<object>($"/companies/{companyId}/tags", new { tag });
        }

        /// <summary>
        /// Remove tag from company
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="tag">Tag to remove</param>
        public async Task RemoveTagFromCompanyAsync(int companyId, string tag)
        {
            await DeleteAsync($"/companies/{companyId}/tags/{tag}");
        }

        /// <summary>
        /// Bulk create multiple companies
        /// </summary>
        /// <param name="requests">List of company creation requests</param>
        /// <returns>List of created companies</returns>
        public async Task<List<Company>> BulkCreateCompaniesAsync(List<CreateCompanyRequest> requests)
        {
            var response = await PostAsync<List<Company>>("/companies/bulk", new { companies = requests });
            return response.Data;
        }

        /// <summary>
        /// Bulk update multiple companies
        /// </summary>
        /// <param name="updates">Dictionary of company IDs to update requests</param>
        /// <returns>List of updated companies</returns>
        public async Task<List<Company>> BulkUpdateCompaniesAsync(Dictionary<int, UpdateCompanyRequest> updates)
        {
            var response = await PutAsync<List<Company>>("/companies/bulk", new { updates });
            return response.Data;
        }
    }
}