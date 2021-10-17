using EventsApp.Domain.POCO;
using EventsApp.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EventsApp.AdminPanel.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            var userName = loginVM.Email;
            bool isAdmin = false;
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginVM.Email);
                var userRoles = await _userManager.GetRolesAsync(user);
                isAdmin = userRoles.Contains(Roles.Admin.ToString());

                if (user != null)
                {
                    userName = user.UserName;
                }
            }

            var result = !isAdmin ? Microsoft.AspNetCore.Identity.SignInResult.Failed :
                await _signInManager.PasswordSignInAsync(userName, loginVM.Password, loginVM.RememberMe, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }
            return Redirect("/");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            ViewBag.LoggedOut = true;
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}