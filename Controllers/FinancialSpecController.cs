using _66bitProject.Data;
using _66bitProject.Models;
using _66bitProject.Models.ViewModels;
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
    public class FinancialSpecController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole<int>> roleManager;

        public FinancialSpecController(UserManager<User> userManager, ApplicationDbContext context, RoleManager<IdentityRole<int>> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [Authorize(Roles = "financialSpec")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "financialSpec")]
        public IActionResult DisplayAllEmployeesRevenues()
        {
            var users = context.Users.ToList();
            return View("AllRevenues", users);
        }

        [Authorize(Roles = "financialSpec")]
        [HttpGet]
        public async Task<IActionResult> DisplayAllEmployeeBonuses()
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            var model = new EmployeeBonusesViewModel();
            var adminRole = await roleManager.Roles.SingleAsync(r => r.Name.Equals("admin"));
            var test = userManager.Users.Include(u => u.Bonuses).Include(u => u.Roles);
            var test1 = context.UserRoles.ToList();
            var filteredUsers = userManager.Users.Include(u => u.Bonuses).ToList();
            filteredUsers = filteredUsers.Where(u => !userManager.GetRolesAsync(u).Result.Contains("admin") && u.Id != currentUser.Id).ToList();
            var currentMonthSumsPerUser = new Dictionary<int, int>();
            foreach (var user in filteredUsers)
            {
                var filteredBonuses = user.Bonuses.Where(b => b.Date.Month == DateTime.Now.Month && b.Date.Year == DateTime.Now.Year);
                currentMonthSumsPerUser.Add(user.Id, filteredBonuses.Sum(b => b.Value));
            }
            model.BonusesSums = currentMonthSumsPerUser;
            model.Employees = filteredUsers.ToList();
            ViewBag.BonusesSum = filteredUsers.Select(u => u.Bonuses).SelectMany(b => b).Sum(b => b.Value);
            return View("AllBonuses", model);
        }

        [Authorize(Roles = "financialSpec")]
        [HttpGet]
        public async Task<IActionResult> CreateBonusForEmployee(int userId)
        {
            var model = new CreateBonusViewModel();
            var user = await userManager.FindByIdAsync(userId.ToString());
            model.FullName = user.FullName;
            model.UserId = userId;
            return View("CreateBonus", model);
        }

        [Authorize(Roles = "financialSpec")]
        [HttpPost]
        public async Task<IActionResult> CreateBonusForEmployee(CreateBonusViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId.ToString());
            var bonus = new Bonus
            {
                Commentary = model.Commentary,
                Date = DateTime.Now,
                Value = model.Value,
                Employee = user
            };
            user.Bonuses.Add(bonus);
            await context.SaveChangesAsync();
            return RedirectToAction("DisplayAllEmployeeBonuses");
        }

        [Authorize(Roles = "financialSpec")]
        [HttpGet]
        public IActionResult ShowAllExpenses()
        {
            var users = userManager.Users.Include(u => u.Bonuses).Include(u => u.Revenues).
                Include(u => u.Costs).Include(u => u.Overworks);
            ViewBag.BonusesTotal = users.SelectMany(u => u.Bonuses).
                Where(b => b.Date.Month == DateTime.Now.Month && b.Date.Year == DateTime.Now.Year)
                .Sum(b => b.Value);
            ViewBag.TotalRevs = users.Sum(u => u.MothlyPayment);
            ViewBag.TotalCosts = users.SelectMany(u => u.Costs).Where(ec => ec.Status ?? false
            && ec.Date.Month == DateTime.Now.Month
            && ec.Date.Year == DateTime.Now.Year)
                .Sum(c => c.Value);
            ViewBag.TotalOverworks = users.SelectMany(u => u.Overworks).Where(o => o.Status ?? false
            && o.Date.Year == DateTime.Now.Year
            && o.Date.Month == DateTime.Now.Month)
                .Sum(o => o.CalculatedPayment);
            ViewBag.TotalForMonth = ViewBag.BonusesTotal + ViewBag.TotalCosts + ViewBag.TotalRevs + ViewBag.TotalOverworks;
            return View("AllExpenses");
        }

        [Authorize(Roles = "financialSpec")]
        [HttpGet]
        public IActionResult ShowAllCosts()
        {
            var costs = context.EmployeeCosts.Include(ec => ec.Employee).ToList().Where(ep => ep.Status ?? false).ToList();
            return View("AllCosts", costs);
        }

        [Authorize(Roles = "financialSpec")]
        [HttpGet]
        public IActionResult ShowRevenuesList()
        {
            var users = userManager.Users.ToList();
            ViewBag.TotalRev = users.Sum(u => u.MothlyPayment);
            return View("RevenuesList", users);
        }

        [Authorize(Roles = "financialSpec")]
        [HttpGet]
        public IActionResult ShowCostInfo(int id)
        {
            var cost = context.EmployeeCosts.Include(ec => ec.Employee).Where(ep => ep.Id == id).Single();
            var model = new CostInfoViewModel
            {
                Category = cost.Category,
                Date = cost.Date,
                Description = cost.Description,
                EmployeeFullName = cost.Employee.FullName,
                Name = cost.Name,
                Value = cost.Value
            };
            return View("CostInfo", model);
        }

        [Authorize(Roles = "financialSpec")]
        [HttpGet]
        public IActionResult ShowAllOverworks()
        {
            var overworks = context.Overworks.Include(o => o.Person).Where(o => o.Status ?? false).ToList();
            return View(overworks);
        }

        [Authorize(Roles = "financialSpec")]
        [HttpGet]
        public IActionResult ShowOverworkInfo(int id)
        {
            var overwork = context.Overworks.Include(o => o.Person).Include(o => o.Project).Where(o => o.Id == id).Single();
            var model = new OverworkInfoViewModel
            {
                Description = overwork.Description,
                CalculatedValue = overwork.CalculatedPayment,
                EmployeeFullName = overwork.Person.FullName,
                HoursCount = overwork.HoursCount,
                ProjectName = overwork.Project.Name
            };
            return View("OverworkInfo", model);
        }

        //public IActionResult
    }
}
