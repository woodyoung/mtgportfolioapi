using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MtgPortfolio.API.DbContexts;
using MtgPortfolio.API.Entities;

namespace MtgPortfolio.API.Repositories
{
    public class Repository : IRepository
    {
        private readonly MtgPortfolioDbContext _context;

        public Repository(MtgPortfolioDbContext context)
        {
            _context = context;
        }

        public MtgCardEntity GetMtgCard(int id)
        {
            return _context.MtgCards.FirstOrDefault(c => c.MtgCardId == id);
        }

        public MtgCardEntity GetMtgCardByName(string name)
        {
            return _context.MtgCards.FirstOrDefault(c => c.Name == name);
        }

        public IEnumerable<MtgCardEntity> GetMtgCards()
        {
            return _context.MtgCards;
        }

        public IEnumerable<MtgCardEntity> InsertMtgCards(IEnumerable<MtgCardEntity> mtgCards)
        {
            if (mtgCards == null || !mtgCards.Any()) return null;

            var entities = _context.SetAudit<MtgCardEntity>(mtgCards);

            foreach (var card in entities)
            {
                _context.SetAudit<MtgCardLegalitiesEntity>(card.Legalities);
                _context.SetAudit<MtgCardColorsEntity>(card.Colors);
                _context.SetAudit<MtgCardTypesEntity>(card.Types);
                _context.SetAudit<MtgCardSubTypesEntity>(card.Subtypes);
                _context.SetAudit<MtgCardSupertypesEntity>(card.Supertypes);
            }

            _context.MtgCards.AddRange(entities);
            _context.SaveChanges();

            return mtgCards;
        }
    }
}
