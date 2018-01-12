using MtgPortfolio.API.Entities.Codes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MtgPortfolio.API.Entities
{
    [Table("MtgCardSets")]
    public class MtgCardSetsEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MtgCardSetsId { get; set; }
        public int MtgCardId { get; set; }
        public int SetId { get; set; }

        [ForeignKey("MtgCardId")]
        public virtual MtgCardEntity MtgCard { get; set; }
        [ForeignKey("SetId")]
        public virtual SetEntity Set { get; set; }
    }
}
