using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _66bitProject.Models
{
    public class EmployeeRevenue
    {
        [ForeignKey("PersonId")]
        public User Person { get; set; }
        public int PersonId { get; set; }
        [Key]
        public int RevenueId { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }
        public DateTime date { get; set; }
    }
}
