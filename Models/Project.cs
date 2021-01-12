using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _66bitProject.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ManagerId { get; set; }
        public ICollection<Overwork> Overworks { get; set; }
        public ICollection<EmployeeProject> Employees { get; set; } = new List<EmployeeProject>();
    }
}