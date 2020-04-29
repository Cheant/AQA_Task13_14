using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Task13.PageObjects;

namespace Task13
{
    [TestFixture]
    public class WizzairTests
    {
        private string _wizzairUrl = Constants.WizzairUrl;
        private IWebDriver _chromeDriver = new ChromeDriver(@"D:\");
        private string _departureDate;

        [OneTimeSetUp]
        public void SetUp()
        {
            _chromeDriver.Manage().Window.Maximize();
            _chromeDriver.Navigate().GoToUrl(_wizzairUrl);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _chromeDriver.Close();
            _chromeDriver.Quit();
        }

        [Test, Order(1)]
        [Description(@"
                     1. Enter valid destination station
                     2. Enter valid arrival station
                     3. Click 'Search' button
                     Expected result: 'Select Flight' page is opened")]
        public void FlightSearch()
        {
            PageHome pageHome = new PageHome(_chromeDriver);

            WaitHelper waitHelper = new WaitHelper(_chromeDriver);

            pageHome
                .EnterDepartureLocation(Constants.DepartureLocation)
                .EnterArrivalLocation(Constants.ArrivalLocation);

            _departureDate = pageHome.GetDepartureDate();

            pageHome.ClickSearchButton();

            _chromeDriver.SwitchTo().Window(_chromeDriver.WindowHandles.Last());

            waitHelper.WaitForPageUntilElementIsVisible(By.CssSelector(Locators.FlightSearchDateCSSLocator), 60);

            Assert.That(_chromeDriver.Url, Does.Contain(Constants.PageSelectFlightUrl));
        }

        [Test, Order(2)]
        [Description("Check flight date")]
        public void CheckFlightDate()
        {
            PageSelectFlight pageSelectFlight = new PageSelectFlight(_chromeDriver);

            pageSelectFlight.ClickFlightCookiePolicyButton();

            Assert.That(pageSelectFlight.GetFlightDate(), Does.Contain(_departureDate));
        }

        [Test, Order(3)]
        [Description("Check flight departure location")]
        public void CheckFlightDepartureLocation()
        {
            PageSelectFlight pageSelectFlight = new PageSelectFlight(_chromeDriver);

            Assert.AreEqual(pageSelectFlight.GetFlightDepartureLocation(), Constants.DepartureLocation);
        }

        [Test, Order(4)]
        [Description("Check flight arrival location")]
        public void CheckFlightArrivalLocation()
        {
            PageSelectFlight pageSelectFlight = new PageSelectFlight(_chromeDriver);

            Assert.AreEqual(pageSelectFlight.GetFlightArrivalLocation(), Constants.ArrivalLocation);
        }

        [Test, Order(5)]
        [Description("Check that three flight fares are displayed")]
        public void CheckThreeFlightFaresDisplayed()
        {
            PageSelectFlight pageSelectFlight = new PageSelectFlight(_chromeDriver);

            pageSelectFlight.ClickFlight();

            Assert.AreEqual(pageSelectFlight.GetFlightFaresCount(), Constants.FlightFaresCount);
        }

        [Test, Order(6)]
        [Description("Check that prices for three flight fares are unique")]
        public void CheckFlightFaresPricesUnique()
        {
            PageSelectFlight pageSelectFlight = new PageSelectFlight(_chromeDriver);

            Assert.That(pageSelectFlight.GetFlightPrices, Is.Unique);
        }

        [Test, Order(7)]
        [Description("Check that return flight is not displayed")]
        public void CheckReturnFlightIsNotDisplayed()
        {
            PageSelectFlight pageSelectFlight = new PageSelectFlight(_chromeDriver);

            Assert.AreEqual(pageSelectFlight.GetReturnFlightFaresCount(), 0);
        }

        [Test, Order(8)]
        [Description("Check selecting of a fare")]
        public void SelectingFlightFare()
        {
            PageSelectFlight pageSelectFlight = new PageSelectFlight(_chromeDriver);

            WaitHelper waitHelper = new WaitHelper(_chromeDriver);

            pageSelectFlight
                .ClickLastFareButton()
                .ClickFlightContinueButton();

            waitHelper.WaitForPageUntilElementIsVisible(By.Id(Locators.PassengerFirstNameIDLocator), 60);

            Assert.That(_chromeDriver.Url, Does.Contain(Constants.PagePassengerstUrl));
        }

        [Test, Order(9)]
        [Description("Check flight info stations")]
        public void CheckFlightInfoStations()
        {
            PagePassengers pagePassengers = new PagePassengers(_chromeDriver);

            Assert.AreEqual(pagePassengers.GetFlightInfoStations(), $"{Constants.DepartureLocation} – {Constants.ArrivalLocation}");
        }

        [Test, Order(10)]
        [Description(@"
                     1. Enter valid passenger first name
                     2. Enter valid passenger last name
                     3. Select passenger gender male
                     4. Select any luggage option
                     5. Click 'Continue' button
                     Expected result: 'Sign in' form appears")]
        public void EnterPassengerInfo()
        {
            PagePassengers pagePassengers = new PagePassengers(_chromeDriver);

            pagePassengers
                .EnterPassengerFirstName(Constants.PassengerFirstName)
                .EnterPassengerLastName(Constants.PassengerLastName)
                .SelectPassengerGenderMale()
                .SelectLastPassengerBaggage()
                .SelectFirstPassengerMoreBaggage()
                .ClickPassengerContinueButton();

            Assert.IsTrue(pagePassengers.CheckSigninFormDisplayed());
        }
    }
}
