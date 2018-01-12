using MtgPortfolio.API.Entities.Codes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MtgPortfolio.API.Entities
{
    [Table("MtgCardSubtypes")]
    public class MtgCardSubTypesEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MtgCardSubtypesId { get; set; }
        public int MtgCardId { get; set; }
        public int SubtypeId { get; set; }

        [ForeignKey("MtgCardId")]
        public virtual MtgCardEntity MtgCard { get; set; }
        [ForeignKey("SubtypeId")]
        public SubtypeEntity Subtype{ get; set; }
    }
}
