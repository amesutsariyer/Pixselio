﻿using System;
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
using Pixselio.Entity;
using Pixselio.Web.Models.Request;
using Pixselio.Web.Settings;
using Pixselio.Web.Models;
using Pixselio.Business.Services;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Pixselio.Web.Controllers
{
    public class UserController : BaseController
    {
        private readonly IPhotoService _photoManager;
        private IHostingEnvironment _env;
        public UserController(IPhotoService photoManager, IHostingEnvironment env, IOptions<SettingsMapModel> config) : base(config)
        {
            _photoManager = photoManager;
            _env = env;
        }
        [Authorize]
        [HttpGet]
        public IActionResult Gallery()
        {
           var dto  = _photoManager.GetPhotoByUserId(User.Identity.Name);
            var model = dto.Select(x => new PhotoModel()
            {
                Id = x.Id,
                Extension = x.Extension,
                Name = x.Name,
                Title =x.Title
                
            }).ToList();
            return View(model);
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetPhotoById(int photoId)
        {
            _photoManager.GetPhotoById(photoId);
            return View();
        }
        [Authorize]
        [HttpGet]
        public IActionResult AddPhoto()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPhoto(PhotoModel model)
        {
            var tagModel = model.Tags.Split(',').Select(x => new TagModel()
            {
                Name = x
            });
            var files = Request.Form.Files;

            //Backend Expected Just one file
            if (files.Count == 1)
            {
                var file = files[0];
                if (file.ContentType == "image/jpeg" && file.Length > 0)
                {
                    var guid = Guid.NewGuid();
                    //Define path for upload
                    var webRoot = _env.WebRootPath;
                    var path = System.IO.Path.Combine(webRoot, "images", guid.ToString() + "-" + file.FileName);

                    //Save file tgo local storage
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    _photoManager.Add(new Dto.PhotoDto()
                    {
                        Tags = tagModel.Select(x => new Dto.TagDto()
                        {
                            Name = x.Name
                        }).ToList(),
                        Name = file.FileName,
                        Title = model.Title,
                        Path = path,
                        Extension = guid.ToString() + "-",
                        Size = (int)file.Length,
                        CreatedBy = User.Identity.Name
                    });
                }
                else
                {
                    throw new System.Exception("Please Choose jpg file.");
                }
            }
            else
            {
                throw new System.Exception("Please Choose File.");
            }

            return View("Gallery");
        }
        [Authorize]
        [HttpPost]
        public IActionResult DeletePhoto(int photoId)
        {
            var result = _photoManager.Delete(photoId);
            return View();
        }

    }
}