using MtgPortfolio.API.Entities.Codes;

namespace MtgPortfolio.Api.Services
{
    public interface ICodesService
    {
        BorderEntity GetBorderEntityByCode(string code);
    }
}