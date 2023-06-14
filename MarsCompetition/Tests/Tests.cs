using AventStack.ExtentReports;
using MarsCompetition.Excel;
using MarsCompetition.Pages;
using MarsCompetition.Report;
using MarsCompetition.Utilities;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsCompetition.Tests
{

    [TestFixture]
    [Parallelizable]
    public class Tests:CommonDriver
    {
       
        public static ICollection<ExcelTestCase> GetShareNewSkillTests()
        {
            return ExcelData.ReadShareSkilTestCases("ExcelDataFile/TestCases.xlsx", "ShareNewSkill");
           
        }

        [Test, Order(1), Description("Check if user is able to create new skill"),TestCaseSource(nameof(GetShareNewSkillTests))]
        public void TestCreateNewSkill(ExcelTestCase testCase)
        {

            test.Log(Status.Info,"Running test case " + testCase.TestCaseId);
            Console.WriteLine("Running test case " + testCase.TestCaseId);
            homePage.GoToShareSkillPage();

            var skill = (Skill)testCase.TestData;
            var result = shareSkillPage.CreateShareSkill(skill);
            Console.WriteLine("Result: " + result);
            if (result == testCase.ExpectedResult)
            {
                Console.WriteLine("Test case " + testCase.TestCaseId + " passed");
                CaptureAndLog(Status.Pass,"Test case " + testCase.TestCaseId + " passed, see screenshots below." );
            }
            else
            {
                Console.WriteLine("Test case " + testCase.TestCaseId + " failed");
                CaptureAndLog(Status.Fail,"Test case " + testCase.TestCaseId + " failed, see screenshots below.");
               
            }
            

            //Check if the new skill has been added
            if (result == "Success")
            {
                string[] addedSkill = shareSkillPage.GetFirstSkill();
                Assert.AreEqual(skill.Title, addedSkill[0], "Actual and expected skill record do not match.");
                Assert.AreEqual(skill.Category, addedSkill[1], "Actual and expected skill record do not match.");
                Console.WriteLine("Added skill title: " + addedSkill[0]);
                test.Log(Status.Info,"Added skill title: " + addedSkill[0]);
                Console.WriteLine("Added skill category: " + addedSkill[1]);
                test.Log(Status.Info,"Added skill category: " + addedSkill[1]);

            }
         

        }


        
        public static ICollection<ExcelTestCase> GetEditSkillTests()
        {
            return ExcelData.ReadEditkilTestCases("ExcelDataFile/TestCases.xlsx", "EditSkill");
            
        }
        [Test, Order(2), Description("Check if user is able to edit an existing record with valid data"), TestCaseSource(nameof(GetEditSkillTests))]
        public void TestEditSkill(ExcelTestCase testCase)
        {
            

            test.Log(Status.Info, "Running test case " + testCase.TestCaseId);
            Console.WriteLine("Running test case " + testCase.TestCaseId);
            homePage.GoToManageListingsPage();
            var editskill = (Skill)testCase.TestData;
            manageListingsPage.EditShareSkill(editskill);
            manageListingsPage.GetUpdatedShareSkill();

            //Chcek if the first skill has been updted successfully
            string[] updatedSkill = manageListingsPage.GetUpdatedShareSkill();
            test.Log(Status.Info, "Updated skill title: " + updatedSkill[0]);
            Console.WriteLine("Updated skill title: " + updatedSkill[0]);
            test.Log(Status.Info, "Updated skill description: " + updatedSkill[1]);
            Console.WriteLine("Updated skill description: " + updatedSkill[1]);
            Assert.AreEqual(editskill.Title, updatedSkill[0], "Actual and expected skill record do not match.");
            Assert.AreEqual(editskill.Description, updatedSkill[1], "Actual and expected skill record do not match.");
            
        }
        
        [Test, Order(3), Description("Check if user is able to view detail of an existing record ")]
        public void ViewSkill()
        {
            

            test.Log(Status.Info, "Running test case");

            homePage.GoToManageListingsPage();
            string[] firstSkillDetail = manageListingsPage.GetUpdatedShareSkill();
            manageListingsPage.ViewSkillDetail();

            //Check user can watch the skill's detail
            string[] viewedSkill = manageListingsPage.GetSkillDetail();
            Assert.AreEqual(firstSkillDetail[0], viewedSkill[0], "Actual and expected skill record do not match.");
            Assert.AreEqual(firstSkillDetail[1], viewedSkill[1], "Actual and expected skill record do not match.");
            Assert.AreEqual(firstSkillDetail[2], viewedSkill[2], "Actual and expected skill record do not match.");
           
        }
        [Test, Order(4), Description("Check if user is able to delete an existing record ")]
        public void DeleteSkill()
        {

            homePage.GoToManageListingsPage();
            manageListingsPage.DeleteShareSkill();
            manageListingsPage.GetPopUpMessage();

            //Check if the skill record has been deleted successfully
            string[] deletedSkill = manageListingsPage.GetUpdatedShareSkill();
            string checkPopUpMessage = manageListingsPage.GetPopUpMessage();
            Assert.AreEqual(checkPopUpMessage, deletedSkill[0] + " has been deleted", "Actual and expected skill record do not match.");
        }
    }
}
