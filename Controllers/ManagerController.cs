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
    }
}
