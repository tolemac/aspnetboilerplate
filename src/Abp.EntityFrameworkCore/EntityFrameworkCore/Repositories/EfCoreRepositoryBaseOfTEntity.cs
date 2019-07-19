using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace Abp.EntityFrameworkCore.Repositories
{
    public class EfCoreRepositoryBase<TDbContext, TEntity> : EfCoreRepositoryBase<TDbContext, TEntity, Guid>, IRepository<TEntity>
        where TEntity : class, IEntity
        where TDbContext : DbContext
    {
        public EfCoreRepositoryBase(IDbContextProvider<TDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}