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
using System.Web.UI.WebControls;
using System.Security.Policy;
using System.Linq.Expressions;

namespace CrawlWebBDS
{
    public class CrawlSALENHA_VIP
    {
        public static string cookies_ban;
        public static string cookies_thue;
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

        public static bool crawl_ban_salenha_vip = false;
        public static bool crawl_thue_salenha_vip = false;

        public static bool auto_crawl_ban_salenha_vip = false;
        public static bool auto_crawl_thue_salenha_vip = false;

        public static string userHN;
        public static string passHN;
        public static string userHCM;
        public static string passHCM;

        //public static BackgroundWorker m_oWorker = null;



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
                options.AddUserProfilePreference("profile.default_content_setting_values.images", 2);

                ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                service.HideCommandPromptWindow = true;
                
                driver = new ChromeDriver(service, options);
                List<int> lsType = new List<int>();
                if (crawl_ban_salenha_vip)
                    lsType.Add(1);
                if (crawl_thue_salenha_vip)
                    lsType.Add(2);
                for (int tp = 0; tp < tinh.Count; tp++)
                {
                    var tinhTP = tinh[tp];
                    for (int j = 0; j < lsType.Count; j++)
                    {
                        var type = lsType[j];
                        var cookies = "";
                        var url = "";
                        cookies = cookies_ban;
                        url = "https://nhachinhchu.com.vn/";

                        if (cookies != null && cookies != "")
                        {
                            driver.Navigate().GoToUrl(url);
                            driver.Manage().Cookies.DeleteAllCookies();
                            var cookie = cookies.Split(';');
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
                            driver.Navigate().GoToUrl(url);
                        }
                        WaitLoginScrape(driver, m_oWorker, type, tinhTP);
                        if(type ==1)
                            driver.Navigate().GoToUrl("https://nhachinhchu.com.vn/product/list/100");
                        else if (type ==2)
                            driver.Navigate().GoToUrl("https://nhachinhchu.com.vn/product/list/200");
                        Thread.Sleep(2000);
                        for (int page = toPage; page >= fromPage; page--)
                        {
                            try
                            {
                                driver.FindElement(By.XPath($"//a[@href='javascript:JumpTo({page})']")).Click();
                            }
                            catch
                            {
                            }

                            Thread.Sleep(3000);
                            var lsViewMoreBtn = driver.FindElements(By.XPath("//p[@class='pdt']"));
                            for (int i = 0; i < lsViewMoreBtn.Count; i++)
                            {
                                if (CrawlSALENHA_VIP.runScrape == false) return;
                                try
                                {
                                    var x = rnd.Next(700, 1800);
                                    lsViewMoreBtn[i].Click();
                                    Thread.Sleep(x);
                                }
                                catch (Exception e){ 
                                }
                            }




                            var lsTr = driver.FindElements(By.XPath("//tr[@class='even']")).ToList();
                            lsTr.AddRange(driver.FindElements(By.XPath("//tr[@class='odd']")).ToList());
                            foreach (var tr in lsTr)
                            {
                                if (CrawlSALENHA_VIP.runScrape == false) return;
                                Crawl(m_oWorker, driver, tr);
                                Thread.Sleep(5000);
                            }


                            var sleepTime = rnd.Next(timeBetweenNewFrom, timeBetweenNewTo);
                            Thread.Sleep(sleepTime * 1000);
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
                    options.AddUserProfilePreference("profile.default_content_setting_values.images", 2);

                    ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                    service.HideCommandPromptWindow = true;

                    driver = new ChromeDriver(service, options);
                    List<int> lsType = new List<int>();
                    if (auto_crawl_ban_salenha_vip)
                        lsType.Add(1);
                    if (auto_crawl_thue_salenha_vip)
                        lsType.Add(2);
                    for (int tp = 0; tp < autoTinh.Count; tp++)
                    {
                        var tinhTP = autoTinh[tp];
                        for (int i = 0; i < lsType.Count; i++)
                        {
                            var type = lsType[i];
                            var cookies = "";
                            var url = "";
                           
                            cookies = cookies_ban;
                            url = "https://nhachinhchu.com.vn/";


                            if (cookies != null && cookies != "")
                            {
                                driver.Navigate().GoToUrl(url);
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
                                driver.Navigate().GoToUrl(url);
                            }
                            WaitLoginAuto(driver, m_oWorker, type, tinhTP);
                            if (type == 1)
                                driver.Navigate().GoToUrl("https://nhachinhchu.com.vn/product/list/100");
                            else if (type == 2)
                                driver.Navigate().GoToUrl("https://nhachinhchu.com.vn/product/list/200");
                            Thread.Sleep(2000);
                            for (int page = autoToPage; page >= autoFromPage; page--)
                            {
                                try
                                {
                                    driver.FindElement(By.XPath($"//a[@href='javascript:JumpTo({page})']")).Click();
                                }
                                catch
                                {
                                }

                                Thread.Sleep(3000);
                                var lsViewMoreBtn = driver.FindElements(By.XPath("//p[@class='pdt']"));
                                for (int j = 0; j < lsViewMoreBtn.Count; j++)
                                {
                                    if (CrawlSALENHA_VIP.runAuto == false) return;
                                    try
                                    {
                                        var x = rnd.Next(700, 1800);
                                        lsViewMoreBtn[j].Click();
                                        Thread.Sleep(x);
                                    }
                                    catch (Exception e)
                                    {
                                    }
                                }

                                var lsTr = driver.FindElements(By.XPath("//tr[@class='even']")).ToList();
                                lsTr.AddRange(driver.FindElements(By.XPath("//tr[@class='odd']")).ToList());
                                foreach (var tr in lsTr)
                                {
                                    if (CrawlSALENHA_VIP.runAuto == false) return;
                                    Crawl(m_oWorker, driver, tr);
                                    Thread.Sleep(5000);
                                }
                                var sleepTime = rnd.Next(timeBetweenNewFrom, timeBetweenNewTo);
                                Thread.Sleep(sleepTime * 1000);
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

        public static void Crawl(BackgroundWorker m_oWorker, ChromeDriver driver, IWebElement Data)
        {
            try
            {
                var rawData = Data.Text;
                var content = "";
                var price = "";
                var area = "";
                var category = driver.FindElement(By.Id("Cat")).FindElement(By.XPath("//option[@selected='selected']")).Text;
                var phone = "";
                var address = "";
                var province = driver.FindElement(By.Id("City")).Text;
                var ward = "";
                var district = "";
                var title = "";
                var eleContent = rawData.Split(new[] { "\n" }, StringSplitOptions.None);
                var createDate = eleContent[0];
                var type = eleContent[1];
                title = eleContent[2];

                for (int i = 3; i < eleContent.Length; i++)
                {
                    content += eleContent[i] + "\n";
                    if (eleContent[i].Contains("Khu vực"))
                    {
                        district = eleContent[i].Split(new[] { "Khu vực" }, StringSplitOptions.None)[1].Trim();
                    }
                    else if (eleContent[i].Contains("KV:"))
                    {
                        return;
                        //district += eleContent[i].Split(new[] { "KV:" }, StringSplitOptions.None)[1].Trim().Split(new[] { "LH" }, StringSplitOptions.None)[0].Trim() + "\n";
                        //try
                        //{
                        //    phone += eleContent[i].Split(new[] { "LH:" }, StringSplitOptions.None)[1].Trim() + "\n";
                        //}
                        //catch
                        //{

                        //}
                    }else if (eleContent[i].Contains("Liên hệ") && !eleContent[i].Contains("@"))
                    {
                        phone = eleContent[i].Split(new[] { "Liên hệ" }, StringSplitOptions.None)[1].Substring(0, 11).Trim();
                    }
                    else if (eleContent[i].Contains("Giá"))
                    {
                        price = eleContent[i].Split(new[] { "Giá" }, StringSplitOptions.None)[1].Trim();
                    }
                    else if (eleContent[i].Contains("Diện tích"))
                    {
                        area = eleContent[i].Split(new[] { "Diện tích" }, StringSplitOptions.None)[1].Trim();
                    }
                    else
                    {
                        
                    }
                }

                content = content.Split(new[] { "URL" }, StringSplitOptions.None)[0];

                var bodyTitle = $"\"title\":\"{title.Split('-')[0].Replace("\n", "").Replace("\r", "")}\",";
                var bodyContent = $"\"content\":\"{content.Trim().Replace("\n", "").Replace("\r", "<br>")}\",";
                var bodyPrice = $"\"price\":\"{price.Replace("\n", "").Replace("\r", "<br>")}\",";
                var bodyArea = $"\"area\":\"{area.Replace("\n", "").Replace("\r", "<br>")}\",";
                var bodyCreateDate = $"\"createDate\":\"{createDate.Replace("\n", "").Replace("\r", "<br>")}\",";
                var bodyType = $"\"type\":\"{type.Replace("\n", "").Replace("\r", "<br>")}\",";
                var bodyCategory = $"\"category\":\"{category.Replace("\n", "").Replace("\r", "<br>")}\",";
                var bodyAddress = $"\"address\":\"{address.Replace("\n", "").Replace("\r", "<br>")}\",";
                var bodyPhone = $"\"phone\":\"{phone.Replace("\n", "").Replace("\r", "<br>")}\",";
                var bodyProvince = $"\"province\":\"{province.Replace("\n", "").Replace("\r", "<br>")}\",";
                var bodyWard = $"\"ward\":\"{ward.Replace("\n", "").Replace("\r", "<br>")}\",";
                var bodyDistrict = $"\"district\":\"{district.Replace("\n", "").Replace("\r", "<br>")}\"";

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
                            var body = "[{" + bodyAddress + bodyArea + bodyCategory + bodyTitle + bodyContent + bodyPrice + bodyPhone + bodyCreateDate + bodyType + bodyProvince + bodyWard + bodyDistrict + "}]";
                            //Console.WriteLine(body);
                            body = body.Replace("<br>", "<p>");
                            request.AddParameter("application/json", body, ParameterType.RequestBody);
                            IRestResponse response = client.Execute(request);
                            Console.WriteLine(response.Content);
                            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                m_oWorker.ReportProgress(0, bodyTitle);
                            }
                            else
                            {
                                m_oWorker.ReportProgress(1, bodyTitle);
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
                            var body = "[{" + bodyAddress + bodyArea + bodyCategory + bodyTitle + bodyContent + bodyPrice + bodyPhone + bodyCreateDate + bodyType + bodyProvince + bodyDistrict + "}]";
                            //Console.WriteLine(body);
                            request.AddParameter("application/json", body, ParameterType.RequestBody);
                            IRestResponse response = client.Execute(request);
                            Console.WriteLine(response.Content);
                            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                m_oWorker.ReportProgress(3, bodyTitle);
                            }
                            else
                            {
                                m_oWorker.ReportProgress(4, bodyTitle);
                            }
                        }
                    }
                    catch
                    {
                    }
                });
            }
            catch { }

        }


        public static void WaitLoginScrape(ChromeDriver driver, BackgroundWorker m_oWorker, int type, string tinhTP)
        {
            var url = "https://nhachinhchu.com.vn/";
            var fileCookie = $"cookiesBackup_{tinhTP}.txt";
            
            var fileBackup = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NHACHINHCHU_COM_VN", fileCookie);
            if (File.Exists(fileBackup))
            {
                driver.Navigate().GoToUrl(url);
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
            driver.Navigate().GoToUrl("https://nhachinhchu.com.vn/product");

            while (true)
            {
                m_oWorker.ReportProgress(6, "");
                Thread.Sleep(3000);
                try
                {
                    var logOut = driver.FindElement(By.Id("logoutForm"));
                    if (logOut == null)
                    {
                        AutoLogin(driver, tinhTP);
                    }
                }
                catch { AutoLogin(driver, tinhTP); }

                if (runScrape == false)
                {
                    throw new Exception("Dừng chương trình");
                }
                try
                {
                    var logOut = driver.FindElement(By.Id("logoutForm"));
                    getCookie(driver, type, tinhTP);
                    break;
                }
                catch
                {
                    Thread.Sleep(2000);
                    continue;

                }

            }
        }

        public static void WaitLoginAuto(ChromeDriver driver, BackgroundWorker m_oWorker, int type, string tinhTP)
        {
            var url = "https://nhachinhchu.com.vn/";
            var fileCookie = $"cookiesBackup_{tinhTP}.txt";

            var fileBackup = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NHACHINHCHU_COM_VN", fileCookie);
            if (File.Exists(fileBackup))
            {
                driver.Navigate().GoToUrl(url);
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
            driver.Navigate().GoToUrl("https://nhachinhchu.com.vn/product");

            while (true)
            {
                m_oWorker.ReportProgress(6, "");
                Thread.Sleep(3000);




























































































































                try
                {
                    var logOut = driver.FindElement(By.Id("logoutForm"));
                    if (logOut == null)
                    {
                        AutoLogin(driver, tinhTP);
                    }
                }
                catch { AutoLogin(driver, tinhTP); }

                if (runAuto == false)
                {
                    throw new Exception("Dừng chương trình");
                }
                try
                {
                    var logOut = driver.FindElement(By.Id("logoutForm"));
                    getCookie(driver, type, tinhTP);
                    break;
                }
                catch
                {
                    Thread.Sleep(2000);
                    continue;

                }

            }

        }

        public static void getCookie(ChromeDriver driver, int type, string tinhTP)
        {
            try
            {
                // Get All available cookies
                var lsCookies = driver.Manage().Cookies.AllCookies;
                cookies_ban = "";

                foreach (var cookie in lsCookies)
                {
                    cookies_ban += cookie.ToString().Split(';')[0] + ";";
                    var fileBackup = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NHACHINHCHU_COM_VN", $"cookiesBackup_{tinhTP}.txt");
                    File.WriteAllText(fileBackup, cookies_ban);
                }
                cookies_ban = "";
            }
            catch (Exception ex)
            {
                cookies_ban = "";

            }


            //if (type == 1)
            //{
            //    try
            //    {
            //        // Get All available cookies
            //        var lsCookies = driver.Manage().Cookies.AllCookies;
            //        cookies_ban = "";

            //        foreach (var cookie in lsCookies)
            //        {
            //            cookies_ban += cookie.ToString().Split(';')[0] + ";";
            //            var fileBackup = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NHACHINHCHU_COM_VN", $"cookiesBackup_{tinhTP}.txt");
            //            File.WriteAllText(fileBackup, cookies_ban);
            //        }
            //        cookies_ban = "";
            //    }
            //    catch (Exception ex)
            //    {
            //        cookies_ban = "";

            //    }
            //}
            //else if (type == 2)
            //{

            //    try
            //    {
            //        // Get All available cookies
            //        var lsCookies = driver.Manage().Cookies.AllCookies;
            //        cookies_thue = "";

            //        foreach (var cookie in lsCookies)
            //        {
            //            cookies_thue += cookie.ToString().Split(';')[0] + ";";
            //            var fileBackup = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SALENHA", $"cookiesBackupThue_{tinhTP}.txt");
            //            File.WriteAllText(fileBackup, cookies_thue);
            //        }
            //        cookies_thue = "";
            //    }
            //    catch (Exception ex)
            //    {
            //        cookies_thue = "";

            //    }

            //}
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



        public static bool AutoLogin(ChromeDriver driver, string tinhTP)
        {
            string user = "";
            string pass = "";
            string targetUrl = "https://nhachinhchu.com.vn/member/login";
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
            try
            {
                driver.Navigate().GoToUrl(targetUrl);
                Thread.Sleep(2 * 1000);
                driver.FindElement(By.Id("LoginEmail")).SendKeys(user);
                Thread.Sleep(1000);
                driver.FindElement(By.Id("LoginPassword")).SendKeys(pass);
                Thread.Sleep(1000);

                driver.FindElement(By.Id("btnLogin")).Click();
                Thread.Sleep(6000);
                if (driver.Url.Contains("login"))
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
    }
}
