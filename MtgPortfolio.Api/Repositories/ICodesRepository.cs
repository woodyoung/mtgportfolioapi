using MtgPortfolio.API.Entities.Codes;
using System.Collections.Generic;

namespace MtgPortfolio.API.Repositories
{
    public interface ICodesRepository
    {
        #region Gets
        IEnumerable<LayoutEntity> GetLayoutEntities();

        IEnumerable<BorderEntity> GetBorderEntities();

        IEnumerable<ColorEntity> GetColorEntities();

        IEnumerable<FormatEntity> GetFormatEntities();

        IEnumerable<LegalityEntity> GetLegalityEntities();

        IEnumerable<RarityEntity> GetRarityEntities();

        IEnumerable<SetEntity> GetSetEntities();

        IEnumerable<SubtypeEntity> GetSubtypeEntities();

        IEnumerable<SupertypeEntity> GetSupertypeEntities();

        IEnumerable<TypeEntity> GetTypeEntities();
        #endregion Gets

        #region Inserts

        IEnumerable<LayoutEntity> InsertLayoutEntities(IEnumerable<LayoutEntity> layouts);

        IEnumerable<BorderEntity> InsertBorderEntities(IEnumerable<BorderEntity> borders);

        IEnumerable<ColorEntity> InsertColorEntities(IEnumerable<ColorEntity> colors);

        IEnumerable<FormatEntity> InsertFormatEntities(IEnumerable<FormatEntity> formats);

        IEnumerable<LegalityEntity> InsertLegalityEntities(IEnumerable<LegalityEntity> legalities);

        IEnumerable<RarityEntity> InsertRarityEntities(IEnumerable<RarityEntity> rarities);

        IEnumerable<SetEntity> InsertSetEntities(IEnumerable<SetEntity> sets);

        IEnumerable<TypeEntity> InsertTypeEntities(IEnumerable<TypeEntity> types);

        IEnumerable<SupertypeEntity> InsertSupertypeEntities(IEnumerable<SupertypeEntity> supertypes);

        IEnumerable<SubtypeEntity> InsertSubtypeEntities(IEnumerable<SubtypeEntity> subtypes);
        #endregion Inserts
    }
}