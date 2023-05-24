using MarsCompetition.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsCompetition.Pages
{
    
    public class Loginpage
    {
        readonly IWebDriver driver;
        public Loginpage(IWebDriver driver) { this.driver = driver;}

        public void LoginAction() {
            //Open chrome browser
            driver.Manage().Window.Maximize();
            //Launch Mars portal
            driver.Navigate().GoToUrl("http://localhost:5000");
            //Sign in Mars portal
            signButton.Click();
            //Identify email textbox and enter email
            Wait.WaitToBeVisible(driver, "XPath", "/html/body/div[2]/div/div/div[1]/div/div[1]/input",5);
            emailTextbox.SendKeys("ada@example.com");
            //Identify password textbox and enter password
            passwordTextbox.SendKeys("123456");
            //Identify remember me checkbox and click
            rememberMeCheckbox.Click();
            //Identify login button and click
            loginButton.Click();
            Wait.WaitToBeClickable(driver, "XPath", "//*[@id=\"account-profile-section\"]/div/section[1]/div/div[2]/a", 10);
        }

        IWebElement signButton => driver.FindElement(By.XPath("//*[@id=\"home\"]/div/div/div[1]/div/a"));
        IWebElement emailTextbox => driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div[1]/input"));
        IWebElement passwordTextbox => driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div[2]/input"));
        IWebElement rememberMeCheckbox => driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div[3]/div/input"));
        IWebElement loginButton => driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div[4]/button"));

    }
}
