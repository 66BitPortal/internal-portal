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
            var user = GetCurrentUserAsync().Result;
            return View(db.EmployeeRevenues.Where(er => er.PersonId == user.Id));
        }

        [HttpGet]
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

        public IActionResult GetUnpaidRevenues(int id)// возвращает невыплаченные доходы сотрудника
        {
            var unpaidRev = db.EmployeeRevenues.Where(x => x.PersonId == id && !x.Status);
             return View(unpaidRev);
        }
    }
}