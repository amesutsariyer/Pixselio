﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pixselio.Data;
using Pixselio.Data.Context;
using Pixselio.Web.Models.Request;
using Pixselio.Web.Settings;

namespace Pixselio.Web.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IdentityDbContext _context;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        public AdminController(IdentityDbContext context, SignInManager<User> signInManager, UserManager<User> userManager, IOptions<SettingsMapModel> config) : base(config)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            return View(new LoginRequestModel() { IsLoginSuccess = true });
        }
        public IActionResult LoginFailed()
        {
            return View("Login", new LoginRequestModel()
            {
                IsLoginSuccess = false,
                ErrorMessage = "Username or password wrong."
            });
        }
        public IActionResult Register()
        {
            return View(new RegisterRequestModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequestModel model, string returnUrl = "/Admin")
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    Email = model.Email,
                    UserName = model.UserName
                };
                try
                {
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return View("Register", new RegisterRequestModel()
                        {
                            UserName = user.UserName,
                            IsCreateSuccess = true
                        });
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            // If we got this far, something failed, redisplay form
            return View("Register", new RegisterRequestModel()
            {
                UserName = model.UserName,
                IsCreateSuccess = false,
                ModelBinding = ModelState
            });
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignIn(LoginRequestModel obj, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(obj.UserName.Trim());
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, 
                // set lockoutOnFailure: true
                try
                {
                    var result = await _signInManager.PasswordSignInAsync(user, obj.Password, true, true);
                    if (result.Succeeded)
                    {
                        return LocalRedirect("/Home/Index");
                    }
                    else
                    {
                        return RedirectToAction("LoginFailed", "Admin");
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("LoginFailed", "Admin");
                }
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("LoginFailed", "Admin");
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return LocalRedirect("/Home/Index");
        }
    }
}