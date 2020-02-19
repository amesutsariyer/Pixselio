using Microsoft.EntityFrameworkCore;
using Pixselio.Business.Services;
using Pixselio.Data;
using Pixselio.Dto;
using Pixselio.Entity;
using Pixselio.Core.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace Pixselio.Business.Managers
{
    public class PhotoManager : IPhotoService
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IGenericRepository<Photo> _photoRep;
        private readonly IGenericRepository<Tag> _tagRep;
        private readonly IGenericRepository<PhotosTag> _photoTagRep;
        IMapper _mapper;
        public PhotoManager(IUnitofWork unitofWork, IMapper mapper)
        {
            _unitofWork = unitofWork;
            _photoRep = _unitofWork.GetRepository<Photo>();
            _tagRep = _unitofWork.GetRepository<Tag>();
            _photoTagRep = _unitofWork.GetRepository<PhotosTag>();
            _mapper = mapper;
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

        public bool Delete(int id)
        {
            var result = _photoRep.GetById(id);

            if (result == null)
                return false;
            _photoRep.Delete(result);
            if (_unitofWork.SaveChanges() > 0)
                return true;
            return  false;
        }

        public PhotoDto GetPhotoById(int id)
        {
            var photoEnt = _photoRep.GetById(id);
            var relatedTags = _photoTagRep.GetAll(x => x.PhotoId == id).Include(d => d.Tag).ToList();
            return new PhotoDto()
            {
                Id = photoEnt.Id,
                Extension = photoEnt.Extension,
                Name = photoEnt.Name,
                Path = photoEnt.Path,
                Size = photoEnt.Size,
                Title = photoEnt.Title,
                Tags = relatedTags.Select(x => new TagDto()
                {
                    Name = x.Tag.Name
                }).ToList()
            };
        }

        public List<PhotoDto> GetPhotoByUserId(string createdUserName)
        {
            return _photoRep.GetAll().Where(x => x.CreatedBy == createdUserName && !x.IsDeleted).Select(y => new PhotoDto()
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
