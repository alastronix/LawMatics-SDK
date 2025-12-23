using LawMatics.SDK.Configuration;
using LawMatics.SDK.Exceptions;
using LawMatics.SDK.Models;
using Microsoft.Extensions.Logging;

namespace LawMatics.SDK.Clients
{
    /// <summary>
    /// Client for managing payments and invoices in the Lawmatics API
    /// </summary>
    public class PaymentsClient : BaseClient
    {
        public PaymentsClient(HttpClient httpClient, LawMaticsClientOptions options, ILogger? logger = null)
            : base(httpClient, options, logger)
        {
        }

        #region Payments/Invoices

        /// <summary>
        /// Get all payments with optional filtering and pagination
        /// </summary>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>Paginated list of payments</returns>
        public async Task<ApiResponse<List<Payment>>> GetPaymentsAsync(FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Payment>>($"/payments?{queryParams}");
            return response;
        }

        /// <summary>
        /// Get a specific payment by ID
        /// </summary>
        /// <param name="id">Payment ID</param>
        /// <returns>Payment details</returns>
        public async Task<Payment> GetPaymentAsync(int id)
        {
            var response = await GetAsync<Payment>($"/payments/{id}");
            return response.Data;
        }

        /// <summary>
        /// Create a new payment
        /// </summary>
        /// <param name="request">Payment creation request</param>
        /// <returns>Created payment</returns>
        public async Task<Payment> CreatePaymentAsync(CreatePaymentRequest request)
        {
            var response = await PostAsync<Payment>("/payments", request);
            return response.Data;
        }

        /// <summary>
        /// Update an existing payment
        /// </summary>
        /// <param name="id">Payment ID</param>
        /// <param name="request">Payment update request</param>
        /// <returns>Updated payment</returns>
        public async Task<Payment> UpdatePaymentAsync(int id, UpdatePaymentRequest request)
        {
            var response = await PutAsync<Payment>($"/payments/{id}", request);
            return response.Data;
        }

        /// <summary>
        /// Delete a payment
        /// </summary>
        /// <param name="id">Payment ID</param>
        public async Task DeletePaymentAsync(int id)
        {
            await DeleteAsync($"/payments/{id}");
        }

        /// <summary>
        /// Process a payment (mark as paid)
        /// </summary>
        /// <param name="id">Payment ID</param>
        /// <param name="request">Payment processing request</param>
        /// <returns>Processed payment</returns>
        public async Task<Payment> ProcessPaymentAsync(int id, ProcessPaymentRequest request)
        {
            var response = await PostAsync<Payment>($"/payments/{id}/process", request);
            return response.Data;
        }

