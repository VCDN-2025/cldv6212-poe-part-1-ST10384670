using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Tracing;
using Azure;
using Azure.Data.Tables;

namespace ABCretailpoe1.Models
{
    public class Customer : ITableEntity
    {
        [Key]
        public int CustomerId { get; set; }   // Primary Key

        public string? CustomerName { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }
        public string? ImageUrl { get; set; }

        // Corrected property names to match ITableEntity interface
        public string PartitionKey { get; set; } = string.Empty; // Default value to avoid null issues
        public string RowKey { get; set; } = string.Empty;       // Default value to avoid null issues
        public ETag ETag { get; set; }

        public DateTimeOffset? Timestamp { get; set; }
    }
}
