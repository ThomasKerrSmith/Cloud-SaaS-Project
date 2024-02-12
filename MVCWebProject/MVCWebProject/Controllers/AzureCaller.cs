using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MVCWebProject.Controllers
{
    //23/06/2022 Thomas
    public class AzureCaller : Controller
    {
        public IActionResult Index()
        {
            Response.ContentType = "text/html";
            return View();
        }

        //create class for student grade
        public class StudentGrade
        {
            public string StudentName = "Thomas";
            public int m1;
            public int m2;
            public int m3;
            public int m4;
        }

        //completed 24-06-2022 Thomas
        public async Task<string> getStudentGrade(string StudentName, int m1, int m2, int m3, int m4)
        {
            StudentGrade grade = new();
            grade.StudentName = StudentName;
            grade.m1 = m1;
            grade.m2 = m2;
            grade.m3 = m3;
            grade.m4 = m4;

            //url string for azure function
            string strBaseUrl = "http://localhost:7074/api/Function1";
            
            //convert grade object to string
            string content = JsonConvert.SerializeObject(grade);
            
            // convert to byte
            var buffer = Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            
            // create http client
            HttpClient client = new();

            var response = await client.PostAsync(strBaseUrl, byteContent).ConfigureAwait(false);
            string result = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return "error";
            else
                return result;

            // Test URL
            //https://localhost:44363/AzureCaller/getStudentGrade/Function1?studentName=Thomas&m1=10&m2=20&m3=0&m4=20
        }
    }
}
