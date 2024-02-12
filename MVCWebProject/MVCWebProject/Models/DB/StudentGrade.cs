using System;
using System.Collections.Generic;

#nullable disable

namespace MVCWebProject.Models.DB
{
    public partial class StudentGrade
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int M1 { get; set; }
        public int M2 { get; set; }
        public int M3 { get; set; }
        public int M4 { get; set; }
    }
}
