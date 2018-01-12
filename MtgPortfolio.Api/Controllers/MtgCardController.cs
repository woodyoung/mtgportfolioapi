using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MtgPortfolio.API.Models;
using MtgPortfolio.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MtgPortfolio.API.Controllers
{
    public class MtgCardController : Controller
    {
        private ILogger<MtgCardController> _logger;
        private IMtgCardService _mtgCardService;

        public MtgCardController(
            ILogger<MtgCardController> logger,
            IMtgCardService mtgCardService)
        {
            _logger = logger;
            _mtgCardService = mtgCardService;
        }

        [HttpGet("api/mtgcards")]
        public IActionResult GetMtgCards()
        {
            var cards = _mtgCardService.GetMtgCards();

            return Ok(cards);
        }

        [HttpGet("api/mtgcards/{name}")]
        public IActionResult GetMtgCardByName(string name)
        {
            var card = _mtgCardService.GetMtgCard(name);

            return Ok(card);
        }
    }
}
