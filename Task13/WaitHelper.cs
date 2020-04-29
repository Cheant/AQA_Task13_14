using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Task13
{
    public class WaitHelper
    {
        private IWebDriver driver;

        public WaitHelper(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement WaitForPageUntilElementIsVisible(By locator, int maxSeconds)
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(maxSeconds))
                .Until(ExpectedConditions.ElementExists(locator));
        }

        public IWebElement WaitForPageUntilElementIsClickable(By locator, int maxSeconds)
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(maxSeconds))
                .Until(ExpectedConditions.ElementToBeClickable(locator));
        }

        public bool WaitForPageUntilElementIsInvisible(By locator, int maxSeconds)
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(maxSeconds))
                .Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
        }
    }
}
