using Azure;
using Azure.Data.Tables;

namespace ABCretailpoe1.Models
{
    public class Order : ITableEntity
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalAmount { get; set; }
        public string? Status { get; set; }
        
        public int ProductId { get; set; }

        public string PartitionKey { get; set; } = string.Empty; // Default value to avoid null issues
        public string RowKey { get; set; } = string.Empty;       // Default value to avoid null issues
        public ETag ETag { get; set; }

        public DateTimeOffset? Timestamp { get; set; }
    }
}
