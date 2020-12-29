using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using _66bitProject.Models;
using _66bitProject.Data;

namespace _66bitProject.Controllers
{
    public class EmployeeRevenueController : Controller
    {
        private ApplicationDbContext db;
        private readonly UserManager<User> _userManager;
        public EmployeeRevenueController(ApplicationDbContext context, UserManager<User> userManager)
        {
            db = context;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<int> GetCurrentUserId()
        {
            User user = await GetCurrentUserAsync();
            return user.Id;
        }
        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

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

        public IActionResult GetUnpaidRevenues()// возвращает невыплаченные доходы сотрудника
        {
            var unpaidRev = db.EmployeeRevenues.Where(x => x.PersonId == GetCurrentUserId().Result && x.Status == 0);
             return View(unpaidRev);
        }
    }
}
