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
    public class CardPowerDecimalResolver : BaseResolver, IValueResolver<MtgJsonCard, MtgCardEntity, decimal?>
    {
        public CardPowerDecimalResolver(ICodesCacheService codesService): base(codesService)
        {

        }

        public decimal? Resolve(MtgJsonCard source, MtgCardEntity destination, decimal? destMember, ResolutionContext context)
        {
            if(source.Power == null) return null;

            decimal powerDecimal;
            decimal? result = null;

            var success = decimal.TryParse(source.Power, out powerDecimal);

            if(success) result = powerDecimal;

            return result;
        }
    }
}
