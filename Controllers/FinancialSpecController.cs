using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _66bitProject.Controllers
{
    public class FinancialSpecController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
