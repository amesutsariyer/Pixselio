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
using Pixselio.Entity;
using Pixselio.Dto.Models.Request;
using Pixselio.Web.Settings;
using Pixselio.Dto.Models;
using Pixselio.Business.Services;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using AutoMapper;

namespace Pixselio.Web.Controllers
{
    public class UserController : BaseController
    {
        private readonly IPhotoService _photoManager;
        private IHostingEnvironment _env;

        public UserController(IPhotoService photoManager, IHostingEnvironment env, IMapper mapper, IOptions<SettingsMapModel> config) : base(config, mapper)
        {
            _photoManager = photoManager;
            _env = env;
        }
        [Authorize]
        [HttpGet]
        public IActionResult Gallery()
        {
            return View(GetAllGallery());
        }

        [Authorize]
        [HttpGet]
        public IActionResult Detail(int id)
        {
            var photo = _photoManager.GetPhotoById(id);
            return View(new PhotoModel()
            {
                Id = photo.Id,
                Extension = photo.Extension,
                Name = photo.Name,
                Title = photo.Title,
                Tags = String.Join('#', photo.Tags.Select(x => x.Name).ToList())
            });
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

            return View("Gallery", GetAllGallery());
        }
       
        [Authorize]
        [HttpPost]
        public JsonResult DeletePhoto([FromBody] PhotoModel model)
        {
            return Json(new
            {
                Success = _photoManager.Delete(model.Id)
            });
        }
        #region PRIVATE
        private List<PhotoModel> GetAllGallery()
        {
            var dto = _photoManager.GetPhotoByUserId(User.Identity.Name);
            return dto.Select(x => new PhotoModel()
            {
                Id = x.Id,
                Extension = x.Extension,
                Name = x.Name,
                Title = x.Title

            }).ToList();
        }
        #endregion
    }
}