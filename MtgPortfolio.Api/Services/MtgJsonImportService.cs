using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MtgPortfolio.Api.Services;
using static MtgPortfolio.Api.Shared.StaticHelperMethods;
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
        private readonly IRepository _repo;
        private readonly ICodesRepository _codesRepo;
        private readonly IMapper _mapper;
        private readonly ICodesCacheService _codesCache;

        private IEnumerable<MtgCardEntity> _cards;

        public MtgJsonImportService(ILogger<MtgJsonImportService> logger,
            IRepository repo,
            ICodesRepository codesRepo,
            IMapper mapper,
            ICodesCacheService codesCache)
        {
            _logger = logger;
            _repo = repo;
            _codesRepo = codesRepo;
            _mapper = mapper;
            _codesCache = codesCache;
        }

        public bool ImportMtgJsonToDatabase(string filename)
        {
            MtgJsonSet mtgJsonSet = ImportMtgJsonFromFile(filename);
            
            List<MtgJsonCard> mtgJsonAllCards = MapPropertiesFromSetToCards(mtgJsonSet);

            ImportAndInsertLayouts(mtgJsonAllCards);
            ImportAndInsertBorders(mtgJsonAllCards);
            ImportAndInsertColors(mtgJsonAllCards);
            ImportAndInsertFormats(mtgJsonAllCards);
            ImportAndInsertLegalities(mtgJsonAllCards);
            ImportAndInsertRarities(mtgJsonAllCards);
            ImportAndInsertType(mtgJsonAllCards);
            ImportAndInsertSupertypes(mtgJsonAllCards);
            ImportAndInsertSubtypes(mtgJsonAllCards);

            ImportAndInsertSets(mtgJsonSet);

            ImportAndInsertCards(mtgJsonAllCards);

            return true;
        }

        #region ImportAndInsert Methods
        
        private MtgJsonSet ImportMtgJsonFromFile(string filename)
        {
            MtgJsonSet result = null;

            try
            {
                var path = $".\\MtgJsonSetFiles\\{filename}.json";

                result = JsonConvert.DeserializeObject<MtgJsonSet>(File.ReadAllText(path));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Import from AllSets-x.json failed. {ex.Message}", new object[] { ex });

                throw;
            }

            return result;
        }

        private void ImportAndInsertCards(List<MtgJsonCard> mtgJsonAllCards)
        {
            var distinctCards = mtgJsonAllCards.Distinct();

            //get from DB
            var entitiesFromDb = _repo.GetMtgCards().ToList();

            //Filter to exclude what is already in DB and map to entity
            var entitiesToAdd = GetDistinctNewEntities(distinctCards, entitiesFromDb);

            if (entitiesToAdd != null && entitiesToAdd.Any())
            {
                //Insert new entities to DB
                _cards = _repo.InsertMtgCards(entitiesToAdd);
            }
        }

        private void ImportAndInsertSets(MtgJsonSet mtgJsonSet)
        {
            //get from DB
            var entitiesFromDb = _codesRepo.GetSetEntities().ToList();

            //Filter to exclude what is already in DB and map to entity
            var setEntityToAdd = GetDistinctNewEntities(mtgJsonSet, entitiesFromDb);

            if (setEntityToAdd != null)
            {
                //Insert new entities to DB
                _codesRepo.InsertSetEntity(setEntityToAdd);
            }
        }

        private void ImportAndInsertSubtypes(List<MtgJsonCard> mtgJsonAllCards)
        {
            var distinctTypeCodes = mtgJsonAllCards
                .Where(c => c.SubTypes != null)
                .SelectMany(c => { return c.SubTypes; })
                .Distinct()
                .ToList();

            //get from DB
            var entitiesFromDb = _codesRepo.GetSubtypeEntities().ToList();

            //Filter to exclude what is already in DB and map to entity
            var entitiesToAdd = GetDistinctNewEntities(distinctTypeCodes, entitiesFromDb);

            if (entitiesToAdd != null && entitiesToAdd.Any())
            {
                //Insert new entities to DB
                _codesRepo.InsertSubtypeEntities(entitiesToAdd);
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
            var entitiesFromDb = _codesRepo.GetSupertypeEntities().ToList();

            //Filter to exclude what is already in DB and map to entity
            var entitiesToAdd = GetDistinctNewEntities(distinctTypeCodes, entitiesFromDb);

            if (entitiesToAdd != null && entitiesToAdd.Any())
            {
                //Insert new entities to DB
                _codesRepo.InsertSupertypeEntities(entitiesToAdd);
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
            var entitiesFromDb = _codesRepo.GetTypeEntities().ToList();

            //Filter to exclude what is already in DB and map to entity
            var entitiesToAdd = GetDistinctNewEntities(distinctTypeCodes, entitiesFromDb);

            if (entitiesToAdd != null && entitiesToAdd.Any())
            {
                //Insert new entities to DB
                _codesRepo.InsertTypeEntities(entitiesToAdd);
            }
        }

        private void ImportAndInsertRarities(List<MtgJsonCard> mtgJsonAllCards)
        {
            var distinctTypeCodes = mtgJsonAllCards
                .Where(c => c.Rarity != null)
                .Select(c => { return c.Rarity; })
                .Distinct()
                .ToList();

            //get from DB
            var entitiesFromDb = _codesRepo.GetRarityEntities().ToList();

            //Filter to exclude what is already in DB and map to entity
            var entitiesToAdd = GetDistinctNewEntities(distinctTypeCodes, entitiesFromDb);

            if (entitiesToAdd != null && entitiesToAdd.Any())
            {
                //Insert new entities to DB
                _codesRepo.InsertRarityEntities(entitiesToAdd);
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
            var entitiesFromDb = _codesRepo.GetLegalityEntities().ToList();

            //Filter to exclude what is already in DB and map to entity
            var entitiesToAdd = GetDistinctNewEntities(distinctTypeCodes, entitiesFromDb);

            if (entitiesToAdd != null && entitiesToAdd.Any())
            {
                //Insert new entities to DB
                _codesRepo.InsertLegalityEntities(entitiesToAdd);
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
            var entitiesFromDb = _codesRepo.GetFormatEntities().ToList();

            //Filter to exclude what is already in DB and map to entity
            var entitiesToAdd = GetDistinctNewEntities(distinctTypeCodes, entitiesFromDb);

            if (entitiesToAdd != null && entitiesToAdd.Any())
            {
                //Insert new entities to DB
                _codesRepo.InsertFormatEntities(entitiesToAdd);
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
            var entitiesFromDb = _codesRepo.GetColorEntities().ToList();

            //Filter to exclude what is already in DB and map to entity
            var entitiesToAdd = GetDistinctNewEntities(distinctTypeCodes, entitiesFromDb);

            if (entitiesToAdd != null && entitiesToAdd.Any())
            {
                //Insert new entities to DB
                _codesRepo.InsertColorEntities(entitiesToAdd);
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
            var entitiesFromDb = _codesRepo.GetBorderEntities().ToList();

            //Filter to exclude what is already in DB and map to entity
            var entitiesToAdd = GetDistinctNewEntities(distinctTypeCodes, entitiesFromDb);

            if (entitiesToAdd != null && entitiesToAdd.Any())
            {
                //Insert new entities to DB
                _codesRepo.InsertBorderEntities(entitiesToAdd);
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
            var entitiesFromDb = _codesRepo.GetLayoutEntities().ToList();

            //Filter to exclude what is already in DB and map to entity
            var entitiesToAdd = GetDistinctNewEntities(distinctTypeCodes, entitiesFromDb);
            
            if (entitiesToAdd != null && entitiesToAdd.Any())
            {
                //Insert new entities to DB
                _codesRepo.InsertLayoutEntities(entitiesToAdd);
            }
        }

        #endregion ImportAndInsert Methods

        #region Filter Methods

        private SetEntity GetDistinctNewEntities(MtgJsonSet mtgJsonSet, List<SetEntity> setEntities)
        {   
            if (setEntities == null || !setEntities.Any(s => s.Code == mtgJsonSet.Code))
            {
                return _mapper.Map<MtgJsonSet, SetEntity>(mtgJsonSet);
            }

            return null;
        }

        private IEnumerable<MtgCardEntity> GetDistinctNewEntities(IEnumerable<MtgJsonCard> distinctCards, List<MtgCardEntity> entitiesFromDb)
        {
            List<MtgJsonCard> cardsToAdd = new List<MtgJsonCard>();

            if(entitiesFromDb == null)
            {
                cardsToAdd = distinctCards.ToList();
            }
            else
            {
                foreach (var card in distinctCards)
                {
                    if (!entitiesFromDb.Any(s => s.MtgJsonId == card.Id))
                    {
                        cardsToAdd.Add(card);
                    }
                }
            }

            return Map(cardsToAdd);
        }

        private List<T> GetDistinctNewEntities<T>(List<string> codes, List<T> codesInDatabase) where T : BaseCodesType
        {
            List<T> results = new List<T>();

            foreach (var typeCode in codes)
            {
                if (!codesInDatabase.Any(c => c.Code == typeCode))
                {
                    results.Add(_mapper.Map<T>(typeCode));
                }
            }

            return results;
        }

        #endregion Filter Methods

        #region Mapping Methods

        private List<MtgJsonCard> MapPropertiesFromSetToCards(MtgJsonSet mtgJsonSet)
        {
            foreach (var card in mtgJsonSet.Cards)
            {
                card.Border = card.Border ?? mtgJsonSet.Border;
                card.SetTypeCode = mtgJsonSet.Code;
            }

            return mtgJsonSet.Cards.ToList();
        }

        private IEnumerable<MtgCardEntity> Map(List<MtgJsonCard> cardsToAdd)
        {
            List<MtgCardEntity> results = new List<MtgCardEntity>();

            foreach(var card in cardsToAdd)
            {
                results.Add(Map(card));
            }

            return results;
        }

        private MtgCardEntity Map(MtgJsonCard card)
        {
            return new MtgCardEntity()
            {
                Artist = card.Artist,
                BorderId = _codesCache.GetEntityByCode<BorderEntity>(card.Border).Id,
                ConvertedManaCost = TryParseNullableDecimal(card.Cmc),
                Flavor = card.Flavor,
                IsReserved = card.Reserved ?? false,
                IsStarter = card.Starter ?? false,
                IsTimeShifted = card.TimeShifted ?? false,
                LayoutId = _codesCache.GetEntityByCode<LayoutEntity>(card.Layout).Id,
                Loyalty = TryParseNullableDecimal(card.Loyalty),
                ManaCost = card.ManaCost,
                MtgJsonId = card.Id,
                MultiverseId = card.MultiverseId,
                Name = card.Name,
                Number = card.Number,
                OriginalText = card.OriginalText,
                OriginalType = card.OriginalType,
                Power = card.Power,
                RarityId = _codesCache.GetEntityByCode<RarityEntity>(card.Rarity).Id,
                ReleaseDate = TryParseNullableDate(card.ReleaseDate),
                SetId = _codesCache.GetEntityByCode<SetEntity>(card.SetTypeCode).Id,
                Source = card.Source,
                Text = card.Text,
                Type = card.Type,
                Toughness = card.Toughness,
                PowerDecimal = TryParseNullableDecimal(card.Power),
                ToughnessDecimal = TryParseNullableDecimal(card.Toughness),
                Legalities = MapLegalities(card.Legalities),
                Colors = MapColors(card.Colors),
                Types = MapTypes(card.Types),
                Supertypes = MapSupertypes(card.SuperTypes),
                Subtypes = MapSubtypes(card.SubTypes)
            };
        }

        private IEnumerable<MtgCardSubTypesEntity> MapSubtypes(IEnumerable<string> subTypes)
        {
            var result = new List<MtgCardSubTypesEntity>();

            if (subTypes == null) return result;

            foreach (var type in subTypes)
            {
                result.Add(new MtgCardSubTypesEntity()
                {
                    SubtypeId = _codesCache.GetEntityByCode<SubtypeEntity>(type).Id
                });
            }

            return result;
        }

        private IEnumerable<MtgCardSupertypesEntity> MapSupertypes(IEnumerable<string> superTypes)
        {
            var result = new List<MtgCardSupertypesEntity>();

            if (superTypes == null) return result;

            foreach (var type in superTypes)
            {
                result.Add(new MtgCardSupertypesEntity()
                {
                    SupertypeId = _codesCache.GetEntityByCode<SupertypeEntity>(type).Id
                });
            }

            return result;
        }

        private IEnumerable<MtgCardTypesEntity> MapTypes(IEnumerable<string> types)
        {
            var result = new List<MtgCardTypesEntity>();

            if (types == null) return result;

            foreach (var type in types)
            {
                result.Add(new MtgCardTypesEntity() {
                    TypeId = _codesCache.GetEntityByCode<TypeEntity>(type).Id
                });
            }

            return result;
        }

        private IEnumerable<MtgCardColorsEntity> MapColors(IEnumerable<string> colors)
        {
            var result = new List<MtgCardColorsEntity>();

            if (colors == null) return result;

            foreach (var color in colors)
            {
                result.Add(new MtgCardColorsEntity() {
                    ColorId = _codesCache.GetEntityByCode<ColorEntity>(color).Id
                });
            }

            return result;
        }

        private IEnumerable<MtgCardLegalitiesEntity> MapLegalities(List<MtgJsonLegality> legalities)
        {
            var result = new List<MtgCardLegalitiesEntity>();

            if (legalities == null) return result;

            foreach (var legality in legalities)
            {
                result.Add(new MtgCardLegalitiesEntity()
                {
                    LegalityId = _codesCache.GetEntityByCode<LegalityEntity>(legality.Legality).Id,
                    FormatId = _codesCache.GetEntityByCode<FormatEntity>(legality.Format).Id
                });
            }

            return result;
        }

        #endregion Mapping Methods
    }
}