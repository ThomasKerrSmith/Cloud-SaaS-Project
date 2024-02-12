using System;
using System.Collections.Generic;

#nullable disable

namespace MVCWebProject.Models.DB
{
    public partial class StudentDetail
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
