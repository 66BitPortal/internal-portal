using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using _66bitProject.Models.ViewModels;
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
        public int MothlyPayment { get; set; }
        public ICollection<EmployeeRevenue> Revenues { get; set; } = new List<EmployeeRevenue>();
        public ICollection<Overwork> Overworks { get; set; } = new List<Overwork>();
        public ICollection<EmployeeCost> Costs { get; set; } = new List<EmployeeCost>();
        public ICollection<EmployeeProject> Projects { get; set; } = new List<EmployeeProject>();
        public ICollection<Bonus> Bonuses { get; set; } = new List<Bonus>();

        #endregion

        /// <summary>
        /// Список ролей пользователя
        /// </summary>
        public ICollection<IdentityRole<int>> Roles { get; set; } = new List<IdentityRole<int>>();

        /// <summary>
        /// Список разрешений пользователя
        /// </summary>
        public virtual ICollection<IdentityUserClaim<int>> UserClaims { get; set; }
    }
}
