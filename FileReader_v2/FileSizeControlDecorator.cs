using System;
using System.Collections.Generic;
using System.Text;

namespace FileReader_v2
{
    /// <summary>
    /// Decorator-class which checks file size.
    /// </summary>
    internal class FileSizeControlDecorator : FileReaderDecorator
    {
        internal FileSizeControlDecorator(IFileReader _decoratee) : base(_decoratee) { }

        public override List<string> Read()
        {
            List<string> list = new List<string>();

            if (IsFileOversized())
            {
                list.Add("Размер файла превышает допустимый (1 мб). Закачка остановлена, выберите другой файл.");
                return list;
            }
            else
            {
                list.Add("Проверка: размер файла не превышает допустимый (1 мб).");
                list.AddRange(decoratee.Read());
                return list;
            }
        }

        /// <summary>
        /// Methods checks if file size is less than 1mb (1024b^2)
        /// </summary>
        /// <returns>true - file oversized, false - not oversized</returns>
        private bool IsFileOversized()
        {
            if (File.Length > 1024 * 1024)
                return true;
            else
                return false;
        }
    }
}
