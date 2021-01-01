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
    public class ProjectController : Controller
    {
        private ApplicationDbContext Context { get; set; }
        private UserManager<User> userManager { get; set; }

        public ProjectController(UserManager<User> userManager, ApplicationDbContext applicationDbContext)
        {
            this.userManager = userManager;
            this.Context = applicationDbContext;
        }

        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            return View(Context.Projects.ToList());
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create()
        {
            var allManagers = await userManager.GetUsersInRoleAsync("manager");
            var allDevs = await userManager.GetUsersInRoleAsync("employee");
            var model = new CreateProjectViewModel()
            {
                AllDevelopers = allDevs.ToList(),
                AllManagers = allManagers.ToList()
            };
            return View(model);
        }
        
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectViewModel model)
        {
            var project = new Project() { Name = model.Name, ManagerId = model.ManagerId, Employees = new List<EmployeeProject>() };
            await Context.Projects.AddAsync(project);
            foreach (var dev in model.DevelopersId) 
            {
                var developer = await userManager.FindByIdAsync(dev.ToString());
                var employeeProject = new EmployeeProject { Employee = developer, Project = project };
                var res = await Context.EmployeeProjects.AddAsync(employeeProject);
            }
            await Context.SaveChangesAsync();
            //Если успешно добавили всё, вовзращаемся к списку проектов
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int projectId)
        {
            var model = new EditProjectViewModel();
            var project = Context.Projects.Include(e => e.Employees).Where(p => p.Id == projectId).Single();
            var employees = await userManager.GetUsersInRoleAsync("employee");
            var manager = await userManager.GetUsersInRoleAsync("manager");
            model.AllDevelopers = employees;
            model.AllManagers = manager;
            model.ManagerId = project.ManagerId;
            model.DevelopersId = project.Employees.Select(p => p.EmployeeId).ToList();
            model.Name = project.Name;
            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditProjectViewModel model)
        {
            var project = Context.Projects.Include(p => p.Employees).Where(p => p.Id == model.ProjectId).Single();
            if (project != null)
            {
                var employeeProjects = Context.EmployeeProjects.Where(ep => ep.ProjectId == model.ProjectId);
                Context.EmployeeProjects.RemoveRange(employeeProjects);
                foreach (var dev in model.DevelopersId)
                {
                    var developer = await userManager.FindByIdAsync(dev.ToString());
                    var employeeProject = new EmployeeProject { Employee = developer, Project = project };
                    var res = await Context.EmployeeProjects.AddAsync(employeeProject);
                }
                project.Name = model.Name;
                project.ManagerId = model.ManagerId;
            }
            await Context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int projectId)
        {
            var project = Context.Projects.FindAsync(projectId);
            if (project != null)
            {
                var employeeProjects = Context.EmployeeProjects.Where(ep => ep.ProjectId == projectId);
                Context.EmployeeProjects.RemoveRange(employeeProjects);
                Context.Projects.Remove(project.Result);
                var test = await Context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
