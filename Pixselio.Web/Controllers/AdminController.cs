using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pixselio.Business.Managers;
using Pixselio.Business.Services;
using Pixselio.Data;
using Pixselio.Dto;
using Pixselio.Entity;
using Pixselio.Dto.Models.Request;
using Pixselio.Web.Settings;
using AutoMapper;

namespace Pixselio.Web.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IUserService _userManager;
        public AdminController(IUserService userManager, IOptions<SettingsMapModel> config,IMapper mapper) : base(config,mapper)
        {
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
                try
                {
                    var result = await _userManager.Register(new RegisterDto() { Email = model.Email, Password = model.Password, UserName = model.UserName });
                    if (result.IsCreateSuccess)
                    {
                        return View("Register", new RegisterRequestModel()
                        {
                            UserName = result.UserName,
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

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, 
                // set lockoutOnFailure: true
                try
                {
                    var result = await _userManager.SignIn(new SignInDto() { Password = obj.Password, UserName = obj.UserName });
                    if (result)
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
        public IActionResult Logout()
        {
            // await _signInManager.SignOutAsync();
            _userManager.SignOut();
            return LocalRedirect("/Home/Index");
        }
    }
}