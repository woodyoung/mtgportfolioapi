using AutoMapper;
using MtgPortfolio.API.Entities.Codes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MtgPortfolio.API.Automapper
{
    public static class AutomapperExtensionMethods
    {
       public static IMappingExpression<string, T> ApplyBaseMapping<T>(this IMappingExpression<string, T> iMappingExpression)
       where T : BaseCodesType
        {
            iMappingExpression
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => char.ToUpper(src[0]) + src.Substring(1)))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => char.ToUpper(src[0]) + src.Substring(1)))
                .ForMember(dest => dest.IsActive, opt => opt.UseValue(true))
                .ForMember(dest => dest.Id, opt => opt.UseValue(0));

            return iMappingExpression;
        }
    }
}
