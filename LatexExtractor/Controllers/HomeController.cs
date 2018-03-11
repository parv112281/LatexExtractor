using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LatexExtractor.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace LatexExtractor.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    string base64string = string.Empty;

                    //byte[] imageBytes = System.IO.File.ReadAllBytes(filePath);

                    //base64string = Convert.ToBase64String(imageBytes);



                    using (var stream = new MemoryStream())
                    {
                        await formFile.CopyToAsync(stream);

                        //using(var memStream = new MemoryStream())
                        //{
                        //    await memStream.CopyToAsync(stream);

                        base64string = Convert.ToBase64String(stream.ToArray());

                        ViewBag.ImageString = base64string;
                        //}



                        //using (var otherStream = new FileStream("/Users/parv/test", FileMode.Create))
                        //{
                        //    stream.Seek(0, SeekOrigin.Begin);
                        //    stream.CopyTo(otherStream);
                        //}
                    }
                }
            }


            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return View();
        }

        //public IActionResult About()
        //{
        //    ViewData["Message"] = "Your application description page.";

        //    return View();
        //}

        //public IActionResult Contact()
        //{
        //    ViewData["Message"] = "Your contact page.";

        //    return View();
        //}

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
