using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using NUnit.Framework;
using System.Text.RegularExpressions;

namespace selenium_training_csharp
{
    [TestFixture]
    public class ProductTests : TestBase
    {
        [Test]
        public void StickersTest_HW8()
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

        [Test]
        public void ProductTest_HW10()
        {
            driver.Url = "http://localhost/litecart/";
            var productName = driver.FindElement(By.CssSelector("#box-campaigns li.product div.name"));
            var regularPrice = driver.FindElement(By.CssSelector("#box-campaigns li.product div.price-wrapper s"));
            var salePrice = driver.FindElement(By.CssSelector("#box-campaigns li.product div.price-wrapper strong"));
            string productNameText = productName.Text;
            string regularPriceText = regularPrice.Text;
            string salePriceText = salePrice.Text;

            AssertStyles(salePrice, regularPrice);

            driver.FindElement(By.CssSelector("#box-campaigns li.product a")).Click();
            var productNameFromPage = driver.FindElement(By.CssSelector("#box-product h1.title"));
            var regularPriceFromPage = driver.FindElement(By.CssSelector("#box-product div.price-wrapper s"));
            var salePriceFromPage = driver.FindElement(By.CssSelector("#box-product div.price-wrapper strong"));

            Assert.AreEqual(productNameText, productNameFromPage.Text);
            Assert.AreEqual(regularPriceText, regularPriceFromPage.Text);
            Assert.AreEqual(salePriceText, salePriceFromPage.Text);
            AssertStyles(salePriceFromPage, regularPriceFromPage);
            
        }
        public void AssertStyles(IWebElement salePrice, IWebElement regularPrice)
        {
            Assert.True(regularPrice.GetCssValue("text-decoration-line") == "line-through");
            Assert.True(IsElementGrey(regularPrice));
            Assert.True(IsElementRed(salePrice));
            Assert.True(GetFontSize(regularPrice) < GetFontSize(salePrice));
            Assert.True(GetFontWeight(regularPrice) < GetFontWeight(salePrice));
        }
        public bool IsElementGrey(IWebElement element)
        {
            var color = element.GetCssValue("color");
            string pattern = @"\d+";
            var matches = Regex.Matches(color, pattern);
            if (matches.Count >= 3)
            {
                int r = Convert.ToInt32(matches[0].Value);
                int g = Convert.ToInt32(matches[1].Value);
                int b = Convert.ToInt32(matches[2].Value);
                if (r == g && g == b)
                    return true;
            }
            return false;            
        }
        public bool IsElementRed (IWebElement element)
        {
            var color = element.GetCssValue("color");
            string pattern = @"\d+";
            var matches = Regex.Matches(color, pattern);
            if (matches.Count >= 3)
            {
                int r = Convert.ToInt32(matches[0].Value);
                int g = Convert.ToInt32(matches[1].Value);
                int b = Convert.ToInt32(matches[2].Value);
                if (r != 0 && g == 0 && b == 0) 
                    return true;
            }
            return false;
        }
        public int GetFontSize(IWebElement element)
        {
            var fontSize = element.GetCssValue("font-size");
            string pattern = @"\d+";
            var match = Regex.Match(fontSize, pattern);
            return Convert.ToInt32(match.Value);
        }
        public int GetFontWeight(IWebElement element)
        {
            return Convert.ToInt32(element.GetCssValue("font-weight"));
        }
    }
}
