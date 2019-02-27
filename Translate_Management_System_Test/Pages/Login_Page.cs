using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Translate_Management_System_Test.Pages
{
    class Login_Page
    {
        private IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;

        }

        public IWebElement txtUserName => _driver.FindElement(By.Name("email"));
        public IWebElement txtPassord => _driver.FindElement(By.Name("password"));
        public IWebElement btnLogin => _driver.FindElement(By.XPath("/html/body/div/div/div/form/div/div[3]/button"));

        public void PerformLogin(string userName, string password)
        {
            txtUserName.SendKeys(userName);
            txtPassord.SendKeys(password);
            btnLogin.Submit();
        }

        public void PerformLogout_and_control()
        {
            //Drop down aria-expanded = true
            _driver.FindElement(By.CssSelector("#app-navbar-collapse > ul.nav.navbar-nav.navbar-right > li > a")).Click();
            //Click Logout button
            _driver.FindElement(By.CssSelector("#app-navbar-collapse > ul.nav.navbar-nav.navbar-right > li > ul > li > a")).Click();

            if ("Login" == _driver.FindElement(By.XPath("/html/body/div/div/div/form/div/div[3]/button")).Text)
            {
                Console.WriteLine("Succesful Logout");
            }
        }

        public void Succesful_Login_Control()
        {
            var username_in_navbar = _driver.FindElement(By.CssSelector("#app-navbar-collapse > ul.nav.navbar-nav.navbar-right > li > a")).Text;
            if (username_in_navbar == "moderator")
            {
                Console.WriteLine("Moderator Successfully Login.");
            }
            else if (username_in_navbar == "translator")
            {
                Console.WriteLine("Translator Successfully Login.");
            }
        }

        public void Unsuccesful_Login_Control()
        {
            var error_message = _driver.FindElement(By.XPath("/html/body/div/div/div/form/div/div[2]/div[1]/div")).Text;
            if (error_message == "The selected E-Mail is invalid.")
            {
                Console.WriteLine("The selected E-Mail is invalid.");
            }
            else if (error_message == "These credentials do not match our records.")
            {
                Console.WriteLine("Wrong password");
            }
        }

    }
}
