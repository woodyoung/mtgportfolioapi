using MtgPortfolio.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MtgPortfolio.Api.Automapper.CustomResolvers
{
    public class BaseResolver
    {
        protected readonly ICodesCacheService _codesService;

        public BaseResolver(ICodesCacheService codesService)
        {
            _codesService = codesService;
        }
    }
}
