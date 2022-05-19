using System;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Company.Function
{
    public static class WeatherFunction
    {
        [FunctionName("WeatherFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];
            string lat = req.Query["lat"];
            string lon = req.Query["lon"];
            string ip = req.Query["ip"];

            string requestBody = String.Empty;
            using (StreamReader streamReader =  new  StreamReader(req.Body))
            {
                requestBody = await streamReader.ReadToEndAsync();
            }
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;
            lat = lat ?? data?.lat;
            lon = lon ?? data?.lon;
            ip = ip ?? data?.ip;

            string keyUrl;
            var apiKey = "";

            if (!String.IsNullOrEmpty(lat) && !String.IsNullOrEmpty(lon)) {
                keyUrl = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}&units=imperial";
            }
            else if (!String.IsNullOrEmpty(name)){
                keyUrl = $"https://api.openweathermap.org/data/2.5/weather?q={name}&appid={apiKey}&units=imperial";
            }
            else if (!String.IsNullOrEmpty(ip)){
                var clientInfo = new HttpClient();
                HttpResponseMessage responseInfo = clientInfo.GetAsync($"http://ipinfo.io/{ip}?token=").Result;
                responseInfo.EnsureSuccessStatusCode();
                string resultInfo = responseInfo.Content.ReadAsStringAsync().Result;
                var dataIP = (JObject)JsonConvert.DeserializeObject(resultInfo);
                string cord = dataIP["loc"].Value<string>();
                string[] cords = cord.Split(',');
                string latIP = cords[0];
                string lonIP = cords[1];
                keyUrl = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}&units=imperial";
            }
            else{
                keyUrl = null;
            }

            var client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(keyUrl).Result;
            response.EnsureSuccessStatusCode();
            string result = response.Content.ReadAsStringAsync().Result;

            return !String.IsNullOrEmpty(keyUrl)
                ? (ActionResult)new OkObjectResult(result)
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
