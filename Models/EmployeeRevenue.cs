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
        public virtual User Person { get; set; }
        [ForeignKey("PersonId")]
        public int PersonId { get; set; }
        [Key]
        public int RevenueId { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }
        public DateTime Date { get; set; }
    }
}
