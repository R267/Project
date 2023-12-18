﻿using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace ImgHub.Controllers
{
    public class ImageHubController : Controller
    {
        private readonly string rootPath = Path.Combine (Directory.GetCurrentDirectory(),"wwwroot//uploads");
        public IActionResult Index()
        {
            bool dirExists = Directory.Exists(rootPath);
            if (!dirExists)
                Directory.CreateDirectory(rootPath);
            List<string> imageNames = Directory.GetFiles(rootPath).Select(Path.GetFileName).ToList();
            return View(imageNames);
        }
        [HttpPost]
        public async Task <IActionResult> Index(IFormFile file)
        {
            if (file !=null)
            {
                var path = Path.Combine(rootPath, Guid.NewGuid()+ Path.GetExtension(file.FileName));
				using (var fs = new FileStream(path, FileMode.Create))
				{
                    await file.CopyToAsync(fs);

				}
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index");
			}
            return View();
        }
    }
}
