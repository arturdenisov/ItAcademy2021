using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileReader
{
    /// <summary>
    /// File reader for files which extensions are not specified.
    /// </summary>
    internal class DefaultFileReader : IFileReader
    {
        public FileInfo File { get; set; }

        internal DefaultFileReader(FileInfo file)
        {
            File = file;
        }

        public List<string> Read()
        {
            List<string> list = new List<string>
            {
                $"Не удалось прочитать файл. Формат *{File.Extension} пока не поддерживается."
            };
            return list;
        }
    }
}
