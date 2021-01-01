using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _66bitProject.Models
{
    public class EmployeeCost//расходы сотрудника
    { 
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        
        public string Description { get; set; }//описание
        public string Name { get; set; }//название
        public int Value { get; set; }//сумма
        public string Category { get; set; }//категория
        public int Status { get; set; }//1 or 0, bool
        public DateTime Date { get; set; }

    }
}