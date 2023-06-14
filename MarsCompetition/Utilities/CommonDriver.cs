using AventStack.ExtentReports;
using MarsCompetition.Pages;
using MarsCompetition.Report;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace MarsCompetition.Utilities
{
    public class CommonDriver
       
    {    
        
        public IWebDriver driver;
        public Loginpage loginPage;
        public HomePage homePage;
        public ShareSkillPage shareSkillPage;
        public ManageListingsPage manageListingsPage;
        public ExtentReports extent = TestReport.GetInstance();
        public ExtentTest? test = null;

        //Page object initialization

        [SetUp]
        public void Setup()
        {
            this.driver = new ChromeDriver();
            this.loginPage = new Loginpage(driver);
            this.homePage = new HomePage(driver);
            this.shareSkillPage = new ShareSkillPage(driver);
            this.manageListingsPage = new ManageListingsPage(driver);
            loginPage.LoginAction();
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

        }
        [TearDown]

        public void AfterTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = TestContext.CurrentContext.Result.StackTrace;
            Status logstatus;
            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    CaptureAndLog(Status.Fail, "Fail, screenshots below");
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }
            test.Log(logstatus, "Test ended with " +logstatus + stacktrace);
            extent.Flush();
            driver.Close();

        }
        public void CaptureAndLog(Status status, string msg)
        {
            DateTime time = DateTime.Now;
            String fileName = "Screenshot_" + time.ToString("h_mm_ss") + ".png";
            String screenShotPath = Capture(fileName);
            test.AddScreenCaptureFromPath(screenShotPath);
            test.Log(status, msg);
        }
       
        public string Capture(String screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            var localPath = Path.Combine(TestReport.GetScreenShotPath(), screenShotName);
            screenshot.SaveAsFile(localPath, ScreenshotImageFormat.Png);
            string relPath = Path.GetRelativePath(TestReport.GetReportPath(), localPath);
            return relPath;
        }

    }
}
