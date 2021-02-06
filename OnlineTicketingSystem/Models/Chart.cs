using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTicketingSystem.Models
{
    public class Chart
    {       
        public string ProjectTitle { get; set; }

        public string DeptName { get; set; }

        public string EmpName { get; set; }

        public string Status { get; set; }
        
        public int Year { get; set; }

        public int TotTickets { get; set; }
        
    }
}