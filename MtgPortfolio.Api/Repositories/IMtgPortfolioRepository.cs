using MtgPortfolio.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MtgPortfolio.API.Repositories
{
    public interface IMtgPortfolioRepository 
    {
        IEnumerable<MtgCardEntity> GetMtgCards();
        MtgCardEntity GetMtgCardByName(string name);
        MtgCardEntity GetMtgCard(int id);
    }
}
