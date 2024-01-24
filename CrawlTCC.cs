using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Web.UI.WebControls;

namespace CrawlWebBDS
{
    public class CrawlTCC
    {
        public static string cookies;
        public static int fromPage;
        public static int toPage;
        public static List<string> tinh = new List<string>();

        public static int timeBetweenNewFrom;
        public static int timeBetweenNewTo;

        public static string linkAPI1 = null;
        public static string linkAPI2 = null;



        public static int autoFromPage;
        public static int autoToPage;
        public static List<string> autoTinh = new List<string>();
        public static DateTime beginTime;
        public static DateTime endTime;
        public static List<int> dayinWeek = new List<int>();
        public static int loopTime;

        public static bool runAuto = false;
        public static bool runScrape = false;
        public static bool layXuoiMode = false;
        public static bool autoLogin = false;

        public static bool runningAuto = false;


        public static Random rnd = new Random();

        public static string userHN;
        public static string passHN;
        public static string userHCM;
        public static string passHCM;

        public static bool CheckTimeValid(string strTime)
        {
            return true;
        }

        public static void ScrapeRunCrawl(BackgroundWorker m_oWorker)
        {
            ChromeDriver driver = null;
            try
            {
                var options = new ChromeOptions();
                //options.AddArgument("--headless");
                options.AddArgument("--window-size=1200,900");
                //options.AddArgument("--headless");
                options.AddArgument("ignore-certificate-errors");
                options.AddExcludedArgument("enable-automation");
                //options.AddAdditionalCapability("useAutomationExtension", false);
                options.AddArgument("--disable-web-security");
                options.AddArgument("--disable-blink-features=AutomationControlled");
                options.AddArgument("--no-sandbox");
                options.AddArgument("--start-maximized");
                //options.AddArgument("--disable-dev-shm-usage");
                options.AddUserProfilePreference("credentials_enable_service", false);
                options.AddUserProfilePreference("profile.password_manager_enabled", false);
                //options.AddUserProfilePreference("profile.default_content_setting_values.images", 2);

                ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                service.HideCommandPromptWindow = true;

                driver = new ChromeDriver(service, options);
                for (int tp = 0; tp < tinh.Count; tp++)
                {
                    var tinhTP = tinh[tp];
                    WaitLoginScrape(driver, m_oWorker, tinhTP);
                    var targetUrl = "";
                    if (tinhTP == "HN")
                    {
                        targetUrl = "http://hanoi.tinchinhchu.net.vn/";
                    }
                    else if (tinhTP == "HCM")
                    {
                        targetUrl = "http://hcm.tinchinhchu.net.vn/";
                    }
                    driver.Navigate().GoToUrl(targetUrl);
                    Thread.Sleep(2000);

                    if (!layXuoiMode)
                    {
                        for (int page = 1; page <= toPage; page++)
                        {
                            if (runScrape == false)
                            {
                                throw new Exception("Dừng chương trình thủ công");
                            }
                            try
                            {
                                var divPage = driver.FindElement(By.XPath("//div[@class='PageList']"));
                                var lsSpanPage = divPage.FindElements(By.TagName("span"));
                                for (int i = 0; i < lsSpanPage.Count; i++)
                                {
                                    if (lsSpanPage[i].Text == page.ToString())
                                    {
                                        try
                                        {
                                            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                                            executor.ExecuteScript("arguments[0].scrollIntoView(true);", lsSpanPage[i]);
                                            lsSpanPage[i].Click();
                                            break;
                                        }
                                        catch
                                        {
                                            continue;
                                        }
                                    }
                                }

                                Thread.Sleep(2000);
                            }
                            catch (Exception ex) { }
                        }
                        for (int page = toPage; page >= fromPage; page--)
                        {
                            if (runScrape == false)
                            {
                                throw new Exception("Dừng chương trình thủ công");
                            }
                            try
                            {
                                var divPage = driver.FindElement(By.XPath("//div[@class='PageList']"));
                                var lsSpanPage = divPage.FindElements(By.TagName("span"));
                                for (int i = 0; i < lsSpanPage.Count; i++)
                                {
                                    if (lsSpanPage[i].Text == page.ToString())
                                    {
                                        try
                                        {
                                            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                                            executor.ExecuteScript("arguments[0].scrollIntoView(true);", lsSpanPage[i]);
                                            lsSpanPage[i].Click();
                                            break;
                                        }
                                        catch
                                        {
                                            continue;
                                        }
                                    }
                                }
                            }
                            catch { }
                            for (int i = 0; i < 20; i++)
                            {
                                var elementNews = driver.FindElement(By.Id($"dnn_ctr453_View_ctl00_grvNews_hplDetails_{i.ToString()}"));
                                elementNews.Click();
                                string url = elementNews.GetAttribute("href");
                                Crawl(m_oWorker, driver, url);
                                var sleepTime = rnd.Next(timeBetweenNewFrom, timeBetweenNewTo);
                                Thread.Sleep(sleepTime * 1000);

                                if (runScrape == false)
                                {
                                    throw new Exception("Dừng chương trình thủ công");
                                }
                            }
                        }
                    }
                    else
                    {

                        for (int page = fromPage; page <= toPage; page++)
                        {
                            if (runScrape == false)
                            {
                                throw new Exception("Dừng chương trình thủ công");
                            }
                            try
                            {
                                var divPage = driver.FindElement(By.XPath("//div[@class='PageList']"));
                                var lsSpanPage = divPage.FindElements(By.TagName("span"));
                                for (int i = 0; i < lsSpanPage.Count; i++)
                                {
                                    if (lsSpanPage[i].Text == page.ToString())
                                    {
                                        try
                                        {
                                            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                                            executor.ExecuteScript("arguments[0].scrollIntoView(true);", lsSpanPage[i]);
                                            lsSpanPage[i].Click();
                                            break;
                                        }
                                        catch
                                        {
                                            continue;
                                        }
                                    }
                                }
                                Thread.Sleep(2000);
                            }
                            catch { }
                            for (int i = 0; i < 20; i++)
                            {
                                try
                                {
                                    var elementNews = driver.FindElement(By.Id($"dnn_ctr453_View_ctl00_grvNews_hplDetails_{i.ToString()}"));
                                    elementNews.Click();
                                    string url = elementNews.GetAttribute("href");
                                    Crawl(m_oWorker, driver, url);
                                    //driver.FindElement(By.Id("fancybox-close")).Click();
                                    var sleepTime = rnd.Next(timeBetweenNewFrom, timeBetweenNewTo);
                                    Thread.Sleep(sleepTime * 1000);
                                }
                                catch { }

                                if (runScrape == false)
                                {
                                    throw new Exception("Dừng chương trình thủ công");
                                }

                            }
                        }
                    }
                    try
                    {
                        Logout(driver, m_oWorker, tinhTP);
                    }
                    catch { }
                }
                
            }
            catch (Exception ex)
            {
                m_oWorker.ReportProgress(1, ex.Message);
            }
            finally
            {
                try
                {
                    driver.Close();
                    driver.Quit();
                }
                catch
                {

                }
                m_oWorker.ReportProgress(2, "");
                //MessageBox.Show("Chạy thủ công hoàn thành");
            }
        }

