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
    public class CardToughnessDecimalResolver : BaseResolver, IValueResolver<MtgJsonCard, MtgCardEntity, decimal?>
    {
        public CardToughnessDecimalResolver(ICodesCacheService codesService): base(codesService)
        {

        }

        public decimal? Resolve(MtgJsonCard source, MtgCardEntity destination, decimal? destMember, ResolutionContext context)
        {
            if(source.Toughness == null) return null;

            decimal toughnessDecimal;
            decimal? result = null;

            var success = decimal.TryParse(source.Toughness, out toughnessDecimal);

            if(success) result = toughnessDecimal;

            return result;
        }
    }
}
