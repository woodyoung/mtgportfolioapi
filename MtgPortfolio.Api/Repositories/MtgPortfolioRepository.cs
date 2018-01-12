using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MtgPortfolio.API.Entities;

namespace MtgPortfolio.API.Repositories
{
    public class MtgPortfolioRepository : IMtgPortfolioRepository
    {
        private readonly MtgPortfolioDbContext _context;

        public MtgPortfolioRepository(MtgPortfolioDbContext context)
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
    }
}
