using EventsApp.Domain.POCO;
using EventsApp.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EventsApp.AdminPanel.Controllers
{
    [Authorize(Policy = "AtLeastAdmin")]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _userManager.Users.ToListAsync());
        }

        [HttpGet("[action]")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    MailAddress address = new MailAddress(registerVM.Email);
                    string userName = address.User;
                    var user = new ApplicationUser
                    {
                        UserName = userName,
                        Email = registerVM.Email,
                        FirstName = registerVM.FirstName,
                        LastName = registerVM.LastName
                    };

                    var result = await _userManager.CreateAsync(user, registerVM.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());

                        return Redirect("/");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else ModelState.AddModelError(string.Empty, "invalid register attempt");

                return View();
            }
            catch (Exception)
            {
                return View("Error", "Home");
            }
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            return View(await _userManager.FindByIdAsync(id));
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> Edit([FromRoute] string id, [FromForm] ApplicationUser model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByIdAsync(id);
                    user.Email = model.Email;
                    user.UserName = model.UserName;
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;

                    var result = (await _userManager.UpdateAsync(user));
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                return View();
            }
            catch (Exception)
            {
                return View("Error", "Home");
            }
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            return View(await _userManager.FindByIdAsync(id));
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                return View();
            }
            catch (Exception)
            {
                return View("Error", "Home");
            }
        }
    }
}