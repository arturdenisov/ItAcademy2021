using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace FileReader
{
    internal class TxtFileReader : IFileReader
    {
        public FileInfo File { get; set; }

        internal TxtFileReader(FileInfo file)
        {
            File = file;
        }

        public List<string> Read()
        {
            return System.IO.File.ReadAllLines(File.FullName).ToList();
        }
    }
}
