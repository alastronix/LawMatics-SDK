using LawMatics.SDK.Configuration;
using LawMatics.SDK.Models;
using Microsoft.Extensions.Logging;

namespace LawMatics.SDK.Clients
{
    /// <summary>
    /// Client for managing expenses in the LawMatics API.
    /// </summary>
    public class ExpensesClient : BaseClient
    {
        public ExpensesClient(HttpClient httpClient, LawMaticsClientOptions options, ILogger? logger)
            : base(httpClient, options, logger)
        {
        }

        /// <summary>
        /// Gets all expenses with optional pagination and filtering.
        /// </summary>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="matterId">Filter by matter ID.</param>
        /// <param name="category">Filter by expense category.</param>
        /// <param name="dateFrom">Filter by expense date from.</param>
        /// <param name="dateTo">Filter by expense date to.</param>
        /// <param name="amountFrom">Filter by minimum amount.</param>
        /// <param name="amountTo">Filter by maximum amount.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of expenses.</returns>
        public async Task<PagedResponse<Expense>> GetExpensesAsync(
            int page = 1,
            int pageSize = 20,
            int? matterId = null,
            string? category = null,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            decimal? amountFrom = null,
            decimal? amountTo = null,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["page"] = page.ToString(),
                ["page_size"] = pageSize.ToString()
            };

            if (matterId.HasValue)
                parameters["matter_id"] = matterId.Value.ToString();

            if (!string.IsNullOrEmpty(category))
                parameters["category"] = category;

            if (dateFrom.HasValue)
                parameters["date_from"] = dateFrom.Value.ToString("yyyy-MM-dd");

            if (dateTo.HasValue)
                parameters["date_to"] = dateTo.Value.ToString("yyyy-MM-dd");

            if (amountFrom.HasValue)
                parameters["amount_from"] = amountFrom.Value.ToString();

            if (amountTo.HasValue)
                parameters["amount_to"] = amountTo.Value.ToString();

            return await GetAsync<PagedResponse<Expense>>("expenses", parameters, cancellationToken);
        }

