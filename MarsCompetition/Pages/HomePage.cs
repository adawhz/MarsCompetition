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
        //public void GoToProfilePage()
        //{
        //    //Navigate to profile page
           
        //    Actions actions = new Actions(driver);
        //    actions.MoveToElement(hiButton).Perform();

        //    //Wait.WaitToBeClickable(driver, "XPath", "//*[@id=\"account-profile-section\"]/div/div[1]/div[2]/div/span/div/a[1]", 10);
        //    profileButton.Click();
        //    Wait.WaitToBeClickable(driver, "XPath", "//*[@id=\"account-profile-section\"]/div/section[1]/div/a[3]", 10);

        //}
        public void GoToShareSkillPage()
        {

            //Navigate to share skill page
            Wait.WaitToBeClickable(driver, "XPath", "//*[@id=\"account-profile-section\"]/div/section[1]/div/div[2]/a", 5);
            shareSkillButton.Click();
            Wait.WaitToBeVisible(driver, "Id", "requiredField", 5);
        }
        public void GoToManageListingsPage()
        {
            //Navigate to manage listings page
            Wait.WaitToBeClickable(driver, "XPath", "//*[@id=\"account-profile-section\"]/div/section[1]/div/a[3]", 10);
            manageListings.Click();
            Wait.WaitToBeVisible(driver, "XPath", "//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/thead/tr/th[1]", 10);
        }
        private IWebElement shareSkillButton => driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[1]/div/div[2]/a"));
        private IWebElement manageListings => driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[1]/div/a[3]"));
        private IWebElement profileButton => driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/div[1]/div[2]/div/span/div/a[1]"));
        
        private IWebElement hiButton => driver.FindElement(By.XPath("/html/body/div/div/div/div[1]/div[2]/div/span"));

    }
}
