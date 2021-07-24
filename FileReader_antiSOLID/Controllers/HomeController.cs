using FileReader_antiSOLID.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace FileReader_antiSOLID.Controllers
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
        public IActionResult Upload(IFormFile postedFile)
        {
            if (postedFile == null) return View("Index");

            //Check if the file-storage directory created
            string dirPath = Path.Combine(environment.WebRootPath, "Uploads");
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            //Save a file at the file-storage directory
            string fileName = Path.GetFileName(postedFile.FileName);
            string fullFileName = Path.Combine(dirPath, fileName);
            using (FileStream stream = new FileStream(fullFileName, FileMode.Create))
            {
                postedFile.CopyTo(stream);
            }

            ViewBag.Message = String.Format("Файл <b>{0}</b> успешно загружен.<br/>", fileName);

            //Select a definite approach to read the file-extension
            FileInfo file = new FileInfo(fullFileName);
            List<string> fileContent = new List<string>();
            if (!file.Exists)
            {
                fileContent.Add("Произошла ошибка. Файл не найден.");
            }
            else if (file.Extension == ".txt")
            {
                fileContent = System.IO.File.ReadAllLines(file.FullName).ToList();
            }
            else if (file.Extension == ".xlsx")
            {
                using (var excelWorkbook = new XLWorkbook(file.FullName))
                {
                    //Read just first worksheet
                    var nonEmptyDataRows = excelWorkbook.Worksheet(1).RowsUsed();

                    //Traverse rows
                    foreach (var dataRow in nonEmptyDataRows)
                    {
                        //Traverse all cells (columns) in rows
                        foreach (var cell in dataRow.Cells())
                        {
                            fileContent.Add(cell.Value.ToString());
                        }
                    }
                }
            }
            else
            {
                fileContent.Add($"Не удалось прочитать файл.Формат *{file.Extension} пока не поддерживается.");
            }

            //Recursive cleaning of the file storage directory and its content
            Directory.Delete(dirPath, true);

            fileContent.Add($"Всего сгенерировано строк: {fileContent.Count}.");
            ViewBag.FileContent = fileContent;
            return View();
        }
    }
}
