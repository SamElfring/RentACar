using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentACar.Models;
using RentACar.Models.ViewModels;
using RentACar.Services;
using RentACar.Utility;
using System;
using System.Threading.Tasks;

namespace RentACar.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;
        IUserService _userService;

        public AccountController(ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IUserService userService)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Inloggen mislukt");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> Register()
        {
            // Create roles if they do not exists
            if (!_roleManager.RoleExistsAsync(Helper.Admin).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole(Helper.Admin));
                await _roleManager.CreateAsync(new IdentityRole(Helper.Employee));
                await _roleManager.CreateAsync(new IdentityRole(Helper.User));

                await CreateStandardUsers();
            }

            ViewBag.IsAdmin = await _userService.IsAdmin();
            return View();
        }

        private async Task CreateStandardUsers()
        {
            ApplicationUser admin = new ApplicationUser()
            {
                UserName = "Beheerder_Account",
                Email = "beheerderaccount@gmail.com",
                Initials = "B.A.",
                MiddleName = null,
                LastName = "Account",
                Adress = "Gieterij 200",
                City = "Hengelo",
                ZipCode = "2569JK"
            };
            await _userManager.CreateAsync(admin, "Beheerder!1");
            await _userManager.AddToRoleAsync(admin, "Admin");

            ApplicationUser employee = new ApplicationUser()
            {
                UserName = "Werknemer_Account",
                Email = "werknemeraccount@gmail.com",
                Initials = "W.A.",
                MiddleName = null,
                LastName = "Account",
                Adress = "Gieterij 200",
                City = "Hengelo",
                ZipCode = "2569JK"
            };
            await _userManager.CreateAsync(employee, "Werknemer!1");
            await _userManager.AddToRoleAsync(employee, "Employee");

            ApplicationUser user = new ApplicationUser()
            {
                UserName = "Gebruiker_Account",
                Email = "gebruikeraccount@gmail.com",
                Initials = "G.A.",
                MiddleName = null,
                LastName = "Account",
                Adress = "Gieterij 200",
                City = "Hengelo",
                ZipCode = "2569JK"
            };
            await _userManager.CreateAsync(user, "Gebruiker!1");
            await _userManager.AddToRoleAsync(user, "User");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Initials = model.Initials,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    Adress = model.Adress,
                    City = model.City,
                    ZipCode = model.ZipCode
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.RoleName);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                // Add all errors to modelstate
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }
    }
}
