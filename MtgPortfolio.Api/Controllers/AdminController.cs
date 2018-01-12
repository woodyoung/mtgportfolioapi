using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MtgPortfolio.API.Models.MtgJson;
using MtgPortfolio.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MtgPortfolio.API.Controllers
{
    public class AdminController : Controller
    {
        private ILogger<AdminController> _logger;
        private IMtgJsonImportService _service;

        public AdminController(
            ILogger<AdminController> logger,
            IMtgJsonImportService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost("api/mtgJsonAllSetsX")]
        public IActionResult InsertMtgCards()
        {
            try
            {
                var isSuccessful = _service.ImportMtgJsonToDatabase();

                return Ok(isSuccessful);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message, new object[] { ex });

                throw;
            }
        }
    }
}
