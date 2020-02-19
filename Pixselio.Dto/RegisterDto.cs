using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pixselio.Dto
{
    public class RegisterDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IEnumerable<IdentityError> Errors { get; set; }
        public bool IsCreateSuccess { get; set; }
    }
}
