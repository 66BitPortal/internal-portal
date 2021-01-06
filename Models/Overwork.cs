using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _66bitProject.Models
{
    public class Overwork
    {
        public User Person { get; set; }//кто переработал
        public int Id { get; set; }
        public Project Project { get; set; }// где переработал
        public int HoursCount { get; set; }// сколько переработал
        public bool? Status { get; set; }//одобрено \ не одобрено
        public string Description { get; set; }//комментарий
        public DateTime Date { get; set; }//когда переработал
    }
}