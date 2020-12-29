using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using _66bitProject.Models;
using _66bitProject.Data;

namespace _66bitProject.Controllers
{
    public class EmployeeCostController : Controller
    {
        private ApplicationDbContext db;
        public EmployeeCostController (ApplicationDbContext context)
        {
            db = context;
        }
        [Route("employeeCost")]
        public async Task<IActionResult> Index()
        {
            
            return View(await db.EmployeeCosts.ToListAsync());
        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCost cost)
        {

            db.EmployeeCosts.Add(cost);
            await db.SaveChangesAsync();
            return View();
        }
    }
}
