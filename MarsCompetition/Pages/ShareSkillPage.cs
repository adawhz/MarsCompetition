using MarsCompetition.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsCompetition.Pages
{
    public class ShareSkillPage
    {
        private readonly IWebDriver driver;
        public ShareSkillPage(IWebDriver driver) { this.driver = driver; }
       private void SelectCategory(string category,string subcategory)
        {
            //Select category from category dropdown
            categoryDropdown.Click();
            //categoryOption.Click();
            var found = false;
            foreach (var option in categoryOptions)
            {
                if (option.Text == category) 
                { 
                    found = true;
                    option.Click();
                }
            }
            if (!found) { return; }

            //Select subcategory from subcategory dropdown
            subcategoryDropdown.Click();
            foreach (var option in subcategoryOptions)
            {
                if (option.Text == subcategory)
                    option.Click();
            }
        }
        private void SetServiceType(string serviceType)
        {
            if (serviceType == "Hourly basis service")
                hourlyServiceType.Click();
            else
                oneoffServiceType.Click();
        }
        private void SetLocationTyppe(string locationType)
        {
            if (locationType == "On-site")
                onsiteLocationType.Click();
            else
                onlineLocationType.Click();

        }
        private void SetAvailableDay(int id, string startTime, string endTime)
        {
            IWebElement selectDay = driver.FindElement(By.XPath($"//*[@id=\"service-listing-section\"]/div[2]/div/form/div[7]/div[2]/div/div[{id}]/div[1]/div/input"));
            selectDay.Click();
            IWebElement startTimeInput = driver.FindElement(By.XPath($"//*[@id=\"service-listing-section\"]/div[2]/div/form/div[7]/div[2]/div/div[{id}]/div[2]/input"));
            startTimeInput.SendKeys(startTime);
            IWebElement endTimeInput = driver.FindElement(By.XPath($"//*[@id=\"service-listing-section\"]/div[2]/div/form/div[7]/div[2]/div/div[{id}]/div[3]/input"));
            endTimeInput.SendKeys(endTime);
        }
        private void SetSkillTradeType(string skillTradeType,string[] skillExchange, string skillCredit)
        {
            if (skillTradeType == "Skill-exchange")
            {
                skillExchangeRadio.Click();
                //Identify skill-exchange textbox and input skill-exchange
                foreach (var skillTrade in skillExchange)
                {
                    
                    skillExchangeTextbox.SendKeys(skillTrade);
                    skillExchangeTextbox.SendKeys(Keys.Return);
                }
            }

            else
                
            {
                //Identify credit textbox and input credit
                skillCreditRadio.Click();
                skillCreditTextbox.SendKeys(skillCredit);
            }
               
        }
       
        private void SetActiveType(string activeType)
        {
            if (activeType =="Active")
                activeRadio.Click();
            else
                hiddenRadio.Click();    
        }
        public string CreateShareSkill(Skill skill)
        {
            Wait.WaitToBeVisible(driver, "XPath", "//*[@id=\"requiredField\"]", 10);
            //Add new title          
            titleTextbox.SendKeys(skill.Title);

            //Add new description           
            descriptionTextbox.SendKeys(skill.Description);

            //select category
            SelectCategory(skill.Category,skill.Subcategory);

            //Identify tags textbox and Input tags
            tagsTextbox.Click();
            foreach (var tag in  skill.Tags)
            {
                tagsTextbox.SendKeys(tag);
                tagsTextbox.SendKeys(Keys.Return);
            }

            //Choose service type from radio
            SetServiceType(skill.ServiceType);

            //Choose location type from radio
            SetLocationTyppe(skill.LocationType);

            //Choose start date from placeholder
            chooseStartDate.Click();
            chooseStartDate.SendKeys(skill.StartDate);

            //Choose end date from placeholder
            chooseEndDate.Click();
            chooseEndDate.SendKeys(skill.EndDate);

            //select available day 
            foreach (var day in skill.AvailableDays)
            {
                SetAvailableDay(day.Id, day.StartTime, day.EndTime);
            }

            //Choose skill-exchange from skill trade
            SetSkillTradeType(skill.SkillTrade,skill.SkillExchange,skill.Credit);

            //Upload work sample

            //Choose active type
            SetActiveType(skill.Active);

            //Identify save button and click
            saveButton.Click();
            try 
            { 
                Wait.WaitToBeVisible(driver, "XPath", "//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/thead/tr/th[3]", 10);
                return "Success";
            } 
            catch (Exception ex)
            {
                return "Failure";
            }
        }

        public String[] GetFirstSkill() 
        {
            //Check if the new share skill has been added successfully
                Wait.WaitToBeVisible(driver, "XPath", "//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/thead/tr/th[3]", 10);
                return new[] { firstSkillTitle.Text, firstShareSkillCategory.Text };
        }
        private IWebElement titleTextbox => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[1]/div/div[2]/div/div[1]/input"));
        private IWebElement descriptionTextbox => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[2]/div/div[2]/div[1]/textarea"));
        private IWebElement categoryDropdown => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[3]/div[2]/div/div/select"));
        private ICollection<IWebElement> categoryOptions => driver.FindElements(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[3]/div[2]/div/div/select/option"));
        private IWebElement subcategoryDropdown => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[3]/div[2]/div/div[2]/div[1]/select"));
        private ICollection<IWebElement> subcategoryOptions => driver.FindElements(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[3]/div[2]/div/div[2]/div[1]/select/option"));
        private IWebElement tagsTextbox => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[4]/div[2]/div/div/div/div/input"));
        private IWebElement hourlyServiceType => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[5]/div[2]/div[1]/div[1]/div/input"));
        private IWebElement oneoffServiceType => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[5]/div[2]/div[1]/div[2]/div/input"));
        private IWebElement onsiteLocationType => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[6]/div[2]/div/div[1]/div/input"));
        private IWebElement onlineLocationType => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[6]/div[2]/div/div[2]/div/input"));
        private IWebElement chooseStartDate => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[7]/div[2]/div/div[1]/div[2]/input"));
        private IWebElement chooseEndDate => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[7]/div[2]/div/div[1]/div[4]/input"));
        private IWebElement skillExchangeRadio => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[8]/div[2]/div/div[1]/div/input"));
        private IWebElement skillCreditRadio => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[8]/div[2]/div/div[2]/div/input"));
        private IWebElement skillExchangeTextbox => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[8]/div[4]/div/div/div/div/div/input"));
        private IWebElement skillCreditTextbox => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[8]/div[4]/div/div/input"));
        private IWebElement activeRadio => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[10]/div[2]/div/div[1]/div/input"));
        private IWebElement hiddenRadio => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[10]/div[2]/div/div[2]/div/input"));
        private IWebElement saveButton => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[11]/div/input[1]"));
        private IWebElement firstSkillTitle => driver.FindElement(By.XPath("//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/tbody/tr[1]/td[3]"));
        private IWebElement firstShareSkillCategory => driver.FindElement(By.XPath("//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/tbody/tr[1]/td[2]"));


    }
}
