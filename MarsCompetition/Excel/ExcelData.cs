using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDataReader;
using System.Data;
using System.IO;
using OpenQA.Selenium;
using MarsCompetition.Tests;
using MarsCompetition.Pages;

namespace MarsCompetition.Excel
{
    public class ExcelData
    {
        static ExcelData()
        { 
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
        public static DataTable? ReadExcelDataset(string filepath,string sheetname)
        {   
            
            Console.WriteLine("Reading excel data file...");

            using (var stream = File.Open(filepath, FileMode.Open, FileAccess.Read))
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

                    return result.Tables[sheetname];
                    
                }
            }
        }

        private static string[] availableDayColumns = new[] { "SunTime", "MonTime", "TueTime", "WedTime", "ThurTime", "FriTime", "SatTime" };
        private static string GetCellValue(DataTable dataSet,int row,string col) 
        {
            var value = dataSet.Rows[row][col];
            if (value == DBNull.Value || value == null)
            {
                return "";
            }
            else if (value is DateTime time)
            {
                return time.ToString("yyyy-MM-dd");
            }
            else
            {
                return value.ToString();
            }
        }
        public static ICollection<ExcelTestCase> ReadShareSkilTestCases(string filepath, string sheetname)
        {

            var dataSet = ReadExcelDataset(filepath,sheetname);
            var tests = new List<ExcelTestCase>();
            for (int i = 0; i < dataSet.Rows.Count; i++)
            {
                var availableDays = new List<AvailableDay>();
                for (int j = 0; j < availableDayColumns.Length;j++) 
                {
                    var day = availableDayColumns[j];
                    var time = GetCellValue(dataSet, i, day);
                    if (time != "")
                    {
                        var hours = time.Split('-');
                        availableDays.Add(new AvailableDay(j + 2, hours[0], hours[1]));
                    }
                    
                }

                Skill newSkill = new()
                {
                    Title = GetCellValue(dataSet, i, "Title"),
                    Description = GetCellValue(dataSet, i, "Description"),
                    Category = GetCellValue(dataSet, i, "Category"),
                    Subcategory = GetCellValue(dataSet, i, "Subcategory"),
                    Tags = GetCellValue(dataSet, i, "Tags").Split(','),
                    ServiceType = GetCellValue(dataSet, i, "ServiceType"),
                    LocationType = GetCellValue(dataSet, i, "LocationType"),
                    StartDate = GetCellValue(dataSet, i, "StartDate"),
                    EndDate = GetCellValue(dataSet, i, "EndDate"),
                    AvailableDays = availableDays.ToArray(),
                    SkillTrade = GetCellValue(dataSet, i, "SkillTrade"),
                    SkillExchange =GetCellValue(dataSet, i, "SkillExchange").Split(","),
                    Credit = GetCellValue(dataSet, i, "Credit"),
                    Active = GetCellValue(dataSet, i, "Active"),

                };
                var testCaseId = GetCellValue(dataSet, i, "Test Case ID");
                var expectedResult = GetCellValue(dataSet, i, "ExpectedResult");
                ExcelTestCase testCase = new ExcelTestCase()
                {
                    TestCaseId = testCaseId,
                    ExpectedResult = expectedResult,
                    TestData = newSkill,

                };  
                tests.Add(testCase);
            }
            return tests;
        }

        public static ICollection<ExcelTestCase> ReadEditkilTestCases(string filepath, string sheetname)
        {

            var dataSet = ReadExcelDataset(filepath, sheetname);
            var tests = new List<ExcelTestCase>();
            for (int i = 0; i < dataSet.Rows.Count; i++)
            {
                

                Skill newSkill = new()
                {
                    Title = GetCellValue(dataSet, i, "Title"),
                    Description = GetCellValue(dataSet, i, "Description"),
                };
                var testCaseId = GetCellValue(dataSet, i, "Test Case ID");
                var expectedResult = GetCellValue(dataSet, i, "ExpectedResult");
                ExcelTestCase testCase = new ExcelTestCase()
                {
                    TestCaseId = testCaseId,
                    ExpectedResult = expectedResult,
                    TestData = newSkill,

                };
                tests.Add(testCase);
            }
            return tests;
        }
    }
}
