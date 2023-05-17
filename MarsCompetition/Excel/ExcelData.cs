using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDataReader;
using System.Data;
using System.IO;
using OpenQA.Selenium;

namespace MarsCompetition.Excel
{
    public class ExcelData
    {
        public void readExcel()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            Console.WriteLine("Reading excel data file...");

            using (var stream = File.Open("ExcelDataFile/MarsCompetition.xlsx", FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        UseColumnDataType = true,
                        FilterSheet = (tableReader, sheetIndex) => true,
                        // Gets or sets a callback to obtain configuration options for a DataTable. 
                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                        {
                            // Gets or sets a value indicating whether to use a row from the 
                            // data as column names.
                            UseHeaderRow = true
                        }
                    });

                    var sheet1 = result.Tables[0];
                    foreach (DataRow row in sheet1.Rows)
                    {
                        //Console.WriteLine()
                    }
                }
            }
        }
    }
}
