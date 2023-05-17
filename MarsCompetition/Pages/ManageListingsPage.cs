using MarsCompetition.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsCompetition.Pages
{
    public class ManageListingsPage
    {
        private readonly IWebDriver driver;

        public ManageListingsPage(IWebDriver driver) { this.driver = driver; }

        public void CheckSkillDetail()
        {
            //Get last skill detail
            //Navigate to the last page
            IWebElement goToLastpage = driver.FindElement(By.XPath("//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[2]/button[last()]"));
            goToLastpage.Click();
            Wait.WaitToBeClickable(driver, "XPath", "//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/tbody/tr[last()]/td[8]/div/button[1]/", 10);

            //Click eye icon button of last skill record
            IWebElement lastEyeButton = driver.FindElement(By.XPath("//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/tbody/tr[last()]/td[8]/div/button[1]"));
            lastEyeButton.Click();
            Wait.WaitToBeVisible(driver,"XPath", "//*[@id=\"service-detail-section\"]/div[2]/div/div[1]/div/div/div/div", 10);

        }

       // public string GetSkillDetail()
       // {

       // }

        public void EditShareSkill(string title, string description)
        {
            //Navigate to the first page
            IWebElement goToFirstpage = driver.FindElement(By.XPath("//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[2]/button[first()]"));

            //*[@id="listing-management-section"]/div[2]/div[1]/div[2]/button[2]
            goToFirstpage.Click();
            Wait.WaitToBeClickable(driver, "XPath", "//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/tbody/tr[last()]/td[8]/div/button[2]", 10);

            //Edit the last listing
            //Click edit button of last listing
            IWebElement lastEditButton = driver.FindElement(By.XPath("//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/tbody/tr[last()]/td[8]/div/button[2]"));
            lastEditButton.Click();
            Wait.WaitToBeVisible(driver, "XPath", "//*[@id=\"requiredField\"]", 10);

            //Edit the title of last listing
            Wait.WaitToBeClickable(driver,"XPath", "//*[@id=\"service-listing-section\"]/div[2]/div/form/div[1]/div/div[2]/div/div[1]/input", 10);
            IWebElement lastTitleToBeEdit = driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[1]/div/div[2]/div/div[1]/input"));
            lastTitleToBeEdit.Clear();
            lastTitleToBeEdit.SendKeys(title);

            //Edit the discription of last listing
            IWebElement lastDiscriptionTobeEdit = driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[2]/div/div[2]/div[1]/textarea"));
            lastDiscriptionTobeEdit.Clear();
            lastDiscriptionTobeEdit.SendKeys(description);

            //Click on save button to update the listing
            IWebElement updateButton = driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[11]/div/input[1]"));
            updateButton.Click();
            Wait.WaitToBeVisible(driver, "XPath", "//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/thead/tr/th[1]", 10);

        }
        
        public string[] GetUpdatedShareSkill()
        {
            //Navigate to the last page
            IWebElement goToLastpage = driver.FindElement(By.XPath("//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[2]/button[last()]"));
            goToLastpage.Click();
            Wait.WaitToBeClickable(driver, "XPath", "//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/tbody/tr[last()]/td[8]/div/button[2]", 10);

            //Check if the last listing title and description have been edited successfully
            IWebElement getEditedlistingTitle = driver.FindElement(By.XPath("//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/tbody/tr[last()]/td[3]"));
            IWebElement getEditedListingDescription = driver.FindElement(By.XPath("//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/tbody/tr[last()]/td[4]"));
            return new[] { getEditedlistingTitle.Text, getEditedListingDescription.Text };


        }

        public void DeleteShareSkill()
        {
            //Delete the first skill record
            //Navigate to the first page
            Wait.WaitToBeClickable(driver, "XPath", "//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[2]/button[last()]", 10);
            IWebElement goToFirstpage = driver.FindElement(By.XPath("//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[2]/button[2]"));
            goToFirstpage.Click();
            Wait.WaitToBeVisible(driver,"XPath", "//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/tbody/tr[last()]/td[3]", 10);

            //Delete the first record of share skill listings
            IWebElement titleOfFirstListing = driver.FindElement(By.XPath("//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/tbody/tr[1]/td[3]"));
            if (titleOfFirstListing.Text == "AdaZhang")
            {
                
                //Click on delete button of last record
                Wait.WaitToBeClickable(driver, "XPath", "//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/tbody/tr[1]/td[8]/div/button[3]", 10);
                IWebElement deleteButton = driver.FindElement(By.XPath("//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/tbody/tr[1]/td[8]/div/button[3]"));
                deleteButton.Click();
            }
            else
            {
                Assert.Fail("Record to be deleted not found.");
            }

            //Confirm to Delete and click on Ok button
            Wait.WaitToBeClickable(driver, "XPath", "/html/body/div[2]/div/div[3]/button[2]", 10);
            IWebElement confirmDeleteButton = driver.FindElement(By.XPath("/html/body/div[2]/div/div[3]/button[2]"));
            confirmDeleteButton.Click();
            Wait.WaitToBeVisible(driver,"CssSelector",".ns-box",10);
       
        }

        public string GetPopUpMessage()
        {
            IWebElement popupNotice = driver.FindElement(By.CssSelector(".ns-box"));
            return popupNotice.Text;
        }


    }
}
