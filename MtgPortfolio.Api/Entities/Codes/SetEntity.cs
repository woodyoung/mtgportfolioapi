﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MtgPortfolio.API.Entities.Codes
{
    [Table("Set", Schema = "Codes")]
    public class SetEntity : BaseCodesType
    {
        public BorderEntity Border { get; set; }
        public int BorderId { get; set; }
        [MaxLength(160)]
        public string SetType { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
