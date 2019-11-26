# Page-Object-Model-Ve-Multiple-Browser-Ornegi
Selenyum test projelerinde kullanılan Page Object Model tasarım deseni ve Multiple Browser yapısı kullanılarak geliştirilmiş bir projedir.
   
                                                     PAGE OBJECT MODEL             
                                                      
Yazılan test caselerde kodun tekrarını azaltmak için kullanılan bir model türüdür. Kodun bakımını ve okunabilirliğini kolaylaştırır. Proje için oluşturulan yüzlerce test casede herhangi bir değişiklik olduğu takdirde (element id değiştirildiğinde, element in yeri değiştirildiğinde veya element kaldırıldığında vs), her bir test case’i güncellemek yerine object class değiştirilecek ve bu sayede değiştirilmesi gereken test caseler toplu olarak güncellenmiş olacak.

![Object-Class](https://user-images.githubusercontent.com/46024317/64187461-555c7c80-ce79-11e9-8a71-23e19f706815.png)

Test olarak youtube.com da anasayfa ve trendler sayfaları aralarında gezinip daha sonradan youtube üzerinde search yapıp çıkan sonuçlar arasından ilk videoyu açacak bir senaryo hazırladım. Senaryoyu yazarken POM & C# &NUnit kullanacağız.

Öncelikle Visual Studio üzerinde Bir Unit Test Project (.Net FrameWork) projesi açıp Nuget Packages kullanarak DotNetSeleniumExtras.PageObjects,DotNetSeleniumExtras.WaitHelpers, Selenium.WebDriver,Selenium.Chrome.WebDriver, Selenium.Firefox.WebDriver paketlerini ekleyelim.

PageObjects klasörü ekleyip klasör içine kullanacağımız Classlarımızı oluşturalım. Senaryomuz gereği ana sayfa trend sayfası ve sonuç sayfasında gezeceğimiz için bu sayfaların classlarını oluşturmalıyız.
  
  ![Solution](https://user-images.githubusercontent.com/46024317/64187463-555c7c80-ce79-11e9-9847-3d7c8d9ceeca.PNG)
  
  Classları ekledikten sonra solution explorer  penceresi yukarıdaki gibi olmalıdır.
  
 Classları oluşturduktan sonra sırasıyla dolduralım. İlk olarak HomePage classı ile başlayalım;
OpenQA.Selenium, OpenQA.Selenium. Support.UI, SeleniumExtrax.PageObjects namespacelerini ekleyelim. Daha Sonra kodlamaya geçelim;

        private IWebDriver driver; //web driver
        private WebDriverWait wait;// web driveri belirlenen olay gerçekleşene kadar istenilen süre kadar bekletmeyi sağlayan nesne
        public HomePage(IWebDriver driver)
        {//her sayfadaki test sürücüsü test sınıfındaki sürücüyü kullanması için kullanılan constructor
            this.driver = driver;
            PageFactory.InitElements(driver, this);//Bu yapı tanımlanarak [FindsBy] ek açıklaması ile ana sayfadaki web öğelerini bulunabilir.
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));//web driver istenilen durum gerçekleşene kadar 10 sn bekleyecek
        }

İlk olarak yukarıdaki kodları eklememiz gerekiyor. Kodların açıklaması şu şekilde;

           private IWebDriver driver;                    
Bir web driver nesnesi tanımlarız.

           private WebDriverWait wait;
Bir WebdriverWait nesnesi tanımlarız. Kullanım amacı;web driveri belirlenen olay gerçekleşene kadar istenilen süre kadar bekletmeyi sağlar.

        public HomePage(IWebDriver driver)
        {//her sayfadaki test sürücüsü test sınıfındaki sürücüyü kullanması için kullanılan constructor
            this.driver = driver;
            PageFactory.InitElements(driver, this);//Bu yapı tanımlanarak [FindsBy] ek açıklaması ile ana sayfadaki web öğelerini bulunabilir.
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));//web driver istenilen durum gerçekleşene kadar 10 sn bekleyecek
        }
        
