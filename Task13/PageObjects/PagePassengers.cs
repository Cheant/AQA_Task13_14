using System.Linq;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;

namespace Task13.PageObjects
{
    public class PagePassengers
    {
        private IWebDriver _driver;

        public PagePassengers(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = Locators.PassengerFlightInfoStationsCSSLocator)]
        private IWebElement _passengerFlightInfoStations { get; set; }

        [FindsBy(How = How.Id, Using = Locators.PassengerFirstNameIDLocator)]
        private IWebElement _passengerFirstName { get; set; }

        [FindsBy(How = How.Id, Using = Locators.PassengerLastNameIDLocator)]
        private IWebElement _passengerLastName { get; set; }

        [FindsBy(How = How.CssSelector, Using = Locators.PassengerGenderMaleCSSLocator)]
        private IWebElement _passengerGenderMale { get; set; }

        [FindsBy(How = How.CssSelector, Using = Locators.PassengerBaggageOptionsCSSLocator)]
        private IList<IWebElement> _passengerBaggage { get; set; }

        [FindsBy(How = How.CssSelector, Using = Locators.PassengerMoreBaggageOptionCSSLocator)]
        private IWebElement _passengerMoreBaggage { get; set; }

        [FindsBy(How = How.Id, Using = Locators.PassengerContinueButtonIDLocator)]
        private IWebElement _passengerContinueButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = Locators.SigninFormCSSLocator)]
        private IWebElement _signinForm { get; set; }

        public string GetFlightInfoStations()
        {
            WaitHelper waitHelper = new WaitHelper(_driver);

            waitHelper.WaitForPageUntilElementIsVisible(By.CssSelector(Locators.PassengerFlightInfoStationsCSSLocator), 60);

            return _passengerFlightInfoStations.Text;
        }

        public PagePassengers EnterPassengerFirstName(string passengerFirstName)
        {
            WaitHelper waitHelper = new WaitHelper(_driver);

            waitHelper.WaitForPageUntilElementIsVisible(By.Id(Locators.PassengerFirstNameIDLocator), 60);

            _passengerFirstName.Clear();
            _passengerFirstName.SendKeys(passengerFirstName);

            return this;
        }

        public PagePassengers EnterPassengerLastName(string passengerLastName)
        {
            WaitHelper waitHelper = new WaitHelper(_driver);

            waitHelper.WaitForPageUntilElementIsVisible(By.Id(Locators.PassengerLastNameIDLocator), 60);

            _passengerLastName.Clear();
            _passengerLastName.SendKeys(passengerLastName);

            return this;
        }

        public PagePassengers SelectPassengerGenderMale()
        {
            WaitHelper waitHelper = new WaitHelper(_driver);

            waitHelper.WaitForPageUntilElementIsVisible(By.CssSelector(Locators.PassengerGenderMaleCSSLocator), 60);

            _passengerGenderMale.Click();

            return this;
        }

        public PagePassengers SelectLastPassengerBaggage()
        {
            WaitHelper waitHelper = new WaitHelper(_driver);

            Actions actions = new Actions(_driver);

            actions.MoveToElement(_passengerBaggage.Last());
            actions.Perform();

            waitHelper.WaitForPageUntilElementIsClickable(By.CssSelector(Locators.PassengerBaggageOptionsCSSLocator), 60);

            _passengerBaggage.Last().Click();

            return this;
        }

        public PagePassengers SelectFirstPassengerMoreBaggage()
        {
            WaitHelper waitHelper = new WaitHelper(_driver);

            waitHelper.WaitForPageUntilElementIsVisible(By.CssSelector(Locators.PassengerMoreBaggageOptionCSSLocator), 60);

            Actions actions = new Actions(_driver);

            actions.MoveToElement(_passengerMoreBaggage);
            actions.Perform();

            _passengerMoreBaggage.Click();

            return this;
        }

        public PagePassengers ClickPassengerContinueButton()
        {
            WaitHelper waitHelper = new WaitHelper(_driver);

            Actions actions = new Actions(_driver);

            actions.MoveToElement(_passengerContinueButton);
            actions.Perform();

            waitHelper.WaitForPageUntilElementIsClickable(By.Id(Locators.PassengerContinueButtonIDLocator), 60);

            _passengerContinueButton.Click();

            return this;
        }

        public bool CheckSigninFormDisplayed()
        {
            WaitHelper waitHelper = new WaitHelper(_driver);

            waitHelper.WaitForPageUntilElementIsVisible(By.CssSelector(Locators.SigninFormCSSLocator), 60);

            return _signinForm.Displayed;
        }
    }
}
