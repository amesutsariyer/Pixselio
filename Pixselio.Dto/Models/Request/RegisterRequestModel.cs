using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;


namespace Pixselio.Dto.Models.Request
{
    public class RegisterRequestModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public ModelStateDictionary ModelBinding { get; set; }
        public bool IsCreateSuccess { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
   
    }
}
