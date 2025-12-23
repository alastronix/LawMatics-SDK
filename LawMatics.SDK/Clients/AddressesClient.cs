using LawMatics.SDK.Configuration;
using LawMatics.SDK.Models;
using Microsoft.Extensions.Logging;

namespace LawMatics.SDK.Clients
{
    /// <summary>
    /// Client for managing addresses in the LawMatics API.
    /// </summary>
    public class AddressesClient : BaseClient
    {
        public AddressesClient(HttpClient httpClient, LawMaticsClientOptions options, ILogger? logger = null)
            : base(httpClient, options, logger)
        {
        }

        /// <summary>
        /// Gets all addresses with optional pagination and filtering.
        /// </summary>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="contactId">Filter by contact ID.</param>
        /// <param name="companyId">Filter by company ID.</param>
        /// <param name="addressType">Filter by address type.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of addresses.</returns>
        public async Task<PagedResponse<Address>> GetAddressesAsync(
            int page = 1,
            int pageSize = 20,
            int? contactId = null,
            int? companyId = null,
            string? addressType = null,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["page"] = page.ToString(),
                ["page_size"] = pageSize.ToString()
            };

            if (contactId.HasValue)
                parameters["contact_id"] = contactId.Value.ToString();

            if (companyId.HasValue)
                parameters["company_id"] = companyId.Value.ToString();

            if (!string.IsNullOrEmpty(addressType))
                parameters["address_type"] = addressType;

            return await GetAsync<PagedResponse<Address>>("addresses", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets a specific address by ID.
        /// </summary>
        /// <param name="id">The address ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The address details.</returns>
        public async Task<Address?> GetAddressAsync(int id, CancellationToken cancellationToken = default)
        {
            return await GetAsync<Address>($"addresses/{id}", cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Creates a new address.
        /// </summary>
        /// <param name="request">The address creation request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created address.</returns>
        public async Task<Address?> CreateAddressAsync(CreateAddressRequest request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<Address>("addresses", request, cancellationToken);
        }

        /// <summary>
        /// Updates an existing address.
        /// </summary>
        /// <param name="id">The address ID.</param>
        /// <param name="request">The address update request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated address.</returns>
        public async Task<Address?> UpdateAddressAsync(int id, UpdateAddressRequest request, CancellationToken cancellationToken = default)
        {
            return await PutAsync<Address>($"addresses/{id}", request, cancellationToken);
        }

        /// <summary>
        /// Deletes an address.
        /// </summary>
        /// <param name="id">The address ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if deletion was successful.</returns>
        public async Task<bool> DeleteAddressAsync(int id, CancellationToken cancellationToken = default)
        {
            return await DeleteAsync($"addresses/{id}", cancellationToken);
        }

        /// <summary>
        /// Gets addresses for a specific contact.
        /// </summary>
        /// <param name="contactId">The contact ID.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of addresses for the contact.</returns>
        public async Task<PagedResponse<Address>> GetAddressesByContactAsync(
            int contactId,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            return await GetAddressesAsync(page, pageSize, contactId: contactId, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets addresses for a specific company.
        /// </summary>
        /// <param name="companyId">The company ID.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of addresses for the company.</returns>
        public async Task<PagedResponse<Address>> GetAddressesByCompanyAsync(
            int companyId,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            return await GetAddressesAsync(page, pageSize, companyId: companyId, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Sets an address as the primary address for a contact or company.
        /// </summary>
        /// <param name="id">The address ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if the operation was successful.</returns>
        public async Task<bool> SetPrimaryAddressAsync(int id, CancellationToken cancellationToken = default)
        {
            var request = new { is_primary = true };
            var response = await PutAsync<object>($"addresses/{id}/set-primary", request, cancellationToken);
            return response != null;
        }

        /// <summary>
        /// Validates an address format.
        /// </summary>
        /// <param name="address">The address to validate.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Validation result with suggestions if applicable.</returns>
        public async Task<AddressValidationResult?> ValidateAddressAsync(Address address, CancellationToken cancellationToken = default)
        {
            return await PostAsync<AddressValidationResult>("addresses/validate", address, cancellationToken);
        }
    }

    /// <summary>
    /// Represents the result of an address validation.
    /// </summary>
    public class AddressValidationResult
    {
        /// <summary>
        /// Gets or sets whether the address is valid.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("is_valid")]
        public bool IsValid { get; set; }

        /// <summary>
        /// Gets or sets the validation score (0-100).
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("score")]
        public int Score { get; set; }

        /// <summary>
        /// Gets or sets the standardized address if validation succeeded.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("standardized_address")]
        public Address? StandardizedAddress { get; set; }

        /// <summary>
        /// Gets or sets validation messages.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("messages")]
        public List<string>? Messages { get; set; }

        /// <summary>
        /// Gets or sets address suggestions.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("suggestions")]
        public List<Address>? Suggestions { get; set; }
    }
}