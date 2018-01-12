using System;
using System.Collections.Generic;

namespace MtgPortfolio.API.Models.MtgJson
{
    public class MtgJsonCard
    {
        public string Id { get; set; }
        public string Layout { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Names { get; set; }
        public string ManaCost { get; set; }
        public decimal ConvertedManaCost { get; set; }
        public IEnumerable<string> Colors { get; set; }
        public string Type { get; set; }
        public IEnumerable<string> ColorIdentity { get; set; }
        public IEnumerable<string> Types { get; set; }
        public IEnumerable<string> SuperTypes { get; set; }
        public IEnumerable<string> SubTypes { get; set; }
        public string Rarity { get; set; }
        public string Text { get; set; }
        public string Flavor { get; set; }
        public string Artist { get; set; }
        public string Number { get; set; }
        public string Power { get; set; }
        public string Toughness { get; set; }
        public string Loyalty { get; set; }
        public Int64 MultiverseId { get; set; }
        public string Border { get; set; }
        public bool? IsTimeShifted { get; set; }
        public bool? IsReserved { get; set; }
        public string ReleaseDate { get; set; }
        public bool? IsStarter { get; set; }
        public string MciNumber { get; set; }
        public IEnumerable<MtgJsonRuling> Rulings { get; set; }
        public IEnumerable<MtgJsonForeignLanguageName> ForeignNames { get; set; }
        public IEnumerable<string> Printings { get; set; }
        public string OriginalText { get; set; }
        public string OriginalType { get; set; }
        public IEnumerable<MtgJsonLegality> Legalities { get; set; }
        public string Source { get; set; }
    }
}