using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MMisba.Data;
using MMisba.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MMisba.Controllers
{
    public class UploadController : Controller
    {
        private readonly ILogger<UploadController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        
        public UploadController(ILogger<UploadController> logger, ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
         {
            _logger = logger;
            _context = context;
            _hostEnvironment = hostEnvironment;
        }
        [HttpGet]
        public IActionResult UploadImage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UploadImage(ImageCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var path = _hostEnvironment.WebRootPath;
                
                var filePath = "Content/Image"+model.ImagePath.FileName;
                var fullPath = Path.Combine(path, filePath);
                UploadFile(model.ImagePath, fullPath);
                var data = new Image()
                {
                    
                    Name = model.Name,
                    ImagePath = filePath

                };
                _context.Add(data);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }

        }
        public void UploadFile(IFormFile file, string path)
        {

            FileStream stream = new FileStream(path, FileMode.Create);

            file.CopyTo(stream);


        }
        public IActionResult Index()
        {
            var data = _context.Images.ToList();
            return View(data);
        }
    }
}
