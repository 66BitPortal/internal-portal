using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _66bitProject.Models;
using _66bitProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _66bitProject.Controllers
{
    public class UserController : Controller
    {
        UserManager<User> userManager;
        RoleManager<IdentityRole<int>> roleManager;

        public UserController(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;

        }

        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            return View(userManager.Users.ToList());
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model, string role)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    FullName = model.FullName,
                    BirthDate = model.BirthDate,
                    HourPayment = model.HourPayment,
                    PaymentDay = model.PaymentDate,
                    NumberOfPayments = model.PaymentFrequency,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.Email
            };
                //Для пароля нужно написать верификатор, пока что для тестов сделал так
                if (String.IsNullOrEmpty(model.Password))
                {
                    //Здесь нужно выдавать оповещение об ошибке, пока что заглушка
                    return NotFound();
                }
                var result = await userManager.CreateAsync(user, model.Password);
                var roleResult = await userManager.AddToRoleAsync(user, role);
                if (result.Succeeded && roleResult.Succeeded)
                {
                    return Redirect("Index");
                }
                else
                {
                    //Return error?
                }
            }

            return View(model);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            var model = new CreateUserViewModel();
            model.AllRoles = roleManager.Roles.ToList();
            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model, string role)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.Users.SingleAsync(u => u.Id == model.Id);
                if (user != null)
                {
                    user.FullName = model.FullName;
                    user.BirthDate = model.BirthDate;
                    user.HourPayment = model.HourPayment;
                    user.Email = model.Email;
                    user.NumberOfPayments = model.PaymentFrequency;
                    user.PaymentDay = model.PaymentDate;
                    user.PhoneNumber = model.PhoneNumber;
                    user.UserName = model.Email;
                    var oldRoles = await userManager.GetRolesAsync(user);
                    //Защита от снятия админом роли с самого себя, плохой код, но идей пока нет
                    if (!oldRoles.Contains("admin") && (role != "admin"))
                    {
                        await userManager.RemoveFromRolesAsync(user, oldRoles);
                        await userManager.AddToRoleAsync(user, role);
                    }
                    if (!String.IsNullOrEmpty(model.NewPassword))
                    {
                        await userManager.RemovePasswordAsync(user);
                        await userManager.AddPasswordAsync(user, model.NewPassword);
                    }
                    var result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        //Для тестов
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id)
        {
            User user = await userManager.Users.Include(u => u.Roles)
                .SingleAsync(u => u.Id == id);
            var userRoles = await userManager.GetRolesAsync(user);
            var roles = roleManager.Roles.ToList();
            if (user != null)
            {
                EditUserViewModel model = new EditUserViewModel
                {
                    FullName = user.FullName,
                    Email = user.Email,
                    BirthDate = user.BirthDate,
                    Id = id,
                    AllRoles = roles,
                    Roles = userRoles,
                    PaymentDate = user.PaymentDay,
                    PaymentFrequency = user.NumberOfPayments,
                    HourPayment = user.HourPayment,
                    PhoneNumber = user.PhoneNumber,
                };
                return View(model);
            }
            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            User user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                IdentityResult res = await userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
    }
}