        public static void AutoRunCrawl(BackgroundWorker m_oWorker)
        {
            while (runAuto)
            {
                var loop = 0;
                DateTime runTime = DateTime.Now;
                if (runTime.TimeOfDay <= beginTime.TimeOfDay || runTime.TimeOfDay >= endTime.TimeOfDay || !dayinWeek.Contains((int)runTime.DayOfWeek))
                {
                    runningAuto = false;
                    runTime = DateTime.Now;
                    TimeSpan Addtime = new TimeSpan(0, 0, loopTime, 0);
                    runTime.Add(Addtime).ToLongTimeString();
                    m_oWorker.ReportProgress(5, "Thời gian chạy tiếp theo: " + runTime.Add(Addtime).ToLongTimeString());
                    loop = loopTime * 60 * 1000;
                    while (loop > 0)
                    {
                        if (runAuto == false) break;
                        loop -= 1000;
                        Thread.Sleep(1000);
                    }
                    if (runAuto == false) break;
                    continue;
                }
                m_oWorker.ReportProgress(0, "đang chạy");
                runningAuto = true;
                ChromeDriver driver = null;
                try
                {
                    var options = new ChromeOptions();
                    //options.AddArgument("--headless");
                    options.AddArgument("--window-size=1200,900");
                    //options.AddArgument("--headless");
                    options.AddArgument("ignore-certificate-errors");
                    options.AddExcludedArgument("enable-automation");
                    //options.AddAdditionalCapability("useAutomationExtension", false);
                    options.AddArgument("--disable-web-security");
                    options.AddArgument("--disable-blink-features=AutomationControlled");
                    options.AddArgument("--no-sandbox");
                    options.AddArgument("--start-maximized");
                    //options.AddArgument("--disable-dev-shm-usage");
                    options.AddUserProfilePreference("credentials_enable_service", false);
                    options.AddUserProfilePreference("profile.password_manager_enabled", false);
                    //options.AddUserProfilePreference("profile.default_content_setting_values.images", 2);

                    ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                    service.HideCommandPromptWindow = true;

                    driver = new ChromeDriver(service, options);
                    for (int tp = 0; tp < autoTinh.Count; tp++)
                    {
                        var tinhTP = autoTinh[tp];
                        WaitLoginAuto(driver, m_oWorker, tinhTP);
                        var targetUrl = "";
                        if (tinhTP == "HN")
                        {
                            targetUrl = "http://hanoi.tinchinhchu.net.vn/";
                        }
                        else if (tinhTP == "HCM")
                        {
                            targetUrl = "http://hcm.tinchinhchu.net.vn/";
                        }
                        driver.Navigate().GoToUrl(targetUrl);
                        Thread.Sleep(2000);

                        if (!layXuoiMode)
                        {
                            for (int page = 1; page <= autoToPage; page++)
                            {
                                if (runAuto == false)
                                {
                                    throw new Exception("Dừng chương trình tự động");
                                }
                                try
                                {
                                    var divPage = driver.FindElement(By.XPath("//div[@class='PageList']"));
                                    var lsSpanPage = divPage.FindElements(By.TagName("span"));
                                    for (int i = 0; i < lsSpanPage.Count; i++)
                                    {
                                        if (lsSpanPage[i].Text == page.ToString())
                                        {
                                            try
                                            {
                                                IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                                                executor.ExecuteScript("arguments[0].scrollIntoView(true);", lsSpanPage[i]);
                                                lsSpanPage[i].Click();
                                                break;
                                            }
                                            catch
                                            {
                                                continue;
                                            }
                                        }
                                    }
                                    Thread.Sleep(2000);
                                }
                                catch { }
                            }
                            for (int page = autoToPage; page >= autoFromPage; page--)
                            {
                                if (runAuto == false)
                                {
                                    throw new Exception("Dừng chương trình tự động");
                                }
                                try
                                {
                                    driver.FindElement(By.Id($"dnn_ctr453_View_ctl00_rptPager_lnkPage_{page}")).Click();
                                    Thread.Sleep(2000);
                                }
                                catch { }
                                for (int i = 0; i < 20; i++)
                                {
                                    var elementNews = driver.FindElement(By.Id($"dnn_ctr453_View_ctl00_grvNews_hplDetails_{i.ToString()}"));
                                    elementNews.Click();
                                    string url = elementNews.GetAttribute("href");
                                    Crawl(m_oWorker, driver, url);
                                    var sleepTime = rnd.Next(timeBetweenNewFrom, timeBetweenNewTo);
                                    Thread.Sleep(sleepTime * 1000);

                                    if (runAuto == false)
                                    {
                                        throw new Exception("Dừng chương trình tự động");
                                    }
                                }
                            }
                        }
                        else
                        {

                            for (int page = autoFromPage; page <= autoToPage; page++)
                            {
                                if (runAuto == false)
                                {
                                    throw new Exception("Dừng chương trình tự động");
                                }
                                try
                                {
                                    var divPage = driver.FindElement(By.XPath("//div[@class='PageList']"));
                                    var lsSpanPage = divPage.FindElements(By.TagName("span"));
                                    for (int i = 0; i < lsSpanPage.Count; i++)
                                    {
                                        if (lsSpanPage[i].Text == page.ToString())
                                        {
                                            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                                            executor.ExecuteScript("arguments[0].scrollIntoView(true);", lsSpanPage[i]);
                                            lsSpanPage[i].Click();
                                        }
                                    }
                                    Thread.Sleep(2000);
                                }
                                catch { }
                                for (int i = 0; i < 20; i++)
                                {
                                    try
                                    {
                                        var elementNews = driver.FindElement(By.Id($"dnn_ctr453_View_ctl00_grvNews_hplDetails_{i.ToString()}"));
                                        elementNews.Click();
                                        string url = elementNews.GetAttribute("href");
                                        Crawl(m_oWorker, driver, url);
                                        //driver.FindElement(By.Id("fancybox-close")).Click();
                                        var sleepTime = rnd.Next(timeBetweenNewFrom, timeBetweenNewTo);
                                        Thread.Sleep(sleepTime * 1000);
                                    }
                                    catch { }

                                    if (runAuto == false)
                                    {
                                        throw new Exception("Dừng chương trình thủ công");
                                    }

                                }
                            }
                        }
                        try
                        {
                            Logout(driver, m_oWorker, tinhTP);
                        }
                        catch { }

                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                    m_oWorker.ReportProgress(1, ex.Message);
                }
                finally
                {
                    runningAuto = false;
                    try
                    {
                        driver.Close();
                        driver.Quit();

                    }
                    catch
                    {

                    }

                }
                if (runAuto == false) break;

                runTime = DateTime.Now;
                TimeSpan time = new TimeSpan(0, 0, loopTime, 0);
                runTime.Add(time).ToLongTimeString();
                m_oWorker.ReportProgress(5, "Thời gian chạy tiếp theo: " + runTime.Add(time).ToLongTimeString());
                loop = loopTime * 60 * 1000;
                while (loop > 0)
                {
                    if (runAuto == false) break;
                    loop -= 1000;
                    Thread.Sleep(1000);
                }
                if (runAuto == false) break;

            }

            m_oWorker.ReportProgress(2, "");
        }

        public static void Crawl(BackgroundWorker m_oWorker, ChromeDriver driver, string url)
        {
            try
            {
                Thread.Sleep(1000);
                driver.SwitchTo().Frame(driver.FindElement(By.Id("fancybox-frame")));
                Thread.Sleep(2000);
                var newUrl = url;
                var title = driver.FindElement(By.Id("dnn_ctr453_Details_lblTitle")).Text.Trim();
                var price = driver.FindElement(By.Id("dnn_ctr453_Details_lblPriceText")).Text.Trim().Split('/')[0];
                var type = driver.FindElement(By.Id("dnn_ctr453_Details_lblType")).Text.Trim();
                var province = driver.FindElement(By.Id("dnn_ctr453_Details_lblProvince")).Text;
                var district = driver.FindElement(By.Id("dnn_ctr453_Details_lblDistrict")).Text.Trim();
                var createDate = driver.FindElement(By.Id("dnn_ctr453_Details_lblCreateDate")).Text.Trim();
                var category = driver.FindElement(By.Id("dnn_ctr453_Details_lblCategory")).Text.Trim();
                var content = driver.FindElement(By.Id("dnn_ctr453_Details_lblContent")).Text.Trim().Replace("\r\n", "<br>");
                var phone = driver.FindElement(By.Id("dnn_ctr453_Details_lblPhone")).FindElement(By.TagName("a")).Text;

                var bodyUrl = $"\"url\":\"{newUrl}\",";
                var bodyTitle = $"\"title\":\"{title.Split('-')[0]}\",";
                var bodyContent = $"\"content\":\"{content.Trim()}\",";
                var bodyPrice = $"\"price\":\"{price}\",";
                var bodyCreateDate = $"\"createDate\":\"{createDate}\",";
                var bodyType = $"\"type\":\"{type}\",";
                var bodyProvince = $"\"province\":\"{province}\",";
                var bodyCategory = $"\"category\":\"{category}\",";
                var bodyPhone = $"\"phone\":\"{phone}\",";
                var bodyDistrict = $"\"district\":\"{district}\"";


                Task.Run(() =>
                {
                    try
                    {
                        if (linkAPI1 != null && linkAPI1 != "")
                        {
                            var client = new RestClient(linkAPI1);
                            var request = new RestRequest(Method.POST);
                            request.AddHeader("Content-Type", "application/json");
                            var body = "[{" + bodyUrl + bodyProvince + bodyCategory + bodyTitle + bodyContent + bodyPrice + bodyCreateDate + bodyType + bodyPhone + bodyDistrict + "}]";
                            WriteLog(body);
                            request.AddParameter("application/json", body, ParameterType.RequestBody);
                            IRestResponse response = client.Execute(request);
                            Console.WriteLine(response.Content);
                            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                m_oWorker.ReportProgress(0, url);
                            }
                            else
                            {
                                m_oWorker.ReportProgress(1, url);
                            }
                        }
                    }
                    catch { }
                });
                Task.Run(() =>
                {
                    try
                    {
                        if (linkAPI2 != null && linkAPI2 != "")
                        {
                            var client = new RestClient(linkAPI2);
                            var request = new RestRequest(Method.POST);
                            request.AddHeader("Content-Type", "application/json");
                            var body = "[{" + bodyUrl + bodyProvince + bodyCategory + bodyTitle + bodyContent + bodyPrice + bodyCreateDate + bodyType + bodyPhone + bodyDistrict + "}]";
                            request.AddParameter("application/json", body, ParameterType.RequestBody);
                            IRestResponse response = client.Execute(request);
                            Console.WriteLine(response.Content);
                            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                m_oWorker.ReportProgress(3, url);
                            }
                            else
                            {
                                m_oWorker.ReportProgress(4, url);
                            }
                        }
                    }
                    catch { }
                });
            }
            catch (Exception ex) { }
            finally
            {
                try
                {
                    driver.SwitchTo().ParentFrame();
                    Thread.Sleep(1000);
                    driver.FindElement(By.Id("fancybox-close")).Click();
                }
                catch (Exception ex) { }
            }

        }

