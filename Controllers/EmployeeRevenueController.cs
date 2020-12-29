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
    public class EmployeeRevenueController : Controller
    {
        private ApplicationDbContext db;
        public EmployeeRevenueController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.EmployeeRevenues.ToListAsync());
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeRevenue revenue)
        {
            db.EmployeeRevenues.Add(revenue);
            await db.SaveChangesAsync();
            return View();
        }
    }
}
