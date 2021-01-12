using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _66bitProject.Models.ViewModels
{
    public class Bonus
    {
        [Key]
        public int Id { get; set; }
        public User Employee { get; set; }
        public int Value { get; set; }
        public string Commentary { get; set; }
        public DateTime date { get; set; }
    }
}
