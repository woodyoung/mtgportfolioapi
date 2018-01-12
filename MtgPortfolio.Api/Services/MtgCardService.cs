using AutoMapper;
using Microsoft.Extensions.Logging;
using MtgPortfolio.API.Entities;
using MtgPortfolio.API.Models;
using MtgPortfolio.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MtgPortfolio.API.Services
{
    public class MtgCardService : IMtgCardService
    {
        private ILogger<MtgCardService> _logger;
        private IMtgPortfolioRepository _repository;

        public MtgCardService(ILogger<MtgCardService> logger, IMtgPortfolioRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public MtgCard GetMtgCard(string name)
        {
            var entity = _repository.GetMtgCardByName(name);
            MtgCard card = null;

            if (entity != null)
            {
                card = new MtgCard()
                {
                    Name = entity.Name
                };
            }

            return card;
        }

        public IEnumerable<MtgCard> GetMtgCards()
        {
            var cardEntities = _repository.GetMtgCards();
            IList<MtgCard> cards = new List<MtgCard>();

            foreach (var cardEntity in cardEntities)
            {
                cards.Add(Mapper.Map<MtgCardEntity, MtgCard>(cardEntity));
            }

            return cards;
        }
    }
}
