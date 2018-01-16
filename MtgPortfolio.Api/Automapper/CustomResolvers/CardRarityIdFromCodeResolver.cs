using AutoMapper;
using MtgPortfolio.Api.Services;
using MtgPortfolio.API.Entities;
using MtgPortfolio.API.Entities.Codes;
using MtgPortfolio.API.Models.MtgJson;
using System;

namespace MtgPortfolio.Api.Automapper.CustomResolvers
{
    public class CardRarityIdFromCodeResolver : BaseResolver, IValueResolver<MtgJsonCard, MtgCardEntity, int>
    {
        public CardRarityIdFromCodeResolver(ICodesCacheService codesService) : base(codesService)
        {

        }

        public int Resolve(MtgJsonCard source, MtgCardEntity destination, int destMember, ResolutionContext context)
        {
            return _codesService.GetEntityByCode<RarityEntity>(source.Rarity).Id;
        }
    }
}
