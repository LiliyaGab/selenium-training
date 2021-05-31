using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace selenium_training_csharp
{
    public class TestBase
    {
        public IWebDriver driver;
        public WebDriverWait wait;

        [SetUp]
        public void Start()
        {
            var options = new ChromeOptions();
            options.AddArgument("--disable-features=RendererCodeIntegrity");
            driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }

        public bool IsElementPresent (By locator)
        {
            try
            {
                wait.Until(d => d.FindElement(locator));
                return true;
            }
            catch(TimeoutException ex)
            {
                return false;
            }
        }
    }
}
