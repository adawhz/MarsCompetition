using MarsCompetition.Excel;
using MarsCompetition.Pages;
using MarsCompetition.Utilities;
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
       
        [Test, Order(1), Description("Check if user is able to create new skill")]
        public void ShareNewSkillTestSuite()
        {
            var newSkillTests = ExcelData.ReadShareSkilTestCases("ExcelDataFile/TestCases.xlsx", "ShareNewSkill");
            foreach (var testCase in newSkillTests)
            {
                TestCreateNewSkill(testCase);
            }
        }
      
        public void TestCreateNewSkill(ExcelTestCase testCase)
        {
           
            Console.WriteLine("Running test case " + testCase.TestCaseId);
            homePage.GoToShareSkillPage();

            var skill = (Skill)testCase.TestData;
            var result = shareSkillPage.CreateShareSkill(skill);
            Console.WriteLine("Result: " + result);
            if (result == testCase.ExpectedResult)
            {
                Console.WriteLine("Test case " + testCase.TestCaseId + " passed");
            }
            else
            {
                Console.WriteLine("Test case " + testCase.TestCaseId + " failed");
                return;
            }

            //Check if the new skill has been added
            if (result == "Success")
            {
                string[] addedSkill = shareSkillPage.GetFirstSkill();
                Assert.AreEqual(skill.Title, addedSkill[0], "Actual and expected skill record do not match.");
                Assert.AreEqual(skill.Category, addedSkill[1], "Actual and expected skill record do not match.");
                Console.WriteLine("Added skill title: " + addedSkill[0]);
                Console.WriteLine("Added skill category: " + addedSkill[1]);
            }

        }

        [Test, Order(2), Description("Check if user is able to edit an existing record with valid data")]
        public void EditSkillTestSuite()
        {
            var editSkillTests = ExcelData.ReadEditkilTestCases("ExcelDataFile/TestCases.xlsx", "EditSkill");
            foreach (var testCase in editSkillTests)
            {
                TestEditSkill(testCase);
            }
        }
        public void TestEditSkill(ExcelTestCase testCase)
        {
            Console.WriteLine("Running test case " + testCase.TestCaseId);
            homePage.GoToManageListingsPage();
            var editskill = (Skill)testCase.TestData;
            manageListingsPage.EditShareSkill(editskill);
            manageListingsPage.GetUpdatedShareSkill();

            //Chcek if the first skill has been updted successfully
            string[] updatedSkill = manageListingsPage.GetUpdatedShareSkill();
            Console.WriteLine("Updated skill title: " + updatedSkill[0]);
            Console.WriteLine("Updated skill description: " + updatedSkill[1]);
            Assert.AreEqual(editskill.Title, updatedSkill[0], "Actual and expected skill record do not match.");
            Assert.AreEqual(editskill.Description, updatedSkill[1], "Actual and expected skill record do not match.");
        }
        [Test, Order(3), Description("Check if user is able to delete an existing record ")]
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
        [Test, Order(4), Description("Check if user is able to view detail of an existing record ")]
        public void ViewSkill()
        {
           
            homePage.GoToManageListingsPage();
            string[] firstSkillDetail = manageListingsPage.GetUpdatedShareSkill();
            manageListingsPage.ViewSkillDetail();

            //Check user can watch the skill's detail
            string[] viewedSkill = manageListingsPage.GetSkillDetail();
            Assert.AreEqual(firstSkillDetail[0], viewedSkill[0], "Actual and expected skill record do not match.");
            Assert.AreEqual(firstSkillDetail[1], viewedSkill[1], "Actual and expected skill record do not match.");
            Assert.AreEqual(firstSkillDetail[2], viewedSkill[2], "Actual and expected skill record do not match.");
        }
    }
}
