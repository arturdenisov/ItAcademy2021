using System;
using System.Collections.Generic;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileReader_v2
{
    /// <summary>
    /// Decorator-class which checks file accessibility.
    /// </summary>
    internal class FileAccessControlDecorator : FileReaderDecorator
    {
        internal FileAccessControlDecorator(IFileReader _decoratee) : base(_decoratee) { }

        public override List<string> Read()
        {
            List<string> list = new List<string>();

            if (IsFileLocked())
            {
                list.Add("Доступ к файлу запрещен. Закачка остановлена.");
                return list;
            }
            else
            {
                list.Add("Проверка: доступ к файлу разрешен.");
                list.AddRange(decoratee.Read());
                return list;
            }
        }

        /// <summary>
        /// Method checks is file locke by other thread.
        /// </summary>
        /// <returns>true - file is locked; false - is unlocked</returns>
        private bool IsFileLocked()
        {
            // Try to open the file with the indicated access.
            try
            {
                FileStream fs =
                    new FileStream(File.FullName, FileMode.Open, FileAccess.Read);
                fs.Close();
                return false;
            }
            catch (IOException)
            {
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
