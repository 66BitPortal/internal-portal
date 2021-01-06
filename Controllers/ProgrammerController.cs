using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _66bitProject.Controllers
{
    public class ProgrammerController : Controller
    {
        [Authorize(Roles = "employee")]
        //Все вычисления и вызовы к ним проводить здесь, готовые резы посылаем на индекс
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "employee")]
        public IActionResult EmployeeOwnCosts()
        {
            return View();
        }
    }
}