Yukarıdaki Constructor ile de her sayfadaki test driverin TestClasstaki driveri kullanması sağlanmıştır.

            PageFactory.InitElements(driver, this);
Bu yapı tanımlanarak [FindsBy] ek açıklaması şle ana sayfadaki web öğelerini bulunabilir.
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10)); 
web driver istenilen durum gerçekleşene kadar 10 saniye bekleyecek.

 Buraya kadar açıkladıktan sonra kodları yazmaya devam edelim.
 
        [FindsBy(How = How.XPath, Using = "//*[@id='items']/ytd-guide-entry-renderer[2]")]//Pagefactory yapısı kullanılarak Trendler butonunu bulma
        private IWebElement YoutubeTrends; //trendler butonu

        public void GoToHomePage()//Ana sayfaya yönlendiren fonksiyon
        {
            driver.Navigate().GoToUrl("https://www.youtube.com");//youtube ana sayfasına yönlendirme
        }

        public TrendPage GoToTrendPage()//trend sayfasına yönlendiren fonksiyon
        {
            YoutubeTrends = wait.Until<IWebElement>((d) =>//wait metodu kullanarak elementi bulana kadar bekletip olası hataları önlemiş oluyoruz.
            {
                try
                {
                    IWebElement element = d. FindElement(By.XPath("//*[@id='items']/ytd-guide-entry-renderer[2]"));
                    if (element.Displayed)
                    {//element görünürlüğü true olduğu zaman
                        return element;
                    }
                }
                catch (NoSuchElementException) { }//hata yakalamaları
                catch (StaleElementReferenceException) { }

                return null;
            });
            YoutubeTrends.Click();//youtube trendler butonuna tıklama
            return new TrendPage(driver);//trendler sayfasının açılması
        }

Kodların ne işe yaradığı yorum satırları ile belirtilmiştir. Kısaca Youtube ana sayfasında yer alan trendler butonun bulunarak YoutubeTrends butonuna atandığı ve istenildiği zaman GoToTrendPage fonksiyonu yardımıyla YoutubeTrends butonuna tıklatılarak trendler sayfasına gidilebilmesidir. Ayrıca GoToHomePage fonksiyonu ile de youtube ana sayfasına gidilebilir.
Yukarıdaki kodları da yazdıktan sonra HomePage classını tamamlamış oluyoruz.

![HomePage](https://user-images.githubusercontent.com/46024317/64187459-54c3e600-ce79-11e9-9261-9c6898e3d000.PNG)

HomePage classının görüntüsü yukarıdaki gibi olmalıdır.

Şimdi ResultPage sayfasının kodlarını yazmaya başlayabiliriz.

OpenQA.Selenium, OpenQA.Selenium. Support.UI, SeleniumExtrax.PageObjects namespacelerini ekleyelim.
Daha Sonra kodlamaya geçelim;

            private IWebDriver driver; //web driver
        private WebDriverWait wait;// web driveri belirlenen olay gerçekleşene kadar istenilen süre kadar bekletmeyi sağlayan nesne
        public ResultPage(IWebDriver driver)
        {//her sayfadaki test sürücüsü test sınıfındaki sürücüyü kullanması için kullanılan constructor
            this.driver = driver;
            PageFactory.InitElements(driver, this);//Bu yapı tanımlanarak [FindsBy] ek açıklaması ile ana sayfadaki web öğelerini bulunabilir.
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));//web driver istenilen durum gerçekleşene kadar 10 sn bekleyecek
        }
        
Yukarıdaki kodları her kullandığımız Class içerisinde yazmak zorundayız çünkü her sayfa aynı web driveri kullanmak zorundadır.
Kodları yazmaya devam edelim.

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

Kodların açıklamaları yorum satırları ile kod satırlarına eklenmiştir. Kısaca Yapılan search sonuçlarının listelendiği sayfada ilk sıradaki videonun bulunarak firstArticle elementine atanması ve clickOnFirstArticle Metodu yardımıyla istenildiği zaman o videonun tıklatılarak açılmasıdır.

