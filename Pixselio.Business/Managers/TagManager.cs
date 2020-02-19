using Pixselio.Business.Services;
using Pixselio.Data;
using Pixselio.Dto;
using Pixselio.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pixselio.Business.Managers
{
    public class TagManager : ITagService
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IGenericRepository<Tag> _tagRepo;
        public TagManager(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
            _tagRepo = _unitofWork.GetRepository<Tag>();
        }
        public string Add(TagDto dto)
        {
            var tag = new Tag
            {
                Name = dto.Name
            };

            _tagRepo.Add(tag);

            if (_unitofWork.SaveChanges() > 0)
                return "Created Success";
            return "Process Failed";
        }
    }
}
