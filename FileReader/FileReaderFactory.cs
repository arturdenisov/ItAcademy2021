using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace FileReader
{
    /// <summary>
    /// Static class for correct creation of the correct IFileReader type.
    /// </summary>
    public static class FileReaderFactory
    {
        /// <summary>
        /// Select a definite type of IFileReader implementation on the base of file extension analysis.
        /// </summary>
        /// <param name="filePath">Full file path</param>
        /// <returns>
        /// Correct implementation of IFileReader
        /// </returns>
        public static IFileReader SelectIFileReader(string filePath)
        {
            FileInfo file;
            if (!File.Exists(filePath))
                return new FileNotFound();
            else
                file = new FileInfo(filePath);

            return file.Extension switch
            {
                ".txt"  => new TxtFileReader(file),
                ".xlsx" => new XlsxFileReader(file),
                _       => new DefaultFileReader(file),
            };
        }
    }
}
