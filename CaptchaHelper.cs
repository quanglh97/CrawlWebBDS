using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CrawlWebBDS
{
    public class CaptchaHelper
    {
        public static string apiKeyCaptcha = "";
        public static int timeSolveCaptha = 60;
        public static string CreateTask(string apiKey, string websiteKey, string targetDomain)
        {
            targetDomain = "http://hanoi.tinchinhchu.net.vn/dangnhap/tabid/149/Default.aspx?returnurl=%2f";
            try
            {
                var client = new RestClient("https://api.capmonster.cloud/createTask");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                var body = @"{""clientKey"":""" + apiKey + @""",""task"":{""type"":""NoCaptchaTaskProxyless"",""websiteURL"":""" + targetDomain + @""",""websiteKey"":""" + websiteKey + @"""}}";
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                var jsonResult = (JObject)JsonConvert.DeserializeObject(response.Content);

                if (jsonResult["errorId"].ToString().Equals("0"))
                {
                    return "0|" + jsonResult["taskId"].ToString();
                }

                if (jsonResult["errorId"].ToString().Equals("1"))
                {
                    return "1|" + jsonResult["errorCode"].ToString();
                }

            }
            catch { }

            return "1|";
        }

        // Return "" if can not solve
        public static string GetTaskResult(string apiKey, string taskId)
        {
            try
            {
                var client = new RestClient("https://api.capmonster.cloud/getTaskResult");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                var body = @"{""clientKey"":""" + apiKey + @""",""taskId"":" + taskId + "}";
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                var jsonResult = (JObject)JsonConvert.DeserializeObject(response.Content);

                if (jsonResult["errorId"].ToString().Equals("0"))
                {
                    if (jsonResult["status"].ToString().Equals("ready"))
                        return "0|" + jsonResult["solution"]["gRecaptchaResponse"].ToString();
                    else
                        return "0|NOT_READY";
                }

                if (jsonResult["errorId"].ToString().Equals("1"))
                {
                    return "1|" + jsonResult["errorCode"].ToString();
                }
            }
            catch { }

            return "1|";
        }

        public static string ResutlByAntiCaptchaTop(string apiKey, string websiteKey, string targetDomain)
        {
            //targetDomain = "http://hanoi.tinchinhchu.net.vn/dangnhap/tabid/149/Default.aspx?returnurl=%2f";
            try
            {
                var client = new RestClient("https://anticaptcha.top/api/captcha");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                var body = @"{""apikey"":""" + apiKey + @""",""type"":11,""pageurl"":""" + targetDomain + @""",""googlesitekey"":""" + websiteKey + @"""}";
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                JObject jObject = JObject.Parse(response.Content);
                if (jObject["success"].ToString() == "True")
                {
                    var result = jObject["captcha"].ToString();
                    return result;
                }
            }
            catch { }

            return null;
        }
    }
}
