using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pixselio.Web.Settings;

namespace Pixselio.Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IOptions<SettingsMapModel> _config;
        public BaseController(IOptions<SettingsMapModel> config)
        {
            _config = config;
        }
    }
}