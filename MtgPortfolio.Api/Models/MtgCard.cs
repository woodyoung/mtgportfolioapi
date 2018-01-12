using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MtgPortfolio.API.Models
{
    public class MtgCard
    {
        public int MtgCardId { get; set; }
        public string MtgJsonId { get; set; }
        public int LayoutId { get; set; }
        public string Name { get; set; }
        public string ManaCost { get; set; }
        public decimal ConvertedManaCost { get; set; }
        public string Type { get; set; }
        public int RarityId { get; set; }
        public string Text { get; set; }
        public string Flavor { get; set; }
        public string Artist { get; set; }
        public string Number { get; set; }
        public decimal Power { get; set; }
        public decimal Toughness { get; set; }
        public decimal Loyalty { get; set; }
        public Int64 MultiverseId { get; set; }
        public bool IsTimeShifted { get; set; }
        public bool IsReserved { get; set; }
        public DateTime RelaseDate { get; set; }
        public bool IsStarter { get; set; }
        public string OriginalText { get; set; }
        public string OriginalType { get; set; }
        public string Source { get; set; }
        public int SetId { get; set; }

        
        public IEnumerable<string> LegalityCodes { get; set; }
        public IEnumerable<string> ColorCodes { get; set; }
        public IEnumerable<string> SetCodes { get; set; }
        public IEnumerable<string> TypeCodes { get; set; }
        public IEnumerable<string> SubtypeCodes { get; set; }
        public IEnumerable<string> SupertypeCodes { get; set; }
    }
}
