using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Collections.Generic;
using FileReader_v2;
using WebServer.Infrastructure;

namespace WebServer.Controllers
{
    public class HomeController : Controller
    {
        readonly private IWebHostEnvironment environment;

        public HomeController(IWebHostEnvironment _environment)
        {
            environment = _environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Uploads the user's file to the server and returns its contents.
        /// </summary>
        /// <param name="postedFile">User's posted file</param>
        /// <returns>View with message "File uploaded" and file-content.</returns>
        [HttpPost]
        public IActionResult Upload(IFormFile postedFile)
        {
            if (postedFile == null) return View("Index");

            //Save a file at the server
            string dirPath = Path.Combine(environment.WebRootPath, "Uploads");
            ViewBag.Message = FileUploader.Upload(dirPath, postedFile);

            //Read content from the file
            string fullFilePath = Path.Combine(dirPath, postedFile.FileName);
            IFileReader fileReader = AcceptSizeControlledFileReaderFactory.SelectIFileReader(fullFilePath);
            List<string> fileContent = fileReader.Read();
            fileContent.Add($"Всего сгенерировано строк: {fileContent.Count}.");
            ViewBag.FileContent = fileContent;

            //Delete file-storage directory with all files
            FileUploader.CleanFileStorage(dirPath);

            return View();
        }
    }
}
