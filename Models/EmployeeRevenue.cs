using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _66bitProject.Models
{
    public class EmployeeRevenue//Доходы сотрудника
    {
        [ForeignKey("PersonId")]
        public User Person { get; set; }
        public int PersonId { get; set; }
        [Key]
        public int Id { get; set; }

        public string Category { get; set; }// ЗП либо дополнительные начисления
        public int Value { get; set; }//сумма
        public DateTime date { get; set; }
    }
}
