using Pixselio.Business.Services;
using Pixselio.Data;
using Pixselio.Dto;
using Pixselio.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pixselio.Business.Managers
{
    public class PhotoManager : IPhotoService
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IGenericRepository<Photo> _photoRep;
        private readonly IGenericRepository<Tag> _tagRep;
        private readonly IGenericRepository<PhotosTag> _photoTagRep;
        public PhotoManager(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
            _photoRep = _unitofWork.GetRepository<Photo>();
            _tagRep = _unitofWork.GetRepository<Tag>();
            _photoTagRep = _unitofWork.GetRepository<PhotosTag>();
        }


        public string Add(PhotoDto dto)
        {
            var photoEnt = new Photo
            {
                Name = dto.Name,
                Title = dto.Title,
                Extension = dto.Extension,
                Path = dto.Path,
                Size = dto.Size,
                CreatedBy = dto.CreatedBy
            };

            foreach (var tag in dto.Tags)
            {
                var tagEnt = new Tag() { Id = tag.Id, Name = tag.Name };
                var ptEnt = new PhotosTag() { Photo = photoEnt, Tag = tagEnt };
                _photoTagRep.Add(ptEnt);
            }

            if (_unitofWork.SaveChanges() > 0)
                return "Success";
            return "Failed";
        }

        public string Delete(int id)
        {
            var result = _photoRep.GetById(id);

            if (result == null)
                return "No Record";

            _photoRep.Delete(result);
            if (_unitofWork.SaveChanges() > 0)
                return "Removed Success";
            return "Failed removed process";
        }

        public PhotoDto GetPhotoById(int id)
        {
            var ent = _photoRep.GetById(id);
            var pt = _photoTagRep.GetAll(x => x.PhotoId == id).ToList();
            return new PhotoDto()
            {
                Id = ent.Id,
                Extension = ent.Extension,
                Name = ent.Name,
                Path = ent.Path,
                Size = ent.Size,
                Title = ent.Title
            };
        }

        public List<PhotoDto> GetPhotoByUserId(string createdUserName)
        {
            return _photoRep.GetAll().Where(x => x.CreatedBy == createdUserName).Select(y => new PhotoDto()
            {
                Id = y.Id,
                Extension = y.Extension,
                Name = y.Name,
                Path = y.Path,
                Size = y.Size,
                Title = y.Title
            }).ToList();

        }

    }
}
