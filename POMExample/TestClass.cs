using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using POMExample.PageObjects;
using System.Threading;
using NUnit.Framework;
namespace POMExample
{

    public class TestClass
    {
        private IWebDriver driverForChrome;//Chrome driver nesnesi
        private IWebDriver driverForFirefox;//Firefox driver nesnesi
   //   private IWebDriver driver3;
        HomePage HomePageForChrome;//Chrome driver de kullanılan homePage nesnesi
        HomePage HomePageForFirefox;//Firefox driver de kullanılan homePage nesnesi
     // HomePage home3;
        TrendPage TrendPageForChrome;//Chrome driver de kullanılan TrendPage nesnesi
        TrendPage TrendPageForFirefox;//Firefox driver de kullanılan homePage nesnesi
    //  TrendPage trend3;
        ResultPage ResultPageForChrome;//Chrome driver de kullanılan ResultPage nesnesi
        ResultPage ResultPageForFirefox;//Firefox driver de kullanılan homePage nesnesi
   //   ResultPage result3;

        public void SetUpForChrome()
        {
            driverForChrome = new ChromeDriver(); //Chrome driverin nesnesinin tanımlanması
            driverForChrome.Manage().Window.Maximize();//driverin ekranı kaplaması
        }
       
        public void SetUpForFirefox()
        {
            driverForFirefox = new FirefoxDriver();//driverin nesnesinin tanımlanması
            driverForFirefox.Manage().Window.Maximize();//driverin ekranı kaplaması
        }
        /*
        public void SetUpForIE()
        {
            driver3 = new InternetExplorerDriver("C:\\Users\\bilal\\.nuget\\packages\\selenium.internetexplorer.webdriver\\3.141.5\\driver");//driver nesnesinin tanımlanması --- internet explorer yolu yanlış hatası verdiği için yolu elle tanımladım
            driver3.Manage().Window.Maximize();//driverin ekranı kaplaması
        }
        
       
        [Test]
        public void GoToHomePageForIE()
        {
            home3 = new HomePage(this.driver3);
            home3.GoToHomePage();

        }
       
        [Test]
        public void GoToTrendsPageForIE()
        {
            home3 = new HomePage(this.driver3);
            home3.GoToHomePage();
            trend3 = home3.GoToTrendPageForIE();
            driver3.Close();
        }
        
        [Test]
        public void DoSearchForIE()
        {
            home3 = new HomePage(this.driver3);
            home3.GoToHomePage();
            trend3 = home3.GoToTrendPageForIE();
            result3 = trend3.searchForChrome("selenium c#");
        }

        [Test]
        public void ClickOnFirstArticleForIE()
        {
            SetUpForIE();
            home3 = new HomePage(driver3);
            home3.GoToHomePage();
            trend3 = home3.GoToTrendPageForIE();
            Thread.Sleep(3000);
            result3 = trend3.searchForChrome("selenium c#");
            result3.clickOnFirstArticle();
            
        }
        */
        [Test]
        public void GoToHomePageForChrome()
        {//Chrome üzerinden youtube ana sayfasına gitme testi
            HomePageForChrome = new HomePage(this.driverForChrome);//homePage nesnesini tanımlayarak chrome driver nesnesini parametre yolluyoruz
            HomePageForChrome.GoToHomePage();//GoToHomePage fonksiyonu ile driveri youtube ana sayfasına yönlendiriyoruz.
           
        }
        
        [Test]
        public void GoToTrendsPageForChrome()
        {//Chrome üzerinden youtube trendler sayfasına gitme testi
            HomePageForChrome = new HomePage(this.driverForChrome);//homePage nesnesini tanımlayarak chrome driver nesnesini parametre yolluyoruz
            HomePageForChrome.GoToHomePage();//GoToHomePage fonksiyonu ile driveri youtube ana sayfasına yönlendiriyoruz.
            TrendPageForChrome = HomePageForChrome.GoToTrendPage();//TrendPageForChrome nesnesi ise homePage classı içinde oluşturduğumuz fonksiyon yardımıyla tanımlıyoruz ve Youtube trendler sayfasına yönlendiriyoruz.
            driverForChrome.Close();//Son işlem olduğu için driveri kapatıyoruz.
        }

        [Test]
        public void DoSearchForChrome()
        {//Chrome üzerinden youtube üzerinde arama yapma testi
            HomePageForChrome = new HomePage(this.driverForChrome);//homePage nesnesini tanımlayarak chrome driver nesnesini parametre yolluyoruz
            HomePageForChrome.GoToHomePage();//GoToHomePage fonksiyonu ile driveri youtube ana sayfasına yönlendiriyoruz.
            TrendPageForChrome = HomePageForChrome.GoToTrendPage();//TrendPageForChrome nesnesini ise homePage classı içinde oluşturduğumuz fonksiyon yardımıyla tanımlıyoruz ve Youtube trendler sayfasına yönlendiriyoruz.
            Thread.Sleep(3000);//Trend sayfasının yüklenmesini bekleme
            ResultPageForChrome = TrendPageForChrome.searchForChrome("selenium c#");/*ResultPageForChrome nesnesini TrendPage classı içinde oluşturduğumuz fonksiyon yardımıyla tanımlıyoruz.
            *
            * 
            * çalışma mantığı;
            * ilk olarak chrome driveri açıp youtube anasayfasına gider. Daha sonra youtube trendler sayfasına geçer. En son "selenium c#" yazısını youtube üzerinde arar.
            * 
            * 
            */
        }

