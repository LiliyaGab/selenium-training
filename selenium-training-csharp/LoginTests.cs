using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace selenium_training_csharp
{
    [TestFixture]
    class LoginTests : TestBase
    {
     
        [Test]
        public void LoginTest_HW3()
        {
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();           
        }
        public static Random rnd = new Random();
        [Test]
        public void RegistrationTest_HW11()
        {
            driver.Url = "http://localhost/litecart/en/";
            var account = new Account();
            driver.FindElement(By.XPath("//*[text()='New customers click here']")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("firstname")));
            driver.FindElement(By.Name("firstname")).SendKeys(account.FirstName);
            driver.FindElement(By.Name("lastname")).SendKeys(account.LastName);
            driver.FindElement(By.Name("address1")).SendKeys(account.Address);
            driver.FindElement(By.Name("postcode")).SendKeys(account.Postcode);
            driver.FindElement(By.Name("city")).SendKeys(account.City);
            var country = driver.FindElement(By.Name("country_code"));
            (driver as IJavaScriptExecutor).ExecuteScript("arguments[0].value=\"US\"; arguments[0].dispatchEvent(new Event('change'))", country);
            var zone = driver.FindElement(By.CssSelector("select[name=zone_code]"));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("select[name=zone_code]")));
            (driver as IJavaScriptExecutor).ExecuteScript("arguments[0].value=\"AK\"; arguments[0].dispatchEvent(new Event('change'))", zone);     
            driver.FindElement(By.Name("email")).SendKeys(account.Email);
            driver.FindElement(By.Name("phone")).SendKeys(account.Phone);
            driver.FindElement(By.Name("password")).SendKeys(account.Password);
            driver.FindElement(By.Name("confirmed_password")).SendKeys(account.Password);
            driver.FindElement(By.Name("create_account")).Click();
            driver.FindElement(By.XPath("//*[text()='Logout']")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("email")));
            driver.FindElement(By.Name("email")).SendKeys(account.Email);
            driver.FindElement(By.Name("password")).SendKeys(account.Password);
            driver.FindElement(By.Name("login")).Click();
            driver.FindElement(By.XPath("//*[text()='Logout']")).Click();
        }

    }
}
