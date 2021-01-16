using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using _66bitProject.Models;
using _66bitProject.Data;
using _66bitProject.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace _66bitProject.Controllers
{
    public class OverworkController : Controller
    {
        private ApplicationDbContext db;
        private readonly UserManager<User> userManager;
        [HttpGet]
        public async Task<int> GetCurrentUserId()
        {
            User user = await GetCurrentUserAsync();
            return user.Id;
        }

        private async Task<User> GetCurrentUserAsync() => await userManager.GetUserAsync(HttpContext.User);

        public OverworkController(ApplicationDbContext context, UserManager<User> userManager)
        {
            db = context;
            this.userManager = userManager;
        }

        [Authorize(Roles = "employee, manager")]
        public IActionResult ShowOwnOverworks()
        {
            var currentUserId = int.Parse(userManager.GetUserId(HttpContext.User));
            var overworks = db.Overworks.Include(o => o.Person).Where(o => o.Person.Id == currentUserId);
            return View(overworks);
        }

        [Authorize(Roles = "manager")]
        public IActionResult AllOverworks()
        {
            var overworks = db.Overworks.Include(o => o.Person);
            return View(overworks);
        }

        [Authorize(Roles = "manager")]
        [HttpGet]
        public async Task<IActionResult> ChangeStatus(int? id)
        {
            var overwork = await db.Overworks.Include(o => o.Person).Where(o => o.Id == id).SingleAsync();
            return View(overwork);
        }

        [Authorize(Roles = "manager")]
        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int id, bool status)
        {
            var overwork = await db.Overworks.FindAsync(id);
            overwork.Status = status;
            await db.SaveChangesAsync();
            return RedirectToAction("AllOverworks");
        }

        [Authorize(Roles = "employee, manager")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Projects = await db.Projects.ToListAsync();
            CreateOverworkViewModel model = new CreateOverworkViewModel();
            return View(model);
        }

        [Authorize(Roles = "employee, manager")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateOverworkViewModel model)
        {
            var project = await db.Projects.Where(p => p.Id == model.ProjectId).SingleAsync();
            var personId = GetCurrentUserId().Result;
            var person = await db.Users.FindAsync(personId);
            var newOverwork = new Overwork() {
                Description = model.Description,
                HoursCount = model.HoursCount,
                Date = DateTime.Today,
                Project = project,
                Person = person,
                CalculatedPayment = person.HourPayment * 2 * model.HoursCount
            };
            await db.Overworks.AddAsync(newOverwork);
            await db.SaveChangesAsync();
            return RedirectToAction("ShowOwnOverworks");
        }
    }
}