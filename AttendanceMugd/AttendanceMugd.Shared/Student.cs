using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceMugd
{
    class Student
    {
        public string Roll_no { get; set; }
        public string FullName { get; set; }
        public int dateLastUpdated { get; set; }
        public int monthLastUpdated { get; set; }
        public string Id { get; set; }
        public string Mobile_no { get; set; }
        public string EmailId { get; set; }
        public bool IsMember { get; set; }
        public string college { get; set; }
        public int Consecutive { get; set;}
        public DateTime LastAttended { get; set; }
        public int Attendance { get; set; }
    }
}
