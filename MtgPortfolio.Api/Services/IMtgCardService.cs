using MtgPortfolio.API.Models;
using System.Collections.Generic;

namespace MtgPortfolio.API.Services
{
    public interface IMtgCardService
    {
        MtgCard GetMtgCard(string name);
        IEnumerable<MtgCard> GetMtgCards();
    }
}