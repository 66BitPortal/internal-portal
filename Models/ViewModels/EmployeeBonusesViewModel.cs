using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _66bitProject.Models.ViewModels
{
    public class EmployeeBonusesViewModel
    {
        public List<User> Employees { get; set; }
        public Dictionary<int, int> BonusesSums { get; set; }
        public int UserId { get; set; }
    }
}
