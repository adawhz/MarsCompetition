using System.Data;
using System.Data.Common;
using System.Threading;
using ExcelDataReader;
using MarsCompetition.Pages;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using excel = Microsoft.Office.Interop.Excel;


namespace MarsCompetition
{ 
    class Program
    {
        public static void Main()
        {
            ChromeDriver driver = new ChromeDriver();
            Loginpage loginpage = new Loginpage(driver);
            HomePage homePage = new HomePage(driver);
            ShareSkillPage shareSkillPage = new ShareSkillPage(driver);
            ManageListingsPage manageListingsPage = new ManageListingsPage(driver); 

            loginpage.LoginAction();
            homePage.GoToShareSkillPage();
            //homePage.GoToManageListingsPage();
            Skill newSkill = new()
            {
                Title = "Hello",
                Description = "over",
                Category = "Digital Marketing",
                Subcategory = "Video Marketing",
                Tags = new[] { "write", "sell" },
                ServiceType = "One-off service",
                LocationType = "On-site",
                StartDate = "2023-07-20",
                EndDate = "2024-09-10",
                AvailableDays = new[] {
                    new AvailableDay(2, "10:00", "15:00"),
                    new AvailableDay(4, "12:00", "15:00"),
                    new AvailableDay(6, "9:00", "17:00"),
                },
                SkillTrade = "Credit",
                SkillExchange = new[] { "dancing", "singing" }, 
                Credit = "9",
                Active = "Active",
               
            };

            shareSkillPage.CreateShareSkill(newSkill);
           //manageListingsPage.EditShareSkill("Ada", "I like playing guitar.");
          // manageListingsPage.GetUpdatedShareSkill();
          // manageListingsPage.DeleteShareSkill();
          // manageListingsPage.GetPopUpMessage();
          // manageListingsPage.CheckSkillDetail();
           //manageListingsPage.GetSkillDetail();

            //Check if the new skill has been added
            string[] addedSkill = shareSkillPage.GetFirstSkill();
            Console.WriteLine("Added skill title: " + addedSkill[0]);
            Console.WriteLine("Added skill category: " + addedSkill[1]);
            
            //Chcek if the skill has been updted successfully
            // string[] updatedSkill = manageListingsPage.GetUpdatedShareSkill();
            // Console.WriteLine("Updated skill title: " + updatedSkill[0]);
            // Console.WriteLine("Updated skill description: " + updatedSkill[1]);


            //Check if the skill record has been deleted successfully
            //string[] deletedSkill = shareSkillPage.GetLastSkill();
            //string checkPopUpMessage = manageListingsPage.GetPopUpMessage();
            //Assert.AreEqual(checkPopUpMessage, deletedSkill[0] + " has been deleted", "Actual and expected skill record do not match.");

            //Check user can watch the skill's detail

        }

    }

}