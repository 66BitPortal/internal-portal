using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _66bitProject.Models.ViewModels
{
    public class CreateBonusViewModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        [Required]
        public int Value { get; set; }
        [Required]
        public string Commentary { get; set; }
    }
}
