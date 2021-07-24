using System;
using System.Collections.Generic;
using System.IO;

namespace FileReader_v2
{
    /// <summary>
    /// Reader abstraction for work with a file of any extension.
    /// </summary>
    public interface IFileReader
    {
        /// <summary>
        /// Reading file
        /// </summary>
        public FileInfo File { get; set; }

        /// <summary>
        /// Read the file
        /// </summary>
        /// <returns>
        /// String-list of  the file content
        /// </returns>
        public List<string> Read();
    }
}
