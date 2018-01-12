using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MtgPortfolio.API.Entities.Codes
{
    [Table("Rarity", Schema = "Codes")]
    public class RarityEntity : BaseCodesType
    {
    }
}
