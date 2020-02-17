using System;
using System.Collections.Generic;
using System;
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
    public class UserController : BaseController
    {
        private readonly IdentityDbContext _context;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        public UserController(IdentityDbContext context, SignInManager<User> signInManager, UserManager<User> userManager, IOptions<SettingsMapModel> config) :base(config)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetPhotos()
        {
            return View();
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetPhotoById(int photoId)
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddPhoto()
        {
            return View();
        }
        [Authorize]
      
        public IActionResult DeletePhoto(int photoId)
        {
            return View();
        }

    }
}