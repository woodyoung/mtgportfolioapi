using MtgPortfolio.API.Entities.Codes;
using MtgPortfolio.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MtgPortfolio.Api.Services
{
    public class CodesService : ICodesService
    {
        private readonly ICodesRepository _codesRepo;
        private int _expirationInterval;

        private IEnumerable<BorderEntity> _borderEntities;
        private DateTime _borderInitializationDateTime;

        public CodesService(ICodesRepository codesRepo)
        {
            _codesRepo = codesRepo;

            //TODO: set this from configuration instead of hard coding
            _expirationInterval = 100000;
        }

        public BorderEntity GetBorderEntityByCode(string code)
        {
            if (_borderEntities == null || !_borderEntities.Any() || IsExpired(_borderEntities))
                InitializeBorderEntities();

            return _borderEntities.FirstOrDefault(c => c.Code == code);
        }

        private bool IsExpired(IEnumerable<BorderEntity> borderEntities)
        {
            TimeSpan span = DateTime.Now - _borderInitializationDateTime;
            int ms = (int)span.TotalMilliseconds;

            return ms > _expirationInterval;
        }

        private void InitializeBorderEntities()
        {
            _borderInitializationDateTime = DateTime.Now;
            _borderEntities = _codesRepo.GetBorderEntities();
        }
    }
}
