using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _66bitProject.Models;
using _66bitProject.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _66bitProject.Controllers
{
    public class UserController : Controller
    {
        UserManager<User> userManager;

        public UserController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName,
                    BirthDate = model.BirthDate,
                    Position = model.Position,
                    Department = model.Department
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    //Redirect
                }
                else
                {
                    //throw some kind of Exception or Redirect?
                }
            }

            return View(model);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.MiddleName = model.MiddleName;
                    user.BirthDate = model.BirthDate;
                    user.Position = model.Position;
                    user.Department = model.Department;
                   // user.Revenue = new EmployeeRevenue { Amount = model.RevAmount, PaymentFrequency = model.PaymentFrequency, Employee = user };

                    var result = await userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        //return some kind of success note?
                    }
                }
                else
                {
                    //Is this possible in our situation?
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                EditUserViewModel model = new EditUserViewModel
                {
                    BirthDate = user.BirthDate,
                    Department = user.Department,
                    FirstName = user.FirstName,
                    Id = id,
                    LastName = user.LastName,
                    MiddleName = user.MiddleName,
                    Revenue = user.Revenue,
                    Position = user.Position,
                    Roles = user.Roles
                };
                return View(model);
            }
            //Is it even possible to not find user in our situation?
            return NotFound();
        }


    }
}
