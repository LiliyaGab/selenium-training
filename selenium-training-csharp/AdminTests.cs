using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Reflection;

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
        [Test]
        public void AddProductTest_HW12()
        {
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            driver.FindElement(By.CssSelector("a[href$=catalog]")).Click();
            driver.FindElement(By.CssSelector("a[href$=edit_product]")).Click();

            driver.FindElement(By.Name("status")).Click();
            driver.FindElement(By.Name("name[en]")).SendKeys("Test product");
            driver.FindElement(By.Name("code")).SendKeys("123");
            driver.FindElement(By.Name("quantity")).Clear();
            driver.FindElement(By.Name("quantity")).SendKeys("10");
            var file = "product.jpg";
            var absPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), file);
            driver.FindElement(By.Name("new_images[]")).SendKeys(absPath);
            (driver as IJavaScriptExecutor).ExecuteScript($"document.getElementsByName('date_valid_from')[0].value = '{DateTime.Today.ToString("yyyy-MM-dd")}'");
            (driver as IJavaScriptExecutor).ExecuteScript($"document.getElementsByName('date_valid_to')[0].value = '{DateTime.Today.AddDays(20).ToString("yyyy-MM-dd")}'"); 
                
            driver.FindElement(By.CssSelector("a[href$=tab-information]")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("manufacturer_id")));
            (new SelectElement(driver.FindElement(By.Name("manufacturer_id")))).SelectByValue("1");
            driver.FindElement(By.Name("keywords")).SendKeys("test");
            driver.FindElement(By.Name("short_description[en]")).SendKeys("short description");
            driver.FindElement(By.CssSelector(".trumbowyg-editor")).SendKeys("description");
            driver.FindElement(By.Name("head_title[en]")).SendKeys("test product");
            driver.FindElement(By.Name("meta_description[en]")).SendKeys("meta description");

            driver.FindElement(By.CssSelector("a[href$=tab-prices]")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("purchase_price")));
            driver.FindElement(By.Name("purchase_price")).Clear();
            driver.FindElement(By.Name("purchase_price")).SendKeys("6");
            (new SelectElement(driver.FindElement(By.Name("purchase_price_currency_code")))).SelectByValue("USD");
            driver.FindElement(By.Name("prices[USD]")).SendKeys("5");
            driver.FindElement(By.Name("prices[EUR]")).SendKeys("4");
            driver.FindElement(By.Name("save")).Click();
            Assert.True(IsElementPresent(By.XPath("//a[text()='Test product']")));


        }
        [Test]
        public void LinksTest_HW14()
        {
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            WaitUntilElementIsVisible(By.CssSelector("i.fa-sign-out"));
            driver.FindElement(By.CssSelector("a[href$=countries]")).Click();
            WaitUntilElementIsClickable(By.CssSelector("i.fa.fa-pencil"));
            driver.FindElement(By.CssSelector("i.fa.fa-pencil")).Click();
            var linkElements = driver.FindElements(By.CssSelector("#content a[target=_blank] i.fa-external-link"));
            for (int i=0; i< linkElements.Count; i++)
            {
                var mainWindow = driver.CurrentWindowHandle;
                var oldWindows = driver.WindowHandles;
                linkElements[i].Click();
                string newWindow = wait.Until(d => d.WindowHandles.First(x => !oldWindows.Contains(x)));
                driver.SwitchTo().Window(newWindow);
                driver.Close();
                driver.SwitchTo().Window(mainWindow);
                               
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
        public void SetDatepicker(IWebDriver driver, string cssSelector, string date)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until<bool>(
                d => driver.FindElement(By.CssSelector(cssSelector)).Displayed);
            (driver as IJavaScriptExecutor).ExecuteScript(
                String.Format("$('{0}').datepicker('setDate', '{1}')", cssSelector, date));
        }
    }
}
