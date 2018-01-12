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
        private readonly ILogger<MtgCardService> _logger;
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public MtgCardService(ILogger<MtgCardService> logger, IRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
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
                cards.Add(_mapper.Map<MtgCardEntity, MtgCard>(cardEntity));
            }

            return cards;
        }
    }
}
