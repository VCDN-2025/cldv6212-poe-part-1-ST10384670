using ABCretailpoe1.Models;
using ABCretailpoe1.Services;
using Microsoft.AspNetCore.Mvc;

namespace ABCretailpoe1.Controllers
{
    public class CustomersController : Controller
    {
        private readonly TableStorage _tableStorage;

        public CustomersController(TableStorage tableStorage)
        {

            _tableStorage = tableStorage;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _tableStorage.GetAllCustomersAsync();
            return View(customers);
        }


        public async Task<IActionResult> Delete(string partitionKey, string rowKey)
        {
            if (string.IsNullOrEmpty(partitionKey) || string.IsNullOrEmpty(rowKey))
            {
                return BadRequest("PartitionKey and RowKey are required to delete a customer.");
            }

            await _tableStorage.DeleteCustomerAsync(partitionKey, rowKey);
            return RedirectToAction("Index");
        }






        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            customer.PartitionKey = "Customers";
            customer.RowKey = Guid.NewGuid().ToString();

            await _tableStorage.AddCustomerAsync(customer);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(string partitionKey, string rowKey)
        {
            var customer = await _tableStorage.GetCustomerAsync(partitionKey, rowKey);
            if (customer == null)
            {


                return NotFound();
            }

            return View(customer);
        }

        public async Task<IActionResult> Edit(string partitionKey, string rowKey)
        {
            var customer = await _tableStorage.GetCustomerAsync(partitionKey, rowKey);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }       





        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


    }
}
