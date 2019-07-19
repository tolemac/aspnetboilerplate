using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using System;

namespace Abp.Zero.SampleApp.EntityFrameworkCore
{
    public class AppEfRepositoryBase<TEntity, TPrimaryKey> : EfCoreRepositoryBase<AppDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        public AppEfRepositoryBase(IDbContextProvider<AppDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }

    public class AppEfRepositoryBase<TEntity> : AppEfRepositoryBase<TEntity, Guid>, IRepository<TEntity>
        where TEntity : class, IEntity
    {
        public AppEfRepositoryBase(IDbContextProvider<AppDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}