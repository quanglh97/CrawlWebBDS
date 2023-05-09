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

                //if (crawl_ban_salenha) { 
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
                        if (type == 1)
                        {
                            cookies = cookies_ban;
                            url = "https://salenha.com/";
                        }
                        else if (type == 2)
                        {
                            cookies = cookies_thue;
                            url = "https://salenha.com/cho-thue";
                        }
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

                        for (int page = toPage; page >= fromPage; page--)
                        {
                            string urlCrawl = $"{url}?search=&page={page.ToString()}";
                            driver.Navigate().GoToUrl(urlCrawl);
                            Thread.Sleep(5 * 1000);
                            var lsTagTitle = driver.FindElements(By.XPath("//div[@class='col-sm-11']"));

                            var lsSdtBtn = driver.FindElements(By.XPath("//button[@class='btn btn-show-phone']"));
                            for (int i = 0; i < lsSdtBtn.Count; i++)
                            {
                                var x = rnd.Next(700, 1800);
                                lsSdtBtn[i].Click();
                                Thread.Sleep(x);
                            }

                            var lsViewMoreBtn = driver.FindElements(By.XPath("//a[@class='btn-view-more']"));
                            for (int i = 0; i < lsViewMoreBtn.Count; i++)
                            {
                                try
                                {
                                    var x = rnd.Next(700, 1800);
                                    lsViewMoreBtn[i].Click();
                                    Thread.Sleep(x);
                                }
                                catch { }
                            }
                            var lsLoaiTin = driver.FindElements(By.XPath("//div[@class='col-xs-3 col-sm-3']"));//col-xs-3 col-sm-3
                            var lsTinDiv = driver.FindElements(By.XPath("//div[@class='col-xs-9 col-sm-9']"));

                            List<string> lsTin = new List<string>();

                            for (int i = 0; i < lsTinDiv.Count; i++)
                            {
                                var tempTin = lsTinDiv[i].Text.Split(new[] { "Thêm vào" }, StringSplitOptions.None)[0];
                                tempTin = tempTin.Trim() + "\n" +lsLoaiTin[i].Text.Split('\n')[1];
                                lsTin.Add(tempTin);
                            }

                            //for(int i = 0; i < lsTin.Count; i++)
                            //{
                            //    var tinUrl = driver.Url + "_Tin_" + i; 
                            //    Crawl(m_oWorker, lsTin[i], tinUrl);
                            //}


                            //List<string> lsLinkNew = new List<string>();
                            //foreach (var p in lsTagTitle)
                            //{
                            //    var href = p.FindElement(By.TagName("a")).GetAttribute("href");
                            //    lsLinkNew.Add(href);
                            //}

                            //var sleepTime = rnd.Next(timeBetweenNewFrom, timeBetweenNewTo);
                            //for (int i = 0; i < lsLinkNew.Count; i++)
                            //{
                            //    if (runScrape == false)
                            //    {
                            //        throw new Exception("Dừng chương trình thủ công");
                            //    }

                            //    driver.Navigate().GoToUrl(lsLinkNew[i]);
                            //    Thread.Sleep(2 * 1000);
                            //    //Task.Run(() =>
                            //    //{
                            //    //    Crawl(m_oWorker, driver, lsLinkNew[i]);
                            //    //});

                            //    Crawl(m_oWorker, driver, lsLinkNew[i]);

                            //    Thread.Sleep(sleepTime * 1000);
                            //}
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
                            if (type == 1)
                            {
                                cookies = cookies_ban;
                                url = "https://salenha.com/";
                            }
                            else if (type == 2)
                            {
                                cookies = cookies_thue;
                                url = "https://salenha.com/cho-thue";
                            }

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
                            for (int page = autoToPage; page >= autoFromPage; page--)
                            {
                                string urlCrawl = $"{url}?search=&page={page.ToString()}";
                                driver.Navigate().GoToUrl(urlCrawl);
                                Thread.Sleep(5 * 1000);
                                var lsTagTitle = driver.FindElements(By.XPath("//div[@class='col-xs-9 col-sm-9']"));
                                List<string> lsLinkNew = new List<string>();
                                foreach (var p in lsTagTitle)
                                {
                                    var href = p.FindElement(By.TagName("a")).GetAttribute("href");
                                    lsLinkNew.Add(href);
                                }

                                var sleepTime = rnd.Next(timeBetweenNewFrom, timeBetweenNewTo);
                                for (int j = 0; j < lsLinkNew.Count; j++)
                                {
                                    if (runAuto == false)
                                    {
                                        throw new Exception("Dừng chương trình tự động");
                                    }
                                    driver.Navigate().GoToUrl(lsLinkNew[j]);
                                    Thread.Sleep(2 * 1000);
                                    //Task.Run(() =>
                                    //{
                                    //    Crawl(m_oWorker, driver, lsLinkNew[i]);
                                    //});
                                    //Crawl(m_oWorker, driver, lsLinkNew[j]);
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

        public static void Crawl(BackgroundWorker m_oWorker, string tin, string url)
        {
            try
            {
                var newUrl = url;
                var eleTin = tin.Split('\n');
                var title = eleTin[0];
                eleTin = eleTin.Where((val, idx) => idx != 0).ToArray();
                var category = eleTin[eleTin.Length-1];
                var address = eleTin[eleTin.Length - 2];
                var phone = eleTin[eleTin.Length - 3];
                var area = eleTin[eleTin.Length - 4];
                var price = eleTin[eleTin.Length - 5];

                var type = "";
                if (newUrl.Contains("thue"))
                    type = "Tin cho thuê";
                else
                {
                    type = "Tin bán nhà";
                }

                //tempEle = driver.FindElements(By.XPath("//select[@class='form-control tinh']"))[1];
                //SelectElement selectProvince = new SelectElement(tempEle);
                //var province = selectProvince.SelectedOption.Text;
                //tempEle = driver.FindElements(By.XPath("//select[@class='form-control quan']"))[1];
                //SelectElement selectDistrict = new SelectElement(tempEle);
                //var district = selectDistrict.SelectedOption.Text;
                //tempEle = driver.FindElements(By.XPath("//select[@class='form-control huyen']"))[1];
                //SelectElement selectWard = new SelectElement(tempEle);
                //var ward = selectWard.SelectedOption.Text;

                //var createDate = driver.FindElements(By.XPath("//p[@class='small text-right']"))[0].Text.Split(new[] { "Mã tin" }, StringSplitOptions.None)[0].Trim();
                //var lsPContent = driver.FindElements(By.XPath("//p"));
                //var content = "";
                //foreach (var p in lsPContent)
                //{
                //    content += p.Text.Trim() + "<br>";
                //}
                //content = content.Replace("\r\n", "<br>").Split(new string[] { "Ngày " }, StringSplitOptions.None)[0];


                //content += "<br>" + "Diện tích: " + area;
                //content += "<br>" + "Địa chỉ: " + address;
                //try
                //{
                //    driver.FindElements(By.XPath("//button[@class='btn btn-show-phone']"))[0].Click();
                //    Thread.Sleep(1000);
                //}
                //catch { }


                //var bodyUrl = $"\"url\":\"{newUrl}\",";
                //var bodyTitle = $"\"title\":\"{title.Split('-')[0]}\",";
                //var bodyContent = $"\"content\":\"{content.Trim()}\",";
                ////var bodyContent = $"\"content\":\"tesst\",";
                //var bodyPrice = $"\"price\":\"{price}\",";
                //var bodyArea = $"\"area\":\"{area}\",";
                //var bodyCreateDate = $"\"createDate\":\"{createDate}\",";
                //var bodyType = $"\"type\":\"{type}\",";
                //var bodyCategory = $"\"category\":\"{category}\",";
                //var bodyAddress = $"\"address\":\"{address}\",";
                //var bodyPhone = $"\"phone\":\"{phone}\",";
                //var bodyProvince = $"\"province\":\"{province}\",";
                //var bodyWard = $"\"ward\":\"{ward}\",";
                //var bodyDistrict = $"\"district\":\"{district}\"";
                //Task.Run(() =>
                //{
                //    try
                //    {
                //        if (linkAPI1 != null && linkAPI1 != "")
                //        {
                //            var client = new RestClient(linkAPI1);
                //            var request = new RestRequest(Method.POST);
                //            request.AddHeader("Content-Type", "application/json");
                //            //var body = sendData;// "[" + DictionaryToString(data) + "]" ;
                //            var body = "[{" + bodyUrl + bodyAddress + bodyArea + bodyCategory + bodyTitle + bodyContent + bodyPrice + bodyPhone + bodyCreateDate + bodyType + bodyProvince + bodyWard + bodyDistrict + "}]";
                //            //Console.WriteLine(body);
                //            request.AddParameter("application/json", body, ParameterType.RequestBody);
                //            IRestResponse response = client.Execute(request);
                //            Console.WriteLine(response.Content);
                //            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                //            {
                //                m_oWorker.ReportProgress(0, url);
                //            }
                //            else
                //            {
                //                m_oWorker.ReportProgress(1, url);
                //            }
                //        }
                //    }
                //    catch { }
                //});

                //Task.Run(() =>
                //{
                //    try
                //    {
                //        if (linkAPI2 != null && linkAPI2 != "")
                //        {
                //            var client = new RestClient(linkAPI2);
                //            var request = new RestRequest(Method.POST);
                //            request.AddHeader("Content-Type", "application/json");
                //            //var body = sendData;// "[" + DictionaryToString(data) + "]" ;
                //            var body = "[{" + bodyUrl + bodyAddress + bodyArea + bodyCategory + bodyTitle + bodyContent + bodyPrice + bodyPhone + bodyCreateDate + bodyType + bodyProvince + bodyDistrict + "}]";
                //            //Console.WriteLine(body);
                //            request.AddParameter("application/json", body, ParameterType.RequestBody);
                //            IRestResponse response = client.Execute(request);
                //            Console.WriteLine(response.Content);
                //            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                //            {
                //                m_oWorker.ReportProgress(3, url);
                //            }
                //            else
                //            {
                //                m_oWorker.ReportProgress(4, url);
                //            }
                //        }
                //    }
                //    catch
                //    {
                //    }
                //});
            }
            catch { }

        }


        public static void WaitLoginScrape(ChromeDriver driver, BackgroundWorker m_oWorker, int type, string tinhTP)
        {
            var url = "https://salenha.com/";
            var fileCookie = $"cookiesBackup_{tinhTP}.txt";
            if (type == 2)
            {
                url = "https://salenha.com/cho-thue";
                fileCookie = $"cookiesBackupThue_{tinhTP}.txt";
            }
            var fileBackup = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SALENHA_VIP", fileCookie);
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
            driver.Navigate().GoToUrl(url);
            while (true)
            {
                m_oWorker.ReportProgress(6, "");
                if (runScrape == false)
                {
                    throw new Exception("Dừng chương trình");
                }
                try
                {
                    var login = driver.FindElement(By.XPath("//span[@class='glyphicon glyphicon-log-in']"));////span[@class='glyphicon glyphicon-log-in']
                    Thread.Sleep(2 * 1000);
                }
                catch
                {
                    if (driver.Url.Contains("login"))
                    {
                        Thread.Sleep(2000);
                        continue;
                    }
                    getCookie(driver, type, tinhTP);
                    break;

                }

            }
        }

        public static void WaitLoginAuto(ChromeDriver driver, BackgroundWorker m_oWorker, int type, string tinhTP)
        {
            var url = "https://salenha.com/";
            var fileCookie = $"cookiesBackup_{tinhTP}.txt";
            if (type == 2)
            {
                url = "https://salenha.com/cho-thue";
                fileCookie = $"cookiesBackupThue_{tinhTP}.txt";
            }
            var fileBackup = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SALENHA_VIP", fileCookie);
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
            driver.Navigate().GoToUrl(url);
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
                        getCookie(driver, type, tinhTP);
                        break;
                    }
                }
                catch
                {
                }
                Thread.Sleep(2 * 1000);
            }


        }

        public static void getCookie(ChromeDriver driver, int type, string tinhTP)
        {
            if (type == 1)
            {
                try
                {
                    // Get All available cookies
                    var lsCookies = driver.Manage().Cookies.AllCookies;
                    cookies_ban = "";

                    foreach (var cookie in lsCookies)
                    {
                        cookies_ban += cookie.ToString().Split(';')[0] + ";";
                        var fileBackup = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SALENHA", $"cookiesBackup_{tinhTP}.txt");
                        File.WriteAllText(fileBackup, cookies_ban);
                    }
                    cookies_ban = "";
                }
                catch (Exception ex)
                {
                    cookies_ban = "";

                }
            }
            else if (type == 2)
            {

                try
                {
                    // Get All available cookies
                    var lsCookies = driver.Manage().Cookies.AllCookies;
                    cookies_thue = "";

                    foreach (var cookie in lsCookies)
                    {
                        cookies_thue += cookie.ToString().Split(';')[0] + ";";
                        var fileBackup = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SALENHA", $"cookiesBackupThue_{tinhTP}.txt");
                        File.WriteAllText(fileBackup, cookies_thue);
                    }
                    cookies_thue = "";
                }
                catch (Exception ex)
                {
                    cookies_thue = "";

                }

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
