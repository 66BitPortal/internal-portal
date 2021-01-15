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
    public class ProgrammerController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ApplicationDbContext context;

        public ProgrammerController(UserManager<User> userManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        [Authorize(Roles = "employee")]
        //Все вычисления и вызовы к ним проводить здесь, готовые резы посылаем на индекс
        public async Task<IActionResult> Index()
        {
            var userId = userManager.GetUserAsync(HttpContext.User).Result.Id;
            var currentUser = await context.Users.Include(u => u.Costs).Where(u => u.Id == userId).SingleAsync();
            ViewBag.Costs = currentUser.Costs.Where(c => c.Status ?? false).Sum(c => c.Value);
            return View(currentUser);
        }

        [Authorize(Roles = "employee")]
        public IActionResult EmployeeOwnCosts()
        {
            return View();
        }

        [Authorize(Roles = "employee")]
        public IActionResult EmployeeOwnOverworks()
        {
            return View();
        }
    }
}
