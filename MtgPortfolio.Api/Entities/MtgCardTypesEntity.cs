using MtgPortfolio.API.Entities.Codes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MtgPortfolio.API.Entities
{
    [Table("MtgCardTypes")]
    public class MtgCardTypesEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MtgCardTypesId { get; set; }
        public int MtgCardId { get; set; }
        public int TypeId { get; set; }

        [ForeignKey("MtgCardId")]
        public virtual MtgCardEntity MtgCard { get; set; }
        [ForeignKey("TypeId")]
        public TypeEntity Type { get; set; }
    }
}
