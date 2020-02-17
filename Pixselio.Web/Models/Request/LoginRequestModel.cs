using System.ComponentModel.DataAnnotations;

namespace Pixselio.Web.Models.Request
{
    public class LoginRequestModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsKeepAlive { get; set; }
        public bool IsLoginSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
}
