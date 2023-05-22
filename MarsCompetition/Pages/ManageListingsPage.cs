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

        public void EditShareSkill(Skill skill)
        {



            //Edit the first listing
            //Click edit button of first listing
            Wait.WaitToBeClickable(driver, "XPath", "//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/tbody/tr[1]/td[8]/div/button[2]", 10);
            firstEditButton.Click();
            Wait.WaitToBeVisible(driver, "XPath", "//*[@id=\"requiredField\"]", 10);

            //Edit the title
            Wait.WaitToBeClickable(driver, "XPath", "//*[@id=\"service-listing-section\"]/div[2]/div/form/div[1]/div/div[2]/div/div[1]/input", 10);
            titleTextbox.Clear();
            titleTextbox.SendKeys(skill.Title);

            //Edit the discription 
            Wait.WaitToBeClickable(driver, "XPath", "//*[@id=\"service-listing-section\"]/div[2]/div/form/div[2]/div/div[2]/div[1]/textarea", 10);
            descriptionTextbox.Clear();
            descriptionTextbox.SendKeys(skill.Description);

            //Click on save button to update the listing
            IWebElement updateButton = driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[11]/div/input[1]"));
            updateButton.Click();
            Wait.WaitToBeVisible(driver, "XPath", "//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/thead/tr/th[1]", 10);

        }

        public string[] GetUpdatedShareSkill()
        {
            //Check if the first listing title and description have been edited successfully
            return new[] { firstSkillTitle.Text, firstSkillDescription.Text };
        }


        public void CheckSkillDetail()
        {
            //Get first skill detail 
            Wait.WaitToBeClickable(driver, "XPath", "//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/tbody/tr[1]/td[8]/div/button[1]", 10);

            //Click eye icon button of first skill record
            IWebElement firstEyeButton = driver.FindElement(By.XPath("//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/tbody/tr[last()]/td[8]/div/button[1]"));
            firstEyeButton.Click();
            Wait.WaitToBeVisible(driver,"XPath", "//*[@id=\"service-detail-section\"]/div[2]/div/div[1]/div/div/div/div", 10);
        }

        public string[] GetSkillDetail()
        {
            IWebElement getFirstSkillTitle = driver.FindElement(By.XPath("//*[@id=\"service-detail-section\"]/div[2]/div/div[2]/div[1]/div[1]/div[2]/h1/span"));
            IWebElement getFirstSkillDescription = driver.FindElement(By.XPath("//*[@id=\"service-detail-section\"]/div[2]/div/div[2]/div[1]/div[1]/div[2]/div[2]/div/div/div[1]/div/div/div/div[2]"));
            IWebElement getFirstSkillCategory = driver.FindElement(By.XPath("//*[@id=\"service-detail-section\"]/div[2]/div/div[2]/div[1]/div[1]/div[2]/div[2]/div/div/div[2]/div/div[1]/div/div[2]"));
            return new[] { getFirstSkillTitle.Text, getFirstSkillDescription.Text, getFirstSkillCategory.Text };


        }

       

        public void DeleteShareSkill()
        {
            //Delete the first skill record
            //Navigate to the first page
            Wait.WaitToBeClickable(driver, "XPath", "//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[2]/button[last-1()]", 10);
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
        private IWebElement firstEditButton => driver.FindElement(By.XPath("//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/tbody/tr[1]/td[8]/div/button[2]"));
        private IWebElement titleTextbox => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[1]/div/div[2]/div/div[1]/input"));
        private IWebElement descriptionTextbox => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[2]/div/div[2]/div[1]/textarea"));
        private IWebElement firstSkillTitle => driver.FindElement(By.XPath("//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/tbody/tr[1]/td[3]"));
        private IWebElement firstSkillDescription => driver.FindElement(By.XPath("//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/tbody/tr[1]/td[4]"));



    }
}
