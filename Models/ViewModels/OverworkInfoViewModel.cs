using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _66bitProject.Models.ViewModels
{
    public class OverworkInfoViewModel
    {
        public int HoursCount { get; set; }
        public string Description { get; set; }
        public int CalculatedValue { get; set; }
        public string EmployeeFullName { get; set; }
        public string ProjectName { get; set; }
    }
}
