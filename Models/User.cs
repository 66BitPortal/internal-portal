using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace _66bitProject.Models
{
    public class User : IdentityUser<int>
    {
        #region Поля профиля
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public int HourPayment { get; set; }
        public DateTime PaymentDay { get; set; }
        public int NumberOfPayments { get; set; }
        public ICollection<EmployeeRevenue> Revenues { get; set; }
        public ICollection<Overwork> Overworks { get; set; }
        public ICollection<EmployeeCost> Costs { get; set; }
        public ICollection<EmployeeProject> Projects { get; set; } = new List<EmployeeProject>();

        #endregion

        /// <summary>
        /// Список ролей пользователя
        /// </summary>
        public virtual ICollection<IdentityRole<int>> Roles { get; set; }

        /// <summary>
        /// Список разрешений пользователя
        /// </summary>
        public virtual ICollection<IdentityUserClaim<int>> UserClaims { get; set; }
    }
}
