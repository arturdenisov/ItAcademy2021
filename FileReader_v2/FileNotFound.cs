using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileReader_v2
{
    /// <summary>
    /// Abstraction of a file reader for not-found files.
    /// Plug for work universalization with file readers.
    /// </summary>
    internal class FileNotFound : IFileReader
    {
        public FileInfo File { get; set; }

        public List<string> Read()
        {
            List<string> list = new List<string>
            {
                "Произошла ошибка. Файл не найден."
            };
            return list;
        }
    }
}
