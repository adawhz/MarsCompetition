using MarsCompetition.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsCompetition.Pages
{
    public class HomePage
    {
        private readonly IWebDriver driver;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }
        public void GoToShareSkillPage()
        {

            //Navigate to share skill page
            Wait.WaitToBeClickable(driver, "LinkText", "Share Skill", 5);
            shareSkillButton.Click();
            Wait.WaitToBeVisible(driver, "Id", "requiredField", 5);
        }
        public void GoToManageListingsPage()
        {
            //Navigate to manage listings page
            Wait.WaitToBeClickable(driver, "LinkText", "Manage Listings", 10);
            manageListings.Click();
            Wait.WaitToBeVisible(driver, "XPath", "//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/thead/tr/th[1]", 10);
        }
        private IWebElement shareSkillButton => driver.FindElement(By.LinkText("Share Skill"));
        private IWebElement manageListings => driver.FindElement(By.LinkText ("Manage Listings"));
    }
}
