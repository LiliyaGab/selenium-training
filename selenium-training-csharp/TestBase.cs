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
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.IE;

namespace selenium_training_csharp
{
    public class TestBase
    {
        public IWebDriver driver;
        public WebDriverWait wait;
        public TimeSpan implicitWaitTimeout = TimeSpan.FromSeconds(5);

        [SetUp]
        public void Start()
        {
            var options = new ChromeOptions();
            options.AddArgument("--disable-features=RendererCodeIntegrity");
            driver = new ChromeDriver(options);
            //driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = implicitWaitTimeout;
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
                return driver.FindElements(locator).Count > 0;
            }
            catch(TimeoutException ex)
            {
                return false;
            }           
        }
    }
}
