using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _66bitProject.Models
{
    public class Department
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public ICollection<User> Employees { get; set; }
    }
}
