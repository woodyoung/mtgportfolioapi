using AutoMapper;
using MtgPortfolio.API.Automapper;
using MtgPortfolio.API.Entities;
using MtgPortfolio.API.Entities.Codes;
using MtgPortfolio.API.Models;
using MtgPortfolio.API.Models.MtgJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MtgPortfolio.Api.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<MtgCardEntity, MtgCard>()
                .ReverseMap();

            CreateMap<string, LayoutEntity>()
                .ApplyBaseMapping();
            CreateMap<string, BorderEntity>()
                .ApplyBaseMapping();
            CreateMap<string, ColorEntity>()
                .ApplyBaseMapping();
            CreateMap<string, FormatEntity>()
                .ApplyBaseMapping();
            CreateMap<string, LegalityEntity>()
                .ApplyBaseMapping();
            CreateMap<string, RarityEntity>()
                .ApplyBaseMapping();
            CreateMap<string, SetEntity>()
                .ApplyBaseMapping();
            CreateMap<string, SubtypeEntity>()
                .ApplyBaseMapping();
            CreateMap<string, SupertypeEntity>()
                .ApplyBaseMapping();
            CreateMap<string, TypeEntity>()
                .ApplyBaseMapping();

            CreateMap<MtgJsonSet, SetEntity>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.IsActive, opt => opt.UseValue(1))
                .ForMember(dest => dest.BorderId, opt => opt.ResolveUsing<SetBorderIdFromCodeResolver>())
                .ForMember(dest => dest.SetType, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate));
        }
    }
}
