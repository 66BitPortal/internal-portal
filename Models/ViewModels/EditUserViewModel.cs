using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace _66bitProject.Models.ViewModels
{
    public class EditUserViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        public string NewPassword { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public int HourPayment { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; }
        //Сделать ограничение на размер (одна/две? или больше?)
        [Required]
        public int PaymentFrequency { get; set; }
        public virtual ICollection<string> Roles { get; set; }
        public virtual ICollection<IdentityUserClaim<int>> UserClaims { get; set; }
        public virtual ICollection<IdentityRole<int>> AllRoles { get; set; }
    }
}
