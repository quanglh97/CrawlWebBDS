using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrawlWebBDS
{
    public class CrawlLienHeChinhChu
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
                options.AddUserProfilePreference("profile.default_content_setting_values.images", 2);

                ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                service.HideCommandPromptWindow = true;

                driver = new ChromeDriver(service, options);
                if (cookies != null && cookies != "")
                {
                    driver.Navigate().GoToUrl("https://lienhechinhchu.com/my-account/");
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
                    driver.Navigate().GoToUrl("https://lienhechinhchu.com/my-account/");
                }
                WaitLoginScrape(driver, m_oWorker);
                //hanoi
                for (int tp = 0; tp < tinh.Count; tp++)
                {
                    var tinhTP = tinh[tp];
                    for (int page = toPage; page >= fromPage; page--)
                    {
                        string url = $"https://lienhechinhchu.com/filter/page/" + page.ToString() + "/?filterwp=38145&s=&loai_tin_3=&thanh_pho_4=" + tinhTP + "&quan_huyen_hn_5=&quan_huyen_hcm_6=&loai_bds_7=&dien_tich_8=&khoang_gia_9=";
                        driver.Navigate().GoToUrl(url);
                        Thread.Sleep(5 * 1000);
                        var lsTagP = driver.FindElements(By.XPath("//p[@class='name product-title']"));
                        List<string> lsLinkNew = new List<string>();
                        foreach (var p in lsTagP)
                        {
                            var href = p.FindElement(By.TagName("a")).GetAttribute("href");
                            lsLinkNew.Add(href);
                        }

                        var sleepTime = rnd.Next(timeBetweenNewFrom, timeBetweenNewTo);
                        for (int i = 0; i < lsLinkNew.Count; i++)
                        {
                            if (runScrape == false)
                            {
                                throw new Exception("Dừng chương trình thủ công");
                            }
                            driver.Navigate().GoToUrl(lsLinkNew[i]);
                            Thread.Sleep(2 * 1000);
                            Crawl(m_oWorker, driver, lsLinkNew[i]);
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
                    if (cookies != null && cookies != "")
                    {
                        driver.Navigate().GoToUrl("https://lienhechinhchu.com/my-account/");
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
                        driver.Navigate().GoToUrl("https://lienhechinhchu.com/my-account/");
                    }
                    WaitLoginAuto(driver, m_oWorker);
                    //hanoi
                    for (int tp = 0; tp < autoTinh.Count; tp++)
                    {
                        var tinhTP = autoTinh[tp];
                        for (int page = autoToPage; page >= autoFromPage; page--)
                        {
                            string url = $"https://lienhechinhchu.com/filter/page/" + page.ToString() + "/?filterwp=38145&s=&loai_tin_3=&thanh_pho_4=" + tinhTP + "&quan_huyen_hn_5=&quan_huyen_hcm_6=&loai_bds_7=&dien_tich_8=&khoang_gia_9=";
                            driver.Navigate().GoToUrl(url);
                            Thread.Sleep(5 * 1000);
                            var lsTagP = driver.FindElements(By.XPath("//p[@class='name product-title']"));
                            List<string> lsLinkNew = new List<string>();
                            foreach (var p in lsTagP)
                            {
                                var href = p.FindElement(By.TagName("a")).GetAttribute("href");
                                lsLinkNew.Add(href);
                            }

                            var sleepTime = rnd.Next(timeBetweenNewFrom, timeBetweenNewTo);
                            for (int i = 0; i < lsLinkNew.Count; i++)
                            {
                                if (runAuto == false)
                                {
                                    throw new Exception("Dừng chương trình tự động");
                                }
                                driver.Navigate().GoToUrl(lsLinkNew[i]);
                                Thread.Sleep(2 * 1000);
                                Crawl(m_oWorker, driver, lsLinkNew[i]);
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

        public static void Crawl(BackgroundWorker m_oWorker, ChromeDriver driver, string url)
        {
            var newUrl = url;
            var title = driver.FindElement(By.XPath("//h1[@class='product-title product_title entry-title']")).Text;
            var lsInfoTag = driver.FindElement(By.XPath("//div[@class='product-short-description']")).FindElements(By.TagName("div"));
            var price = lsInfoTag[0].Text;
            var area = lsInfoTag[1].Text;
            var type = lsInfoTag[2].Text;
            var province = lsInfoTag[3].Text;
            var createDate = lsInfoTag[4].Text;
            var district = lsInfoTag[5].Text;
            var category = type.Contains("Bán")?"danh mục bán":"danh mục thuê";
            var lsContent = driver.FindElement(By.XPath("//div[@class='woocommerce-Tabs-panel woocommerce-Tabs-panel--description panel entry-content active']")).FindElements(By.TagName("li"));
            var content = "";
            for (int i = 0; i < lsContent.Count - 2; i++)
            {
                content += lsContent[i].Text + "<br>";
            }

            var address = lsContent[lsContent.Count - 2].Text;
            content += "<br>" + address;
            var phone = lsContent[lsContent.Count - 1].Text;

            var bodyUrl = $"\"url\":\"{newUrl}\",";
            var bodyTitle = $"\"title\":\"{title.Split('-')[0]}\",";
            var bodyContent = $"\"content\":\"{content.Replace(",", ". ").Replace("\n", ". ").Trim()}\",";
            //var bodyContent = $"\"content\":\"tesst\",";
            var bodyPrice = $"\"price\":\"{price}\",";
            var bodyArea = $"\"area\":\"{area.Split(':')[1]}\",";
            var bodyCreateDate = $"\"createDate\":\"{createDate.Split(':')[1]}\",";
            var bodyType = $"\"type\":\"{type.Split(':')[1]}\",";
            var bodyCategory = $"\"category\":\"{category}\",";
            var bodyAddress = $"\"address\":\"{address}\",";
            var bodyPhone = $"\"phone\":\"{phone.Split(':')[1]}\",";
            var bodyProvince = $"\"province\":\"{province.Split(':')[1]}\",";
            var bodyDistrict = $"\"district\":\"{district.Split(':')[1]}\"";
            if(linkAPI1 != null && linkAPI1 != "")
            {
                var client = new RestClient(linkAPI1);
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                //var body = sendData;// "[" + DictionaryToString(data) + "]" ;
                var body = "[{" + bodyUrl + bodyAddress + bodyArea + bodyCategory + bodyTitle + bodyContent + bodyPrice + bodyPhone + bodyCreateDate + bodyType + bodyProvince + bodyDistrict + "}]";
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

            if (linkAPI2 != null && linkAPI2 != "")
            {
                var client = new RestClient(linkAPI2);
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                //var body = sendData;// "[" + DictionaryToString(data) + "]" ;
                var body = "[{" + bodyUrl + bodyAddress + bodyArea + bodyCategory + bodyTitle + bodyContent + bodyPrice + bodyPhone + bodyCreateDate + bodyType + bodyProvince + bodyDistrict + "}]";
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


        public static void WaitLoginScrape(ChromeDriver driver, BackgroundWorker m_oWorker)
        {
            var fileBackup = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LHCC_COM", "cookiesBackup.txt");
            //var fileBackup = AppDomain.CurrentDomain.BaseDirectory + "cookiesBackup.txt";
            if (File.Exists(fileBackup))
            {
                driver.Navigate().GoToUrl("https://lienhechinhchu.com/my-account/");
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
            driver.Navigate().GoToUrl("https://lienhechinhchu.com/my-account/");
            while (true)
            {
                m_oWorker.ReportProgress(6, "");
                if (runScrape == false)
                {
                    throw new Exception("Dừng chương trình");
                }

                if (driver.Url.Contains("https://lienhechinhchu.com/my-account/"))
                {
                    getCookie(driver);
                    break;
                }
                Thread.Sleep(2 * 1000);
            }
        }

        public static void WaitLoginAuto(ChromeDriver driver, BackgroundWorker m_oWorker)
        {
            var fileBackup = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LHCC_COM", "cookiesBackup.txt");
            //var fileBackup = AppDomain.CurrentDomain.BaseDirectory + "cookiesBackup.txt";
            if (File.Exists(fileBackup))
            {
                driver.Navigate().GoToUrl("https://lienhechinhchu.com/my-account/");
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
            driver.Navigate().GoToUrl("https://lienhechinhchu.com/my-account/");
            while (true)
            {
                m_oWorker.ReportProgress(6, "");
                if (runAuto == false)
                {
                    throw new Exception("Dừng chương trình");
                }

                if (driver.Url.Contains("https://lienhechinhchu.com/my-account/"))
                {
                    getCookie(driver);
                    break;
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
                    var fileBackup = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LHCC_COM", "cookiesBackup.txt");
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
