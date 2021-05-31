using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using NUnit.Framework;

namespace selenium_training_csharp
{
    [TestFixture]
    public class ProductTests : TestBase
    {
        [Test]
        public void StickersTest()
        {
            driver.Url = "http://localhost/litecart/";
            var productLocator = By.CssSelector("li.product");
            var stickerLocator = By.CssSelector("div.sticker");
            var products = driver.FindElements(productLocator);
            for (int i=0; i< products.Count; i++)
            {
                Assert.True(products[i].FindElements(stickerLocator).Count == 1);
            }
        }
    }
}