        public static void WaitLoginScrape(ChromeDriver driver, BackgroundWorker m_oWorker, string tinhTP)
        {
            var fileCookie = $"cookiesBackup_{tinhTP}.txt";
            var fileBackup = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TCC", fileCookie);
            var targetUrl = "";
            if (tinhTP == "HN")
            {
                targetUrl = "http://hanoi.tinchinhchu.net.vn/";
            }
            else if (tinhTP == "HCM")
            {
                targetUrl = "http://hcm.tinchinhchu.net.vn/";
            }
            driver.Navigate().GoToUrl(targetUrl);
            driver.Manage().Cookies.DeleteAllCookies();


            if (File.Exists(fileBackup))
            {
                var cookiebackUp = File.ReadAllText(fileBackup);
                var cookie = cookiebackUp.Split(';');
                driver.Manage().Cookies.DeleteAllCookies();
                foreach (string ck in cookie)
                {
                    if (ck == "") break;
                    var datacookie = ck.Trim().Split('=');
                    if (datacookie.Length >= 2)
                    {
                        Cookie name = new Cookie(datacookie[0], ck.Trim().Split(new[] { datacookie[0] + '=' }, StringSplitOptions.None)[1]);
                        driver.Manage().Cookies.AddCookie(name);
                    }
                }
            }
            while (true)
            {
                driver.Navigate().GoToUrl(targetUrl);
                Thread.Sleep(3 * 1000);
                m_oWorker.ReportProgress(6, "");
                if (runScrape == false)
                {
                    throw new Exception("Dừng chương trình");
                }
                if (autoLogin)
                {
                    try
                    {
                        var logOut = driver.FindElement(By.XPath("//li[@class='ui-state-default ui-corner-top ui-tabs-selected ui-state-active']"));
                        if (logOut != null)
                        {
                            getCookie(driver, tinhTP);
                            break;
                        }
                    }
                    catch
                    {
                        AutoLogin(driver, tinhTP);
                    }
                    Thread.Sleep(3 * 1000);
                }
                else
                {
                    try
                    {
                        var logOut = driver.FindElement(By.XPath("//li[@class='ui-state-default ui-corner-top ui-tabs-selected ui-state-active']"));
                        if (logOut != null)
                        {
                            getCookie(driver, tinhTP);
                            break;
                        }

                    }
                    catch { }
                    Thread.Sleep(30 * 1000);
                }
            }
        }

