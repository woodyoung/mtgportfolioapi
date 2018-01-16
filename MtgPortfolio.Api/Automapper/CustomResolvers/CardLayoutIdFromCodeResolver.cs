using AutoMapper;
using MtgPortfolio.Api.Services;
using MtgPortfolio.API.Entities;
using MtgPortfolio.API.Entities.Codes;
using MtgPortfolio.API.Models.MtgJson;
using System;

namespace MtgPortfolio.Api.Automapper.CustomResolvers
{
    public class CardLayoutIdFromCodeResolver : BaseResolver, IValueResolver<MtgJsonCard, MtgCardEntity, int>
    {
        public CardLayoutIdFromCodeResolver(ICodesCacheService codesService) : base(codesService)
        {

        }

        public int Resolve(MtgJsonCard source, MtgCardEntity destination, int destMember, ResolutionContext context)
        {
            return _codesService.GetEntityByCode<LayoutEntity>(source.Layout).Id;
        }
    }
}
