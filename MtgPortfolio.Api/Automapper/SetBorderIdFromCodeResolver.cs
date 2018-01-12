using MtgPortfolio.Api.Automapper;
using MtgPortfolio.API.Entities.Codes;
using MtgPortfolio.API.Models.MtgJson;
using AutoMapper;
using MtgPortfolio.Api.Services;

namespace MtgPortfolio.Api
{
    public class SetBorderIdFromCodeResolver : IValueResolver<MtgJsonSet, SetEntity, int>
    {
        private readonly ICodesService _codesService;

        public SetBorderIdFromCodeResolver(ICodesService codesService) { _codesService = codesService; }

        public int Resolve(MtgJsonSet source, SetEntity destination, int destMember, ResolutionContext context)
        {
            return _codesService.GetBorderEntityByCode(source.Border).Id;
        }
    }
}