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
        public PhotoManager(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
            _photoRep = _unitofWork.GetRepository<Photo>();
        }


        public string Add(PhotoDto dto)
        {
            var photo = new Photo
            {
                Name = dto.Name,
                Title = dto.Title,
                Extension =dto.Extension,
                Path = dto.Path,
                Size=dto.Size,
                CreatedBy =dto.CreatedBy
            };

            _photoRep.Add(photo);

            if (_unitofWork.SaveChanges() > 0)
                return "Created Success";
            return "Process Failed";
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
