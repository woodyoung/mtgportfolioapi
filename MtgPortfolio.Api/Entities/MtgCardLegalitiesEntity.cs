using MtgPortfolio.API.Entities.Codes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MtgPortfolio.API.Entities
{
    [Table("MtgCardLegalities")]
    public class MtgCardLegalitiesEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MtgCardLegalitiesId { get; set; }
        public int MtgCardId { get; set; }
        public int FormatId { get; set; }
        public int LegalityId { get; set; }

        [ForeignKey("MtgCardId")]
        public virtual MtgCardEntity MtgCard { get; set; }
        [ForeignKey("FormatId")]
        public virtual FormatEntity Format { get; set; }
        [ForeignKey("LegalityId")]
        public virtual LegalityEntity Legality { get; set; }
    }
}
