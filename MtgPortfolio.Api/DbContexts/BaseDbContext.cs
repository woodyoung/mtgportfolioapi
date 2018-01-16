using Microsoft.EntityFrameworkCore;
using MtgPortfolio.API.DbContexts;
using MtgPortfolio.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MtgPortfolio.Api.DbContexts
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions options)
            :base(options)
        {
        }

        public IEnumerable<T> SetAudit<T>(IEnumerable<T> entities) where T : BaseEntity
        {
            List<T> result = new List<T>();

            foreach (var entity in entities)
            {
                result.Add(SetAudit<T>(entity));
            }

            return result;
        }
        public T SetAudit<T>(T entity) where T : BaseEntity
        {
            T baseEntity = entity;

            if (string.IsNullOrEmpty(baseEntity.CreatedBy) && (baseEntity.CreatedOn == null || baseEntity.CreatedOn == default(DateTime)))
            {
                var now = DateTime.Now;

                //TODO: Change to Username when set up security
                baseEntity.CreatedBy = "wyoung";
                baseEntity.CreatedOn = now;
                baseEntity.UpdatedBy = "wyoung";
                baseEntity.UpdatedOn = now;
            }
            else
            {
                baseEntity.UpdatedBy = "wyoung";
                baseEntity.UpdatedOn = DateTime.Now;
                this.Entry(baseEntity).Property("CreatedOn").IsModified = false;
                this.Entry(baseEntity).Property("CreatedBy").IsModified = false;
            }

            return baseEntity;
        }
    }
}
