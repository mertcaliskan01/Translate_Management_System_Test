using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Reflection;
using System.Threading;
using Translate_Management_System_Test.Pages;

namespace Tests
{
    public class Tests
    {
        IWebDriver driver = new ChromeDriver(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

        [SetUp]
        public void Setup()
        {
            driver.Navigate().GoToUrl("https://staging-translate.mynextmatch.com/auth/login");
        }

        [Test]
        public void Success_Moderator_Login()
        {
            LoginPage loginPage = new LoginPage(driver);
            loginPage.PerformLogin("moderator@mynextmatch.com", "Mert123?.com");
            loginPage.Succesful_Login_Control();
        }

        [Test]
        public void Success_Translator_Login()
        {
            LoginPage loginPage = new LoginPage(driver);
            loginPage.PerformLogin("translator@mynextmatch.com", "Mert123?.com");
            loginPage.Succesful_Login_Control();
        }

        [Test]
        public void TryLogin_With_Wrong_Email()
        {
            LoginPage loginPage = new LoginPage(driver);
            loginPage.PerformLogin("wrong@mynextmatch.com", "Mert123?.com");
            loginPage.Unsuccesful_Login_Control();
        }

        [Test]
        public void TryLogin_With_Wrong_Password()
        {
            LoginPage loginPage = new LoginPage(driver);
            loginPage.PerformLogin("translator@mynextmatch.com", "Mert123?.asdasd");
            loginPage.Unsuccesful_Login_Control();
        }

        [Test]
        public void Success_Translator_Logout()
        {
            LoginPage loginPage = new LoginPage(driver);
            loginPage.PerformLogin("translator@mynextmatch.com", "Mert123?.com");
            loginPage.PerformLogout_and_control();
        }

        [Test]
        public void Success_Translator_CancelSaving_Sentence()
        {
            LoginPage loginPage = new LoginPage(driver);
            loginPage.PerformLogin("translator@mynextmatch.com", "Mert123?.com");
            Web_Front_End_Page web_Front_End_Page = new Web_Front_End_Page(driver);
            web_Front_End_Page.GoTranslation();

            web_Front_End_Page.Search("Welcome", "approved");
            var before_change = web_Front_End_Page.Before_changed("Welcome");

            web_Front_End_Page.Search("Welcome", "approved");
            web_Front_End_Page.Change_Translation("dismert", "Cancel");

            web_Front_End_Page.Search("Welcome", "approved");
            var after_change = web_Front_End_Page.Before_changed("Welcome");

            if (before_change == after_change)
            {
                Console.WriteLine("word is not changed");
            }
            else
            {
                Console.WriteLine("word is changed");
            }
        }

        [Test]
        public void Success_Translator_Save_Sentence()
        {
            LoginPage loginPage = new LoginPage(driver);
            loginPage.PerformLogin("translator@mynextmatch.com", "Mert123?.com");
            Web_Front_End_Page web_Front_End_Page = new Web_Front_End_Page(driver);
            web_Front_End_Page.GoTranslation();

            web_Front_End_Page.Search("Welcome", "approved");
            web_Front_End_Page.Change_Translation("dismert", "Save");

            web_Front_End_Page.Status_edited_control("Welcome");
        }

        [Test]
        public void Success_Moderator_CancelApproving_Sentence()
        {
            LoginPage loginPage = new LoginPage(driver);
            loginPage.PerformLogin("moderator@mynextmatch.com", "Mert123?.com");
            Web_Front_End_Page web_Front_End_Page = new Web_Front_End_Page(driver);
            web_Front_End_Page.GoTranslation();
            web_Front_End_Page.Search("Welcome", "approved");
            web_Front_End_Page.Change_Translation("dismert", "Save");
            web_Front_End_Page.Search("Welcome", "edited");
            web_Front_End_Page.Approve_or_Disaprove("Yes", "Cancel");
            web_Front_End_Page.Status_edited_control("Welcome");
        }

        [Test]
        public void Success_Moderator_Approve_sentence()
        {
            LoginPage loginPage = new LoginPage(driver);
            loginPage.PerformLogin("moderator@mynextmatch.com", "Mert123?.com");

            Web_Front_End_Page web_Front_End_Page = new Web_Front_End_Page(driver);
            web_Front_End_Page.GoTranslation();
            web_Front_End_Page.Search("Welcome", "approved");
            var before_change = web_Front_End_Page.Before_changed("Welcome");

            web_Front_End_Page.Search("Welcome", "approved");
            web_Front_End_Page.Change_Translation("Hoþgeldinizzz", "Save");
            web_Front_End_Page.Search("Welcome", "edited");
            web_Front_End_Page.Approve_or_Disaprove("Yes", "Approve");
            Thread.Sleep(1000);
            web_Front_End_Page.Status_approved_control("Welcome", "Hoþgeldinizzz", before_change);
        }

        [Test]
        public void Success_Moderator_Disapprove_Sentence()
        {
            LoginPage loginPage = new LoginPage(driver);
            loginPage.PerformLogin("moderator@mynextmatch.com", "Mert123?.com");
            Web_Front_End_Page web_Front_End_Page = new Web_Front_End_Page(driver);
            web_Front_End_Page.GoTranslation();

            web_Front_End_Page.Search("Welcome", "approved");
            var before_change = web_Front_End_Page.Before_changed("Welcome");

            web_Front_End_Page.Search("Welcome", "approved");
            web_Front_End_Page.Change_Translation("hoþgel", "Save");
            web_Front_End_Page.Search("Welcome", "edited");
            web_Front_End_Page.Approve_or_Disaprove("No", "Disapprove");
            Thread.Sleep(1000);
            web_Front_End_Page.Status_approved_control("Welcome", "hoþgel", before_change);
        }

        [Test]
        public void Success_Tanslator_EditApproved_Sentence()
        {
            LoginPage loginPage = new LoginPage(driver);
            loginPage.PerformLogin("translator@mynextmatch.com", "Mert123?.com");
            Web_Front_End_Page web_Front_End_Page = new Web_Front_End_Page(driver);
            web_Front_End_Page.GoTranslation();

            web_Front_End_Page.Search("Welcome", "approved");
            var before_change = web_Front_End_Page.Before_changed("Welcome");

            web_Front_End_Page.Search("Welcome", "approved");
            web_Front_End_Page.Change_Translation("Hoþ", "");

            web_Front_End_Page.Search("Welcome", "approved");
            var after_change = web_Front_End_Page.Before_changed("Welcome");

            if (before_change == after_change)
            {
                Console.WriteLine("word is not changed");
            }
            else
            {
                Console.WriteLine("word is changed");
            }
            Console.WriteLine("\n not expected to change.");
        }

        [Test]
        public void Success_Moderator_ApproveEdited_Sentence()
        {
            LoginPage loginPage = new LoginPage(driver);
            loginPage.PerformLogin("moderator@mynextmatch.com", "Mert123?.com");
            Web_Front_End_Page web_Front_End_Page = new Web_Front_End_Page(driver);
            web_Front_End_Page.GoTranslation();

            web_Front_End_Page.Search("Welcome", "approved");
            var before_change = web_Front_End_Page.Before_changed("Welcome");

            web_Front_End_Page.Search("Welcome", "approved");
            web_Front_End_Page.Change_Translation("Hoþgeldinizz", "Save");
            web_Front_End_Page.Search("Welcome", "edited");
            web_Front_End_Page.Approve_or_Disaprove("Yes", "Approve");
            Thread.Sleep(1000);
            web_Front_End_Page.Status_approved_control("Welcome", "Hoþgeldinizz", before_change);
        }


        [TearDown]
        public void Close()
        {
            driver.Close();
        }
    }
}