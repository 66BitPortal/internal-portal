﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _66bitProject.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //Возможно не нужно?
        public ICollection<Overwork> Overworks { get; set; }
    }
}