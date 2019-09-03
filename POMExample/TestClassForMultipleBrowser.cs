using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using POMExample.PageObjects;
using System.Threading.Tasks;

namespace POMExample
{
   public class TestClassForMultipleBrowser
    {
        [Test]
        public void RunAllBrowser()
        {
            var options = new ParallelOptions();//paralel test koşmamızı sağlayan class
            options.MaxDegreeOfParallelism = 3;//maximum paralel işlem sayısı
            Parallel.Invoke(options,//paralel bir şekilde işlemlerimizi yapmamızı sağlayan yöntem
              () => RunChrome(),//1.fonksiyon
              () => RunFirefox(),//2.fonksiyon
              ()=> RunInternetExplorer()//3.fonksiyon
            );
        }
        public void RunChrome()
        {
            IWebDriver driver;//web driver nesnesi
            HomePage home;//Homepage nesnesi
            driver = new ChromeDriver(); //driverin nesnesinin tanımlanması
            driver.Manage().Window.Maximize();//driverin ekranı kaplaması
            home = new HomePage(driver);//homePage classını kullanarak multiple browser yaklaşımını Page Object Model üzerinde test etme
            home.GoToHomePage();//youtube ana sayfasına yönlendirme
            driver.Close();//driverin kapanması
        }
        public void RunFirefox()
        {
            IWebDriver driver;//web driver nesnesi
            HomePage home;//Homepage nesnesi
            driver = new FirefoxDriver();//driverin nesnesinin tanımlanması
            driver.Manage().Window.Maximize();//driverin ekranı kaplaması
            home = new HomePage(driver);//homePage classını kullanarak multiple browser yaklaşımını Page Object Model üzerinde test etme
            home.GoToHomePage();//youtube ana sayfasına yönlendirme
            driver.Close();//driverin kapanması
        }
        public void RunInternetExplorer()
        {
            IWebDriver driver;//web driver nesnesi
            HomePage home;//Homepage nesnesi
            driver = new InternetExplorerDriver("C:\\Users\\bilal\\.nuget\\packages\\selenium.internetexplorer.webdriver\\3.141.5\\driver");//driver nesnesinin tanımlanması --- internet explorer yolu yanlış hatası verdiği için yolu elle tanımladım
            driver.Manage().Window.Maximize();//driverin ekranı kaplaması
            home = new HomePage(driver);//homePage classını kullanarak multiple browser yaklaşımını Page Object Model üzerinde test etme
            home.GoToHomePage();//youtube ana sayfasına yönlendirme
            driver.Close();//driverin kapanması
        }
      
    }
}
