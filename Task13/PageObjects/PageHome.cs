using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Task13.PageObjects
{
    public class PageHome
    {
        private IWebDriver _driver;

        public PageHome(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = Locators.DepartureLocationIDLocator)]
        private IWebElement _departureLocation { get; set; }

        [FindsBy(How = How.Id, Using = Locators.ArrivalLocationIDLocator)]
        private IWebElement _arrivalLocation { get; set; }

        [FindsBy(How = How.XPath, Using = Locators.SearchLocationsXPathLocator)]
        private IList<IWebElement> _searchDepartureLocations { get; set; }

        [FindsBy(How = How.XPath, Using = Locators.SearchLocationsXPathLocator)]
        private IList<IWebElement> _searchArrivalLocations { get; set; }

        [FindsBy(How = How.CssSelector, Using = Locators.FlightSearchButtonCSSLocator)]
        private IWebElement _flightSearchButton { get; set; }

        [FindsBy(How = How.Id, Using = Locators.DepartureDateIDLocator)]
        private IWebElement _departureDate { get; set; }

        public PageHome EnterDepartureLocation(string departureLocation)
        {
            WaitHelper waitHelper = new WaitHelper(_driver);

            waitHelper.WaitForPageUntilElementIsVisible(By.Id(Locators.DepartureLocationIDLocator), 60);

            _departureLocation.Clear();
            _departureLocation.Click();
            _departureLocation.SendKeys(departureLocation);

            waitHelper.WaitForPageUntilElementIsVisible(By.XPath(Locators.SearchLocationsXPathLocator), 60);

            foreach (IWebElement element in _searchDepartureLocations)
            {
                if (element.Text == Constants.DepartureLocation)
                {
                    element.Click();
                    break;
                }
            }

            return this;
        }

        public PageHome EnterArrivalLocation(string arrivalLocation)
        {
            WaitHelper waitHelper = new WaitHelper(_driver);

            waitHelper.WaitForPageUntilElementIsVisible(By.Id(Locators.ArrivalLocationIDLocator), 60);

            _arrivalLocation.Clear();
            _arrivalLocation.Click();
            _arrivalLocation.SendKeys(arrivalLocation);

            waitHelper.WaitForPageUntilElementIsVisible(By.XPath(Locators.SearchLocationsXPathLocator), 60);

            foreach (IWebElement element in _searchArrivalLocations)
            {
                if (element.Text == Constants.ArrivalLocation)
                {
                    element.Click();
                    break;
                }
            }

            return this;
        }

        public PageHome ClickSearchButton()
        {
            _flightSearchButton.Click();

            return this;
        }

        public string GetDepartureDate()
        {
            WaitHelper waitHelper = new WaitHelper(_driver);

            waitHelper.WaitForPageUntilElementIsVisible(By.Id(Locators.DepartureDateIDLocator), 60);

            return _departureDate.Text;
        }
    }
}
