using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Todo.Api.ViewModels;
using Todo.Domain;

namespace TodoListApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        //public async Task<IActionResult> About()
        //{
        //    //get user by email
        //    var user = _userManager.Users.SingleOrDefault(x => x.Email == "xx@hotmail.com");
        //    //get user from user manager
        //    var userFromManager = await _userManager.GetUserAsync(User);
        //    var externalAccessToken = await _userManager.GetAuthenticationTokenAsync(userFromManager, "Microsoft", "access_token");
        //    ViewData["Message"] = "Your application description page.";
        //    return View();
        //}

        //
        // POST: /Account
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Required][FromBody]LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var userFromManager = await _userManager.GetUserAsync(User);
                    return Ok(await _userManager.GetAuthenticationTokenAsync(userFromManager, "Microsoft", "access_token"));
                }
            }
            return StatusCode(401);
        }
    }
}
