using LawMatics.SDK.Configuration;
using LawMatics.SDK.Models;
using Microsoft.Extensions.Logging;

namespace LawMatics.SDK.Clients
{
    /// <summary>
    /// Client for advanced company search and discovery in the LawMatics API.
    /// </summary>
    public class CompanyFinderClient : BaseClient
    {
        public CompanyFinderClient(HttpClient httpClient, LawMaticsClientOptions options, ILogger? logger)
            : base(httpClient, options, logger)
        {
        }

        /// <summary>
        /// Searches for companies using advanced criteria.
        /// </summary>
        /// <param name="request">The company search request.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of companies matching the search criteria.</returns>
        public async Task<PagedResponse<Company>> SearchCompaniesAsync(
            CompanySearchRequest request,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["page"] = page.ToString(),
                ["page_size"] = pageSize.ToString()
            };

            // Add search parameters
            if (!string.IsNullOrEmpty(request.Name))
                parameters["name"] = request.Name;

            if (!string.IsNullOrEmpty(request.Website))
                parameters["website"] = request.Website;

            if (!string.IsNullOrEmpty(request.Phone))
                parameters["phone"] = request.Phone;

            if (!string.IsNullOrEmpty(request.Email))
                parameters["email"] = request.Email;

            if (!string.IsNullOrEmpty(request.Address))
                parameters["address"] = request.Address;

            if (!string.IsNullOrEmpty(request.City))
                parameters["city"] = request.City;

            if (!string.IsNullOrEmpty(request.State))
                parameters["state"] = request.State;

            if (!string.IsNullOrEmpty(request.Country))
                parameters["country"] = request.Country;

            if (!string.IsNullOrEmpty(request.PostalCode))
                parameters["postal_code"] = request.PostalCode;

            if (!string.IsNullOrEmpty(request.Industry))
                parameters["industry"] = request.Industry;

            if (!string.IsNullOrEmpty(request.Size))
                parameters["size"] = request.Size;

            if (!string.IsNullOrEmpty(request.Revenue))
                parameters["revenue"] = request.Revenue;

            if (request.CreatedDateFrom.HasValue)
                parameters["created_date_from"] = request.CreatedDateFrom.Value.ToString("yyyy-MM-dd");

            if (request.CreatedDateTo.HasValue)
                parameters["created_date_to"] = request.CreatedDateTo.Value.ToString("yyyy-MM-dd");

            if (request.LastContactFrom.HasValue)
                parameters["last_contact_from"] = request.LastContactFrom.Value.ToString("yyyy-MM-dd");

            if (request.LastContactTo.HasValue)
                parameters["last_contact_to"] = request.LastContactTo.Value.ToString("yyyy-MM-dd");

            if (request.HasMatters.HasValue)
                parameters["has_matters"] = request.HasMatters.Value.ToString();

            if (request.ActiveMatters.HasValue)
                parameters["active_matters"] = request.ActiveMatters.Value.ToString();

            if (request.ClosedMatters.HasValue)
                parameters["closed_matters"] = request.ClosedMatters.Value.ToString();

            if (!string.IsNullOrEmpty(request.Tags))
                parameters["tags"] = request.Tags;

            if (!string.IsNullOrEmpty(request.Notes))
                parameters["notes"] = request.Notes;

            if (request.MinContacts.HasValue)
                parameters["min_contacts"] = request.MinContacts.Value.ToString();

            if (request.MaxContacts.HasValue)
                parameters["max_contacts"] = request.MaxContacts.Value.ToString();

            if (request.MinRevenue.HasValue)
                parameters["min_revenue"] = request.MinRevenue.Value.ToString();

            if (request.MaxRevenue.HasValue)
                parameters["max_revenue"] = request.MaxRevenue.Value.ToString();

            if (!string.IsNullOrEmpty(request.SortBy))
                parameters["sort_by"] = request.SortBy;

            if (!string.IsNullOrEmpty(request.SortOrder))
                parameters["sort_order"] = request.SortOrder;

            return await GetAsync<PagedResponse<Company>>("companies/search", parameters, cancellationToken);
        }

