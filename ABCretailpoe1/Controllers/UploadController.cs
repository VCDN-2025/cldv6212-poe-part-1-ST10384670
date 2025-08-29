using Microsoft.AspNetCore.Mvc;
using ABCretailpoe1.Models;
using ABCretailpoe1.Services;

namespace ABCretailpoe1.Controllers
{
    public class UploadController : Controller
    {
        private readonly BlobStorage _blobStorage;

        public UploadController(BlobStorage blobStorage)
        {
            _blobStorage = blobStorage;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UploadModel model, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                // Use your BlobStorage service to upload
                var fileUrl = await _blobStorage.UploadAsync(file.FileName, stream);
                model.FileUrl = fileUrl;
            }

            ViewBag.Message = "File uploaded successfully!";
            return View(model);
        }
    }
}
