using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _66bitProject.Models.ViewModels
{
    public class CreateUserViewModel
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public int HourPayment { get; set; }
        [Required]
        public int MonthlyPayment { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; }
        //Сделать ограничение на размер (одна/две? или больше?)
        [Required]
        public int PaymentFrequency { get; set; }
        public virtual ICollection<IdentityRole<int>> Roles { get; set; } = new List<IdentityRole<int>>();
        public virtual ICollection<IdentityUserClaim<int>> UserClaims { get; set; } = new List<IdentityUserClaim<int>>();
        public virtual ICollection<IdentityRole<int>> AllRoles { get; set; }
    }
}