        public static void WaitLoginAuto(ChromeDriver driver, BackgroundWorker m_oWorker, string tinhTP)
        {
            var fileCookie = $"cookiesBackup_{tinhTP}.txt";
            var fileBackup = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TCC", fileCookie);

            var targetUrl = "";
            if (tinhTP == "HN")
            {
                targetUrl = "http://hanoi.tinchinhchu.net.vn/";
            }
            else if (tinhTP == "HCM")
            {
                targetUrl = "http://hcm.tinchinhchu.net.vn/";
            }
            driver.Navigate().GoToUrl(targetUrl);
            driver.Manage().Cookies.DeleteAllCookies();
            if (File.Exists(fileBackup))
            {
                var cookiebackUp = File.ReadAllText(fileBackup);
                driver.Manage().Cookies.DeleteAllCookies();
                var cookie = cookiebackUp.Split(';');
                driver.Manage().Cookies.DeleteAllCookies();
                foreach (string ck in cookie)
                {
                    if (ck == "") break;
                    var datacookie = ck.Trim().Split('=');
                    if (datacookie.Length >= 2)
                    {
                        Cookie name = new Cookie(datacookie[0], ck.Trim().Split(new[] { datacookie[0] + '=' }, StringSplitOptions.None)[1]);
                        driver.Manage().Cookies.AddCookie(name);
                    }
                }
            }
            driver.Navigate().GoToUrl(targetUrl);
            Thread.Sleep(3 * 1000);
            while (true)
            {
                m_oWorker.ReportProgress(6, "");
                if (runAuto == false)
                {
                    throw new Exception("Dừng chương trình");
                }

                if (autoLogin)
                {
                    try
                    {

                        var logOut = driver.FindElement(By.XPath("//li[@class='ui-state-default ui-corner-top ui-tabs-selected ui-state-active']"));
                        if (logOut != null)
                        {
                            getCookie(driver, tinhTP);
                            break;
                        }
                    }
                    catch
                    {
                        AutoLogin(driver, tinhTP);
                    }
                    Thread.Sleep(3 * 1000);
                }
                else
                {
                    try
                    {

                        var logOut = driver.FindElement(By.XPath("//li[@class='ui-state-default ui-corner-top ui-tabs-selected ui-state-active']"));
                        if (logOut != null)
                        {
                            getCookie(driver, tinhTP);
                            break;
                        }

                    }
                    catch { }
                    Thread.Sleep(30 * 1000);
                }
            }
        }

