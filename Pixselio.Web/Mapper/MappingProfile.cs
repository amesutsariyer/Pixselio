using AutoMapper;
using Pixselio.Data;
using Pixselio.Entity;
using Pixselio.Web.Models;

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
