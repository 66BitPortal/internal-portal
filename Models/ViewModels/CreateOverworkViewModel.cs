using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _66bitProject.Models.ViewModels
{
    public class CreateOverworkViewModel
    {
        public string Project { get; set; }// где переработал
        public int HoursCount { get; set; }// сколько переработал
        public string Description { get; set; }//комментарий
    }
}
