using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Reflection;

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

        [TearDown]
        public void Close()
        {
            driver.Close();
        }
    }
}