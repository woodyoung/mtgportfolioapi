using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MtgPortfolio.API.Models.MtgJson
{
    public class MtgJsonAllSets
    {
        public Dictionary<string, MtgJsonSet> MtgJsonSets { get; set; }
    }
}