        public static void Logout(ChromeDriver driver, BackgroundWorker m_oWorker, string tinhTP)
        {
            var targetUrl = "";
            if (tinhTP == "HN")
            {
                targetUrl = "http://hanoi.tinchinhchu.net.vn/";
            }
            else if (tinhTP == "HCM")
            {
                targetUrl = "http://hcm.tinchinhchu.net.vn/";
            }
            driver.Navigate().GoToUrl(targetUrl);
            Thread.Sleep(3 * 1000);
           
            var logOut = driver.FindElement(By.Id("dnn_dnnLogin_loginLink")); //dnn_dnnLogin_loginLink
            if (logOut != null)
            {
                driver.Navigate ().GoToUrl(logOut.GetAttribute("href"));  
                logOut.Click();
            }
        }


            public static void getCookie(ChromeDriver driver, string tinhTP)
        {
            try
            {
                // Get All available cookies
                var lsCookies = driver.Manage().Cookies.AllCookies;
                cookies = "";

                foreach (var cookie in lsCookies)
                {
                    cookies += cookie.ToString().Split(';')[0] + ";";
                    var fileBackup = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TCC", $"cookiesBackup_{tinhTP}.txt");
                    File.WriteAllText(fileBackup, cookies);
                }
                cookies = "";
            }
            catch (Exception ex)
            {
                cookies = "";

            }
        }

