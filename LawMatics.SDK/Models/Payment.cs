using System.Text.Json.Serialization;

namespace LawMatics.SDK.Models
{
    /// <summary>
    /// Represents a payment/invoice in Lawmatics
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// Unique identifier for the payment
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Invoice number
        /// </summary>
        [JsonPropertyName("invoice_number")]
        public string InvoiceNumber { get; set; } = string.Empty;

        /// <summary>
        /// Payment description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Payment status (draft, sent, paid, overdue, cancelled)
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Payment type (invoice, payment, refund)
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Total amount
        /// </summary>
        [JsonPropertyName("total_amount")]
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Amount paid
        /// </summary>
        [JsonPropertyName("amount_paid")]
        public decimal AmountPaid { get; set; }

        /// <summary>
        /// Balance due
        /// </summary>
        [JsonPropertyName("balance_due")]
        public decimal BalanceDue { get; set; }

        /// <summary>
        /// Currency code
        /// </summary>
        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        /// <summary>
        /// Due date
        /// </summary>
        [JsonPropertyName("due_date")]
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Paid date
        /// </summary>
        [JsonPropertyName("paid_date")]
        public DateTime? PaidDate { get; set; }

        /// <summary>
        /// Invoice items
        /// </summary>
        [JsonPropertyName("items")]
        public List<InvoiceItem>? Items { get; set; }

        /// <summary>
        /// Associated contact
        /// </summary>
        [JsonPropertyName("contact")]
        public Contact? Contact { get; set; }

        /// <summary>
        /// Associated company
        /// </summary>
        [JsonPropertyName("company")]
        public Company? Company { get; set; }

        /// <summary>
        /// Associated matter
        /// </summary>
        [JsonPropertyName("matter")]
        public Matter? Matter { get; set; }

        /// <summary>
        /// Payment method
        /// </summary>
        [JsonPropertyName("payment_method")]
        public string? PaymentMethod { get; set; }

        /// <summary>
        /// Transaction ID
        /// </summary>
        [JsonPropertyName("transaction_id")]
        public string? TransactionId { get; set; }

        /// <summary>
        /// Payment gateway used
        /// </summary>
        [JsonPropertyName("gateway")]
        public string? Gateway { get; set; }

        /// <summary>
        /// Tax amount
        /// </summary>
        [JsonPropertyName("tax_amount")]
        public decimal TaxAmount { get; set; }

        /// <summary>
        /// Discount amount
        /// </summary>
        [JsonPropertyName("discount_amount")]
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// Notes
        /// </summary>
        [JsonPropertyName("notes")]
        public string? Notes { get; set; }

        /// <summary>
        /// Payment URL
        /// </summary>
        [JsonPropertyName("payment_url")]
        public string? PaymentUrl { get; set; }

        /// <summary>
        /// Invoice PDF URL
        /// </summary>
        [JsonPropertyName("pdf_url")]
        public string? PdfUrl { get; set; }

