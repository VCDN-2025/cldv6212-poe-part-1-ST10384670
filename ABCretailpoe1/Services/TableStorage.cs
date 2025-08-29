using ABCretailpoe1.Models;
using Azure.Data.Tables;

namespace ABCretailpoe1.Services
{
    public class TableStorage
    {
        public readonly TableClient _customerTableClient;
        public readonly TableClient _productTableClient;
        public readonly TableClient _orderTableClient;

        public TableStorage(string connectionString)
        {
            _customerTableClient = new TableClient(connectionString, "Customer");
            _productTableClient = new TableClient(connectionString, "Product");
            _orderTableClient = new TableClient(connectionString, "Order");
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            var customers = new List<Customer>();

            await foreach (var customer in _customerTableClient.QueryAsync<Customer>())
            {
                customers.Add(customer);
            }
            return customers;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var products = new List<Product>();
            await foreach (var product in _productTableClient.QueryAsync<Product>())
            {
                products.Add(product);
            }
            return products;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            var orders = new List<Order>();
            await foreach (var order in _orderTableClient.QueryAsync<Order>())
            {
                orders.Add(order);
            }
            return orders;
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            if (string.IsNullOrEmpty(customer.PartitionKey) || string.IsNullOrEmpty(customer.RowKey))
            {
                throw new ArgumentException("PartitionKey and RowKey must be set.");
            }
            try
            {
                await _customerTableClient.AddEntityAsync(customer);
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
                throw new InvalidOperationException("Error adding customer to Table Storage", ex);
            }
        }

        public async Task AddProductAsync(Product product)
        {
            if (string.IsNullOrEmpty(product.PartitionKey) || string.IsNullOrEmpty(product.RowKey))
            {
                throw new ArgumentException("PartitionKey and RowKey must be set.");
            }
            try
            {
                await _productTableClient.AddEntityAsync(product);
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
                throw new InvalidOperationException("Error adding product to Table Storage", ex);
            }
        }

        public async Task AddOrderAsync(Order order)
        {
            if (string.IsNullOrEmpty(order.PartitionKey) || string.IsNullOrEmpty(order.RowKey))
            {
                throw new ArgumentException("PartitionKey and RowKey must be set.");
            }
            try
            {
                await _orderTableClient.AddEntityAsync(order);
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
                throw new InvalidOperationException("Error adding order to Table Storage", ex);
            }
        }

        public async Task DeleteCustomerAsync(string partitionKey, string rowKey)
        {
           await _customerTableClient.DeleteEntityAsync(partitionKey, rowKey);
        }

        public async Task DeleteProductAsync(string partitionKey, string rowKey)
        {
            await _productTableClient.DeleteEntityAsync(partitionKey, rowKey);
        }

        public async Task DeleteOrderAsync(string partitionKey, string rowKey)
        {
            await _orderTableClient.DeleteEntityAsync(partitionKey, rowKey);
        }

        internal async Task<string?> GetProductAsync(string partitionKey, string rowKey)
        {
            throw new NotImplementedException();
        }

        internal async Task<string?> GetCustomerAsync(string partitionKey, string rowKey)
        {
            throw new NotImplementedException();
        }
    }
}
