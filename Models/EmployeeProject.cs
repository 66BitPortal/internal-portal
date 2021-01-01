using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _66bitProject.Models
{
    public class EmployeeProject
    {
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public User Employee { get; set; }
    }
}