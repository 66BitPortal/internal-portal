using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using _66bitProject.Models;
using _66bitProject.Data;
using Microsoft.AspNetCore.Identity;
using _66bitProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace _66bitProject.Controllers
{
    public class EmployeeCostController : Controller
    {
        private ApplicationDbContext db;
        private readonly UserManager<User> UserManager;

        public EmployeeCostController (ApplicationDbContext context, UserManager<User> userManager)
        {
            UserManager = userManager;
            db = context;
        }

        //Отображение расходов для программиста
        [Authorize(Roles = "employee, manager")]
        public IActionResult ShowOwnCosts()
        {
            var currentUserId = int.Parse(UserManager.GetUserId(HttpContext.User));
            var costs = db.EmployeeCosts.Where(c => c.Employee.Id== currentUserId).ToList();
            return View(costs);
        }
        
        [Authorize(Roles = "employee, manager")]
        [HttpGet]
        public IActionResult Create()
        {
            CreateEmployeeCostViewModel model = new CreateEmployeeCostViewModel();
            return View(model);
        }

        [Authorize(Roles = "employee, manager")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeCostViewModel model)
        {
            var user = await UserManager.GetUserAsync(HttpContext.User);
            var cost = new EmployeeCost
            {
                Category = model.Category,
                Date = model.Date,
                Description = model.Description,
                Value = model.Value,
                Name = model.Name,
                Employee = user
            };
            await db.EmployeeCosts.AddAsync(cost);
            await db.SaveChangesAsync();
            return RedirectToAction("ShowOwnCosts");
        }
    }
}