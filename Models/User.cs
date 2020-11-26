using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace _66bitProject.Models
{
    public class User : IdentityUser<int>
    {
        #region Поля профиля

        public string FirstName { get; set; }
        public int Year { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Position { get; set; }

        public Department DepartmentID { get; set; }

        #endregion

        /// <summary>
        /// Список ролей пользователя
        /// </summary>
        public virtual ICollection<IdentityRole<int>> Roles { get; set; } = new List<IdentityRole<int>>();

        /// <summary>
        /// Список разрешений пользователя
        /// </summary>
        public virtual ICollection<IdentityUserClaim<int>> UserClaims { get; set; } = new List<IdentityUserClaim<int>>();



    }
}
