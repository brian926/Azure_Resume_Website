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

            string requestBody = String.Empty;
            using (StreamReader streamReader =  new  StreamReader(req.Body))
            {
                requestBody = await streamReader.ReadToEndAsync();
            }
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            Console.WriteLine("Read query");
            name = name ?? data?.name;
            lat = lat ?? data?.lat;
            lon = lon ?? data?.lon;

            string keyUrl = null;
            Console.WriteLine("Grabbing API key");
            var apiKey = Environment.GetEnvironmentVariable("AzureWeatherConnectionString", EnvironmentVariableTarget.Process);

            if (!String.IsNullOrEmpty(lat) && !String.IsNullOrEmpty(lon)) {
                keyUrl = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}&units=imperial";
            }
            else if (!String.IsNullOrEmpty(name)){
                keyUrl = $"https://api.openweathermap.org/data/2.5/weather?q={name},us&appid={apiKey}&units=imperial";
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
