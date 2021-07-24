using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace WebServer.Infrastructure
{
    /// <summary>
    /// Static class for upload light single files to the server.
    /// </summary>
    internal static class FileUploader
    {
        /// <summary>
        /// Upload single file to the server.
        /// </summary>
        /// <param name="dirPath">Full file-storage directory path</param>
        /// <param name="postedFile">User's posted file to the server</param>
        /// <returns></returns>
        internal static string Upload(string dirPath, IFormFile postedFile)
        {
            //Check if the file-storage directory created
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

            return string.Format("Файл <b>{0}</b> успешно загружен.<br/>", fileName);
        }

        /// <summary>
        /// Recursive cleaning of the file storage directory and its content (subdirs and all).
        /// </summary>
        /// <param name="dirPath">Full file-storage directory path</param>
        /// <returns></returns>
        internal static bool CleanFileStorage(string dirPath)
        {
            try
            {
                Directory.Delete(dirPath, true);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}