        public static bool AutoLogin(ChromeDriver driver, string tinhTP)
        {
            try
            {
                string user = "";
                string pass = "";
                string targetUrl = "";
                if (tinhTP.Contains("HN"))
                {
                    user = userHN;
                    pass = passHN;
                    targetUrl = "http://hanoi.tinchinhchu.net.vn/";
                }
                else
                {
                    user = userHCM;
                    pass = passHCM;
                    targetUrl = "http://hcm.tinchinhchu.net.vn/";
                }
                try
                {
                    driver.FindElement(By.Id("dnn_ctr545_Login_Login_DNN_txtUsername")).SendKeys(user);
                    Thread.Sleep(1000);
                    driver.FindElement(By.Id("dnn_ctr545_Login_Login_DNN_txtPassword")).SendKeys(pass);
                    Thread.Sleep(1000);
                    try
                    {
                        driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@title='reCAPTCHA']")));
                        driver.FindElement(By.XPath("//div[@class='recaptcha-checkbox-border']")).Click();
                        driver.SwitchTo().ParentFrame();
                    }
                    catch
                    {

                    }
                    Thread.Sleep(1000);
                    driver.FindElement(By.Id("dnn_ctr545_Login_Login_DNN_cmdLogin")).Click();
                    Thread.Sleep(3000);
                    if (driver.Url.Contains("dangnhap"))
                    {
                        throw new Exception();
                    }

                }
                catch
                {
                    driver.Navigate().GoToUrl(targetUrl);
                    Thread.Sleep(3000);
                    driver.FindElement(By.Id("dnn_ctr545_Login_Login_DNN_txtUsername")).SendKeys(user);
                    Thread.Sleep(1000);
                    driver.FindElement(By.Id("dnn_ctr545_Login_Login_DNN_txtPassword")).SendKeys(pass);
                    Thread.Sleep(1000);
                    solveCaptcha(driver);
                }

            }
            catch
            {

            }
            return true;
        }

        public static void solveCaptcha(ChromeDriver driver)
        {
            //driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@title='reCAPTCHA']")));
            //driver.FindElement(By.XPath("//div[@class='recaptcha-checkbox-border']")).Click();
            //driver.SwitchTo().ParentFrame();
            string sitekey = driver.PageSource.Split(new[] { "data-sitekey=\"" }, StringSplitOptions.None)[1].Split('"')[0].Trim();
            // fill response
            string g_response = CaptchaHelper.ResutlByAntiCaptchaTop(CaptchaHelper.apiKeyCaptcha, sitekey, driver.Url);
            if (driver.FindElements(By.Id("g-recaptcha-response")).Count > 0)
            {
                try
                {
                    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                    js.ExecuteScript($"document.getElementById('g-recaptcha-response').innerHTML='{g_response}';");
                    driver.FindElement(By.Id("dnn_ctr545_Login_Login_DNN_cmdLogin")).Click();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                };
            }
        }



        public static string DictionaryToString(Dictionary<string, string> dictionary)
        {
            string dictionaryString = "{";
            foreach (KeyValuePair<string, string> keyValues in dictionary)
            {
                dictionaryString += keyValues.Key + " : " + keyValues.Value + ", ";
            }
            return dictionaryString.TrimEnd(',', ' ') + "}";
        }

        public static void WriteLog(string data)
        {
            var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TCC");
            if (!Directory.Exists(pathStringFolder))
            {
                Directory.CreateDirectory(pathStringFolder);
            }

            var pathSetting = System.IO.Path.Combine(pathStringFolder, "logData.txt");
            File.AppendAllText(pathSetting, data + Environment.NewLine);
        }
    }
}
