using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
namespace POMExample.PageObjects
{
    class ResultPage
    {
        private IWebDriver driver; //web driver
        private WebDriverWait wait;// web driveri belirlenen olay gerçekleşene kadar istenilen süre kadar bekletmeyi sağlayan nesne
        public ResultPage(IWebDriver driver)
        {//her sayfadaki test sürücüsü test sınıfındaki sürücüyü kullanması için kullanılan constructor
            this.driver = driver;
            PageFactory.InitElements(driver, this);//Bu yapı tanımlanarak [FindsBy] ek açıklaması ile ana sayfadaki web öğelerini bulunabilir.
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));//web driver istenilen durum gerçekleşene kadar 10 sn bekleyecek
        }

        [FindsBy(How = How.XPath, Using = "//*[@id='contents']/ytd-video-renderer[1]/div[1]")]//firstArticle elementinin bulunması
        private IWebElement firstArticle;//yapılan search sonucları arasındaki ilk video

        public void clickOnFirstArticle()//search sayfasında ilk videoya tıklayan fonksiyon
        {
            firstArticle = wait.Until<IWebElement>((d) =>//wait metodu kullanarak elementi bulana kadar bekletip olası hataları önlemiş oluyoruz.
            {
                try
                {
                    IWebElement element = d.FindElement(By.XPath("//*[@id='contents']/ytd-video-renderer[1]/div[1]"));
                    if (element.Displayed)
                    {//element görünürlüğü true olduğu zaman
                        return element;
                    }
                }
                catch (NoSuchElementException) { }//NoSuchElementException hatasının yakalanması
                catch (StaleElementReferenceException) { }//StaleElementReferenceException hatasının yakalanması

                return null;
            });
            firstArticle.Click();//İlk Videonun tıklanması
        }
    }
}
