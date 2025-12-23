using LawMatics.SDK.Configuration;
using LawMatics.SDK.Exceptions;
using LawMatics.SDK.Models;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using System.Linq;

namespace LawMatics.SDK.Clients
{
    /// <summary>
    /// Base client class with common functionality for all API clients
    /// </summary>
    public abstract class BaseClient
    {
        protected readonly HttpClient _httpClient;
        protected readonly LawMaticsClientOptions _options;
        protected readonly ILogger? _logger;
        protected readonly JsonSerializerOptions _jsonOptions;

        protected BaseClient(HttpClient httpClient, LawMaticsClientOptions options, ILogger? logger = null)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _logger = logger;

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
            };
        }

        protected async Task<ApiResponse<T>> GetAsync<T>(string endpoint)
        {
            return await SendRequestAsync<T>(HttpMethod.Get, endpoint);
        }

        protected async Task<ApiResponse<T>> PostAsync<T>(string endpoint, object? data = null)
        {
            return await SendRequestAsync<T>(HttpMethod.Post, endpoint, data);
        }

        protected async Task<ApiResponse<T>> PutAsync<T>(string endpoint, object? data = null)
        {
            return await SendRequestAsync<T>(HttpMethod.Put, endpoint, data);
        }

        protected async Task DeleteAsync(string endpoint)
        {
            await SendRequestAsync<object>(HttpMethod.Delete, endpoint);
        }

        protected async Task<T?> GetAsync<T>(string endpoint, Dictionary<string, string> parameters, CancellationToken cancellationToken = default)
        {
            var queryString = string.Join("&", parameters.Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"));
            var fullEndpoint = $"{endpoint}?{queryString}";
            var response = await GetAsync<T>(fullEndpoint);
            return response.Data;
        }

        protected async Task<T?> GetAsync<T>(string endpoint, CancellationToken cancellationToken = default)
        {
            var response = await SendRequestAsync<T>(HttpMethod.Get, endpoint);
            return response.Data;
        }

        protected async Task<T?> PostAsync<T>(string endpoint, object data, CancellationToken cancellationToken = default)
        {
            var response = await SendRequestAsync<T>(HttpMethod.Post, endpoint, data);
            return response.Data;
        }

        protected async Task<T?> PutAsync<T>(string endpoint, object data, CancellationToken cancellationToken = default)
        {
            var response = await SendRequestAsync<T>(HttpMethod.Put, endpoint, data);
            return response.Data;
        }

        protected async Task<bool> DeleteAsync(string endpoint, CancellationToken cancellationToken = default)
        {
            await SendRequestAsync<object>(HttpMethod.Delete, endpoint);
            return true;
        }

        protected async Task<T?> DeleteAsync<T>(string endpoint, CancellationToken cancellationToken = default)
        {
            var response = await SendRequestAsync<T>(HttpMethod.Delete, endpoint);
            return response.Data;
        }

        protected async Task<byte[]> GetFileAsync(string endpoint, Dictionary<string, string>? parameters = null, CancellationToken cancellationToken = default)
        {
            var fullEndpoint = endpoint;
            if (parameters != null && parameters.Count > 0)
            {
                var queryString = string.Join("&", parameters.Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"));
                fullEndpoint = $"{endpoint}?{queryString}";
            }

            var response = await _httpClient.GetAsync(fullEndpoint, cancellationToken);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync(cancellationToken);
        }

        protected async Task<ApiResponse<T>> SendRequestAsync<T>(HttpMethod method, string endpoint, object? data = null)
        {
            var request = new HttpRequestMessage(method, endpoint);

            if (data != null)
            {
                var json = JsonSerializer.Serialize(data, _jsonOptions);
                request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            }

            try
            {
                _logger?.LogDebug("Sending {Method} request to {Endpoint}", method.Method, endpoint);

                var response = await _httpClient.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();

                _logger?.LogDebug("Received response with status {StatusCode}", response.StatusCode);

                if (!response.IsSuccessStatusCode)
                {
                    await HandleErrorResponseAsync(response, content);
                }

                if (string.IsNullOrEmpty(content))
                {
                    return new ApiResponse<T> { Data = default! };
                }

                var result = JsonSerializer.Deserialize<ApiResponse<T>>(content, _jsonOptions);
                return result ?? new ApiResponse<T> { Data = default! };
            }
            catch (LawMaticsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error sending request to {Endpoint}", endpoint);
                throw new LawMaticsException($"Error sending request to {endpoint}: {ex.Message}", ex);
            }
            finally
            {
                request.Dispose();
            }
        }

        private async Task HandleErrorResponseAsync(HttpResponseMessage response, string content)
        {
            LawMaticsException? exception = null;

            try
            {
                var errorResponse = JsonSerializer.Deserialize<ApiErrorResponse>(content, _jsonOptions);
                if (errorResponse != null)
                {
                    exception = response.StatusCode switch
                    {
                        HttpStatusCode.Unauthorized => new LawMaticsAuthenticationException(errorResponse.Message ?? "Authentication failed"),
                        HttpStatusCode.NotFound => new LawMaticsNotFoundException(errorResponse.Message ?? "Resource not found"),
                        HttpStatusCode.TooManyRequests => new LawMaticsRateLimitException(errorResponse.Message ?? "Rate limit exceeded"),
                        _ => new LawMaticsApiException(errorResponse.Message ?? "API request failed", (int)response.StatusCode, errorResponse.ErrorCode)
                    };
                }
            }
            catch
            {
                // Fallback if error response parsing fails
            }

            exception ??= response.StatusCode switch
            {
                HttpStatusCode.Unauthorized => new LawMaticsAuthenticationException("Authentication failed"),
                HttpStatusCode.NotFound => new LawMaticsNotFoundException("Resource not found"),
                HttpStatusCode.TooManyRequests => new LawMaticsRateLimitException("Rate limit exceeded"),
                _ => new LawMaticsApiException($"API request failed with status {response.StatusCode}", (int)response.StatusCode)
            };

            throw exception;
        }

        protected string BuildQueryString(object parameters)
        {
            var properties = parameters.GetType().GetProperties()
                .Where(p => p.GetValue(parameters) != null)
                .Select(p => $"{char.ToLowerInvariant(p.Name[0])}{p.Name.Substring(1)}={Uri.EscapeDataString(p.GetValue(parameters)?.ToString() ?? "")}");

            return string.Join("&", properties);
        }
    }
}