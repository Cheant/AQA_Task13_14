using System.Linq;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;

namespace Task13.PageObjects
{
    public class PageSelectFlight
    {
        private IWebDriver _driver;

        public PageSelectFlight(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = Locators.FlightCookiePolicyButtonCSSLocator)]
        private IWebElement _flightCookiePolicyButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = Locators.FlightDateCSSLocator)]
        private IWebElement _flightDate { get; set; }

        [FindsBy(How = How.CssSelector, Using = Locators.FlightLocationsCSSLocator)]
        private IList<IWebElement> _flightLocations { get; set; }

        [FindsBy(How = How.CssSelector, Using = Locators.FlightInfoButtonsCSSLocator)]
        private IList<IWebElement> _flightInfoButtons { get; set; }

        [FindsBy(How = How.CssSelector, Using = Locators.FlightFaresCSSLocator)]
        private IList<IWebElement> _flightFares { get; set; }

        [FindsBy(How = How.CssSelector, Using = Locators.FlightFareButtonsCSSLocator)]
        private IList<IWebElement> _flightFareButtons { get; set; }

        [FindsBy(How = How.CssSelector, Using = Locators.FlightFarePricesCSSLocator)]
        private IList<IWebElement> _flightFarePrices { get; set; }

        [FindsBy(How = How.Id, Using = Locators.ReturnFlightFaresIDLocator)]
        private IList<IWebElement> _returnFlightFares { get; set; }

        [FindsBy(How = How.Id, Using = Locators.FlightContinueButtonIDLocator)]
        private IWebElement _flightContinueButton { get; set; }

        public PageSelectFlight ClickFlightCookiePolicyButton()
        {
            WaitHelper waitHelper = new WaitHelper(_driver);

            waitHelper.WaitForPageUntilElementIsVisible(By.CssSelector(Locators.FlightCookiePolicyButtonCSSLocator), 60);

            _flightCookiePolicyButton.Click();

            return this;
        }

        public string GetFlightDate()
        {
            WaitHelper waitHelper = new WaitHelper(_driver);

            waitHelper.WaitForPageUntilElementIsVisible(By.CssSelector(Locators.FlightDateCSSLocator), 60);

            return _flightDate.Text;
        }

        public string GetFlightDepartureLocation()
        {
            WaitHelper waitHelper = new WaitHelper(_driver);

            waitHelper.WaitForPageUntilElementIsVisible(By.CssSelector(Locators.FlightLocationsCSSLocator), 60);

            return _flightLocations.First().Text;
        }

        public string GetFlightArrivalLocation()
        {
            WaitHelper waitHelper = new WaitHelper(_driver);

            waitHelper.WaitForPageUntilElementIsVisible(By.CssSelector(Locators.FlightLocationsCSSLocator), 60);

            return _flightLocations.Last().Text;
        }

        public PageSelectFlight ClickFlight()
        {
            WaitHelper waitHelper = new WaitHelper(_driver);

            if (waitHelper.WaitForPageUntilElementIsInvisible(By.CssSelector(Locators.FlightInfoSpinnerCSSLocator), 60))
            {
                waitHelper.WaitForPageUntilElementIsVisible(By.CssSelector(Locators.FlightInfoButtonsCSSLocator), 60);

                _flightInfoButtons.First().Click();
            }

            return this;
        }

        public int GetFlightFaresCount()
        {
            WaitHelper waitHelper = new WaitHelper(_driver);

            waitHelper.WaitForPageUntilElementIsVisible(By.CssSelector(Locators.FlightFaresCSSLocator), 60);

            return _flightFares.Count;
        }

        public IList<IWebElement> GetFlightPrices()
        {
            WaitHelper waitHelper = new WaitHelper(_driver);

            waitHelper.WaitForPageUntilElementIsVisible(By.CssSelector(Locators.FlightFarePricesCSSLocator), 60);

            return _flightFarePrices;
        }

        public int GetReturnFlightFaresCount()
        {
            return _returnFlightFares.Count;
        }

        public PageSelectFlight ClickLastFareButton()
        {
            WaitHelper waitHelper = new WaitHelper(_driver);

            Actions actions = new Actions(_driver);


            if (waitHelper.WaitForPageUntilElementIsInvisible(By.CssSelector(Locators.FlightInfoSpinnerCSSLocator), 60))
            {
                waitHelper.WaitForPageUntilElementIsVisible(By.CssSelector(Locators.FlightFareButtonsCSSLocator), 60);

                actions.MoveToElement(_flightFareButtons.Last());
                actions.Perform();

                _flightFareButtons.Last().Click();
            }

            return this;
        }

        public PageSelectFlight ClickFlightContinueButton()
        {
            WaitHelper waitHelper = new WaitHelper(_driver);

            Actions actions = new Actions(_driver);

            actions.MoveToElement(_flightContinueButton);
            actions.Perform();

            waitHelper.WaitForPageUntilElementIsClickable(By.Id(Locators.FlightContinueButtonIDLocator), 60);

            _flightContinueButton.Click();

            return this;
        }
    }
}
