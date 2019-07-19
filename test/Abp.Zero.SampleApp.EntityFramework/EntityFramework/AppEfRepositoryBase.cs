using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using System;

namespace Abp.Zero.SampleApp.EntityFramework
{
    public class AppEfRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<AppDbContext, TEntity, TPrimaryKey>
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