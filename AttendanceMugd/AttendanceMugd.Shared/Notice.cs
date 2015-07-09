using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceMugd
{
    class Notice
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string image { get; set; }
        public string type { get; set; }
        public string college { get; set; }
        public string issuedBy { get; set; }
    }
}
