using MtgPortfolio.Api.DbContexts;
using MtgPortfolio.API.Entities.Codes;
using MtgPortfolio.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MtgPortfolio.Api.Services
{
    public class CodesCacheService : ICodesCacheService
    {
        private readonly CodesDbContext _context;
        private int _expirationInterval;

        private Dictionary<String, IEnumerable<BaseCodesType>> _entitiesCacheDictionary;
        private Dictionary<String, DateTime> _initDateTimeDictionary;

        public CodesCacheService(CodesDbContext context)
        {
            _context = context;
            _entitiesCacheDictionary = new Dictionary<string, IEnumerable<BaseCodesType>>();
            _initDateTimeDictionary = new Dictionary<string, DateTime>();

            //TODO: set this from configuration instead of hard coding
            _expirationInterval = 100000;
        }

        public T GetEntityByCode<T>(string code) where T : BaseCodesType
        {
            if (IsEmpty<T>() || IsExpired<T>())
                InitializeEntities<T>();

            return _entitiesCacheDictionary[typeof(T).FullName]?.FirstOrDefault(c => c.Code == code) as T;
        }

        public IEnumerable<T> GetEntities<T>() where T : BaseCodesType
        {
            if (IsEmpty<T>() || IsExpired<T>())
                InitializeEntities<T>();

            return _entitiesCacheDictionary[typeof(T).FullName] as IEnumerable<T>;
        }

        private void InitializeEntities<T>()
        {
            var key = typeof(T).FullName;

            if(_entitiesCacheDictionary.ContainsKey(key))
            {
                _initDateTimeDictionary[key]= DateTime.Now;
                _entitiesCacheDictionary[key] = GetEntitiesFromContext<T>();
            }
            else
            {
                _initDateTimeDictionary.Add(key, DateTime.Now);
                _entitiesCacheDictionary.Add(key, GetEntitiesFromContext<T>());
            }
            
        }

        private IEnumerable<BaseCodesType> GetEntitiesFromContext<T>()
        {
            var typeName = typeof(T).Name;

            switch(typeName)
            {
                case nameof(BorderEntity):
                    return _context.Borders;
                case nameof(ColorEntity):
                    return _context.Colors;
                case nameof(FormatEntity):
                    return _context.Formats;
                case nameof(LayoutEntity):
                    return _context.Layouts;
                case nameof(LegalityEntity):
                    return _context.Legalites;
                case nameof(RarityEntity):
                    return _context.Rarities;
                case nameof(SetEntity):
                    return _context.Sets;
                case nameof(SubtypeEntity):
                    return _context.Subtypes;
                case nameof(SupertypeEntity):
                    return _context.Supertypes;
                case nameof(TypeEntity):
                    return _context.Types;
                default:
                    return null;
            }            
        }

        private bool IsEmpty<T>()
        {
            var key = typeof(T).FullName;
            
            return !_entitiesCacheDictionary.ContainsKey(key) || _entitiesCacheDictionary[key] == null || !_entitiesCacheDictionary[key].Any();
        }

        private bool IsExpired<T>()
        {
            TimeSpan span = DateTime.Now - _initDateTimeDictionary[typeof(T).FullName];
            int ms = (int)span.TotalMilliseconds;

            return ms > _expirationInterval;
        }
    }
}
