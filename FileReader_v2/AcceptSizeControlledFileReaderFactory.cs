using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace FileReader_v2
{
    public static class AcceptSizeControlledFileReaderFactory
    {
        public static IFileReader SelectIFileReader(string filePath)
        {
            FileInfo file;
            if (!File.Exists(filePath))
                return new FileNotFound();
            else
                file = new FileInfo(filePath);

            IFileReader reader;
            switch (file.Extension)
            {
                case ".txt": 
                    reader = new TxtFileReader(file); break;
                case ".xlsx":
                    reader = new XlsxFileReader(file); break;
                default:
                    reader = new DefaultFileReader(file); break;
            }

            return
                new FileAccessControlDecorator(
                    new FileSizeControlDecorator(
                        reader));
        }
    }
}
