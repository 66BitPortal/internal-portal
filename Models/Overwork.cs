using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _66bitProject.Models
{
    public class Overwork
    {
        public int PersonID { get; set; }
        public int OverworkID { get; set; }
        public int ProjectID { get; set; }
        public int HoursCount { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
