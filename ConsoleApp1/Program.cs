using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace ConsoleApp1
{
    public class Tests
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void TestPageTitle()
        {
            _driver.Navigate().GoToUrl("https://kantiana.ru/news/");

            string expectedTitle = "Новости";
            string actualTitle = _driver.Title;
            Assert.That(actualTitle, Is.EqualTo(expectedTitle), "Заголовок страницы не равен ожидаемому");
        }

        [Test]
        public void TestElementVisibility()
        {
            _driver.Navigate().GoToUrl("https://kantiana.ru/news/");

            IWebElement element = _driver.FindElement(By.XPath("/html/body/div[3]/header/div/div[1]/div/div[1]/a"));

            Assert.That(element.Displayed, Is.True, "Заголовок страницы не видим");
        }

        [Test]
        public void TestNavigation()
        {
            _driver.Navigate().GoToUrl("https://kantiana.ru/news/");

            IWebElement link = _driver.FindElement(By.XPath("/html/body/div[3]/header/div/div[3]/div[3]/div/div/div[1]/div/div[1]/div[2]/form/button"));
            link.Click();

            Assert.That(_driver.Url, Does.Contain("projects"), "Переход по ссылке не выполнен.");
        }

        [Test]
        public void TestFillTextField()
        {
            _driver.Navigate().GoToUrl("https://kantiana.ru/news/");

            IWebElement name = _driver.FindElement(By.XPath("/html/body/div[3]/header/div/div[3]/div[3]/div/div/div[1]/div/div[1]/div[2]/form/input"));
            name.SendKeys("Тестовый поиск");

           


            Assert.That(name.GetAttribute("value"), Is.EqualTo("тестовый поиск"), "не совпадает");
            
        }

        [Test]
        public void TestButtonClick()
        {
            _driver.Navigate().GoToUrl("https://kantiana.ru/news/");

            IWebElement button = _driver.FindElement(By.XPath("/html/body/div[3]/header/div/div[3]/div[3]/div/div/div[1]/div/div[1]/div[2]/form/button"));

            button.Click();

            Assert.Throws<NoSuchElementException>(() => _driver.FindElement(By.XPath("/html/body")));
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}