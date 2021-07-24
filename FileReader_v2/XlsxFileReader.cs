using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ClosedXML.Excel;

namespace FileReader_v2
{
    internal class XlsxFileReader : IFileReader
    {
        public FileInfo File { get; set; }

        internal XlsxFileReader(FileInfo file)
        {
            File = file;
        }
        public List<string> Read()
        {
            List<string> fileContent = new List<string>();

            using (var excelWorkbook = new XLWorkbook(File.FullName))
            {
                //Read just first worksheet
                var nonEmptyDataRows = excelWorkbook.Worksheet(1).RowsUsed();

                //Traverse rows
                foreach (var dataRow in nonEmptyDataRows)
                {
                    //Traverse all cells (columns) in rows
                    foreach (var cell in dataRow.Cells())
                    {
                        fileContent.Add(cell.Value.ToString());
                    }
                }
            }
            return fileContent;
        }
    }
}