Tüm kodlar yazıldığı zaman ResultPage classının yapısı aşağıdaki fotoğraftaki gibi olmalıdır.

![resultpage](https://user-images.githubusercontent.com/46024317/64187462-555c7c80-ce79-11e9-9a9b-3f84a5d2df35.PNG)

ResultPage classını da tamamladık. Şimdi TrendPage classını kodlamaya başlayabiliriz.

OpenQA.Selenium, OpenQA.Selenium. Support.UI, SeleniumExtrax.PageObjects namespacelerini ekleyelim.
Daha Sonra kodlamaya geçelim;
         
        private IWebDriver driver; //web driver
        private WebDriverWait wait;// web driveri belirlenen olay gerçekleşene kadar istenilen süre kadar bekletmeyi sağlayan nesne
        public TrendPage(IWebDriver driver)//her sayfadaki test sürücüsü test sınıfındaki sürücüyü kullanması için kullanılan constructor
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));//web driver istenilen durum gerçekleşene kadar 10 sn bekleyecek
            PageFactory.InitElements(driver, this);//Bu yapı tanımlanarak [FindsBy] ek açıklaması ile ana sayfadaki web öğelerini bulunabilir.
        }

Yukarıdaki kod satırı diğer classlarda da olduğu gibi aynı şekilde oluşturulmalıdır.

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

Kodların açıklamaları yorum satırları ile kod satırlarına eklenmiştir. Kısaca youtube sayfasındaki search barı search butonu bulunarak elementlere atanmıştır. Bu elementler searchForChrome ve searchForFirefox fonksiyonlarında kullanılarak site de search bara input girip daha sonra search butona tıklatarak site üzerinde arama yapılmasını sağlar. Chrome ve firefox için ayrı fonksiyonlar tanımladık. Çünkü site yapısı her tarayıcı için farklı olduğundan birinde çalışan kod diğer tarayıcıda çalışmayabiliyor.

Tüm kodlar yazıldığı zaman TrendPage classının yapısı aşağıdaki fotoğraftaki gibi olmalıdır.

