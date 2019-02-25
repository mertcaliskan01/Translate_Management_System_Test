using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium.Support.UI;

namespace Translate_Management_System_Test.Pages
{
    class Web_Front_End_Page
    {
        private IWebDriver _driver;

        public Web_Front_End_Page(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement txtSearch => _driver.FindElement(By.Name("query"));
        public IWebElement btnSearch => _driver.FindElement(By.XPath("/html/body/div/div[2]/div/form/div/div/div/div[1]/div/div/div"));
        public IWebElement btnSave => _driver.FindElement(By.Name("translate"));
        public IWebElement btnCancel => _driver.FindElement(By.XPath("//*[@id='translate0']/td/form/div/div[2]/div/div[1]/button"));
        public IWebElement btnAction => _driver.FindElement(By.XPath("//*[@id='translationTable']/tbody/tr[1]/td[3]/button"));
        public IWebElement txtWrite => _driver.FindElement(By.Name("translation"));

        public void Search(string word, string status)
        {
            txtSearch.Clear();
            txtSearch.SendKeys(word);
            new SelectElement(_driver.FindElement(By.Id("status"))).SelectByValue(status);
            btnSearch.Click();
        }

        public void Change_Translation(string word, string save_or_cancel)
        {
            btnAction.Click();
            txtWrite.Clear();
            txtWrite.SendKeys(word);
            if (save_or_cancel == "Cancel")
            {
                btnCancel.Click();
            }
            if (save_or_cancel == "Save")
            {
                btnSave.Submit();
            }
        }

        public void Approve_or_Disaprove(string yes_or_no, string approve_disapprove_Cancel)
        {
            btnAction.Click();

            if (yes_or_no == "Yes")
            {
                _driver.FindElement(By.XPath("//*[@id='translate0']/td/div/div[1]/div/div[3]/button[1]")).Click();

                if (approve_disapprove_Cancel == "Approve")
                {
                    _driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[3]/button[2]")).Click();
                }
                else if (approve_disapprove_Cancel == "Cancel")
                {
                    _driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[3]/button[1]")).Click();
                }
                else
                {
                    Console.WriteLine("You cannot press the Disapprove button after pressing the no button.");
                }
            }
            else if (yes_or_no == "No")
            {
                _driver.FindElement(By.XPath("//*[@id='translate0']/td/div/div[1]/div/div[3]/button[2]")).Click();

                if (approve_disapprove_Cancel == "Disapprove")
                {
                    _driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[3]/button[2]")).Click();
                }
                else if (approve_disapprove_Cancel == "Cancel")
                {
                    _driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[3]/button[1]")).Click();
                }
                else
                {
                    Console.WriteLine("You cannot press the approve button after pressing the no button.");
                }
            }
        }

        public void Status_edited_control(string search_word)
        {
            Search(search_word, "edited");
            btnAction.Click();

            var new_word = _driver.FindElement(By.XPath("//*[@id='translate0']/td/div/div[1]/div/div[1]/p"));
            var old_word = _driver.FindElement(By.XPath("//*[@id='translate0']/td/div/div[2]/div/div[1]/p"));
            if (new_word == old_word)
            {
                Console.WriteLine("Not Changed");
            }
            else
            {
                Console.WriteLine("Word changed but not approved");
            }
        }

        public void Status_approved_control(string search_word, string writed_word, string Before_changed)
        {
            Console.WriteLine("Before changed :" + Before_changed);
            Console.WriteLine("Writed word :" + writed_word);
            Search(search_word, "approved");
            btnAction.Click();
            var final = txtWrite.Text;
            Console.WriteLine("After writing :" + final);

            if (Before_changed == final)
            {
                Console.WriteLine("Not Changed");
            }
            else
            {
                Console.WriteLine("Changed");
            }
        }

        public string Before_changed(string search_word)
        {
            Search(search_word, "approved");
            btnAction.Click();
            return _driver.FindElement(By.Name("translation")).Text;
        }

        public void GoTranslation()
        {
            //Welcome  Page Press Start
            _driver.FindElement(By.XPath("/html/body/div/div[2]/p[5]/a[1]")).Click();
            //Select Front-End
            _driver.FindElement(By.XPath("/html/body/div/div[2]/div/div/div[2]/a[3]")).Click();
        }
    }
}