using ABCretailpoe1.Models;
using ABCretailpoe1.Services;
using Microsoft.AspNetCore.Mvc;

namespace ABCretailpoe1.Controllers
{
    public class OrdersController : Controller
    {
        private readonly TableStorage _tableStorage;

        public OrdersController(TableStorage tableStorage)
        {
            _tableStorage = tableStorage;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _tableStorage.GetAllOrdersAsync();
            return View(orders);
        }

        public async Task<IActionResult> Delete(string partitionKey, string rowKey)
        {
            await _tableStorage.DeleteOrderAsync(partitionKey, rowKey);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            order.PartitionKey = "Order";
            order.RowKey = Guid.NewGuid().ToString();
            await _tableStorage.AddOrderAsync(order);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
