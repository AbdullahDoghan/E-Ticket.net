using E_Ticket.net.Data.DbContextConfg;
using E_Ticket.net.Data.Static;
using E_Ticket.net.Data.Vm;
using E_Ticket.net.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticket.net.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _Db;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _Db = db;
        }

        public async Task<IActionResult> Users()
        {
            var users = await _Db.Users.ToListAsync();
            return View(users);

        }

        public IActionResult Login() => View(new LoginVm());


        [HttpPost]
        public async Task<IActionResult> Login(LoginVm loginVm)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVm);
            }

            var user = await _userManager.FindByEmailAsync(loginVm.EmailAddress);
            if (user != null)
            {
                var passwordc = await _userManager.CheckPasswordAsync(user , loginVm.Password);
                if (passwordc)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVm.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Movies");
                    }
                    TempData["Error"] = "Wrong credentials. Plase try again!";
                    return View(loginVm);
                }
            }
            TempData["Error"] = "Wrong credentials. Plase try again!";
            return View(loginVm);
        }
        public IActionResult Register() => View(new RegisterVm());

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm registerVm)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVm);
            }
            var user =await _userManager.FindByEmailAsync(registerVm.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This Email Address is aleardy in use!";
                return View(registerVm);
            }
            var newUser = new ApplicationUser()
            {
                FullName = registerVm.FullName,
                Email = registerVm.EmailAddress,
                UserName = registerVm.EmailAddress,
            };
            var newUserRespons = await _userManager.CreateAsync(newUser, registerVm.Password);
            if (newUserRespons.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }
            return View("RegisterCompleted");
        
        }
        [HttpPost]

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Movies");
        }

    }
    }
    
