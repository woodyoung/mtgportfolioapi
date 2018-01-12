using System;
using System.Collections.Generic;

namespace MtgPortfolio.API.Models.MtgJson
{
    public class MtgJsonSet
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string GathererCode { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Border { get; set; }
        public string Type { get; set; }
        public string Block { get; set; }
        public bool? OnlineOnly { get; set; }
        public IEnumerable<string> BoosterSetup { get; set; }
        public IEnumerable<MtgJsonCard> Cards { get; set; }
    }
}