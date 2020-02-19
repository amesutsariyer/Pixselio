using Pixselio.Business.Services;
using Pixselio.Data;
using Pixselio.Dto;
using Pixselio.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pixselio.Business.Managers
{
    public class PhotoTagManager : IPhotoTagService
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IGenericRepository<PhotosTag> _photoTagRepo;
        public PhotoTagManager(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
            _photoTagRepo = _unitofWork.GetRepository<PhotosTag>();
        }
        public string Add(PhotosTagDto dto)
        {
            var tag = new PhotosTag
            {
                PhotoId  =dto.PhotoId,
                TagId=dto.TagId
            };

            _photoTagRepo.Add(tag);

            if (_unitofWork.SaveChanges() > 0)
                return "Created Success";
            return "Process Failed";
        }
    }
}
