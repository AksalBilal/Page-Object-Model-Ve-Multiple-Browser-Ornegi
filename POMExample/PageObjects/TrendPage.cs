using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;

namespace POMExample.PageObjects
{
    class TrendPage
    {
        private IWebDriver driver; //web driver
        private WebDriverWait wait;// web driveri belirlenen olay gerçekleşene kadar istenilen süre kadar bekletmeyi sağlayan nesne
        public TrendPage(IWebDriver driver)//her sayfadaki test sürücüsü test sınıfındaki sürücüyü kullanması için kullanılan constructor
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));//web driver istenilen durum gerçekleşene kadar 10 sn bekleyecek
            PageFactory.InitElements(driver, this);//Bu yapı tanımlanarak [FindsBy] ek açıklaması ile ana sayfadaki web öğelerini bulunabilir.
        }
        
        [FindsBy(How = How.XPath, Using = "//*[@id='search']")]//Pagefactory yapısı kullanarak xpath ile element bulma
        private IWebElement searchTextForChrome;//Chrome için Youtube arama inputu

        [FindsBy(How = How.XPath, Using = "//*[@id='search-input']/input")]//Pagefactory yapısı kullanarak xpath ile element bulma
        private IWebElement searchTextForFirefox;//Firefox için Youtube arama inputu

        [FindsBy(How = How.XPath, Using = "//*[@id='search-icon-legacy']/yt-icon")]//Pagefactory yapısı kullanarak xpath ile element bulma
        private IWebElement searchButton;//Youtube Arama butonu

        public ResultPage searchForChrome(string text)
        {//Girilen texte göre youtube üzerinde arama yapan fonksiyon
            searchTextForChrome.SendKeys(text);//texti inputa aktarma
            searchButton.Click();//arama butonuna tıklama
            return new ResultPage(driver);//Sonuç sayfasına yönlendirme
        }

        public ResultPage searchForFirefox(string text)
        {//Girilen texte göre youtube üzerinde arama yapan fonksiyon
            searchTextForFirefox.SendKeys(text);//texti inputa aktarma
            searchTextForFirefox.SendKeys(Keys.Enter);//entera basarak arama yapma
            return new ResultPage(driver);//Sonuç sayfasına yönlendirme
        }
    }
}
