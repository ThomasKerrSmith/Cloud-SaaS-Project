using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


//Azure function to calculate total cost of an item bought  
namespace ItemCalculation
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string itemName = req.Query["itemName"];
            string quantity = req.Query["Quantity"];
            string price = req.Query["Price"];
            decimal total;

           

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            itemName = itemName ?? data?.itemName;
           

            string responseMessage = "Function failed";
            if(itemName != null)
            {
                total = decimal.Parse(quantity) * decimal.Parse(price);
                responseMessage = $"Your Total is: ${total}"; 
            }
            
            return new OkObjectResult(responseMessage);
        }
    }
}
