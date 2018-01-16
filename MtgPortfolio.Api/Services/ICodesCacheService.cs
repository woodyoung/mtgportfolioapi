using MtgPortfolio.API.Entities.Codes;
using System.Collections.Generic;

namespace MtgPortfolio.Api.Services
{
    public interface ICodesCacheService
    {
        T GetEntityByCode<T>(string code) where T : BaseCodesType;
        IEnumerable<T> GetEntities<T>() where T : BaseCodesType;
    }
}