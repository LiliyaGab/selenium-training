using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace selenium_training_csharp
{
    [TestFixture]
    public class AdminTests : TestBase
    {
        [Test]
        public void MenuItemsTest()
        {
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("sidebar")));
            IsElementPresent(By.Id("sidebar"));
            var menuItemLocator = By.XPath("//ul[@id='box-apps-menu']/li/a/span[@class='name']");
            var subItemLocator = By.CssSelector("ul.docs span.name");
            var headerLocator = By.CssSelector("h1");
            var menuItems = driver.FindElements(menuItemLocator);
            for (int i = 0; i < menuItems.Count; i++)
            {
                menuItems[i].Click();
                var subItems = driver.FindElements(subItemLocator);
                if (subItems.Count == 0)
                {
                    Assert.True(IsElementPresent(headerLocator));
                }                   
                for (int j = 0; j < subItems.Count; j++)
                {
                    subItems[j].Click();
                    Thread.Sleep(500);
                    Assert.True(IsElementPresent(headerLocator));
                    subItems = driver.FindElements(subItemLocator);
                }                    
                menuItems = driver.FindElements(menuItemLocator);
            }
        }
    }
}
