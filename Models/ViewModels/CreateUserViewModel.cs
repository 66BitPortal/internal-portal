using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _66bitProject.Models.ViewModels
{
    public class CreateUserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Position { get; set; }
        public Department Department { get; set; }
        public EmployeeRevenue Revenue { get; set; }
        public virtual ICollection<IdentityRole<int>> Roles { get; set; } = new List<IdentityRole<int>>();
        public virtual ICollection<IdentityUserClaim<int>> UserClaims { get; set; } = new List<IdentityUserClaim<int>>();
    }
}
