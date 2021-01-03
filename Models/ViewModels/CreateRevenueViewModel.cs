using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _66bitProject.Models.ViewModels
{
    public class CreateRevenueViewModel
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public bool Status { get; set; }
        public int Value { get; set; }
        public DateTime Date { get; set; }
    }
}
