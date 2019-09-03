using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace POMExample.PageObjects
{
    class HomePage
    {
        private IWebDriver driver; //web driver
        private WebDriverWait wait;// web driveri belirlenen olay gerçekleşene kadar istenilen süre kadar bekletmeyi sağlayan nesne
        public HomePage(IWebDriver driver)
        {//her sayfadaki test sürücüsü test sınıfındaki sürücüyü kullanması için kullanılan constructor
            this.driver = driver;
            PageFactory.InitElements(driver, this);//Bu yapı tanımlanarak [FindsBy] ek açıklaması ile ana sayfadaki web öğelerini bulunabilir.
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));//web driver istenilen durum gerçekleşene kadar 10 sn bekleyecek
        }
        [FindsBy(How = How.XPath, Using = "//*[@id='items']/ytd-guide-entry-renderer[2]")]//Pagefactory yapısı kullanılarak Trendler butonunu bulma
        private IWebElement YoutubeTrends; //trendler butonu
        public void GoToHomePage()//Ana sayfaya yönlendiren fonksiyon
        {
            driver.Navigate().GoToUrl("https://www.youtube.com");//youtube ana sayfasına yönlendirme
        }
        public TrendPage GoToTrendPage() //trend sayfasına yönlendiren fonksiyon
        {
            YoutubeTrends = wait.Until<IWebElement>((d) =>//wait metodu kullanarak elementi bulana kadar bekletip olası hataları önlemiş oluyoruz.
            {
                try
                {
                    IWebElement element = d.FindElement(By.XPath("//*[@id='items']/ytd-guide-entry-renderer[2]"));
                    if (element.Displayed)
                    {//element görünürlüğü true olduğu zaman
                        return element;
                    }
                }
                catch (NoSuchElementException) { }
                catch (StaleElementReferenceException) { }

                return null;
            });
            YoutubeTrends.Click();//Trendler butonuna tıklama
            return new TrendPage(driver);
        }

      /*  public TrendPage GoToTrendPageForIE()
        {
            YoutubeTrendsForIE = wait.Until<IWebElement>((d) =>
            {
                try
                {
                    IWebElement element = d.FindElement(By.XPath("//*[@id='appbar-nav']/ul/li[2]/a"));
                    if (element.Displayed)
                    {
                        return element;
                    }
                }
                catch (NoSuchElementException) { }
                catch (StaleElementReferenceException) { }

                return null;
            });
            YoutubeTrendsForIE.Click();
            return new TrendPage(driver);
        }*/
    }
}
