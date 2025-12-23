using LawMatics.SDK.Configuration;
using LawMatics.SDK.Exceptions;
using LawMatics.SDK.Models;
using Microsoft.Extensions.Logging;

namespace LawMatics.SDK.Clients
{
    /// <summary>
    /// Client for managing custom fields in the Lawmatics API
    /// </summary>
    public class CustomFieldsClient : BaseClient
    {
        public CustomFieldsClient(HttpClient httpClient, LawMaticsClientOptions options, ILogger? logger = null)
            : base(httpClient, options, logger)
        {
        }

        /// <summary>
        /// Get all custom fields with optional filtering and pagination
        /// </summary>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>Paginated list of custom fields</returns>
        public async Task<ApiResponse<List<CustomField>>> GetCustomFieldsAsync(FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<CustomField>>($"/custom-fields?{queryParams}");
            return response;
        }

        /// <summary>
        /// Get a specific custom field by ID
        /// </summary>
        /// <param name="id">Custom field ID</param>
        /// <returns>Custom field details</returns>
        public async Task<CustomField> GetCustomFieldAsync(int id)
        {
            var response = await GetAsync<CustomField>($"/custom-fields/{id}");
            return response.Data;
        }

        /// <summary>
        /// Create a new custom field
        /// </summary>
        /// <param name="request">Custom field creation request</param>
        /// <returns>Created custom field</returns>
        public async Task<CustomField> CreateCustomFieldAsync(CustomField request)
        {
            var response = await PostAsync<CustomField>("/custom-fields", request);
            return response.Data;
        }

        /// <summary>
        /// Update an existing custom field
        /// </summary>
        /// <param name="id">Custom field ID</param>
        /// <param name="request">Custom field update request</param>
        /// <returns>Updated custom field</returns>
        public async Task<CustomField> UpdateCustomFieldAsync(int id, CustomField request)
        {
            var response = await PutAsync<CustomField>($"/custom-fields/{id}", request);
            return response.Data;
        }

        /// <summary>
        /// Delete a custom field
        /// </summary>
        /// <param name="id">Custom field ID</param>
        public async Task DeleteCustomFieldAsync(int id)
        {
            await DeleteAsync($"/custom-fields/{id}");
        }

