using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pixselio.Web.Settings;

namespace Pixselio.Web.Controllers
{
    public class UserController : BaseController
    {
        public UserController(IOptions<SettingsMapModel> config):base(config)
        {

        }
    }
}