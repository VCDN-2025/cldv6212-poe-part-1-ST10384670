using ABCretailpoe1.Models;
using ABCretailpoe1.Services;
using Microsoft.AspNetCore.Mvc;

namespace ABCretailpoe1.Controllers
{
    public class ProductsController : Controller
    {
        private readonly TableStorage _tableStorage;

        public ProductsController(TableStorage tableStorage)
        {
            
            _tableStorage = tableStorage;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _tableStorage.GetAllProductsAsync();
            return View(products);
        }

        public async Task<IActionResult> Delete(string partitionKey, string rowKey)
        {
           await _tableStorage.DeleteOrderAsync(partitionKey, rowKey);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            product.PartitionKey = "Product";
            product.RowKey = Guid.NewGuid().ToString();
            await _tableStorage.AddProductAsync(product);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
