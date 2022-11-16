using OpenQA.Selenium.Chrome;
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
using System.Security.Policy;
using static System.Net.Mime.MediaTypeNames;

namespace CrawlWebBDS
{
    public class CrawlNNCC
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

        public static bool layXuoiMode = false;
        public static bool autoLogin = true;


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

                    driver.Navigate().GoToUrl("https://nguonnhachinhchu.vn/danh-muc/nguon-nha-chinh-chu-big-data-bat-dong-san");
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
                                // By.XPath("//span[contains(@class,'smtListName')][contains(text(),'lastname')]");
                                driver.FindElement(By.XPath($"//a[@class='page-link'][contains(text(),'{page.ToString()}')]")).Click();
                                Thread.Sleep(2000);
                            }
                            catch { }
                        }
                        for (int page = toPage; page >= fromPage; page--)
                        {
                            if (runScrape == false)
                            {
                                throw new Exception("Dừng chương trình thủ công");
                            }
                            driver.FindElement(By.XPath($"//a[@class='page-link'][contains(text(),'{page.ToString()}')]")).Click();
                            Thread.Sleep(2000);
                            var lsTagTitle = driver.FindElements(By.XPath($"//a[@class='see-more']"));
                            for (int i = 0; i < lsTagTitle.Count; i++)
                            {
                                if (runScrape == false)
                                {
                                    throw new Exception("Dừng chương trình thủ công");
                                }
                                lsTagTitle[i].Click();
                                Thread.Sleep(1000);
                            }

                            var lsPost = driver.FindElements(By.XPath($"//tr[@role='row']"));
                            for (int i = 1; i < lsPost.Count; i++)
                            {
                                if (runScrape == false)
                                {
                                    throw new Exception("Dừng chương trình thủ công");
                                }
                                var href = "https://nguonnhachinhchu.vn/danh-muc/nguon-nha-chinh-chu-big-data-bat-dong-san" + "_page_" + page.ToString() + "_stt_" + i.ToString();
                                Crawl(m_oWorker, driver, href, lsPost[i], i);
                                var sleepTime = rnd.Next(timeBetweenNewFrom, timeBetweenNewTo);
                                Thread.Sleep(sleepTime * 1000);
                            }
                        }
                    }
                    else
                    {
                        for (int page = fromPage; page <= toPage; page++)
                        {
                            try
                            {
                                driver.FindElement(By.XPath($"//a[@class='page-link'][contains(text(),'{page.ToString()}')]")).Click();
                                Thread.Sleep(2000);
                            }
                            catch { }

                            if (runScrape == false)
                            {
                                throw new Exception("Dừng chương trình thủ công");
                            }
                            driver.FindElement(By.XPath($"//a[@class='page-link'][contains(text(),'{page.ToString()}')]")).Click();
                            Thread.Sleep(2000);
                            var lsTagTitle = driver.FindElements(By.XPath($"//a[@class='see-more']"));
                            for (int i = 0; i < lsTagTitle.Count; i++)
                            {
                                if (runScrape == false)
                                {
                                    throw new Exception("Dừng chương trình thủ công");
                                }
                                lsTagTitle[i].Click();
                                Thread.Sleep(1000);
                            }

                            var lsPost = driver.FindElements(By.XPath($"//tr[@role='row']"));
                            for (int i = 1; i < lsPost.Count; i++)
                            {
                                if (runScrape == false)
                                {
                                    throw new Exception("Dừng chương trình thủ công");
                                }
                                var href = "https://nguonnhachinhchu.vn/danh-muc/nguon-nha-chinh-chu-big-data-bat-dong-san" + "_page_" + page.ToString() + "_stt_" + i.ToString();
                                Crawl(m_oWorker, driver, href, lsPost[i], i);
                                var sleepTime = rnd.Next(timeBetweenNewFrom, timeBetweenNewTo);
                                Thread.Sleep(sleepTime * 1000);
                            }
                        }
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
                    for (int tp = 0; tp < autoTinh.Count; tp++)
                    {
                        var tinhTP = autoTinh[tp];

                        WaitLoginAuto(driver, m_oWorker, tinhTP);

                        driver.Navigate().GoToUrl("https://nguonnhachinhchu.vn/danh-muc/nguon-nha-chinh-chu-big-data-bat-dong-san");
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
                                    // By.XPath("//span[contains(@class,'smtListName')][contains(text(),'lastname')]");
                                    driver.FindElement(By.XPath($"//a[@class='page-link'][contains(text(),'{page.ToString()}')]")).Click();
                                    Thread.Sleep(2000);
                                }
                                catch { }
                            }
                            for (int page = autoToPage; page >= autoFromPage; page--)
                            {
                                if (runAuto == false)
                                {
                                    throw new Exception("Dừng chương trình thủ công");
                                }
                                driver.FindElement(By.XPath($"//a[@class='page-link'][contains(text(),'{page.ToString()}')]")).Click();
                                Thread.Sleep(2000);
                                var lsTagTitle = driver.FindElements(By.XPath($"//a[@class='see-more']"));
                                for (int i = 0; i < lsTagTitle.Count; i++)
                                {
                                    if (runAuto == false)
                                    {
                                        throw new Exception("Dừng chương trình tự động");
                                    }
                                    lsTagTitle[i].Click();
                                    Thread.Sleep(1000);
                                }

                                var lsPost = driver.FindElements(By.XPath($"//tr[@role='row']"));
                                for (int i = 1; i < lsPost.Count; i++)
                                {
                                    if (runAuto == false)
                                    {
                                        throw new Exception("Dừng chương trình tự động");
                                    }
                                    var href = "https://nguonnhachinhchu.vn/danh-muc/nguon-nha-chinh-chu-big-data-bat-dong-san" + "_page_" + page.ToString() + "_stt_" + i.ToString() + "_" + tinhTP;
                                    Crawl(m_oWorker, driver, href, lsPost[i], i);
                                    var sleepTime = rnd.Next(timeBetweenNewFrom, timeBetweenNewTo);
                                    Thread.Sleep(sleepTime * 1000);
                                }
                            }
                        }
                        else
                        {
                            for (int page = autoFromPage; page <= autoToPage; page++)
                            {
                                try
                                {
                                    // By.XPath("//span[contains(@class,'smtListName')][contains(text(),'lastname')]");
                                    driver.FindElement(By.XPath($"//a[@class='page-link'][contains(text(),'{page.ToString()}')]")).Click();
                                    Thread.Sleep(2000);
                                }
                                catch { }

                                if (runAuto == false)
                                {
                                    throw new Exception("Dừng chương trình thủ công");
                                }
                                driver.FindElement(By.XPath($"//a[@class='page-link'][contains(text(),'{page.ToString()}')]")).Click();
                                Thread.Sleep(2000);
                                var lsTagTitle = driver.FindElements(By.XPath($"//a[@class='see-more']"));
                                for (int i = 0; i < lsTagTitle.Count; i++)
                                {
                                    if (runAuto == false)
                                    {
                                        throw new Exception("Dừng chương trình tự động");
                                    }
                                    lsTagTitle[i].Click();
                                    Thread.Sleep(1000);
                                }

                                var lsPost = driver.FindElements(By.XPath($"//tr[@role='row']"));
                                for (int i = 1; i < lsPost.Count; i++)
                                {
                                    if (runAuto == false)
                                    {
                                        throw new Exception("Dừng chương trình tự động");
                                    }
                                    var href = "https://nguonnhachinhchu.vn/danh-muc/nguon-nha-chinh-chu-big-data-bat-dong-san" + "_page_" + page.ToString() + "_stt_" + i.ToString() + "_" + tinhTP;
                                    Crawl(m_oWorker, driver, href, lsPost[i], i);
                                    var sleepTime = rnd.Next(timeBetweenNewFrom, timeBetweenNewTo);
                                    Thread.Sleep(sleepTime * 1000);
                                }
                            }
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

        public static void Crawl(BackgroundWorker m_oWorker, ChromeDriver driver, string url, IWebElement elePostInfo, int index)
        {
            try
            {
                var newUrl = url;
                var title = elePostInfo.FindElements(By.TagName("a"))[0].Text.Trim();
                var price = driver.FindElements(By.XPath("//td[@class=' font-weight text-center']"))[index - 1].Text.Trim();
                var type = elePostInfo.FindElements(By.TagName("div"))[1].Text.Trim().Split(':')[1].Split('-')[0];
                var province = driver.FindElement(By.Id("select2-city_id-container")).Text;
                var district = elePostInfo.FindElements(By.XPath("//td[@class=' text-center']"))[index - 1].Text.Trim();
                var ward = elePostInfo.FindElements(By.XPath("//td[@class=' text-center']"))[index - 1].Text.Trim();
                var createDate = elePostInfo.FindElements(By.TagName("b"))[1].Text.Trim();
                var category = elePostInfo.FindElements(By.TagName("div"))[1].Text.Trim().Split(':')[1].Split('-')[0];
                var area = elePostInfo.FindElements(By.TagName("div"))[1].FindElements(By.TagName("div"))[0].Text.Trim();
                var address = elePostInfo.FindElements(By.TagName("div"))[1].FindElements(By.TagName("div"))[2].Text.Trim();
                var content = elePostInfo.FindElements(By.XPath("//div[@class='mt-1']"))[index - 1].Text.Trim().Replace("\r\n", "<br>");
                content += "<br>" + address;
                var phone = elePostInfo.FindElements(By.XPath("//table[@class='table table-bordered']"))[index - 1].FindElements(By.TagName("td"))[2].Text.Trim().Split('\r')[0];
                content += "<br>" + "sđt: " + phone;
                var oner = elePostInfo.FindElements(By.XPath("//table[@class='table table-bordered']"))[index - 1].FindElements(By.TagName("td"))[2].Text.Split('\r')[1].Trim();
                content += "<br>" + "tên: " + oner;

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
                var bodyArea = $"\"area\":\"{area}\",";
                var bodyOner = $"\"oner\":\"{oner}\",";
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
                            var body = "[{" + bodyUrl + bodyAddress + bodyCategory + bodyTitle + bodyContent + bodyPrice + bodyPhone + bodyCreateDate + bodyType + bodyProvince + bodyWard + bodyArea + bodyOner + bodyDistrict + "}]";
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

        public static void WaitLoginScrape(ChromeDriver driver, BackgroundWorker m_oWorker, string tinhTP)
        {
            var fileCookie = $"cookiesBackup_{tinhTP}.txt";
            var fileBackup = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NNCC", fileCookie);

            driver.Navigate().GoToUrl("https://nguonnhachinhchu.vn/");
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
            driver.Navigate().GoToUrl("https://nguonnhachinhchu.vn/");
            while (true)
            {
                m_oWorker.ReportProgress(6, "");
                if (runScrape == false)
                {
                    throw new Exception("Dừng chương trình");
                }
                try
                {
                    var logOut = driver.FindElement(By.XPath("//i[@class='fas fa-sign-out-alt']"));
                    if (logOut != null)
                    {
                        getCookie(driver, tinhTP);
                        break;
                    }
                }
                catch
                {
                    if (autoLogin)
                        AutoLogin(driver, tinhTP);
                }
                Thread.Sleep(2 * 1000);
            }
        }

        public static void WaitLoginAuto(ChromeDriver driver, BackgroundWorker m_oWorker, string tinhTP)
        {
            var fileCookie = $"cookiesBackup_{tinhTP}.txt";
            var fileBackup = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NNCC", fileCookie);

            driver.Navigate().GoToUrl("https://nguonnhachinhchu.vn/");
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
            driver.Navigate().GoToUrl("https://nguonnhachinhchu.vn/");
            while (true)
            {
                m_oWorker.ReportProgress(6, "");
                if (runAuto == false)
                {
                    throw new Exception("Dừng chương trình");
                }
                try
                {
                    var logOut = driver.FindElement(By.XPath("//i[@class='fas fa-sign-out-alt']"));
                    if (logOut != null)
                    {
                        getCookie(driver, tinhTP);
                        break;
                    }
                }
                catch
                {
                    if (autoLogin)
                        AutoLogin(driver, tinhTP);
                }
                Thread.Sleep(2 * 1000);
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
                    var fileBackup = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NNCC", $"cookiesBackup_{tinhTP}.txt");
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
                if (tinhTP.Contains("HN"))
                {
                    user = userHN;
                    pass = passHN;
                }
                else
                {
                    user = userHCM;
                    pass = passHCM;
                }
                driver.FindElements(By.TagName("li"))[1].Click();
                Thread.Sleep(2000);
                driver.FindElement(By.XPath("//input[@name='account']")).SendKeys(user);
                Thread.Sleep(1000);
                driver.FindElements(By.XPath("//div[@class='form-group input-group w-80']"))[7].Click();
                var x = driver.FindElements(By.Name("password"));
                Thread.Sleep(1000);
                x[1].SendKeys(pass);
                driver.FindElements(By.XPath("//input[@class='btn btn-color-logo btn-block btn-lg']"))[1].Click();
                Thread.Sleep(3000);
            }
            catch
            {

            }
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

        public static void WriteLog(string data)
        {
            var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NNCC");
            if (!Directory.Exists(pathStringFolder))
            {
                Directory.CreateDirectory(pathStringFolder);
            }

            var pathSetting = System.IO.Path.Combine(pathStringFolder, "logData.txt");
            File.AppendAllText(pathSetting, data + Environment.NewLine);
        }
    }
}
