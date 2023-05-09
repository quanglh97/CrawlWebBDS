using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace CrawlWebBDS
{
    public class CrawlNCC
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
        public static bool runningAuto = false;


        public static Random rnd = new Random();

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
                if (cookies != null && cookies != "")
                {
                    driver.Navigate().GoToUrl("https://nguonchinhchu.com/login.html");
                    driver.Manage().Cookies.DeleteAllCookies();
                    var cookie = cookies.Split(';');

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
                    driver.Navigate().GoToUrl("https://nguonchinhchu.com/login.html");
                }
                WaitLoginScrape(driver, m_oWorker);


                for (int page = toPage-1; page >= fromPage-1; page--)
                {
                    string url = $"https://nguonchinhchu.com/bds.html?pi=" + page.ToString();
                    driver.Navigate().GoToUrl(url);
                    Thread.Sleep(3 * 1000);
                    var lsTagTitle = driver.FindElements(By.ClassName("fancybox"));

                    for (int i = 0; i < lsTagTitle.Count; i++)
                    {
                        if (runScrape == false)
                        {
                            throw new Exception("Dừng chương trình thủ công");
                        }
                        var href = lsTagTitle[i].GetAttribute("onclick").Split(new[] { "'" }, StringSplitOptions.None)[1];
                        lsTagTitle[i].Click();
                        Thread.Sleep(1000);
                        var sleepTime = rnd.Next(timeBetweenNewFrom, timeBetweenNewTo);
                        Thread.Sleep(2 * 1000);
                        //Task.Run(() =>
                        //{
                        //    Crawl(m_oWorker, driver, href);
                        //});
                        Crawl(m_oWorker, driver, href);
                        driver.FindElement(By.Id("bds-quickview")).FindElement(By.XPath("//button[@class='close']")).Click();
                        Thread.Sleep(sleepTime * 1000);
                    }
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
                    if (cookies != null && cookies != "")
                    {
                        driver.Navigate().GoToUrl("https://nguonchinhchu.com/login.html");
                        driver.Manage().Cookies.DeleteAllCookies();
                        var cookie = cookies.Split(';');

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
                        driver.Navigate().GoToUrl("https://nguonchinhchu.com/login.html");
                    }
                    WaitLoginAuto(driver, m_oWorker);
                    for (int page = autoToPage - 1; page >= autoFromPage - 1; page--)
                    {
                        string url = $"https://nguonchinhchu.com/bds.html?pi=" + page.ToString();
                        driver.Navigate().GoToUrl(url);
                        Thread.Sleep(3 * 1000);
                        var lsTagTitle = driver.FindElements(By.ClassName("fancybox"));

                        for (int i = 0; i < lsTagTitle.Count; i++)
                        {
                            if (runAuto == false)
                            {
                                throw new Exception("Dừng chương trình tự động");
                            }
                            var href = lsTagTitle[i].GetAttribute("onclick").Split(new[] { "'" }, StringSplitOptions.None)[1];
                            lsTagTitle[i].Click();
                            Thread.Sleep(1000);
                            var sleepTime = rnd.Next(timeBetweenNewFrom, timeBetweenNewTo);
                            Thread.Sleep(2 * 1000);
                            //Task.Run(() =>
                            //{
                            //    Crawl(m_oWorker, driver, href);
                            //});

                            Crawl(m_oWorker, driver, href);
                            driver.FindElement(By.Id("bds-quickview")).FindElement(By.XPath("//button[@class='close']")).Click();
                            Thread.Sleep(sleepTime * 1000);
                        }
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
                var newUrl = driver.Url + "/id=" + url;
                var elePostInfo = driver.FindElement(By.Id("bds-quickview"));
                var title = elePostInfo.FindElements(By.XPath("//div[@class='bds-body']"))[0].FindElement(By.TagName("span")).Text.Trim();

                var price = elePostInfo.FindElements(By.XPath("//div[@class='col-sm-4']"))[0].FindElements(By.TagName("span"))[0].Text.Trim();
                var type = "Tin mới";
                var province = elePostInfo.FindElements(By.XPath("//div[@class='col-sm-4']"))[0].FindElements(By.TagName("span"))[5].Text.Trim();
                var district = elePostInfo.FindElements(By.XPath("//div[@class='col-sm-4']"))[0].FindElements(By.TagName("span"))[6].Text.Trim();
                var ward = elePostInfo.FindElements(By.XPath("//div[@class='col-sm-4']"))[0].FindElements(By.TagName("span"))[7].Text.Trim();
                var createDate = elePostInfo.FindElements(By.XPath("//div[@class='col-sm-4']"))[0].FindElements(By.TagName("span"))[3].Text.Trim();
                var category = elePostInfo.FindElements(By.XPath("//div[@class='col-sm-4']"))[0].FindElements(By.TagName("span"))[4].Text.Trim();
                var lsPContent = elePostInfo.FindElements(By.XPath("//div[@class='bds-body']"))[0].FindElements(By.TagName("li"));
                var content = "";
                foreach (var li in lsPContent)
                {
                    content += li.Text.Trim() + "<br>";
                }
                content = content.Replace("\r\n", "<br>").Split(new string[] { "Ngày " }, StringSplitOptions.None)[0];
                var address = elePostInfo.FindElements(By.XPath("//div[@class='col-sm-4']"))[0].FindElements(By.TagName("span"))[8].Text.Trim();
                content += "<br>" + "Địa chỉ: " + address;
                var phone = elePostInfo.FindElements(By.XPath("//div[@class='col-sm-4']"))[0].FindElements(By.TagName("span"))[2].Text.Trim();

                var bodyUrl = $"\"url\":\"{newUrl}\",";
                var bodyTitle = $"\"title\":\"{title.Split('-')[0]}\",";
                var bodyContent = $"\"content\":\"{content.Trim()}\",";
                //var bodyContent = $"\"content\":\"tesst\",";
                var bodyPrice = $"\"price\":\"{price}\",";
                var bodyCreateDate = $"\"createDate\":\"{createDate}\",";
                var bodyType = $"\"type\":\"{type}\",";
                var bodyCategory = $"\"category\":\"{category}\",";
                var bodyAddress = $"\"address\":\"{address}\",";
                var bodyPhone = $"\"phone\":\"{phone}\",";
                var bodyProvince = $"\"province\":\"{province}\",";
                var bodyWard = $"\"ward\":\"{ward}\",";
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
                            //var body = sendData;// "[" + DictionaryToString(data) + "]" ;
                            var body = "[{" + bodyUrl + bodyAddress + bodyCategory + bodyTitle + bodyContent + bodyPrice + bodyPhone + bodyCreateDate + bodyType + bodyProvince + bodyWard + bodyDistrict + "}]";
                            //Console.WriteLine(body);
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
                            //var body = sendData;// "[" + DictionaryToString(data) + "]" ;
                            var body = "[{" + bodyUrl + bodyAddress + bodyCategory + bodyTitle + bodyContent + bodyPrice + bodyPhone + bodyCreateDate + bodyType + bodyProvince + bodyDistrict + "}]";
                            //Console.WriteLine(body);
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

            catch { }

        }

        public static void WaitLoginScrape(ChromeDriver driver, BackgroundWorker m_oWorker)
        {
            var fileBackup = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NCC", "cookiesBackup.txt");
            if (File.Exists(fileBackup))
            {
                driver.Navigate().GoToUrl("https://nguonchinhchu.com/login.html");
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
            driver.Navigate().GoToUrl("https://nguonchinhchu.com/login.html");
            while (true)
            {
                m_oWorker.ReportProgress(6, "");
                if (runScrape == false)
                {
                    throw new Exception("Dừng chương trình");
                }
                try
                {
                    
                    if (!driver.Url.Contains("login"))
                    {
                        getCookie(driver);
                        break;
                    }
                }
                catch
                {
                }
                Thread.Sleep(2 * 1000);
            }
        }

        public static void WaitLoginAuto(ChromeDriver driver, BackgroundWorker m_oWorker)
        {
            var fileBackup = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NCC", "cookiesBackup.txt");
            if (File.Exists(fileBackup))
            {
                driver.Navigate().GoToUrl("https://nguonchinhchu.com/login.html");
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
            driver.Navigate().GoToUrl("https://nguonchinhchu.com/login.html");
            while (true)
            {
                m_oWorker.ReportProgress(6, "");
                if (runAuto == false)
                {
                    throw new Exception("Dừng chương trình");
                }

                try
                {
                    if (!driver.Url.Contains("login"))
                    {
                        getCookie(driver);
                        break;
                    }
                }
                catch
                {
                }
                Thread.Sleep(2 * 1000);
            }
        }

        public static void getCookie(ChromeDriver driver)
        {
            try
            {
                // Get All available cookies
                var lsCookies = driver.Manage().Cookies.AllCookies;
                cookies = "";

                foreach (var cookie in lsCookies)
                {
                    cookies += cookie.ToString().Split(';')[0] + ";";
                    var fileBackup = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NCC", "cookiesBackup.txt");
                    File.WriteAllText(fileBackup, cookies);
                }
                cookies = "";
            }
            catch (Exception ex)
            {
                cookies = "";

            }
        }

        public static bool CheckLogin(ChromeDriver driver)
        {
            return true;
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
    }
}