        /// <summary>
        /// Get payments by status
        /// </summary>
        /// <param name="status">Status to filter by</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of payments with specified status</returns>
        public async Task<ApiResponse<List<Payment>>> GetPaymentsByStatusAsync(string status, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Payment>>($"/payments?status={status}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get payments by contact
        /// </summary>
        /// <param name="contactId">Contact ID to filter by</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of payments for specified contact</returns>
        public async Task<ApiResponse<List<Payment>>> GetPaymentsByContactAsync(int contactId, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Payment>>($"/payments?contact_id={contactId}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get payments by matter
        /// </summary>
        /// <param name="matterId">Matter ID to filter by</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of payments for specified matter</returns>
        public async Task<ApiResponse<List<Payment>>> GetPaymentsByMatterAsync(int matterId, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Payment>>($"/payments?matter_id={matterId}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get payments by payment method
        /// </summary>
        /// <param name="paymentMethod">Payment method to filter by</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of payments with specified method</returns>
        public async Task<ApiResponse<List<Payment>>> GetPaymentsByMethodAsync(string paymentMethod, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Payment>>($"/payments?payment_method={paymentMethod}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get overdue payments
        /// </summary>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of overdue payments</returns>
        public async Task<ApiResponse<List<Payment>>> GetOverduePaymentsAsync(FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Payment>>($"/payments?overdue=true&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get payments due within date range
        /// </summary>
        /// <param name="startDate">Start date filter</param>
        /// <param name="endDate">End date filter</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of payments due within date range</returns>
        public async Task<ApiResponse<List<Payment>>> GetPaymentsDueByDateRangeAsync(DateTime startDate, DateTime endDate, FilterParameters? parameters = null)
        {
            var filterParams = parameters ?? new FilterParameters();
            filterParams.StartDate = startDate;
            filterParams.EndDate = endDate;
            
            var queryParams = BuildQueryString(filterParams);
            var response = await GetAsync<List<Payment>>($"/payments?{queryParams}");
            return response;
        }

        /// <summary>
        /// Send payment reminder
        /// </summary>
        /// <param name="id">Payment ID</param>
        /// <param name="message">Optional custom reminder message</param>
        public async Task SendPaymentReminderAsync(int id, string? message = null)
        {
            object request = string.IsNullOrEmpty(message) ? new { } : new { message };
            await PostAsync<object>($"/payments/{id}/reminder", request);
        }

        /// <summary>
        /// Generate payment receipt
        /// </summary>
        /// <param name="id">Payment ID</param>
        /// <returns>Receipt data or URL</returns>
        public async Task<object> GeneratePaymentReceiptAsync(int id)
        {
            var response = await PostAsync<object>($"/payments/{id}/receipt");
            return response.Data;
        }

        /// <summary>
        /// Void a payment
        /// </summary>
        /// <param name="id">Payment ID</param>
        /// <param name="reason">Reason for voiding</param>
        public async Task VoidPaymentAsync(int id, string reason)
        {
            await PostAsync<object>($"/payments/{id}/void", new { reason });
        }

        /// <summary>
        /// Refund a payment
        /// </summary>
        /// <param name="id">Payment ID</param>
        /// <param name="amount">Refund amount</param>
        /// <param name="reason">Reason for refund</param>
        /// <returns>Refund details</returns>
        public async Task<object> RefundPaymentAsync(int id, decimal amount, string reason)
        {
            var response = await PostAsync<object>($"/payments/{id}/refund", new { amount, reason });
            return response.Data;
        }

        #endregion

        #region Expenses

        /// <summary>
        /// Get all expenses with optional filtering and pagination
        /// </summary>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>Paginated list of expenses</returns>
        public async Task<ApiResponse<List<Expense>>> GetExpensesAsync(FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Expense>>($"/expenses?{queryParams}");
            return response;
        }

        /// <summary>
        /// Get a specific expense by ID
        /// </summary>
        /// <param name="id">Expense ID</param>
        /// <returns>Expense details</returns>
        public async Task<Expense> GetExpenseAsync(int id)
        {
            var response = await GetAsync<Expense>($"/expenses/{id}");
            return response.Data;
        }

        /// <summary>
        /// Create a new expense
        /// </summary>
        /// <param name="request">Expense creation request</param>
        /// <returns>Created expense</returns>
        public async Task<Expense> CreateExpenseAsync(Expense request)
        {
            var response = await PostAsync<Expense>("/expenses", request);
            return response.Data;
        }

        /// <summary>
        /// Update an existing expense
        /// </summary>
        /// <param name="id">Expense ID</param>
        /// <param name="request">Expense update request</param>
        /// <returns>Updated expense</returns>
        public async Task<Expense> UpdateExpenseAsync(int id, Expense request)
        {
            var response = await PutAsync<Expense>($"/expenses/{id}", request);
            return response.Data;
        }

        /// <summary>
        /// Delete an expense
        /// </summary>
        /// <param name="id">Expense ID</param>
        public async Task DeleteExpenseAsync(int id)
        {
            await DeleteAsync($"/expenses/{id}");
        }

        /// <summary>
        /// Get expenses by type
        /// </summary>
        /// <param name="expenseType">Expense type to filter by</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of expenses of specified type</returns>
        public async Task<ApiResponse<List<Expense>>> GetExpensesByTypeAsync(string expenseType, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Expense>>($"/expenses?expense_type={expenseType}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get reimbursable expenses
        /// </summary>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of reimbursable expenses</returns>
        public async Task<ApiResponse<List<Expense>>> GetReimbursableExpensesAsync(FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<Expense>>($"/expenses?reimbursable=true&{queryParams}");
            return response;
        }

        #endregion

        #region Time Entries

        /// <summary>
        /// Get all time entries with optional filtering and pagination
        /// </summary>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>Paginated list of time entries</returns>
        public async Task<ApiResponse<List<TimeEntry>>> GetTimeEntriesAsync(FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<TimeEntry>>($"/time-entries?{queryParams}");
            return response;
        }

        /// <summary>
        /// Get a specific time entry by ID
        /// </summary>
        /// <param name="id">Time entry ID</param>
        /// <returns>Time entry details</returns>
        public async Task<TimeEntry> GetTimeEntryAsync(int id)
        {
            var response = await GetAsync<TimeEntry>($"/time-entries/{id}");
            return response.Data;
        }

        /// <summary>
        /// Create a new time entry
        /// </summary>
        /// <param name="request">Time entry creation request</param>
        /// <returns>Created time entry</returns>
        public async Task<TimeEntry> CreateTimeEntryAsync(TimeEntry request)
        {
            var response = await PostAsync<TimeEntry>("/time-entries", request);
            return response.Data;
        }

        /// <summary>
        /// Update an existing time entry
        /// </summary>
        /// <param name="id">Time entry ID</param>
        /// <param name="request">Time entry update request</param>
        /// <returns>Updated time entry</returns>
        public async Task<TimeEntry> UpdateTimeEntryAsync(int id, TimeEntry request)
        {
            var response = await PutAsync<TimeEntry>($"/time-entries/{id}", request);
            return response.Data;
        }

        /// <summary>
        /// Delete a time entry
        /// </summary>
        /// <param name="id">Time entry ID</param>
        public async Task DeleteTimeEntryAsync(int id)
        {
            await DeleteAsync($"/time-entries/{id}");
        }

        /// <summary>
        /// Get time entries by user
        /// </summary>
        /// <param name="userId">User ID to filter by</param>
        /// <param name="parameters">Additional filter parameters</param>
        /// <returns>List of time entries for specified user</returns>
        public async Task<ApiResponse<List<TimeEntry>>> GetTimeEntriesByUserAsync(int userId, FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<TimeEntry>>($"/time-entries?user_id={userId}&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get billable time entries
        /// </summary>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of billable time entries</returns>
        public async Task<ApiResponse<List<TimeEntry>>> GetBillableTimeEntriesAsync(FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<TimeEntry>>($"/time-entries?billable=true&{queryParams}");
            return response;
        }

        /// <summary>
        /// Get unbilled time entries
        /// </summary>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>List of unbilled time entries</returns>
        public async Task<ApiResponse<List<TimeEntry>>> GetUnbilledTimeEntriesAsync(FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<List<TimeEntry>>($"/time-entries?billed=false&{queryParams}");
            return response;
        }

        #endregion

        #region Bulk Operations

        /// <summary>
        /// Bulk create multiple payments
        /// </summary>
        /// <param name="requests">List of payment creation requests</param>
        /// <returns>List of created payments</returns>
        public async Task<List<Payment>> BulkCreatePaymentsAsync(List<CreatePaymentRequest> requests)
        {
            var response = await PostAsync<List<Payment>>("/payments/bulk", new { payments = requests });
            return response.Data;
        }

        /// <summary>
        /// Bulk update multiple payments
        /// </summary>
        /// <param name="updates">Dictionary of payment IDs to update requests</param>
        /// <returns>List of updated payments</returns>
        public async Task<List<Payment>> BulkUpdatePaymentsAsync(Dictionary<int, UpdatePaymentRequest> updates)
        {
            var response = await PutAsync<List<Payment>>("/payments/bulk", new { updates });
            return response.Data;
        }

        /// <summary>
        /// Bulk process multiple payments
        /// </summary>
        /// <param name="paymentIds">List of payment IDs to process</param>
        /// <param name="paymentMethod">Payment method to use</param>
        public async Task BulkProcessPaymentsAsync(List<int> paymentIds, string paymentMethod)
        {
            await PostAsync<object>("/payments/bulk-process", new { payment_ids = paymentIds, payment_method = paymentMethod });
        }

        /// <summary>
        /// Bulk send payment reminders
        /// </summary>
        /// <param name="paymentIds">List of payment IDs to send reminders for</param>
        /// <param name="message">Optional custom reminder message</param>
        public async Task BulkSendPaymentRemindersAsync(List<int> paymentIds, string? message = null)
        {
            var request = new Dictionary<string, object> { { "payment_ids", paymentIds } };
            if (!string.IsNullOrEmpty(message))
                request.Add("message", message);
            
            await PostAsync<object>("/payments/bulk-reminder", request);
        }

        #endregion

        #region Analytics and Reporting

        /// <summary>
        /// Get payment summary statistics
        /// </summary>
        /// <param name="startDate">Start date for statistics</param>
        /// <param name="endDate">End date for statistics</param>
        /// <returns>Payment summary statistics</returns>
        public async Task<object> GetPaymentSummaryAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            var queryString = "";
            if (startDate.HasValue)
                queryString += $"&start_date={startDate.Value:yyyy-MM-dd}";
            if (endDate.HasValue)
                queryString += $"&end_date={endDate.Value:yyyy-MM-dd}";
            
            var endpoint = $"/payments/summary{(!string.IsNullOrEmpty(queryString) ? $"?{queryString.Substring(1)}" : "")}";
            var response = await GetAsync<object>(endpoint);
            return response.Data;
        }

        /// <summary>
        /// Get revenue report
        /// </summary>
        /// <param name="startDate">Start date for report</param>
        /// <param name="endDate">End date for report</param>
        /// <param name="groupBy">Group by parameter (day, week, month, year)</param>
        /// <returns>Revenue report data</returns>
        public async Task<object> GetRevenueReportAsync(DateTime? startDate = null, DateTime? endDate = null, string groupBy = "month")
        {
            var queryString = $"group_by={groupBy}";
            if (startDate.HasValue)
                queryString += $"&start_date={startDate.Value:yyyy-MM-dd}";
            if (endDate.HasValue)
                queryString += $"&end_date={endDate.Value:yyyy-MM-dd}";
            
            var response = await GetAsync<object>($"/payments/revenue-report?{queryString}");
            return response.Data;
        }

        /// <summary>
        /// Get payment analytics
        /// </summary>
        /// <param name="parameters">Optional filter parameters</param>
        /// <returns>Payment analytics data</returns>
        public async Task<object> GetPaymentAnalyticsAsync(FilterParameters? parameters = null)
        {
            var queryParams = BuildQueryString(parameters ?? new FilterParameters());
            var response = await GetAsync<object>($"/payments/analytics?{queryParams}");
            return response.Data;
        }

        #endregion
    }
}