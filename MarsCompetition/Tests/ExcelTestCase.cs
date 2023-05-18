using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsCompetition.Tests
{
    public class ExcelTestCase
    {
        public string TestCaseId { get; set; }
        public string TestCaseName { get; set; }
        public object TestData { get; set; }
        public string ExpectedResult { get; set; }
    }
}
