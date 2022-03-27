using System;
using System.Text;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Company.Function
{
    public static class GetResumeCounter
    {
        [FunctionName("GetResumeCounter")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, 
            [CosmosDB(databaseName:"VisitorCounter", collectionName:"Counter", ConnectionStringSetting="AzureResumeConnectionString", Id="1", PartitionKey="1")]Counter counter, 
            [CosmosDB(databaseName:"VisitorCounter", collectionName:"Counter", ConnectionStringSetting="AzureResumeConnectionString", Id="1", PartitionKey="1")]out Counter updatedCounter,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // Add 1 to the count in the database
            updatedCounter = counter;
            updatedCounter.Count += 1;

            // Get the DateTime now, convert it to EST, and update the DB with it
            TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime targetTime = TimeZoneInfo.ConvertTime(DateTime.Now, est);
            updatedCounter.Time = targetTime.ToString();

            // Return count to the web page
            var jsonToReturn = JsonConvert.SerializeObject(counter);

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK){
                Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/json")
            };
        }
    }
}
