using Pixselio.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pixselio.Business.Services
{
    public interface IUserService
    {
        Task<bool> SignIn(SignInDto dto);
        Task<RegisterDto> Register(RegisterDto dto);
        void SignOut();

    }
}
