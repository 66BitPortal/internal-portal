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
        [ForeignKey("EmployeeId")]
        public User Employee { get; set; }
        public int EmployeeId { get; set; }
        [Key]
        public int RevenueId { get; set; }
        public int Amount { get; set; }
        public int PaymentFrequency { get; set; }
    }
}
