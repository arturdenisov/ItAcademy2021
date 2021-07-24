using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileReader_v2
{
    /// <summary>
    /// Base class of the inheritance chain of decorators.
    /// </summary>
    abstract class FileReaderDecorator : IFileReader
    {
        public FileInfo File { get; set; }

        protected IFileReader decoratee { get; set; }

        protected FileReaderDecorator(IFileReader _decoratee)
        {
            decoratee = _decoratee;
            File = _decoratee.File;
        }

        public abstract List<string> Read();
    }
}
