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

namespace CrawlWebBDS
{
    public class CrawlNNM
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

                List<string> lsHref = new List<string>();

                driver = new ChromeDriver(service, options);
                for (int tp = 0; tp < tinh.Count; tp++)
                {
                    var tinhTP = tinh[tp];
                    WaitLoginScrape(driver, m_oWorker, tinhTP);
                    var targetUrl = "";
                    if (tinhTP == "HN")
                    {
                        targetUrl = "https://nguonnhamoi.com/ban-nha-dat-theo-quan";
                    }
                    else if (tinhTP == "HCM")
                    {
                        targetUrl = "https://nguonnhasaigon.com/ban-nha-dat-theo-quan";
                    }
                    driver.Navigate().GoToUrl(targetUrl);
                    Thread.Sleep(2000);

                    if (!layXuoiMode)
                    {
                        for (int page = toPage; page >= fromPage; page--)
                        {
                            if (runScrape == false)
                            {
                                throw new Exception("Dừng chương trình thủ công");
                            }
                            try
                            {
                                driver.Navigate().GoToUrl(targetUrl + "/page/" + page);
                                Thread.Sleep(1000);
                                var lsItems = driver.FindElements(By.XPath("//div[@class='col post-item']"));
                                foreach (var item in lsItems)
                                {
                                    var href = item.FindElement(By.TagName("a")).GetAttribute("href");
                                    lsHref.Add(href);
                                }
                            }
                            catch (Exception ex) { }
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
                                driver.Navigate().GoToUrl(targetUrl + "/page/" + page);
                                Thread.Sleep(1000);
                                var lsItems = driver.FindElements(By.XPath("//div[@class='col post-item']"));
                                foreach (var item in lsItems)
                                {
                                    var href = item.FindElement(By.TagName("a")).GetAttribute("href");
                                    lsHref.Add(href);
                                }
                            }
                            catch (Exception ex) { }
                        }
                    }
                    foreach (var href in lsHref)
                    {
                        if (runScrape == false)
                        {
                            throw new Exception("Dừng chương trình thủ công");
                        }
                        Crawl(m_oWorker, driver, href, tinhTP);
                        var sleepTime = rnd.Next(timeBetweenNewFrom, timeBetweenNewTo);
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

                    List<string> lsHref = new List<string>();

                    driver = new ChromeDriver(service, options);
                    for (int tp = 0; tp < autoTinh.Count; tp++)
                    {
                        var tinhTP = autoTinh[tp];
                        WaitLoginAuto(driver, m_oWorker, tinhTP);
                        var targetUrl = "";
                        if (tinhTP == "HN")
                        {
                            targetUrl = "https://nguonnhamoi.com/ban-nha-dat-theo-quan";
                        }
                        else if (tinhTP == "HCM")
                        {
                            targetUrl = "https://nguonnhasaigon.com/ban-nha-dat-theo-quan";
                        }
                        driver.Navigate().GoToUrl(targetUrl);
                        Thread.Sleep(2000);

                        if (!layXuoiMode)
                        {
                            for (int page = autoToPage; page >= autoFromPage; page--)
                            {
                                if (runAuto == false)
                                {
                                    throw new Exception("Dừng chương trình thủ công");
                                }
                                try
                                {
                                    driver.Navigate().GoToUrl(targetUrl + "/page/" + page);
                                    Thread.Sleep(1000);
                                    var lsItems = driver.FindElements(By.XPath("//div[@class='col post-item']"));
                                    foreach (var item in lsItems)
                                    {
                                        var href = item.FindElement(By.TagName("a")).GetAttribute("href");
                                        lsHref.Add(href);
                                    }
                                }
                                catch (Exception ex) { }
                            }
                        }
                        else
                        {
                            for (int page = autoFromPage; page <= autoToPage; page++)
                            {
                                if (runAuto == false)
                                {
                                    throw new Exception("Dừng chương trình thủ công");
                                }
                                try
                                {
                                    driver.Navigate().GoToUrl(targetUrl + "/page/" + page);
                                    Thread.Sleep(1000);
                                    var lsItems = driver.FindElements(By.XPath("//div[@class='col post-item']"));
                                    foreach (var item in lsItems)
                                    {
                                        var href = item.FindElement(By.TagName("a")).GetAttribute("href");
                                        lsHref.Add(href);
                                    }
                                }
                                catch (Exception ex) { }
                            }
                        }

                        foreach (var href in lsHref)
                        {
                            if (runAuto == false)
                            {
                                throw new Exception("Dừng chương trình tự động");
                            }
                            Crawl(m_oWorker, driver, href, tinhTP);
                            var sleepTime = rnd.Next(timeBetweenNewFrom, timeBetweenNewTo);
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

        public static void Crawl(BackgroundWorker m_oWorker, ChromeDriver driver, string url, string tinhTP)
        {
            try
            {
                //url = "https://nguonnhasaigon.com/ban-50-1m2-dat-duong-la-xuan-oai-thu-duc-gia-3-ty.html";
                driver.Navigate().GoToUrl(url);
                Thread.Sleep(1000);
                var newUrl = url;
                var title = driver.FindElement(By.XPath("//h1[@class='entry-title']")).Text.Trim();
                foreach(var rmWord in Contances.lsRemoveWordsNNM)
                {
                    if (title.Contains(rmWord)) Contances.bRMWordsNNM = true;
                    title = title.Replace(rmWord, "");
                }

                var price = "";
                var province = tinhTP;
                var check = driver.FindElement(By.XPath("//h6[@class='entry-category is-xsmall']")).FindElements(By.TagName("a"))[2].Text;
                var district = "";
                var type = "";
                if (check.Contains("DƯỚI") || check.Contains("TRÊN"))
                {
                    type = check;
                    district = driver.FindElement(By.XPath("//h6[@class='entry-category is-xsmall']")).FindElements(By.TagName("a"))[1].Text.Replace("NHÀ ĐẤT ", "");
                }
                else
                {
                    type = driver.FindElement(By.XPath("//h6[@class='entry-category is-xsmall']")).FindElements(By.TagName("a"))[1].Text;
                    district = check.Replace("NHÀ ĐẤT ", "");
                }
                

                
                var category = driver.FindElement(By.XPath("//h6[@class='entry-category is-xsmall']")).FindElements(By.TagName("a"))[0].Text; ;
                var phone = "";
                var address = "";
                var createDate = driver.FindElement(By.XPath("//time[@class='entry-date published updated']")).Text.Trim();
                var content = driver.FindElement(By.XPath("//div[@class='entry-content single-page']")).Text.Trim().Replace("\r\n", "<br>");
                content = content.Split(new string[] { "<br><br><br>" }, StringSplitOptions.None)[0];
                var eleContent = content.Split(new string[] { "<br>" }, StringSplitOptions.None);
                foreach (var ele in eleContent)
                {
                    if (ele.Contains("Giá chào"))
                    {
                        price = ele.Split(':')[1];
                    }
                    else if (ele.Contains("Địa chỉ"))
                    {
                        address = ele.Split(':')[1];
                    }
                    else if (ele.Contains("Liên hệ"))
                    {
                        phone = ele.Split(':')[1];
                    }
                }


                foreach (var rmWord in Contances.lsRemoveWordsNNM)
                {
                    if (content.Contains(rmWord)) Contances.bRMWordsNNM = true;
                    content = content.Replace(rmWord, "");
                }

                var bodyUrl = $"\"url\":\"{newUrl}\",";
                var bodyTitle = $"\"title\":\"{title.Split('-')[0]}\",";
                var bodyContent = $"\"content\":\"{content.Trim()}\",";
                var bodyPrice = $"\"price\":\"{price}\",";
                var bodyCreateDate = $"\"createDate\":\"{createDate}\",";
                var bodyProvince = $"\"province\":\"{province}\",";
                var bodyPhone = $"\"phone\":\"{phone}\",";
                var bodyType = $"\"type\":\"{type}\",";
                var bodyAddress = $"\"address\":\"{address}\",";
                var bodyCategory = $"\"category\":\"{category}\",";
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
                            var body = "[{" + bodyUrl + bodyProvince + bodyTitle + bodyContent + bodyPrice + bodyCreateDate + bodyPhone + bodyType + bodyCategory + bodyAddress + bodyDistrict + "}]";
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
                            var body = "[{" + bodyUrl + bodyProvince + bodyTitle + bodyContent + bodyPrice + bodyCreateDate + bodyPhone + bodyType + bodyCategory + bodyAddress + bodyDistrict + "}]";
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
                //try
                //{
                //    driver.SwitchTo().ParentFrame();
                //    Thread.Sleep(1000);
                //    driver.FindElement(By.Id("fancybox-close")).Click();
                //}
                //catch (Exception ex) { }
            }

        }

        public static void WaitLoginScrape(ChromeDriver driver, BackgroundWorker m_oWorker, string tinhTP)
        {
            var fileCookie = $"cookiesBackup_{tinhTP}.txt";
            var fileBackup = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TCC", fileCookie);
            var targetUrl = "";
            if (tinhTP == "HN")
            {
                targetUrl = "https://nguonnhamoi.com/";
            }
            else if (tinhTP == "HCM")
            {
                targetUrl = "https://nguonnhasaigon.com/";
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
                        var logOut = driver.FindElement(By.Id("wpadminbar"));
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
                        var logOut = driver.FindElement(By.Id("wpadminbar"));
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
                targetUrl = "https://nguonnhamoi.com/";
            }
            else if (tinhTP == "HCM")
            {
                targetUrl = "https://nguonnhasaigon.com/";
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

                        var logOut = driver.FindElement(By.Id("wpadminbar"));
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

                        var logOut = driver.FindElement(By.Id("wpadminbar"));
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
            string user = "";
            string pass = "";
            string targetUrl = "";
            if (tinhTP.Contains("HN"))
            {
                user = userHN;
                pass = passHN;
                targetUrl = "https://nguonnhamoi.com/thanh-vien-dang-nhap";
            }
            else
            {
                user = userHCM;
                pass = passHCM;
                targetUrl = "https://nguonnhasaigon.com/thanh-vien-dang-nhap";
            }
            try
            {
                driver.Navigate().GoToUrl(targetUrl);
                Thread.Sleep(2 * 1000);
                driver.FindElement(By.Name("user_login")).SendKeys(user);
                Thread.Sleep(1000);
                driver.FindElement(By.Name("user_pass")).SendKeys(pass);
                Thread.Sleep(1000);

                driver.FindElement(By.Name("armFormSubmitBtn")).Click();
                Thread.Sleep(3000);
                if (!driver.Url.Contains("the-thanh-vien"))
                {
                    throw new Exception();
                }

            }
            catch
            {
                return false;
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
