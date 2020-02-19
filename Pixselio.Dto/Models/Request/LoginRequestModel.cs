using System.ComponentModel.DataAnnotations;


namespace Pixselio.Dto.Models.Request
{
    public class LoginRequestModel
    {
   
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string UserName { get; set; }
        public bool IsKeepAlive { get; set; }
        public bool IsLoginSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
}
