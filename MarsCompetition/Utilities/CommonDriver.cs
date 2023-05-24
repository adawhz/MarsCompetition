using MarsCompetition.Pages;
using NUnit.Framework;
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
        
        public IWebDriver driver;
        public Loginpage loginPage;
        public HomePage homePage;
        public ShareSkillPage shareSkillPage;
        public ManageListingsPage manageListingsPage;

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

        }
        [TearDown]
        public void CloseTestRun()
        {
            driver.Close();
        }

    }
}