        /// <summary>
        /// Gets a specific expense by ID.
        /// </summary>
        /// <param name="id">The expense ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The expense details.</returns>
        public async Task<Expense?> GetExpenseAsync(int id, CancellationToken cancellationToken = default)
        {
            return await GetAsync<Expense>($"expenses/{id}", cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Creates a new expense.
        /// </summary>
        /// <param name="request">The expense creation request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created expense.</returns>
        public async Task<Expense?> CreateExpenseAsync(CreateExpenseRequest request, CancellationToken cancellationToken = default)
        {
            return await PostAsync<Expense>("expenses", request, cancellationToken);
        }

        /// <summary>
        /// Updates an existing expense.
        /// </summary>
        /// <param name="id">The expense ID.</param>
        /// <param name="request">The expense update request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated expense.</returns>
        public async Task<Expense?> UpdateExpenseAsync(int id, UpdateExpenseRequest request, CancellationToken cancellationToken = default)
        {
            return await PutAsync<Expense>($"expenses/{id}", request, cancellationToken);
        }

        /// <summary>
        /// Deletes an expense.
        /// </summary>
        /// <param name="id">The expense ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if deletion was successful.</returns>
        public async Task<bool> DeleteExpenseAsync(int id, CancellationToken cancellationToken = default)
        {
            return await DeleteAsync($"expenses/{id}", cancellationToken);
        }

        /// <summary>
        /// Gets expenses for a specific matter.
        /// </summary>
        /// <param name="matterId">The matter ID.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of expenses for the matter.</returns>
        public async Task<PagedResponse<Expense>> GetExpensesByMatterAsync(
            int matterId,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            return await GetExpensesAsync(page, pageSize, matterId: matterId, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets expenses within a specified date range.
        /// </summary>
        /// <param name="fromDate">Start date.</param>
        /// <param name="toDate">End date.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of expenses in the specified date range.</returns>
        public async Task<PagedResponse<Expense>> GetExpensesByDateRangeAsync(
            DateTime fromDate,
            DateTime toDate,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            return await GetExpensesAsync(page, pageSize, dateFrom: fromDate, dateTo: toDate, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets expenses by category.
        /// </summary>
        /// <param name="category">The expense category.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of expenses in the specified category.</returns>
        public async Task<PagedResponse<Expense>> GetExpensesByCategoryAsync(
            string category,
            int page = 1,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            return await GetExpensesAsync(page, pageSize, category: category, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets expense categories.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of expense categories.</returns>
        public async Task<List<string>> GetExpenseCategoriesAsync(CancellationToken cancellationToken = default)
        {
            var response = await GetAsync<ExpenseCategoriesResponse>("expenses/categories", cancellationToken: cancellationToken);
            return response?.Categories ?? new List<string>();
        }

        /// <summary>
        /// Gets expense summary for a matter.
        /// </summary>
        /// <param name="matterId">The matter ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Expense summary for the matter.</returns>
        public async Task<ExpenseSummary?> GetExpenseSummaryByMatterAsync(int matterId, CancellationToken cancellationToken = default)
        {
            return await GetAsync<ExpenseSummary>($"expenses/matters/{matterId}/summary", cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets expense summary for a date range.
        /// </summary>
        /// <param name="fromDate">Start date.</param>
        /// <param name="toDate">End date.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Expense summary for the date range.</returns>
        public async Task<ExpenseSummary?> GetExpenseSummaryByDateRangeAsync(
            DateTime fromDate,
            DateTime toDate,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["from_date"] = fromDate.ToString("yyyy-MM-dd"),
                ["to_date"] = toDate.ToString("yyyy-MM-dd")
            };

            return await GetAsync<ExpenseSummary>("expenses/summary", parameters, cancellationToken);
        }

        /// <summary>
        /// Searches expenses by keyword.
        /// </summary>
        /// <param name="query">Search query.</param>
        /// <param name="page">Page number for pagination (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 20).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A paged list of expenses matching the search query.</returns>
        public async Task<PagedResponse<Expense>> SearchExpensesAsync(
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

            return await GetAsync<PagedResponse<Expense>>("expenses", parameters, cancellationToken);
        }

        /// <summary>
        /// Exports expenses to CSV.
        /// </summary>
        /// <param name="matterId">Filter by matter ID (optional).</param>
        /// <param name="dateFrom">Filter by expense date from (optional).</param>
        /// <param name="dateTo">Filter by expense date to (optional).</param>
        /// <param name="category">Filter by category (optional).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The CSV file content.</returns>
        public async Task<byte[]> ExportExpensesToCsvAsync(
            int? matterId = null,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            string? category = null,
            CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>();

            if (matterId.HasValue)
                parameters["matter_id"] = matterId.Value.ToString();

            if (dateFrom.HasValue)
                parameters["date_from"] = dateFrom.Value.ToString("yyyy-MM-dd");

            if (dateTo.HasValue)
                parameters["date_to"] = dateTo.Value.ToString("yyyy-MM-dd");

            if (!string.IsNullOrEmpty(category))
                parameters["category"] = category;

            parameters["format"] = "csv";

            return await GetFileAsync("expenses/export", parameters, cancellationToken);
        }
    }

    /// <summary>
    /// Response model for expense categories.
    /// </summary>
    public class ExpenseCategoriesResponse
    {
        /// <summary>
        /// Gets or sets the list of expense categories.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("categories")]
        public List<string> Categories { get; set; } = new();
    }

    /// <summary>
    /// Represents expense summary information.
    /// </summary>
    public class ExpenseSummary
    {
        /// <summary>
        /// Gets or sets the total amount.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("total_amount")]
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("total_count")]
        public int TotalCount { get; set; }

        /// <summary>
        /// Gets or sets the average amount.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("average_amount")]
        public decimal AverageAmount { get; set; }

        /// <summary>
        /// Gets or sets the breakdown by category.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("category_breakdown")]
        public List<CategoryExpenseBreakdown> CategoryBreakdown { get; set; } = new();

        /// <summary>
        /// Gets or sets the breakdown by month.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("monthly_breakdown")]
        public List<MonthlyExpenseBreakdown> MonthlyBreakdown { get; set; } = new();
    }

    /// <summary>
    /// Represents expense breakdown by category.
    /// </summary>
    public class CategoryExpenseBreakdown
    {
        /// <summary>
        /// Gets or sets the category name.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("category")]
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the total amount for the category.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the count for the category.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("count")]
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the percentage of total.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("percentage")]
        public decimal Percentage { get; set; }
    }

    /// <summary>
    /// Represents expense breakdown by month.
    /// </summary>
    public class MonthlyExpenseBreakdown
    {
        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("month")]
        public string Month { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the total amount for the month.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the count for the month.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("count")]
        public int Count { get; set; }
    }
}