        /// <summary>
        /// Finds companies by name with fuzzy matching.
        /// </summary>
        /// <param name="name">Company name to search for.</param>
        /// <param name="fuzzy">Enable fuzzy matching (default: true).</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of companies matching the name.</returns>
        public async Task<PagedResponse<Company>> FindCompaniesByNameAsync(
            string name,
            bool fuzzy = true,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["name"] = name,
                ["fuzzy"] = fuzzy.ToString(),
                ["page"] = page.ToString(),
                ["page_size"] = pageSize.ToString()
            };

            return await GetAsync<PagedResponse<Company>>("companies/find-by-name", parameters, cancellationToken);
        }

        /// <summary>
        /// Finds companies by domain/website.
        /// </summary>
        /// <param name="website">Website domain to search for.</param>
        /// <param name="exactMatch">Require exact domain match (default: false).</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of companies matching the website.</returns>
        public async Task<PagedResponse<Company>> FindCompaniesByWebsiteAsync(
            string website,
            bool exactMatch = false,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["website"] = website,
                ["exact_match"] = exactMatch.ToString(),
                ["page"] = page.ToString(),
                ["page_size"] = pageSize.ToString()
            };

            return await GetAsync<PagedResponse<Company>>("companies/find-by-website", parameters, cancellationToken);
        }

        /// <summary>
        /// Finds companies by phone number.
        /// </summary>
        /// <param name="phone">Phone number to search for.</param>
        /// <param name="exactMatch">Require exact phone match (default: false).</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of companies matching the phone number.</returns>
        public async Task<PagedResponse<Company>> FindCompaniesByPhoneAsync(
            string phone,
            bool exactMatch = false,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["phone"] = phone,
                ["exact_match"] = exactMatch.ToString(),
                ["page"] = page.ToString(),
                ["page_size"] = pageSize.ToString()
            };

            return await GetAsync<PagedResponse<Company>>("companies/find-by-phone", parameters, cancellationToken);
        }

        /// <summary>
        /// Finds companies by email domain.
        /// </summary>
        /// <param name="emailDomain">Email domain to search for.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of companies matching the email domain.</returns>
        public async Task<PagedResponse<Company>> FindCompaniesByEmailDomainAsync(
            string emailDomain,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["email_domain"] = emailDomain,
                ["page"] = page.ToString(),
                ["page_size"] = pageSize.ToString()
            };

            return await GetAsync<PagedResponse<Company>>("companies/find-by-email-domain", parameters, cancellationToken);
        }

        /// <summary>
        /// Finds companies by geographic location.
        /// </summary>
        /// <param name="location">The location search request.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of companies in the specified location.</returns>
        public async Task<PagedResponse<Company>> FindCompaniesByLocationAsync(
            CompanyLocationSearch location,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["page"] = page.ToString(),
                ["page_size"] = pageSize.ToString()
            };

            if (!string.IsNullOrEmpty(location.Address))
                parameters["address"] = location.Address;

            if (!string.IsNullOrEmpty(location.City))
                parameters["city"] = location.City;

            if (!string.IsNullOrEmpty(location.State))
                parameters["state"] = location.State;

            if (!string.IsNullOrEmpty(location.Country))
                parameters["country"] = location.Country;

            if (!string.IsNullOrEmpty(location.PostalCode))
                parameters["postal_code"] = location.PostalCode;

            if (location.Radius.HasValue)
                parameters["radius"] = location.Radius.Value.ToString();

            if (!string.IsNullOrEmpty(location.RadiusUnit))
                parameters["radius_unit"] = location.RadiusUnit;

            return await GetAsync<PagedResponse<Company>>("companies/find-by-location", parameters, cancellationToken);
        }

        /// <summary>
        /// Finds companies with matters matching specific criteria.
        /// </summary>
        /// <param name="matterSearch">The matter search criteria.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of companies with matching matters.</returns>
        public async Task<PagedResponse<Company>> FindCompaniesByMattersAsync(
            CompanyMatterSearch matterSearch,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["page"] = page.ToString(),
                ["page_size"] = pageSize.ToString()
            };

            if (!string.IsNullOrEmpty(matterSearch.MatterType))
                parameters["matter_type"] = matterSearch.MatterType;

            if (!string.IsNullOrEmpty(matterSearch.MatterStatus))
                parameters["matter_status"] = matterSearch.MatterStatus;

            if (matterSearch.MatterDateFrom.HasValue)
                parameters["matter_date_from"] = matterSearch.MatterDateFrom.Value.ToString("yyyy-MM-dd");

            if (matterSearch.MatterDateTo.HasValue)
                parameters["matter_date_to"] = matterSearch.MatterDateTo.Value.ToString("yyyy-MM-dd");

            if (matterSearch.MinMatterValue.HasValue)
                parameters["min_matter_value"] = matterSearch.MinMatterValue.Value.ToString();

            if (matterSearch.MaxMatterValue.HasValue)
                parameters["max_matter_value"] = matterSearch.MaxMatterValue.Value.ToString();

            if (!string.IsNullOrEmpty(matterSearch.PracticeArea))
                parameters["practice_area"] = matterSearch.PracticeArea;

            if (!string.IsNullOrEmpty(matterSearch.AssignedAttorney))
                parameters["assigned_attorney"] = matterSearch.AssignedAttorney;

            return await GetAsync<PagedResponse<Company>>("companies/find-by-matters", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets company recommendations based on a reference company.
        /// </summary>
        /// <param name="companyId">The reference company ID.</param>
        /// <param name="criteria">Recommendation criteria.</param>
        /// <param name="limit">Maximum number of recommendations to return (default: 10).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of recommended companies.</returns>
        public async Task<List<Company>> GetCompanyRecommendationsAsync(
            int companyId,
            CompanyRecommendationCriteria criteria,
            int limit = 10,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["company_id"] = companyId.ToString(),
                ["limit"] = limit.ToString()
            };

            if (criteria.SimilarIndustry)
                parameters["similar_industry"] = "true";

            if (criteria.SimilarSize)
                parameters["similar_size"] = "true";

            if (criteria.SimilarLocation)
                parameters["similar_location"] = "true";

            if (criteria.SimilarMatterTypes)
                parameters["similar_matter_types"] = "true";

            if (!string.IsNullOrEmpty(criteria.Industry))
                parameters["industry"] = criteria.Industry;

            if (!string.IsNullOrEmpty(criteria.Size))
                parameters["size"] = criteria.Size;

            if (!string.IsNullOrEmpty(criteria.Location))
                parameters["location"] = criteria.Location;

            var response = await GetAsync<CompanyRecommendationsResponse>("companies/recommendations", parameters, cancellationToken);
            return response?.Recommendations ?? new List<Company>();
        }

        /// <summary>
        /// Finds potential duplicate companies.
        /// </summary>
        /// <param name="companyId">The company ID to find duplicates for.</param>
        /// <param name="threshold">Similarity threshold (0.0 to 1.0, default: 0.8).</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of potential duplicate companies.</returns>
        public async Task<PagedResponse<CompanyDuplicate>> FindPotentialDuplicatesAsync(
            int companyId,
            double threshold = 0.8,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["company_id"] = companyId.ToString(),
                ["threshold"] = threshold.ToString("0.00"),
                ["page"] = page.ToString(),
                ["page_size"] = pageSize.ToString()
            };

            return await GetAsync<PagedResponse<CompanyDuplicate>>("companies/find-duplicates", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets company enrichment data from external sources.
        /// </summary>
        /// <param name="companyId">The company ID to enrich.</param>
        /// <param name="sources">Data sources to include (default: all).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Enriched company data.</returns>
        public async Task<CompanyEnrichment?> GetCompanyEnrichmentAsync(
            int companyId,
            List<string>? sources = null,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>();

            if (sources != null && sources.Count > 0)
                parameters["sources"] = string.Join(",", sources);

            return await GetAsync<CompanyEnrichment>($"companies/{companyId}/enrichment", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets company search suggestions for autocomplete.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <param name="field">The field to search in (name, website, email).</param>
        /// <param name="limit">Maximum number of suggestions (default: 10).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Search suggestions.</returns>
        public async Task<List<CompanySuggestion>> GetSearchSuggestionsAsync(
            string query,
            string field = "name",
            int limit = 10,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["query"] = query,
                ["field"] = field,
                ["limit"] = limit.ToString()
            };

            var response = await GetAsync<CompanySuggestionsResponse>("companies/search-suggestions", parameters, cancellationToken);
            return response?.Suggestions ?? new List<CompanySuggestion>();
        }

        /// <summary>
        /// Exports company search results to CSV.
        /// </summary>
        /// <param name="request">The company search request.</param>
        /// <param name="includeContacts">Include contact information.</param>
        /// <param name="includeMatters">Include matter information.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The CSV file content.</returns>
        public async Task<byte[]> ExportCompanySearchToCsvAsync(
            CompanySearchRequest request,
            bool includeContacts = false,
            bool includeMatters = false,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["format"] = "csv",
                ["include_contacts"] = includeContacts.ToString(),
                ["include_matters"] = includeMatters.ToString()
            };

            // Add search parameters
            if (!string.IsNullOrEmpty(request.Name))
                parameters["name"] = request.Name;

            if (!string.IsNullOrEmpty(request.Website))
                parameters["website"] = request.Website;

            if (!string.IsNullOrEmpty(request.City))
                parameters["city"] = request.City;

            if (!string.IsNullOrEmpty(request.State))
                parameters["state"] = request.State;

            if (!string.IsNullOrEmpty(request.Country))
                parameters["country"] = request.Country;

            return await GetFileAsync("companies/search/export", parameters, cancellationToken);
        }
    }

    // Request/Response Models for Company Finder

    /// <summary>
    /// Request model for advanced company search.
    /// </summary>
    public class CompanySearchRequest
    {
        public string? Name { get; set; }
        public string? Website { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
        public string? Industry { get; set; }
        public string? Size { get; set; }
        public string? Revenue { get; set; }
        public DateTime? CreatedDateFrom { get; set; }
        public DateTime? CreatedDateTo { get; set; }
        public DateTime? LastContactFrom { get; set; }
        public DateTime? LastContactTo { get; set; }
        public bool? HasMatters { get; set; }
        public bool? ActiveMatters { get; set; }
        public bool? ClosedMatters { get; set; }
        public string? Tags { get; set; }
        public string? Notes { get; set; }
        public int? MinContacts { get; set; }
        public int? MaxContacts { get; set; }
        public decimal? MinRevenue { get; set; }
        public decimal? MaxRevenue { get; set; }
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; }
    }

    /// <summary>
    /// Request model for location-based company search.
    /// </summary>
    public class CompanyLocationSearch
    {
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
        public double? Radius { get; set; }
        public string? RadiusUnit { get; set; } = "miles";
    }

    /// <summary>
    /// Request model for matter-based company search.
    /// </summary>
    public class CompanyMatterSearch
    {
        public string? MatterType { get; set; }
        public string? MatterStatus { get; set; }
        public DateTime? MatterDateFrom { get; set; }
        public DateTime? MatterDateTo { get; set; }
        public decimal? MinMatterValue { get; set; }
        public decimal? MaxMatterValue { get; set; }
        public string? PracticeArea { get; set; }
        public string? AssignedAttorney { get; set; }
    }

    /// <summary>
    /// Request model for company recommendations.
    /// </summary>
    public class CompanyRecommendationCriteria
    {
        public bool SimilarIndustry { get; set; } = true;
        public bool SimilarSize { get; set; } = true;
        public bool SimilarLocation { get; set; } = true;
        public bool SimilarMatterTypes { get; set; } = true;
        public string? Industry { get; set; }
        public string? Size { get; set; }
        public string? Location { get; set; }
    }

    /// <summary>
    /// Represents a potential duplicate company.
    /// </summary>
    public class CompanyDuplicate
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Website { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public double SimilarityScore { get; set; }
        public List<string> MatchingFields { get; set; } = new();
        public string? Reason { get; set; }
    }

    /// <summary>
    /// Represents enriched company data.
    /// </summary>
    public class CompanyEnrichment
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Industry { get; set; }
        public string? Size { get; set; }
        public string? Revenue { get; set; }
        public string? Founded { get; set; }
        public List<string> Technologies { get; set; } = new();
        public List<string> SocialMedia { get; set; } = new();
        public List<string> News { get; set; } = new();
        public DateTime LastUpdated { get; set; }
        public List<string> DataSources { get; set; } = new();
    }

    /// <summary>
    /// Represents a search suggestion.
    /// </summary>
    public class CompanySuggestion
    {
        public string Text { get; set; } = string.Empty;
        public string? Type { get; set; }
        public int Count { get; set; }
    }

    /// <summary>
    /// Response wrapper for company recommendations.
    /// </summary>
    public class CompanyRecommendationsResponse
    {
        public List<Company> Recommendations { get; set; } = new();
    }

    /// <summary>
    /// Response wrapper for search suggestions.
    /// </summary>
    public class CompanySuggestionsResponse
    {
        public List<CompanySuggestion> Suggestions { get; set; } = new();
    }
}