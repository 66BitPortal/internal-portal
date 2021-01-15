using _66bitProject.Data;
using _66bitProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _66bitProject.Controllers
{
    public class ManagerController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;
        
        public ManagerController(ApplicationDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var currentUser = await context.Users.Include(u => u.Costs).Where(u => u.Id == user.Id).SingleAsync();
            ViewBag.Costs = currentUser.Costs.Where(c => c.Status ?? false).Sum(c => c.Value);
            ViewBag.Projects = await context.Projects.Where(p => p.ManagerId == user.Id).ToListAsync();
            return View(currentUser);
        }

        [Authorize(Roles = "manager")]
        public IActionResult ManagerOwnCosts()
        {
            return View();
        }

        //[Authorize(Roles = "manager")]
        //public IActionResult ManagerDisplayEmployeeCosts()
        //{
        //    var costs = context.EmployeeCosts.ToList();
        //    return View(costs);
        //}

        [Authorize(Roles = "manager")]
        public IActionResult ManagerOwnOverworks()
        {
            return View();
        }

        [Authorize(Roles = "manager")]
        public async Task<IActionResult> ProjectStats(int projectId)
        {
            var project = await context.Projects.Include(p => p.Employees).ThenInclude(ep => ep.Employee).FirstAsync();
            return View(project);
        }
    }
}
