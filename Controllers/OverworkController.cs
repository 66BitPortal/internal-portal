using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using _66bitProject.Models;
using _66bitProject.Data;

namespace _66bitProject.Controllers
{
    public class OverworkController : Controller
    {
        private ApplicationDbContext db;
        public OverworkController(ApplicationDbContext context)
        {
            db = context;
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Overwork overwork)
        {
            db.Overworks.Add(overwork);
            await db.SaveChangesAsync();
            return View();
        }
    }
}
