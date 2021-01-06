using _66bitProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _66bitProject.Controllers
{
    public class ManagerController : Controller
    {
        private readonly ApplicationDbContext context;
        
        public ManagerController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [Authorize(Roles = "manager")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "manager")]
        public IActionResult ManagerOwnCosts()
        {
            return View();
        }

        [Authorize(Roles = "manager")]
        public IActionResult ManagerDisplayEmployeeCosts()
        {
            var costs = context.EmployeeCosts.ToList();
            return View(costs);
        }

        [Authorize(Roles = "manager")]
        public IActionResult ManagerOwnOverworks()
        {
            return View();
        }
    }
}
