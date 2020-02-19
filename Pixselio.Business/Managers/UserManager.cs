using Microsoft.AspNetCore.Identity;
using Pixselio.Business.Services;
using Pixselio.Dto;
using Pixselio.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pixselio.Business.Managers
{
    public class UserManager : IUserService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        public UserManager(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<RegisterDto> Register(RegisterDto dto)
        {
            var user = new User()
            {
                Email = dto.Email,
                UserName = dto.UserName
            };
            try
            {
                var result = await _userManager.CreateAsync(user, dto.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return new RegisterDto()
                    {
                        UserName = user.UserName,
                        IsCreateSuccess = true
                    };
                }
                return new RegisterDto()
                {
                    Errors = result.Errors
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> SignIn(SignInDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName.Trim());
            var result = await _signInManager.PasswordSignInAsync(user, dto.Password, true, true);
            return result.Succeeded;
        }

        public async void SignOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