        /// <summary>
        /// Get custom fields by entity type
        /// </summary>
        /// <param name="entityType">Entity type to filter by (contact, company, matter, etc.)</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of custom fields for specified entity type</returns>
        public async Task<ApiResponse<List<CustomField>>> GetCustomFieldsByEntityTypeAsync(string entityType, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<CustomField>>($"/custom-fields?entity_type={entityType}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get custom fields by field type
        /// </summary>
        /// <param name="fieldType">Field type to filter by (text, number, date, select, etc.)</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of custom fields of specified type</returns>
        public async Task<ApiResponse<List<CustomField>>> GetCustomFieldsByTypeAsync(string fieldType, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<CustomField>>($"/custom-fields?field_type={fieldType}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get active custom fields
        /// </summary>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of active custom fields</returns>
        public async Task<ApiResponse<List<CustomField>>> GetActiveCustomFieldsAsync(FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<CustomField>>($"/custom-fields?is_active=true&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get inactive custom fields
        /// </summary>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of inactive custom fields</returns>
        public async Task<ApiResponse<List<CustomField>>> GetInactiveCustomFieldsAsync(FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<CustomField>>($"/custom-fields?is_active=false&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get required custom fields
        /// </summary>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of required custom fields</returns>
        public async Task<ApiResponse<List<CustomField>>> GetRequiredCustomFieldsAsync(FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<CustomField>>($"/custom-fields?required=true&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get optional custom fields
        /// </summary>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of optional custom fields</returns>
        public async Task<ApiResponse<List<CustomField>>> GetOptionalCustomFieldsAsync(FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<CustomField>>($"/custom-fields?required=false&{queryParams}");
            return response;
        }

        /// <summary>
        /// Search custom fields by name or label
        /// </summary>
        /// <param name="searchTerm">Search term</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of matching custom fields</returns>
        public async Task<ApiResponse<List<CustomField>>> SearchCustomFieldsAsync(string searchTerm, FilterParameters? parameters = null)
        {
            var filterParams = parameters ?? new FilterParameters();
            filterParams.Search = searchTerm;
            
            var queryParams = BuildQueryString(filterParams);
            var response = await GetAsync<List<CustomField>>($"/custom-fields?{queryParams}");
            return response;
        }

        /// <summary>
        /// Activate a custom field
        /// </summary>
        /// <param name="id">Custom field ID</param>
        /// <returns>Updated custom field</returns>
        public async Task<CustomField> ActivateCustomFieldAsync(int id)
        {
            var response = await PostAsync<CustomField>($"/custom-fields/{id}/activate");
            return response.Data;
        }

        /// <summary>
        /// Deactivate a custom field
        /// </summary>
        /// <param name="id">Custom field ID</param>
        /// <returns>Updated custom field</returns>
        public async Task<CustomField> DeactivateCustomFieldAsync(int id)
        {
            var response = await PostAsync<CustomField>($"/custom-fields/{id}/deactivate");
            return response.Data;
        }

        /// <summary>
        /// Get custom field values for a specific entity
        /// </summary>
        /// <param name="entityType">Entity type (contact, company, matter, etc.)</param>
        /// <param name="entityId">Entity ID</param>
        /// <returns>Custom field values for the entity</returns>
        public async Task<Dictionary<string, object>> GetCustomFieldValuesAsync(string entityType, int entityId)
        {
            var response = await GetAsync<Dictionary<string, object>>($"/custom-fields/values/{entityType}/{entityId}");
            return response.Data;
        }

        /// <summary>
        /// Update custom field values for a specific entity
        /// </summary>
        /// <param name="entityType">Entity type (contact, company, matter, etc.)</param>
        /// <param name="entityId">Entity ID</param>
        /// <param name="values">Dictionary of field names to values</param>
        /// <returns>Updated custom field values</returns>
        public async Task<Dictionary<string, object>> UpdateCustomFieldValuesAsync(string entityType, int entityId, Dictionary<string, object> values)
        {
            var response = await PutAsync<Dictionary<string, object>>($"/custom-fields/values/{entityType}/{entityId}", values);
            return response.Data;
        }

        /// <summary>
        /// Clear custom field values for a specific entity
        /// </summary>
        /// <param name="entityType">Entity type (contact, company, matter, etc.)</param>
        /// <param name="entityId">Entity ID</param>
        public async Task ClearCustomFieldValuesAsync(string entityType, int entityId)
        {
            await DeleteAsync($"/custom-fields/values/{entityType}/{entityId}");
        }

        /// <summary>
        /// Get custom field options for select/multi-select fields
        /// </summary>
        /// <param name="fieldId">Custom field ID</param>
        /// <returns>List of field options</returns>
        public async Task<List<CustomFieldOption>> GetCustomFieldOptionsAsync(int fieldId)
        {
            var response = await GetAsync<List<CustomFieldOption>>($"/custom-fields/{fieldId}/options");
            return response.Data;
        }

        /// <summary>
        /// Add option to custom field
        /// </summary>
        /// <param name="fieldId">Custom field ID</param>
        /// <param name="option">Option to add</param>
        /// <returns>Added option</returns>
        public async Task<CustomFieldOption> AddCustomFieldOptionAsync(int fieldId, CustomFieldOption option)
        {
            var response = await PostAsync<CustomFieldOption>($"/custom-fields/{fieldId}/options", option);
            return response.Data;
        }

        /// <summary>
        /// Update custom field option
        /// </summary>
        /// <param name="fieldId">Custom field ID</param>
        /// <param name="optionId">Option ID</param>
        /// <param name="option">Updated option</param>
        /// <returns>Updated option</returns>
        public async Task<CustomFieldOption> UpdateCustomFieldOptionAsync(int fieldId, int optionId, CustomFieldOption option)
        {
            var response = await PutAsync<CustomFieldOption>($"/custom-fields/{fieldId}/options/{optionId}", option);
            return response.Data;
        }

        /// <summary>
        /// Delete custom field option
        /// </summary>
        /// <param name="fieldId">Custom field ID</param>
        /// <param name="optionId">Option ID</param>
        public async Task DeleteCustomFieldOptionAsync(int fieldId, int optionId)
        {
            await DeleteAsync($"/custom-fields/{fieldId}/options/{optionId}");
        }

        /// <summary>
        /// Reorder custom field options
        /// </summary>
        /// <param name="fieldId">Custom field ID</param>
        /// <param name="optionIds">List of option IDs in new order</param>
        public async Task ReorderCustomFieldOptionsAsync(int fieldId, List<int> optionIds)
        {
            await PostAsync<object>($"/custom-fields/{fieldId}/options/reorder", new { option_ids = optionIds });
        }

        #region Custom Contact Types

        /// <summary>
        /// Get all custom contact types
        /// </summary>
        /// <returns>List of custom contact types</returns>
        public async Task<ApiResponse<List<CustomContactType>>> GetCustomContactTypesAsync()
        {
            var response = await GetAsync<List<CustomContactType>>("/custom-contact-types");
            return response;
        }

        /// <summary>
        /// Get a specific custom contact type by ID
        /// </summary>
        /// <param name="id">Custom contact type ID</param>
        /// <returns>Custom contact type details</returns>
        public async Task<CustomContactType> GetCustomContactTypeAsync(int id)
        {
            var response = await GetAsync<CustomContactType>($"/custom-contact-types/{id}");
            return response.Data;
        }

        /// <summary>
        /// Create a new custom contact type
        /// </summary>
        /// <param name="request">Custom contact type creation request</param>
        /// <returns>Created custom contact type</returns>
        public async Task<CustomContactType> CreateCustomContactTypeAsync(CustomContactType request)
        {
            var response = await PostAsync<CustomContactType>("/custom-contact-types", request);
            return response.Data;
        }

        /// <summary>
        /// Update an existing custom contact type
        /// </summary>
        /// <param name="id">Custom contact type ID</param>
        /// <param name="request">Custom contact type update request</param>
        /// <returns>Updated custom contact type</returns>
        public async Task<CustomContactType> UpdateCustomContactTypeAsync(int id, CustomContactType request)
        {
            var response = await PutAsync<CustomContactType>($"/custom-contact-types/{id}", request);
            return response.Data;
        }

        /// <summary>
        /// Delete a custom contact type
        /// </summary>
        /// <param name="id">Custom contact type ID</param>
        public async Task DeleteCustomContactTypeAsync(int id)
        {
            await DeleteAsync($"/custom-contact-types/{id}");
        }

        #endregion

        #region Bulk Operations

        /// <summary>
        /// Bulk create multiple custom fields
        /// </summary>
        /// <param name="requests">List of custom field creation requests</param>
        /// <returns>List of created custom fields</returns>
        public async Task<List<CustomField>> BulkCreateCustomFieldsAsync(List<CustomField> requests)
        {
            var response = await PostAsync<List<CustomField>>("/custom-fields/bulk", new { custom_fields = requests });
            return response.Data;
        }

        /// <summary>
        /// Bulk update multiple custom fields
        /// </summary>
        /// <param name="updates">Dictionary of field IDs to update requests</param>
        /// <returns>List of updated custom fields</returns>
        public async Task<List<CustomField>> BulkUpdateCustomFieldsAsync(Dictionary<int, CustomField> updates)
        {
            var response = await PutAsync<List<CustomField>>("/custom-fields/bulk", new { updates });
            return response.Data;
        }

        /// <summary>
        /// Bulk delete multiple custom fields
        /// </summary>
        /// <param name="fieldIds">List of field IDs to delete</param>
        public async Task BulkDeleteCustomFieldsAsync(List<int> fieldIds)
        {
            await PostAsync<object>("/custom-fields/bulk-delete", new { field_ids = fieldIds });
        }

        /// <summary>
        /// Bulk activate multiple custom fields
        /// </summary>
        /// <param name="fieldIds">List of field IDs to activate</param>
        public async Task BulkActivateCustomFieldsAsync(List<int> fieldIds)
        {
            await PostAsync<object>("/custom-fields/bulk-activate", new { field_ids = fieldIds });
        }

        /// <summary>
        /// Bulk deactivate multiple custom fields
        /// </summary>
        /// <param name="fieldIds">List of field IDs to deactivate</param>
        public async Task BulkDeactivateCustomFieldsAsync(List<int> fieldIds)
        {
            await PostAsync<object>("/custom-fields/bulk-deactivate", new { field_ids = fieldIds });
        }

        #endregion

        #region Field Validation

        /// <summary>
        /// Validate custom field value
        /// </summary>
        /// <param name="fieldId">Custom field ID</param>
        /// <param name="value">Value to validate</param>
        /// <returns>Validation result</returns>
        public async Task<object> ValidateCustomFieldValueAsync(int fieldId, object value)
        {
            var response = await PostAsync<object>($"/custom-fields/{fieldId}/validate", new { value });
            return response.Data;
        }

        /// <summary>
        /// Get custom field validation rules
        /// </summary>
        /// <param name="fieldId">Custom field ID</param>
        /// <returns>Validation rules</returns>
        public async Task<Dictionary<string, object>> GetCustomFieldValidationRulesAsync(int fieldId)
        {
            var response = await GetAsync<Dictionary<string, object>>($"/custom-fields/{fieldId}/validation-rules");
            return response.Data;
        }

        /// <summary>
        /// Update custom field validation rules
        /// </summary>
        /// <param name="fieldId">Custom field ID</param>
        /// <param name="rules">Validation rules</param>
        /// <returns>Updated validation rules</returns>
        public async Task<Dictionary<string, object>> UpdateCustomFieldValidationRulesAsync(int fieldId, Dictionary<string, object> rules)
        {
            var response = await PutAsync<Dictionary<string, object>>($"/custom-fields/{fieldId}/validation-rules", rules);
            return response.Data;
        }

        #endregion
    }
}