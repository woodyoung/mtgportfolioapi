using MtgPortfolio.API.Entities.Codes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MtgPortfolio.API.Entities
{
    [Table("MtgCardSupertypes")]
    public class MtgCardSupertypesEntity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MtgCardSupertypesId { get; set; }
        public int MtgCardId { get; set; }
        public int SupertypeId { get; set; }

        [ForeignKey("MtgCardId")]
        public virtual MtgCardEntity MtgCard { get; set; }
        [ForeignKey("SupertypeId")]
        public SupertypeEntity Supertype { get; set; }
    }
}