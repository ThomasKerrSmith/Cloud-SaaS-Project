using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureFunction
{
    // Thomas 23/06/2022
    public static class CalculateGrade
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string responseMessage = "Welcome ";
            string studentName;
            int m1 = 0;
            int m2 = 0;
            int m3 = 0;
            int m4 = 0;
            int total = 0;
          
            //Check for data in the body Json
            string requBody = new StreamReader(req.Body).ReadToEnd();
            dynamic stuData = JsonConvert.DeserializeObject(requBody);
            studentName = stuData.StudentName;
            m1 = stuData.m1;
            m2 = stuData.m2;
            m3 = stuData.m3;
            m4 = stuData.m4;
      
            total = m1 + m2 + m3 + m4;
            //calculation for grade out of 100 
            if (total < 50)
            {
                responseMessage += studentName + " Sorry you failed with a total of: " + total + " out of 100".ToString();
            }
            else if (total >= 50 && total < 60)
            {

                responseMessage += studentName + " You Passed with D: " + total + " out of 100".ToString();
            }
            else if (total >= 60 && total < 70)
            {
                responseMessage += studentName + " You Passed with C: " + total + " out of 100".ToString();
            }
            else if (total >= 70 && total < 80)
            {
                responseMessage += studentName + " You Passed with B: " + total + " out of 100".ToString();
            }
            else if (total >= 80 && total < 90)
            {
                responseMessage += studentName + " You Passed with A: " + total + " out of 100".ToString();
            }
            else if (total >= 90 && total <= 100)
            {
                responseMessage += studentName + " You Passed with A+: " + total + " out of 100".ToString();
            }
            else
            {
                responseMessage += studentName + " Incorrect Grade Entry".ToString();
            }
            //Return
            return new OkObjectResult(responseMessage);

            //Test Queries
            //Passed With D
            //https://localhost:44363/AzureCaller/getStudentGrade?StudentName=Thomas&m1=0&m2=0&m3=25&m4=25
            //passed with C
            //https://localhost:44363/AzureCaller/getStudentGrade?StudentName=Thomas&m1=10&m2=0&m3=25&m4=25
            //passed with B
            //https://localhost:44363/AzureCaller/getStudentGrade?StudentName=Thomasn&m1=20&m2=0&m3=25&m4=25
            //passed with A
            //https://localhost:44363/AzureCaller/getStudentGrade?StudentName=Thomas&m1=20&m2=20&m3=20&m4=20
            //passed with A+
            //https://localhost:44363/AzureCaller/getStudentGrade?StudentName=Thomas&m1=20&m2=20&m3=25&m4=25
            //Failed with under 50
            //https://localhost:44363/AzureCaller/getStudentGrade?StudentName=Thomas&m1=10&m2=0&m3=0&m4=25
            //Incorrect grade entry
            //https://localhost:44363/AzureCaller/getStudentGrade?StudentName=Thomas&m1=10&m2=100&m3=0&m4=25


        }
    }
}
//Finished Thomas 23/06/2022
//Tested and Debugged by Grifyn and Thomas 27/7/22
