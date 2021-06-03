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
        public void MenuItemsTest_HW7()
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

        [Test]
        public void CountriesTest_HW9_1()
        {
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            driver.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";
            var countriesLocator = By.CssSelector("table.dataTable td:not(td[style]) a");
            Assert.True(IsElementsTextSorted(countriesLocator));
            var zonesCountLocator = By.XPath("//tr[@class='row']/td[6]");
            var zonesCount = driver.FindElements(zonesCountLocator);
            for (int i = 0; i < zonesCount.Count; i++)
            {
                if (zonesCount[i].Text != "0")
                {
                    zonesCount[i].FindElement(By.XPath("../td[@style]//i")).Click();
                    var zonesLocator = By.XPath("//table[@class='dataTable']//tr/td[3]");
                    Assert.True(IsElementsTextSorted(zonesLocator));
                    driver.FindElement(By.Name("cancel")).Click();
                    zonesCount = driver.FindElements(zonesCountLocator);
                }              
            }

        }
        [Test]
        public void GeoZonesTest_HW9_2()
        {
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            driver.Url = "http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones";
            var countriesLocator = By.CssSelector("table.dataTable td:not(td[style]) a");
            var countries = driver.FindElements(countriesLocator);
            for (int i = 0; i < countries.Count; i++) 
            {
                countries[i].Click();
                var zonesLocator = By.XPath("//tr//select[contains(@name,'zone_code')]/option[@selected]");
                Assert.True(IsElementsTextSorted(zonesLocator));
                driver.FindElement(By.Name("cancel")).Click();
                countries = driver.FindElements(countriesLocator);
            }

        }
        public bool IsElementsTextSorted(By elementsLocator)
        {
            var elements = driver.FindElements(elementsLocator);
            var elementText = new List<string>();
            var sortedElementText = new List<string>();
            for (int i=0; i< elements.Count; i++)
            {
                if (elements[i].Text != "")
                {
                    elementText.Add(elements[i].Text);
                    sortedElementText.Add(elements[i].Text);
                }               
            }
            sortedElementText.Sort();
            return elementText.SequenceEqual(sortedElementText);
        }
    }
}
