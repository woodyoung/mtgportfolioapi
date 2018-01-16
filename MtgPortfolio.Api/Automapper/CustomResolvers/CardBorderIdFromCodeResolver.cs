using AutoMapper;
using MtgPortfolio.Api.Services;
using MtgPortfolio.API.Entities;
using MtgPortfolio.API.Entities.Codes;
using MtgPortfolio.API.Models.MtgJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MtgPortfolio.Api.Automapper.CustomResolvers
{
    public class CardBorderIdFromCodeResolver : BaseResolver, IValueResolver<MtgJsonCard, MtgCardEntity, int>
    {
        public CardBorderIdFromCodeResolver(ICodesCacheService codesService): base(codesService)
        {

        }

        public int Resolve(MtgJsonCard source, MtgCardEntity destination, int destMember, ResolutionContext context)
        {
            if(source.Border == null)
            {
                return -1;
            }

            return _codesService.GetEntityByCode<BorderEntity>(source.Border).Id;
        }
    }
}
