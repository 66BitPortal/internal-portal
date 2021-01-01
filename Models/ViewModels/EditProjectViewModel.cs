using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _66bitProject.Models.ViewModels
{
    public class EditProjectViewModel
    {
        public int ProjectId { get; set; }
        [Required]
        public string Name { get; set; }
        public int ManagerId { get; set; }
        public ICollection<int> DevelopersId { get; set; } = new List<int>();
        public ICollection<User> AllManagers { get; set; }
        public ICollection<User> AllDevelopers { get; set; }
    }
}
