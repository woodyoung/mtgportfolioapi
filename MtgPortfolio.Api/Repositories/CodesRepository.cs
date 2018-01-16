using MtgPortfolio.API.DbContexts;
using MtgPortfolio.API.Entities;
using MtgPortfolio.API.Entities.Codes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MtgPortfolio.API.Repositories
{
    public class CodesRepository : ICodesRepository
    {
        private readonly MtgPortfolioDbContext _context;

        public CodesRepository(MtgPortfolioDbContext context)
        {
            _context = context;
        }

        public IEnumerable<LayoutEntity> GetLayoutEntities()
        {
            return _context.Layouts;
        }

        public IEnumerable<BorderEntity> GetBorderEntities()
        {
            return _context.Borders;
        }

        public IEnumerable<ColorEntity> GetColorEntities()
        {
            return _context.Colors;
        }

        public IEnumerable<FormatEntity> GetFormatEntities()
        {
            return _context.Formats;
        }

        public IEnumerable<LegalityEntity> GetLegalityEntities()
        {
            return _context.Legalites;
        }

        public IEnumerable<RarityEntity> GetRarityEntities()
        {
            return _context.Rarities;
        }

        public IEnumerable<SetEntity> GetSetEntities()
        {
            return _context.Sets;
        }

        public IEnumerable<SubtypeEntity> GetSubtypeEntities()
        {
            return _context.Subtypes;
        }

        public IEnumerable<SupertypeEntity> GetSupertypeEntities()
        {
            return _context.Supertypes;
        }

        public IEnumerable<TypeEntity> GetTypeEntities()
        {
            return _context.Types;
        }

        public IEnumerable<LayoutEntity> InsertLayoutEntities(IEnumerable<LayoutEntity> layouts)
        {
            _context.Layouts.AddRange(_context.SetAudit<LayoutEntity>(layouts));
            _context.SaveChanges();

            return layouts;
        }

        public IEnumerable<BorderEntity> InsertBorderEntities(IEnumerable<BorderEntity> borders)
        {
            _context.Borders.AddRange(_context.SetAudit<BorderEntity>(borders));
            _context.SaveChanges();

            return borders;
        }

        public IEnumerable<ColorEntity> InsertColorEntities(IEnumerable<ColorEntity> colors)
        {
            _context.Colors.AddRange(_context.SetAudit<ColorEntity>(colors));
            _context.SaveChanges();

            return colors;
        }

        public IEnumerable<FormatEntity> InsertFormatEntities(IEnumerable<FormatEntity> formats)
        {
            _context.Formats.AddRange(_context.SetAudit<FormatEntity>(formats));
            _context.SaveChanges();

            return formats;
        }

        public IEnumerable<LegalityEntity> InsertLegalityEntities(IEnumerable<LegalityEntity> legalities)
        {
            _context.Legalites.AddRange(_context.SetAudit<LegalityEntity>(legalities));
            _context.SaveChanges();

            return legalities;
        }

        public IEnumerable<RarityEntity> InsertRarityEntities(IEnumerable<RarityEntity> rarities)
        {
            _context.Rarities.AddRange(_context.SetAudit<RarityEntity>(rarities));
            _context.SaveChanges();

            return rarities;
        }
        public SetEntity InsertSetEntity(SetEntity set)
        {
            _context.Sets.Add(_context.SetAudit<SetEntity>(set));
            _context.SaveChanges();

            return set;
        }

        public IEnumerable<SetEntity> InsertSetEntities(IEnumerable<SetEntity> sets)
        {
            _context.Sets.AddRange(_context.SetAudit<SetEntity>(sets));
            _context.SaveChanges();

            return sets;
        }

        public IEnumerable<TypeEntity> InsertTypeEntities(IEnumerable<TypeEntity> types)
        {
            _context.Types.AddRange(_context.SetAudit<TypeEntity>(types));
            _context.SaveChanges();

            return types;
        }

        public IEnumerable<SupertypeEntity> InsertSupertypeEntities(IEnumerable<SupertypeEntity> supertypes)
        {
            _context.Supertypes.AddRange(_context.SetAudit<SupertypeEntity>(supertypes));
            _context.SaveChanges();

            return supertypes;
        }

        public IEnumerable<SubtypeEntity> InsertSubtypeEntities(IEnumerable<SubtypeEntity> subtypes)
        {
            _context.Subtypes.AddRange(_context.SetAudit<SubtypeEntity>(subtypes));
            _context.SaveChanges();

            return subtypes;
        }

    }
}
