using ABCretailpoe1.Models;
using ABCretailpoe1.Services;
using Microsoft.AspNetCore.Mvc;

namespace ABCretailpoe1.Controllers
{
    public class ProductsController : Controller
    {
        private readonly TableStorage _tableStorage;
        private readonly BlobStorage _blobStorage;

        public ProductsController(TableStorage tableStorage, BlobStorage blobStorage)
        {
            _tableStorage = tableStorage;
            _blobStorage = blobStorage;
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
        public async Task<IActionResult> Create(Product product, IFormFile imageFile)
        {
            if (imageFile != null)
            {
                using var stream = imageFile.OpenReadStream();
                var imageUrl = await _blobStorage.UploadAsync(imageFile.FileName, stream);
                product.ImageUrl = imageUrl;
            }

            if (ModelState.IsValid)
            {
               product.PartitionKey = "ProductPartition";
            product.RowKey = Guid.NewGuid().ToString();

            await _tableStorage.AddProductAsync(product);
            return RedirectToAction("Index"); 
            }
            return View(product);



        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(string partitionKey, string rowKey, Product product)

        {
            if (product !=null && !string.IsNullOrEmpty(product.ImageUrl))
            {
                await _blobStorage.DeleteBlobAsync(product.ImageUrl);
            }
            await _tableStorage.DeleteProductAsync(partitionKey, rowKey);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
