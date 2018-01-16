using MtgPortfolio.Api.Automapper;
using MtgPortfolio.API.Entities.Codes;
using MtgPortfolio.API.Models.MtgJson;
using AutoMapper;
using MtgPortfolio.Api.Services;

namespace MtgPortfolio.Api.Automapper.CustomResolvers
{
    public class SetBorderIdFromCodeResolver : BaseResolver, IValueResolver<MtgJsonSet, SetEntity, int>
    {
        public SetBorderIdFromCodeResolver(ICodesCacheService codesService)
        : base(codesService) { }

        public int Resolve(MtgJsonSet source, SetEntity destination, int destMember, ResolutionContext context)
        {
            return _codesService.GetEntityByCode<BorderEntity>(source.Border).Id;
        }
    }
}