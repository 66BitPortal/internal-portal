using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _66bitProject.Models
{
    public class EmployeeCost
    {
        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        [Key]
        public int CostId { get; set; }
        public string Description { get; set; }
        public string Confirmation { get; set; }
        public string Value { get; set; }
        public int Status { get; set; }

    }
}