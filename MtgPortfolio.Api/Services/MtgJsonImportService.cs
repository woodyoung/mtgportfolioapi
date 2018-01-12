using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MtgPortfolio.API.Entities;
using MtgPortfolio.API.Entities.Codes;
using MtgPortfolio.API.Models.MtgJson;
using MtgPortfolio.API.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace MtgPortfolio.API.Services
{
    public class MtgJsonImportService : IMtgJsonImportService
    {
        private readonly ILogger<MtgJsonImportService> _logger;
        private readonly IMtgPortfolioRepository _repo;
        private readonly IMtgPortfolioCodesRepository _codesRepo;
        private List<LayoutEntity> _layoutEntities;
        private List<BorderEntity> _borderEntities;
        private List<ColorEntity> _colorEntities;
        private List<FormatEntity> _formatEntities;
        private List<LegalityEntity> _legalityEntities;
        private List<RarityEntity> _rarityEntities;
        private List<SetEntity> _setEntities;
        private List<SubtypeEntity> _subtypeEntities;
        private List<TypeEntity> _typeEntities;
        private List<SupertypeEntity> _supertypeEntities;

        public MtgJsonImportService(ILogger<MtgJsonImportService> logger,
            IMtgPortfolioRepository repo,
            IMtgPortfolioCodesRepository codesRepo)
        {
            _logger = logger;
            _repo = repo;
            _codesRepo = codesRepo;
        }

        public bool ImportMtgJsonToDatabase()
        {
            var mtgJsonDictionary = ImportMtgJsonFromFile().ToList();

            List<MtgJsonSet> mtgJsonAllSets = GetAllMtgJsonSets(mtgJsonDictionary);

            List<MtgJsonCard> mtgJsonAllCards = GetAllMtgJsonCards(mtgJsonAllSets);

            ImportAndInsertLayouts(mtgJsonAllCards);
            ImportAndInsertBorders(mtgJsonAllCards);
            ImportAndInsertColors(mtgJsonAllCards);
            ImportAndInsertFormats(mtgJsonAllCards);
            ImportAndInsertLegalities(mtgJsonAllCards);
            ImportAndInsertRarities(mtgJsonAllCards);
            ImportAndInsertSets(mtgJsonAllCards);
            ImportAndInsertType(mtgJsonAllCards);
            ImportAndInsertSupertypes(mtgJsonAllCards);
            ImportAndInsertSubtypes(mtgJsonAllCards);

            return true;
        }

        private void ImportAndInsertSubtypes(List<MtgJsonCard> mtgJsonAllCards)
        {
            var distinctTypeCodes = mtgJsonAllCards
                .Where(c => c.SubTypes != null)
                .SelectMany(c => { return c.SubTypes; })
                .Distinct()
                .ToList();

            //get from DB
            _subtypeEntities = _codesRepo.GetSubtypeEntities().ToList();

            //Filter to exclude what is already in DB and map to entity
            var entitiesToAdd = GetDistinctNewEntities(distinctTypeCodes, _subtypeEntities);

            if (entitiesToAdd != null && entitiesToAdd.Any())
            {
                //Insert new entities to DB
                _codesRepo.InsertSubtypeEntities(entitiesToAdd);

                //Add new entities with Ids to variable
                _subtypeEntities.AddRange(entitiesToAdd);
            }
        }

        private void ImportAndInsertSupertypes(List<MtgJsonCard> mtgJsonAllCards)
        {
            var distinctTypeCodes = mtgJsonAllCards
                .Where(c => c.SuperTypes != null)
                .SelectMany(c => { return c.SuperTypes; })
                .Distinct()
                .ToList();

            //get from DB
            _supertypeEntities = _codesRepo.GetSupertypeEntities().ToList();

            //Filter to exclude what is already in DB and map to entity
            var entitiesToAdd = GetDistinctNewEntities(distinctTypeCodes, _supertypeEntities);

            if (entitiesToAdd != null && entitiesToAdd.Any())
            {
                //Insert new entities to DB
                _codesRepo.InsertSupertypeEntities(entitiesToAdd);

                //Add new entities with Ids to variable
                _supertypeEntities.AddRange(entitiesToAdd);
            }
        }

        private void ImportAndInsertType(List<MtgJsonCard> mtgJsonAllCards)
        {
            var distinctTypeCodes = mtgJsonAllCards
                .Where(c => c.Types != null)
                .SelectMany(c => { return c.Types; })
                .Distinct()
                .ToList();

            //get from DB
            _typeEntities = _codesRepo.GetTypeEntities().ToList();

            //Filter to exclude what is already in DB and map to entity
            var entitiesToAdd = GetDistinctNewEntities(distinctTypeCodes, _typeEntities);

            if (entitiesToAdd != null && entitiesToAdd.Any())
            {
                //Insert new entities to DB
                _codesRepo.InsertTypeEntities(entitiesToAdd);

                //Add new entities with Ids to variable
                _typeEntities.AddRange(entitiesToAdd);
            }
        }

        private void ImportAndInsertSets(List<MtgJsonCard> mtgJsonAllCards)
        {
        }

        private void ImportAndInsertRarities(List<MtgJsonCard> mtgJsonAllCards)
        {
            var distinctTypeCodes = mtgJsonAllCards
                .Where(c => c.Rarity != null)
                .Select(c => { return c.Rarity; })
                .Distinct()
                .ToList();

            //get from DB
            _rarityEntities = _codesRepo.GetRarityEntities().ToList();

            //Filter to exclude what is already in DB and map to entity
            var entitiesToAdd = GetDistinctNewEntities(distinctTypeCodes, _rarityEntities);

            if (entitiesToAdd != null && entitiesToAdd.Any())
            {
                //Insert new entities to DB
                _codesRepo.InsertRarityEntities(entitiesToAdd);

                //Add new entities with Ids to variable
                _rarityEntities.AddRange(entitiesToAdd);
            }
        }

        private void ImportAndInsertLegalities(List<MtgJsonCard> mtgJsonAllCards)
        {
            var distinctTypeCodes = mtgJsonAllCards
                .Where(c => c.Legalities != null)
                .SelectMany(c => { return c.Legalities.Where(l => l.Legality != null).Select(l => l.Legality); })
                .Distinct()
                .ToList();

            //get from DB
            _legalityEntities = _codesRepo.GetLegalityEntities().ToList();

            //Filter to exclude what is already in DB and map to entity
            var entitiesToAdd = GetDistinctNewEntities(distinctTypeCodes, _legalityEntities);

            if (entitiesToAdd != null && entitiesToAdd.Any())
            {
                //Insert new entities to DB
                _codesRepo.InsertLegalityEntities(entitiesToAdd);

                //Add new entities with Ids to variable
                _legalityEntities.AddRange(entitiesToAdd);
            }
        }

        private void ImportAndInsertFormats(List<MtgJsonCard> mtgJsonAllCards)
        {
            var distinctTypeCodes = mtgJsonAllCards
                .Where(c => c.Legalities != null)
                .SelectMany(c => { return c.Legalities.Where(l => l.Format != null).Select(l => l.Format); })
                .Distinct()
                .ToList();

            //get from DB
            _formatEntities = _codesRepo.GetFormatEntities().ToList();

            //Filter to exclude what is already in DB and map to entity
            var entitiesToAdd = GetDistinctNewEntities(distinctTypeCodes, _formatEntities);

            if (entitiesToAdd != null && entitiesToAdd.Any())
            {
                //Insert new entities to DB
                _codesRepo.InsertFormatEntities(entitiesToAdd);

                //Add new entities with Ids to variable
                _formatEntities.AddRange(entitiesToAdd);
            }
        }

        private void ImportAndInsertColors(List<MtgJsonCard> mtgJsonAllCards)
        {
            var distinctTypeCodes = mtgJsonAllCards
                .Where(x => x.Colors != null)
                .SelectMany(c => { return c.Colors; })
                .Distinct()
                .ToList();

            //get from DB
            _colorEntities = _codesRepo.GetColorEntities().ToList();

            //Filter to exclude what is already in DB and map to entity
            var entitiesToAdd = GetDistinctNewEntities(distinctTypeCodes, _colorEntities);

            if (entitiesToAdd != null && entitiesToAdd.Any())
            {
                //Insert new entities to DB
                _codesRepo.InsertColorEntities(entitiesToAdd);

                //Add new entities with Ids to variable
                _colorEntities.AddRange(entitiesToAdd);
            }
        }

        private void ImportAndInsertBorders(List<MtgJsonCard> mtgJsonAllCards)
        {
            var distinctTypeCodes = mtgJsonAllCards
                .Where(x => x.Border != null)
                .Select(c => { return c.Border; })
                .Distinct()
                .ToList();

            //get from DB
            _borderEntities = _codesRepo.GetBorderEntities().ToList();

            //Filter to exclude what is already in DB and map to entity
            var entitiesToAdd = GetDistinctNewEntities(distinctTypeCodes, _borderEntities);

            if (entitiesToAdd != null && entitiesToAdd.Any())
            {
                //Insert new entities to DB
                _codesRepo.InsertBorderEntities(entitiesToAdd);

                //Add new entities with Ids to variable
                _borderEntities.AddRange(entitiesToAdd);
            }
        }

        private void ImportAndInsertLayouts(List<MtgJsonCard> mtgJsonAllCards)
        {
            var distinctTypeCodes = mtgJsonAllCards
                .Where(x => x.Layout != null)
                .Select(c => { return c.Layout; })
                .Distinct()
                .ToList();

            //get from DB
            _layoutEntities = _codesRepo.GetLayoutEntities().ToList();

            //Filter to exclude what is already in DB and map to entity
            var entitiesToAdd = GetDistinctNewEntities(distinctTypeCodes, _layoutEntities);
            
            if (entitiesToAdd != null && entitiesToAdd.Any())
            {
                //Insert new entities to DB
                _codesRepo.InsertLayoutEntities(entitiesToAdd);

                //Add new entities with Ids to variable
                _layoutEntities.AddRange(entitiesToAdd);
            }
        }

        private List<T> GetDistinctNewEntities<T>(List<string> codes, List<T> codesInDatabase) where T: BaseCodesType
        {
            List<T> results = new List<T>();

            foreach (var typeCode in codes)
            {
                if (!codesInDatabase.Any(c => c.Code == typeCode))
                {
                    results.Add(Mapper.Map<T>(typeCode));
                }
            }

            return results;
        }

        private List<MtgJsonSet> GetAllMtgJsonSets(List<KeyValuePair<string, MtgJsonSet>> mtgJsonDictionary)
        {
            List<MtgJsonSet> mtgJsonAllSets = new List<MtgJsonSet>();

            mtgJsonDictionary.ToList().ForEach(d => {
                mtgJsonAllSets.Add(d.Value);
            });

            return mtgJsonAllSets;
        }

        private List<MtgJsonCard> GetAllMtgJsonCards(List<MtgJsonSet> mtgJsonAllSets)
        {
            List<MtgJsonCard> mtgJsonCards = new List<MtgJsonCard>();

            mtgJsonAllSets.ForEach(set => {
                mtgJsonCards.AddRange(set.Cards.ToList());
            });

            return mtgJsonCards;
        }

        private Dictionary<string, MtgJsonSet> ImportMtgJsonFromFile()
        {
            Dictionary<string, MtgJsonSet> result = null;

            try
            {
                result = JsonConvert.DeserializeObject<Dictionary<string, MtgJsonSet>>(File.ReadAllText(@".\AllSets-x.json"));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Import from F:\\Projects\\mtgportfolioapi\\MtgPortfolio.API\\AllSets-x.json failed. {ex.Message}", new object[] { ex });

                throw;
            }

            return result;
        }
    }
}