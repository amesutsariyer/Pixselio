using AutoMapper;
using Pixselio.Data;
using Pixselio.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pixselio.Web.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tag, TagModel>();
            CreateMap<TagModel, Tag>();
            CreateMap<Photo, PhotoModel>();
            CreateMap<PhotoModel, Photo>();
        }

    }
}
