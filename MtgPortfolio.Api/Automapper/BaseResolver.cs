using MtgPortfolio.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MtgPortfolio.Api.Automapper
{
    public class BaseResolver
    {
        protected readonly ICodesService _codesService;

        public BaseResolver(ICodesService codesService)
        {
            _codesService = codesService;
        }
    }
}