        [Test]
        public void ClickOnFirstArticleForChrome()
        {//Chrome üzerinden youtube search sayfasında ilk videonun açılması testi
            SetUpForChrome();//Chrome driveri oluşturuyoruz
            HomePageForChrome = new HomePage(this.driverForChrome);//homePage nesnesini tanımlayarak chrome driver nesnesini parametre yolluyoruz
            HomePageForChrome.GoToHomePage();//GoToHomePage fonksiyonu ile driveri youtube ana sayfasına yönlendiriyoruz.
            TrendPageForChrome = HomePageForChrome.GoToTrendPage();//TrendPageForChrome nesnesini ise homePage classı içinde oluşturduğumuz fonksiyon yardımıyla tanımlıyoruz ve Youtube trendler sayfasına yönlendiriyoruz.
            Thread.Sleep(3000);//Trend sayfasının yüklenmesini bekleme
            ResultPageForChrome = TrendPageForChrome.searchForChrome("selenium c#");//ResultPageForChrome nesnesini TrendPage classı içinde oluşturduğumuz fonksiyon yardımıyla tanımlıyoruz.
            ResultPageForChrome.clickOnFirstArticle();/*ResultPage classının ilk videoyu açtırma fonksiyonu çağırıyoruz.
            *
            * çalışma mantığı;
            * ilk olarak chrome driveri açıp youtube anasayfasına gider. Daha sonra youtube trendler sayfasına geçer."selenium c#" yazısını youtube üzerinde arar.Çıkan sonuçlarda ilk videoya tıklar
            * 
            */

        }
        
        [Test]
        public void GoToHomePageForFirefox()
        {
            HomePageForFirefox = new HomePage(this.driverForFirefox);//homePage nesnesini tanımlayarak firefox driver nesnesini parametre yolluyoruz
            HomePageForFirefox.GoToHomePage();// GoToHomePage fonksiyonu ile driveri youtube ana sayfasına yönlendiriyoruz.
        }

        [Test]
        public void GoToTrendsPageForFirefox()
        {
            HomePageForFirefox = new HomePage(this.driverForFirefox);//homePage nesnesini tanımlayarak firefox driver nesnesini parametre yolluyoruz
            HomePageForFirefox.GoToHomePage();// GoToHomePage fonksiyonu ile driveri youtube ana sayfasına yönlendiriyoruz.
            TrendPageForFirefox = HomePageForFirefox.GoToTrendPage();//TrendPageForChrome nesnesi ise homePage classı içinde oluşturduğumuz fonksiyon yardımıyla tanımlıyoruz ve Youtube trendler sayfasına yönlendiriyoruz.
            driverForFirefox.Close();//Son işlem olduğu için driveri kapatıyoruz.
        }

        [Test]
        public void DoSearchForFirefox()
        {
            HomePageForFirefox = new HomePage(this.driverForFirefox);//homePage nesnesini tanımlayarak firefox driver nesnesini parametre yolluyoruz
            HomePageForFirefox.GoToHomePage();// GoToHomePage fonksiyonu ile driveri youtube ana sayfasına yönlendiriyoruz.
            TrendPageForFirefox = HomePageForFirefox.GoToTrendPage();//TrendPageForChrome nesnesi ise homePage classı içinde oluşturduğumuz fonksiyon yardımıyla tanımlıyoruz ve Youtube trendler sayfasına yönlendiriyoruz.
            Thread.Sleep(3000);//Trend sayfasının yüklenmesini bekleme
            ResultPageForFirefox = TrendPageForFirefox.searchForFirefox("selenium c#");/*ResultPageForChrome nesnesini TrendPage classı içinde oluşturduğumuz fonksiyon yardımıyla tanımlıyoruz.
            *
            * 
            * çalışma mantığı;
            * ilk olarak firefox driveri açıp youtube anasayfasına gider. Daha sonra youtube trendler sayfasına geçer. En son "selenium c#" yazısını youtube üzerinde arar.
            * 
            * 
            */
        }

        [Test]
        public void ClickOnFirstArticleForFirefox()
        {
            SetUpForFirefox();//Firefox driveri oluşturuyoruz
            HomePageForFirefox = new HomePage(this.driverForFirefox);//homePage nesnesini tanımlayarak firefox driver nesnesini parametre yolluyoruz
            HomePageForFirefox.GoToHomePage();// GoToHomePage fonksiyonu ile driveri youtube ana sayfasına yönlendiriyoruz.
            TrendPageForFirefox = HomePageForFirefox.GoToTrendPage();//TrendPageForChrome nesnesi ise homePage classı içinde oluşturduğumuz fonksiyon yardımıyla tanımlıyoruz ve Youtube trendler sayfasına yönlendiriyoruz.
            Thread.Sleep(3000);//Trend sayfasının yüklenmesini bekleme
            ResultPageForFirefox = TrendPageForFirefox.searchForFirefox("selenium c#");//ResultPageForChrome nesnesini TrendPage classı içinde oluşturduğumuz fonksiyon yardımıyla tanımlıyoruz.
            ResultPageForFirefox.clickOnFirstArticle();/*ResultPage classının ilk videoyu açtırma fonksiyonu çağırıyoruz.
            *
            * 
            * çalışma mantığı;
            * ilk olarak firefox driveri açıp youtube anasayfasına gider. Daha sonra youtube trendler sayfasına geçer."selenium c#" yazısını youtube üzerinde arar.Çıkan sonuçlarda ilk videoya tıklar
            * 
            * 
            */
        }
      
    }
}
