using _66bitProject.Data;
using _66bitProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace _66bitProject
{

    public class TestInitializer
    {
        public static async Task Initialize(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            string adminMail = "AdminTest@gmail.com";
            string adminPass = "12345";

            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>("admin"));
            }
            if (await roleManager.FindByNameAsync("employee") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>("employee"));
            }
            if (await userManager.FindByEmailAsync(adminMail) == null)
            {
                User admin = new User
                {
                    Email = adminMail,
                    UserName = adminMail,
                    FullName = "Иванов Пётр Сергеевич",
                    PhoneNumber = "+79089256384",
                    BirthDate = new System.DateTime(1990, 5, 20),
                    PaymentDay = new System.DateTime(2021, 1, 25),
                    HourPayment = 500
                };
                IdentityResult result = await userManager.CreateAsync(admin, adminPass);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
