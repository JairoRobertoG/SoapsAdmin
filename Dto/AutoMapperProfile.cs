using AutoMapper;
using Soaps.Model;
using System.Linq;

namespace Soaps.Dto
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Soap, SoapDto>().ForMember(d => d.SoapType, e => e.MapFrom(m => new SoapTypeDto
            {
                Id = m.SoapType.Id,
                Name = m.SoapType.Name
            })).ForMember(d => d.SoapDetails, e => e.MapFrom(m => m.SoapDetails.Select(s => new SoapDetailDto
            {
                Id = s.Id,
                Name = s.Name
            }))).ForMember(d => d.Images, e => e.MapFrom(m => m.Images.Select(s => new ImageDto
            {
                Id = s.Id,
                Name = s.Name
            })));

            CreateMap<SoapDto, Soap>().ForMember(d => d.SoapDetails, e => e.MapFrom(m => m.SoapDetails.Select(s => new SoapDetail
            {
                Id = s.Id,
                Name = s.Name
            }))).ForMember(d => d.Images, e => e.MapFrom(m => m.Images.Select(s => new Image
            {
                Id = s.Id,
                Name = s.Name
            })));
        }
    }
}
