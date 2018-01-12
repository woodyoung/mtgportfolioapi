using MtgPortfolio.API.Entities.Codes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MtgPortfolio.API.Entities
{
    [Table("MtgCard")]
    public class MtgCardEntity: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MtgCardId { get; set; }
        [MaxLength(50)]
        public string MtgJsonId { get; set; }
        [MaxLength(50)]
        public int LayoutId { get; set; }
        [MaxLength(160)]
        public string Name { get; set; }
        [MaxLength(20)]
        public string ManaCost { get; set; }
        public decimal ConvertedManaCost { get; set; }
        [MaxLength(100)]
        public string Type { get; set; }
        public int RarityId { get; set; }
        [MaxLength(1000)]
        public string Text { get; set; }
        [MaxLength(1000)]
        public string Flavor { get; set; }
        [MaxLength(100)]
        public string Artist { get; set; }
        [MaxLength(10)]
        public string Number { get; set; }
        public decimal Power { get; set; }
        public decimal Toughness { get; set; }
        public decimal Loyalty { get; set; }
        public Int64 MultiverseId { get; set; }
        public bool IsTimeShifted { get; set; }
        public bool IsReserved { get; set; }
        public DateTime RelaseDate { get; set; }
        public bool IsStarter { get; set; }

        [MaxLength(1000)]
        public string OriginalText { get; set; }
        [MaxLength(100)]
        public string OriginalType { get; set; }
        public string Source { get; set; }
        public int SetId { get; set; }


        [ForeignKey("LayoutId")]
        public virtual LayoutEntity Layout { get; set; }
        [ForeignKey("SetId")]
        public virtual SetEntity Set { get; set; }
        [ForeignKey("RarityId")]
        public virtual RarityEntity Rarity { get; set; }
        public virtual ICollection<MtgCardLegalitiesEntity> Legalities { get; set; }
        public virtual ICollection<MtgCardColorsEntity> ColorIdentity { get; set; }
        public virtual ICollection<MtgCardSetsEntity> Printings { get; set; }
        public virtual ICollection<MtgCardTypesEntity> Types { get; set; }
        public virtual ICollection<MtgCardSubTypesEntity> Subtypes { get; set; }
    }
}
