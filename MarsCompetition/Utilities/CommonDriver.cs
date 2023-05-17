using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsCompetition.Utilities
{
    public class CommonDriver
    {
        public IWebDriver driver = new ChromeDriver("@Document:/MarsCompetition");
    }
}
