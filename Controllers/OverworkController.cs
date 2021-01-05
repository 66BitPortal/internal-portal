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
        private readonly UserManager<User> _userManager;
        [HttpGet]
        public async Task<int> GetCurrentUserId()
        {
            User user = await GetCurrentUserAsync();
            return user.Id;
        }

        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public OverworkController(ApplicationDbContext context, UserManager<User> userManager)
        {
            db = context;
            _userManager = userManager;
        }

        //[Authorize(Roles="admin")]
        public IActionResult ShowAll()
        {     
            return View(db.Overworks.ToList());
        }// НУЖНО СДЕЛАТЬ ДЛЯ АДМИНА ИЛИ МЕНЕДЖЕРА ОТОБРАЖЕНИЕ ВСЕХ ПЕРЕРАБОТОК

        //[Authorize(Roles = "employee")]
        [Route("overwork")]
        public IActionResult Index()
        {
            var userId = GetCurrentUserId().Result;
            return View(db.Overworks.Where(x => x.PersonId == userId));
        }




        //[Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult ChangeStatus(int? id)
        {
            var newOverwork = db.Overworks.Where(x => x.Id == id).FirstOrDefault();
            newOverwork.Status = !newOverwork.Status;
            db.Overworks.Update(newOverwork);
            return RedirectToAction("Index");//хз куда редиректить
        }//Изменение статуса, в дизайне этого нет

        public IActionResult Create()
        {
            ViewBag.Projects = db.Projects.ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOverworkViewModel overwork)
        {
            var newOverwork = new Overwork() { Description = overwork.Description, HoursCount = overwork.HoursCount };
            var project = db.Projects.Where(x => x.Id.ToString() == overwork.Project).FirstOrDefault();
            var personId = GetCurrentUserId();
            var person = db.Users.Where(x => x.Id == personId.Result).FirstOrDefault();
            var date = DateTime.Today;
            var status = false;
            newOverwork.Project = project;
            newOverwork.ProjectId = project.Id;
            newOverwork.Person = person;
            newOverwork.PersonId = person.Id;
            newOverwork.Date = date;
            newOverwork.Status = status;
            db.Overworks.Add(newOverwork);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}