![trendpage](https://user-images.githubusercontent.com/46024317/64187469-568da980-ce79-11e9-8a97-5bd7864a7ec5.PNG)

Bütün Sayfaların Classlarını doldurduk şimdi test etme zamanı geldi. TestClass içerisinde yazdığımız fonksiyonları kullanarak Oluşturduğumuz senaryoları test edelim.

OpenQA.Selenium, OpenQA.Selenium. Chrome, OpenQA.Selenium. Firefox, SeleniumExtrax.PageObjects, Sysytem.Threading namespacelerini ekleyelim.
Daha Sonra kodlamaya geçelim;

          private IWebDriver driverForChrome;//Chrome driver nesnesi
          private IWebDriver driverForFirefox;//Firefox driver nesnesi
          HomePage HomePageForChrome;//Chrome driver de kullanılan homePage nesnesi
          HomePage HomePageForFirefox;//Firefox driver de kullanılan homePage nesnesi
          TrendPage TrendPageForChrome;//Chrome driver de kullanılan TrendPage nesnesi
          TrendPage TrendPageForFirefox;//Firefox driver de kullanılan homePage nesnesi
          ResultPage ResultPageForChrome;//Chrome driver de kullanılan ResultPage nesnesi
          ResultPage ResultPageForFirefox;//Firefox driver de kullanılan homePage nesnesi
          
Yukarıdaki nesneleri test senaryolarında kullanacağımız için en üstte tanımlamamız gerekiyor.
Daha Sonra Chrome ve Firefox Driverlerini kuracak olan fonksiyonları oluşturalım.

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

Her şey tamam şimdi test senaryolarını yazmaya başlayabiliriz.

        [Test]
        public void GoToHomePageForChrome()
        {//Chrome üzerinden youtube ana sayfasına gitme testi
            HomePageForChrome = new HomePage(this.driverForChrome);//homePage nesnesini tanımlayarak chrome driver nesnesini parametre yolluyoruz
            HomePageForChrome.GoToHomePage();//GoToHomePage fonksiyonu ile driveri youtube ana sayfasına yönlendiriyoruz.
           
        }

Yukarıdaki Testin Açıklaması==> HomePage nesnesine Chrome driveri yollayarak sürücüyü belirtiyoruz. Daha sonra HomePage classında yazmış olduğumuz fonksiyonu kullanarak youtube ana sayfasına yönlendirme yapıyoruz.

                [Test]
                public void DoSearchForChrome()
                {//Chrome üzerinden youtube üzerinde arama yapma testi
                HomePageForChrome = new HomePage(this.driverForChrome);//homePage nesnesini tanımlayarak chrome driver nesnesini parametre yolluyoruz
                HomePageForChrome.GoToHomePage();//GoToHomePage fonksiyonu ile driveri youtube ana sayfasına yönlendiriyoruz.
                TrendPageForChrome = HomePageForChrome.GoToTrendPage();//TrendPageForChrome nesnesini ise homePage classı içinde oluşturduğumuz fonksiyon yardımıyla tanımlıyoruz ve Youtube trendler sayfasına yönlendiriyoruz.
                Thread.Sleep(3000);//Trend sayfasının yüklenmesini bekleme
                ResultPageForChrome = TrendPageForChrome.searchForChrome("selenium c#");//ResultPageForChrome nesnesini TrendPage classı içinde oluşturduğumuz fonksiyon yardımıyla tanımlıyoruz.
                }
         
Yukarıdaki Testin Açıklaması==> HomePage nesnesine Chrome driveri yollayarak sürücüyü belirtiyoruz. Daha sonra HomePage classında yazmış olduğumuz fonksiyonu kullanarak youtube ana sayfasına yönlendirme yapıyoruz. Sonra TrendPage nesnesini homepage sayfasında yazdığımız GoToTrendPage fonksiyonunu çağırırken oluşturuyoruz. Bu arada ana sayfadan trendler butonuna tıklayarak youtube trendler sayfasına da yönlendirmiş oluyoruz. 3 saniye sayfanın yüklenmesini bekliyoruz. Son olarak TrendPage sayfasında yazmış olduğumuz searchForChrome fonksiyonuna “selenium c#” parametresini yollarayak site üzerindeki search input alanına yazmasını ardından search butonuna tıklamasını sağlıyoruz.   

        [Test]
        public void ClickOnFirstArticleForChrome()
        {//Chrome üzerinden youtube search sayfasında ilk videonun açılması testi
            SetUpForChrome();//Chrome driveri oluşturuyoruz
            HomePageForChrome = new HomePage(this.driverForChrome);//homePage nesnesini tanımlayarak chrome driver nesnesini parametre yolluyoruz
            HomePageForChrome.GoToHomePage();//GoToHomePage fonksiyonu ile driveri youtube ana sayfasına yönlendiriyoruz.
            TrendPageForChrome = HomePageForChrome.GoToTrendPage();//TrendPageForChrome nesnesini ise homePage classı içinde oluşturduğumuz fonksiyon yardımıyla tanımlıyoruz ve Youtube trendler sayfasına yönlendiriyoruz.
            Thread.Sleep(3000);//Trend sayfasının yüklenmesini bekleme
            ResultPageForChrome = TrendPageForChrome.searchForChrome("selenium c#");//ResultPageForChrome nesnesini TrendPage classı içinde oluşturduğumuz fonksiyon yardımıyla tanımlıyoruz.
            ResultPageForChrome.clickOnFirstArticle();//ResultPage classının ilk videoyu açtırma fonksiyonu çağırıyoruz.
        }

Yukarıdaki Testin Açıklaması==> HomePage nesnesine Chrome driveri yollayarak sürücüyü belirtiyoruz. Daha sonra HomePage classında yazmış olduğumuz fonksiyonu kullanarak youtube ana sayfasına yönlendirme yapıyoruz. Sonra TrendPage nesnesini homepage sayfasında yazdığımız GoToTrendPage fonksiyonunu çağırırken oluşturuyoruz. Bu arada ana sayfadan trendler butonuna tıklayarak youtube trendler sayfasına da yönlendirmiş oluyoruz. 3 saniye sayfanın yüklenmesini bekliyoruz. Daha Sonra TrendPage sayfasında yazmış olduğumuz searchForChrome fonksiyonuna “selenium c#” parametresini yollarayak site üzerindeki search input alanına yazmasını ardından search butonuna tıklamasını sağlıyoruz. Bu arada searchForChrome fonksiyonu içerisinde ResultPage nesnesini de oluşturmuş oluyoruz. Son olarak ResultPage classı içerisinde tanımlamış olduğumuz clickOnFirstArticle fonksiyonu ile yapmış olduğumuz arama sonucu açılan sayfadaki videolardan ilk videoyu bulup ona tıklayıp açılmasını bekliyoruz. Bunları Chrome driver için yaptık. Aynı testleri firefox Driver için de oluşturalım.

![Firefox](https://user-images.githubusercontent.com/46024317/64187457-542b4f80-ce79-11e9-9575-bd59412d5d8d.PNG)

Firefox için de oluşturduktan sonra Testlerimizin hepsini çalıştırıp sonuçlarına bakalım.

![test](https://user-images.githubusercontent.com/46024317/64187464-55f51300-ce79-11e9-9c06-6604e720271f.PNG)

Yukarıdaki resimde de görüldüğü gibi tüm testlerimiz başarılı bir şekilde çalışıyor.

                                   PAGE OBJECT MODEL İLE MULTİPLE BROWSER YAPISI
          
Page object model yapısı ile beraber Multiple browser yapısı beraber kullanılabilir.

Multiple browser yapısı aynı anda birden fazla browserın farklı işlemleri yürütmesidir.

Hadi bir örnekle bağdaştıralım;
Öncelikle Visual Studio üzerinde Bir test projesi açıp Nuget Packages kullanarak Selenium.Chrome.WebDriver, Selenium.Firefox.WebDriver, Selenium.InternetExplorer.WebDriver, Selenium.WebDriver paketlerini ekleyelim.
Daha Sonra TestClassımız içerisine OpenQA.Selenium, OpenQA.Selenium. Chrome, OpenQA.Selenium. Firefox, OpenQA.Selenium. IE, SeleniumExtrax.PageObjects, Sysytem.Threading.Tasks namespacelerini ekleyelim. Daha Sonra kodlamaya geçelim;

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

İlk olarak yukarıdaki fonksiyonu oluşturalım. Fonksiyon içerisinde her satırın yorum satırında yapılmıştır. Kısaca açıklaması; Chrome driver ve homePage nesnelerini oluşturup açılan browseri tam ekran yapıyoruz. Daha sonra HomePage nesnesini kullanarak Chrome üzerinde youtube ana sayfasına gidiyoruz. Son olarak driveri kapatıyoruz.

İlk fonksiyonu diğer browserlar için ayrı ayrı oluşturalım.

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
    
3 farklı driver için fonksiyonlarımızı oluşturduk. Bu fonksiyonları test yapıp Run All tests yapsaydık sırayla en üstteki test çalışıp bittikten sonra bir sonraki test başlayacaktı. Bizim istediğimiz aynı anda farklı browserları çalıştırmak olduğu için farklı bir yapı kullanacağız.

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
        
Yukarıdaki kod satırları bizim asıl istediğimiz işi yapan fonksiyondur. Paralel.Invoke metodu ile istediğimiz kadar fonksiyonu aynı anda farklı thread’lara atayarak çalıştırabiliriz. Bu metoda oluşturmuş olduğumuz fonksiyonları atayarak aynı anda 3 farklı browser açıp youtube ana sayfasına gidebiliriz. Testi Çalıştırıp test sonucuna bakalım.

![multiple](https://user-images.githubusercontent.com/46024317/64187460-54c3e600-ce79-11e9-8a60-5891b8897b19.PNG)

Yukarıda da göründüğü gibi testimiz başarıyla çalıştı ancak siz aynı anda çalıştığını görmek istiyorsanız kodları kendiniz deneyip çalıştırmanız gerekiyor.





           