        /// <summary>
        /// Date when payment was created
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date when payment was last updated
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Custom fields for the payment
        /// </summary>
        [JsonPropertyName("custom_fields")]
        public Dictionary<string, object>? CustomFields { get; set; }
    }

    /// <summary>
    /// Invoice line item
    /// </summary>
    public class InvoiceItem
    {
        /// <summary>
        /// Item ID
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Item description
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Item quantity
        /// </summary>
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Unit price
        /// </summary>
        [JsonPropertyName("unit_price")]
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Total price for this item
        /// </summary>
        [JsonPropertyName("total")]
        public decimal Total { get; set; }

        /// <summary>
        /// Item type (service, product, fee)
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Tax rate for this item
        /// </summary>
        [JsonPropertyName("tax_rate")]
        public decimal TaxRate { get; set; }

        /// <summary>
        /// Tax amount for this item
        /// </summary>
        [JsonPropertyName("tax_amount")]
        public decimal TaxAmount { get; set; }
    }

    /// <summary>
    /// Request model for creating a new invoice
    /// </summary>
    public class CreateInvoiceRequest
    {
        /// <summary>
        /// Invoice description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Due date
        /// </summary>
        [JsonPropertyName("due_date")]
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Contact ID
        /// </summary>
        [JsonPropertyName("contact_id")]
        public int? ContactId { get; set; }

        /// <summary>
        /// Company ID
        /// </summary>
        [JsonPropertyName("company_id")]
        public int? CompanyId { get; set; }

        /// <summary>
        /// Matter ID
        /// </summary>
        [JsonPropertyName("matter_id")]
        public int? MatterId { get; set; }

        /// <summary>
        /// Currency code
        /// </summary>
        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        /// <summary>
        /// Tax amount
        /// </summary>
        [JsonPropertyName("tax_amount")]
        public decimal? TaxAmount { get; set; }

        /// <summary>
        /// Discount amount
        /// </summary>
        [JsonPropertyName("discount_amount")]
        public decimal? DiscountAmount { get; set; }

        /// <summary>
        /// Notes
        /// </summary>
        [JsonPropertyName("notes")]
        public string? Notes { get; set; }

        /// <summary>
        /// Invoice items (required)
        /// </summary>
        [JsonPropertyName("items")]
        public List<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();

        /// <summary>
        /// Custom fields for the invoice
        /// </summary>
        [JsonPropertyName("custom_fields")]
        public Dictionary<string, object>? CustomFields { get; set; }
    }

    /// <summary>
    /// Request model for updating an existing invoice
    /// </summary>
    public class UpdateInvoiceRequest
    {
        /// <summary>
        /// Invoice description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Invoice status
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Due date
        /// </summary>
        [JsonPropertyName("due_date")]
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Contact ID
        /// </summary>
        [JsonPropertyName("contact_id")]
        public int? ContactId { get; set; }

        /// <summary>
        /// Company ID
        /// </summary>
        [JsonPropertyName("company_id")]
        public int? CompanyId { get; set; }

        /// <summary>
        /// Matter ID
        /// </summary>
        [JsonPropertyName("matter_id")]
        public int? MatterId { get; set; }

        /// <summary>
        /// Currency code
        /// </summary>
        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        /// <summary>
        /// Tax amount
        /// </summary>
        [JsonPropertyName("tax_amount")]
        public decimal? TaxAmount { get; set; }

        /// <summary>
        /// Discount amount
        /// </summary>
        [JsonPropertyName("discount_amount")]
        public decimal? DiscountAmount { get; set; }

        /// <summary>
        /// Notes
        /// </summary>
        [JsonPropertyName("notes")]
        public string? Notes { get; set; }

        /// <summary>
        /// Invoice items
        /// </summary>
        [JsonPropertyName("items")]
        public List<InvoiceItem>? Items { get; set; }

        /// <summary>
        /// Custom fields for the invoice
        /// </summary>
        [JsonPropertyName("custom_fields")]
        public Dictionary<string, object>? CustomFields { get; set; }
    }

    /// <summary>
    /// Request model for processing a payment
    /// </summary>
    public class ProcessPaymentRequest
    {
        /// <summary>
        /// Payment amount
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Payment method (credit_card, bank_transfer, cash, check)
        /// </summary>
        [JsonPropertyName("payment_method")]
        public string PaymentMethod { get; set; } = string.Empty;

        /// <summary>
        /// Transaction ID from payment gateway
        /// </summary>
        [JsonPropertyName("transaction_id")]
        public string? TransactionId { get; set; }

        /// <summary>
        /// Payment gateway used
        /// </summary>
        [JsonPropertyName("gateway")]
        public string? Gateway { get; set; }

        /// <summary>
        /// Payment notes
        /// </summary>
        [JsonPropertyName("notes")]
        public string? Notes { get; set; }

        /// <summary>
        /// Custom fields for the payment
        /// </summary>
        [JsonPropertyName("custom_fields")]
        public Dictionary<string, object>? CustomFields { get; set; }
    }
}