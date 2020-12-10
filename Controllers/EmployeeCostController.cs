using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using _66bitProject.Models;

namespace _66bitProject.Controllers
{
    public class EmployeeCostController
    {
        private ApplicationContext db;
        public EmployeeCostController (ApplicationContext context)
        {
            db = context;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Cost Cost)
        {
            
        }
    }
}
