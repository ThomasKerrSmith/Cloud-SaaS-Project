using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCWebProject.Models.DB;
using Newtonsoft.Json;
using MVCWebProject.Models.DB;

namespace MVCWebProject.Controllers
{
    public class StaffDetailsController : Controller
    {
        private readonly CC21_Team4_Sem1Context _context;

        public StaffDetailsController(CC21_Team4_Sem1Context context)
        {
            _context = context;
        }

        // 23/06/2022 
        //Login using Json in url using p0 and p1
        public string LoginJson(string json)
        {
            //convert string to object
            dynamic JObject = JsonConvert.DeserializeObject(json);

            string username = (string)JObject.SelectToken("Username");
            string password = (string)JObject.SelectToken("Password");
            string Epassword = GetEncryptPassword(password);
            //get details from database 
            string strSQL = "SELECT * FROM StaffDetails WHERE Username=@p0 AND Password = @p1";
            var staffDetail = _context.StaffDetails.FromSqlRaw(strSQL, username, password).ToList();
            var responseMessage = new LoginResponse();

            if (staffDetail.Count == 1)
            {
                responseMessage.isSuccess = true;
                responseMessage.response = "Successfully logged in";
                responseMessage.StaffId = staffDetail[0].StaffId.ToString();
            }
            else
            {
                responseMessage.isSuccess = false;
                responseMessage.response = "Unsuccessful Log In: User id or password is wrong";
                responseMessage.StaffId = "0";
            }
            //convert object to string 
            string strResult = JsonConvert.SerializeObject(responseMessage);
            return strResult;
        }

        //Test Runs 
        // Successful login
        //https://localhost:44363/StaffDetails/LoginJson?json={"Username":"Thomas123","Password":"Thomas123"}
        // Unsuccessful login 
        //https://localhost:44363/StaffDetails/LoginJson?json={"Username":"Thomas","Password":"Thomas"}

        //Class for JsonLogin 
        private class LoginResponse
        {
            public bool isSuccess;
            public string response;
            public string StaffId;
        }

        public string GetEncryptPassword(string _password)
        {
            string Eresult;
            //Encryption for password
            var sp = new System.Security.Cryptography.SHA512Managed();
            var enc = new System.Text.UTF8Encoding();
            byte[] hashIn;
            byte[] hashOut;
            //Convert text to byte for encryption of password 
            hashIn = enc.GetBytes(_password);
            hashOut = sp.ComputeHash(hashIn);

            //convert result into string
            Eresult = Convert.ToBase64String(hashOut);
            return Eresult;

            // Encrypted password for Thomas123
            // vAW4Ts9QSRT630vr3UKwHK+u+TCtt8nfRLmZx5dyChita4yk0VBUKUEW3Lo2Ld8/Tkee55zLZQtVjYAa9eiXmg==
        }

        //23/06/2022
        //Login using params on staff details
        public string Login(string Username, string Password)
        {
            //Query string for accessing database 
            string strSQL = $"SELECT * FROM StaffDetails where Username = '{Username}' AND Password = '{Password}'";
            var staffDetail = _context.StaffDetails.FromSqlRaw(strSQL).ToList();

            if (staffDetail.Count == 1)
            {
                string responseMessage = $"Hello {Username}, you have successfully logged in!";
                return responseMessage;
            }
            else
            {
                string responseMessage = "Invalid Login!";
                return responseMessage;
            }

            //https://localhost:44363/StaffDetails/LoginJson?json={"Username":"Thomas","Password":"Thomas"}
            //https://localhost:44363/StaffDetails/Login?Username=Thomas123&Password=Thomas123
        }

        // GET: StaffDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.StaffDetails.ToListAsync());
        }

        // GET: StaffDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffDetail = await _context.StaffDetails
                .FirstOrDefaultAsync(m => m.StaffId == id);
            if (staffDetail == null)
            {
                return NotFound();
            }

            return View(staffDetail);
        }

        // GET: StaffDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StaffDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaffId,FirstName,LastName,Address,PhoneNumber,Email,Subject,Username,Password")] StaffDetail staffDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staffDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(staffDetail);
        }

        // GET: StaffDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffDetail = await _context.StaffDetails.FindAsync(id);
            if (staffDetail == null)
            {
                return NotFound();
            }
            return View(staffDetail);
        }

        // POST: StaffDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StaffId,FirstName,LastName,Address,PhoneNumber,Email,Subject,Username,Password")] StaffDetail staffDetail)
        {
            if (id != staffDetail.StaffId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staffDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffDetailExists(staffDetail.StaffId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(staffDetail);
        }

        // GET: StaffDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffDetail = await _context.StaffDetails
                .FirstOrDefaultAsync(m => m.StaffId == id);
            if (staffDetail == null)
            {
                return NotFound();
            }

            return View(staffDetail);
        }

        // POST: StaffDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staffDetail = await _context.StaffDetails.FindAsync(id);
            _context.StaffDetails.Remove(staffDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffDetailExists(int id)
        {
            return _context.StaffDetails.Any(e => e.StaffId == id);
        }
    }
}
