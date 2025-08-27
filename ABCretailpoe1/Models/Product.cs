using Azure;
using Azure.Data.Tables;

namespace ABCretailpoe1.Models
{
    public class Product : ITableEntity
    {
        public int ProductId { get; set; }   // Primary Key
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string? ImageUrl { get; set; }

        public string PartitionKey { get; set; } = string.Empty; // Default value to avoid null issues
        public string RowKey { get; set; } = string.Empty;       // Default value to avoid null issues
        public ETag ETag { get; set; }

        public DateTimeOffset? Timestamp { get; set; }
    }
}
