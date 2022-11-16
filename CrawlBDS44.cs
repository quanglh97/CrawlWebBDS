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
using OpenQA.Selenium.Support.UI;

namespace CrawlWebBDS
{
    public class CrawlBDS44
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
                for (int tp = 0; tp < tinh.Count; tp++)
                {
                    var tinhTP = tinh[tp];
                    WaitLoginScrape(driver, m_oWorker, tinhTP);
                        
                    for (int page = toPage; page >= fromPage; page--)
                    {
                        string url = $"https://batdongsan44.com/?type_sort=create&page=" + page.ToString();
                        driver.Navigate().GoToUrl(url);
                        Thread.Sleep(5 * 1000);
                        var lsTagTitle = driver.FindElements(By.XPath("//div[@class='col-sm-11']"));
                        List<string> lsLinkNew = new List<string>();
                        foreach (var p in lsTagTitle)
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
                    for (int tp = 0; tp < autoTinh.Count; tp++)
                    {
                        var tinhTP = autoTinh[tp];

                        WaitLoginAuto(driver, m_oWorker, tinhTP);
                        for (int page = autoToPage; page >= autoFromPage; page--)
                        {
                            string url = $"https://batdongsan44.com/?type_sort=create&page=" + page.ToString();
                            driver.Navigate().GoToUrl(url);
                            Thread.Sleep(5 * 1000);
                            var lsTagTitle = driver.FindElements(By.XPath("//div[@class='col-sm-11']"));
                            List<string> lsLinkNew = new List<string>();
                            foreach (var p in lsTagTitle)
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
            try
            {
                var newUrl = url;
                var title = driver.FindElements(By.XPath("//h3"))[0].Text.Trim();

                var price = driver.FindElements(By.XPath("//span[@class='nhadat']"))[0].Text.Trim();
                var area = driver.FindElements(By.XPath("//span[@class='nhadat']"))[1].Text.Trim();
                var tempEle = driver.FindElements(By.XPath("//select[@class='form-control category']"))[1];
                SelectElement selectCategory = new SelectElement(tempEle);
                var type = selectCategory.SelectedOption.Text;
                if (type.Contains("Cho thuê nhà đất")) 
                    type = "Cho thuê khác";
                tempEle = driver.FindElements(By.XPath("//select[@class='form-control tinh']"))[1];
                SelectElement selectProvince = new SelectElement(tempEle);
                var province = selectProvince.SelectedOption.Text;
                tempEle = driver.FindElements(By.XPath("//select[@class='form-control quan']"))[1];
                SelectElement selectDistrict = new SelectElement(tempEle);
                var district = selectDistrict.SelectedOption.Text;

                var createDate = driver.FindElements(By.XPath("//p[@class='small text-right']"))[0].Text.Trim().Split(new string[] { "Mã tin" }, StringSplitOptions.None)[0];
                var category = type;
                //var lsPContent = driver.FindElements(By.XPath("//p"));
                var lsDivContent = driver.FindElements(By.XPath("//h3"))[0].FindElements(By.XPath("//div"));
                var content = "";
                //foreach (var p in lsPContent)
                //{
                //    content += p.Text.Trim() + "<br>";
                //}


                //test nhé
                foreach (var div in lsDivContent)
                {
                    if(div.Text!=null && div.Text.Trim() != "")
                        content += div.Text.Trim() + "<br>";
                }
                content = content.Split(new string[] {title}, StringSplitOptions.None)[1];
                content = content.Split(new string[] {"Mã tin: "}, StringSplitOptions.None)[0];

                content = content.Replace("\r\n", "<br>").Split(new string[] { "Ngày " }, StringSplitOptions.None)[0];
                var address = driver.FindElements(By.XPath("//span[@class='nhadat']"))[2].Text.Trim();
                //content += "\\nGiá: " + price;
                //content += "<br>"+ "Diện tích: " + area;
                //content += "<br>" + "Địa chỉ: " + address;
                var phone = driver.FindElements(By.XPath("//div[@class='col-md-12 col-sm-12 ']"))[0].FindElement(By.TagName("a")).GetAttribute("href").Split(':')[1];

                var bodyUrl = $"\"url\":\"{newUrl}\",";
                var bodyTitle = $"\"title\":\"{title.Split('-')[0]}\",";
                var bodyContent = $"\"content\":\"{content.Trim()}\",";
                //var bodyContent = $"\"content\":\"tesst\",";
                var bodyPrice = $"\"price\":\"{price}\",";
                var bodyArea = $"\"area\":\"{area}\",";
                var bodyCreateDate = $"\"createDate\":\"{createDate}\",";
                var bodyType = $"\"type\":\"{type}\",";
                var bodyCategory = $"\"category\":\"{category}\",";
                var bodyAddress = $"\"address\":\"{address}\",";
                var bodyPhone = $"\"phone\":\"{phone}\",";
                var bodyProvince = $"\"province\":\"{province}\",";
                var bodyDistrict = $"\"district\":\"{district}\"";
                if (linkAPI1 != null && linkAPI1 != "")
                {
                    var client = new RestClient(linkAPI1);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/json");
                    //var body = sendData;// "[" + DictionaryToString(data) + "]" ;
                    var body = "[{" + bodyUrl + bodyAddress + bodyArea + bodyCategory + bodyTitle + bodyContent + bodyPrice + bodyPhone + bodyCreateDate + bodyType + bodyProvince + bodyDistrict + "}]";
                    //Console.WriteLine(body);
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
            catch { }

        }


        public static void WaitLoginScrape(ChromeDriver driver, BackgroundWorker m_oWorker, string tinhTP)
        {
            var fileCookie = $"cookiesBackup_{tinhTP}.txt";
            var fileBackup = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BDS44", fileCookie);

            driver.Navigate().GoToUrl("https://batdongsan44.com/");
            driver.Manage().Cookies.DeleteAllCookies();

            if (File.Exists(fileBackup))
            {
                driver.Navigate().GoToUrl("https://batdongsan44.com/");
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
            driver.Navigate().GoToUrl("https://batdongsan44.com/");
            while (true)
            {
                m_oWorker.ReportProgress(6, "");
                if (runScrape == false)
                {
                    throw new Exception("Dừng chương trình");
                }
                try
                {
                    var checkLogin = driver.FindElement(By.XPath("//span[@class='glyphicon glyphicon-log-out']"));
                    if (checkLogin != null)
                    {
                        getCookie(driver, tinhTP);
                        break;
                    }
                }
                catch
                {
                }
                Thread.Sleep(2 * 1000);
            }
        }

        public static void WaitLoginAuto(ChromeDriver driver, BackgroundWorker m_oWorker, string tinhTP)
        {
            var fileCookie = $"cookiesBackup_{tinhTP}.txt";
            var fileBackup = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BDS44", fileCookie);

            driver.Navigate().GoToUrl("https://batdongsan44.com/");
            driver.Manage().Cookies.DeleteAllCookies();

            if (File.Exists(fileBackup))
            {
                driver.Navigate().GoToUrl("https://batdongsan44.com/");
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
            driver.Navigate().GoToUrl("https://batdongsan44.com/");
            while (true)
            {
                m_oWorker.ReportProgress(6, "");
                if (runAuto == false)
                {
                    throw new Exception("Dừng chương trình");
                }

                try
                {
                    var checkLogin = driver.FindElement(By.XPath("//span[@class='glyphicon glyphicon-log-out']"));
                    if (checkLogin != null)
                    {
                        getCookie(driver, tinhTP);
                        break;
                    }
                }
                catch
                {
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
                    var fileBackup = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BDS44", $"cookiesBackup_{tinhTP}.txt");
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

        public static void WriteLog(string data)
        {
            var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BDS44");
            if (!Directory.Exists(pathStringFolder))
            {
                Directory.CreateDirectory(pathStringFolder);
            }

            var pathSetting = System.IO.Path.Combine(pathStringFolder, "logData.txt");
            File.AppendAllText(pathSetting, data + Environment.NewLine);
        }
    }
}
