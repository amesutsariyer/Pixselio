using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pixselio.Web.Settings;

namespace Pixselio.Web.Controllers
{

    public class BaseController : Controller
    {
        IMapper _mapper;
        protected readonly IOptions<SettingsMapModel> _config;
        public BaseController(IOptions<SettingsMapModel> config,IMapper mapper)
        {
            _config = config;
            _mapper = mapper;
        }
    }
}