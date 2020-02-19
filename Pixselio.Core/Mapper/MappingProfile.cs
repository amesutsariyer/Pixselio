using AutoMapper;
using Pixselio.Data;
using Pixselio.Dto;
using Pixselio.Dto.Models;
using Pixselio.Entity;
using System.Collections.Generic;
using AutoMapper.QueryableExtensions;

namespace Pixselio.Core.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tag, TagModel>();
            CreateMap<TagModel, Tag>();
            CreateMap<Photo, PhotoModel>();
            CreateMap<PhotoModel, Photo>();

            CreateMap<PhotosTag, PhotosTagDto>();
            CreateMap<PhotosTagDto, PhotosTag>();
           // CreateMap<List<PhotosTag>, List<PhotosTagDto>>();
           // CreateMap<List<PhotosTagDto>, List<PhotosTag>>();

            CreateMap<Photo, PhotoDto>();
            CreateMap<PhotoDto, Photo>();
           //CreateMap<List<Photo>, List<PhotoDto>>();
           //CreateMap<List<PhotoDto>, List<Photo>>();

            CreateMap<Tag, TagDto>();
            CreateMap<TagDto, Tag>()
                .ForMember(x => x.PhotosTag, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.UpdatedBy, opt => opt.Ignore())
                .ForMember(x => x.UpdatedDate, opt => opt.Ignore())
                .ForMember(x => x.IsDeleted, opt => opt.Ignore());
            CreateMap<List<Tag>, List<TagDto>>();
            CreateMap<List<TagDto>, List<Tag>>();
    
        }

    }
}
