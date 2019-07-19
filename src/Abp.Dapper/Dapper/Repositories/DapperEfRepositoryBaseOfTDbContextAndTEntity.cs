using Abp.Data;
using Abp.Domain.Entities;
using Abp.Transactions;
using System;

namespace Abp.Dapper.Repositories
{
    public class DapperEfRepositoryBase<TDbContext, TEntity> : DapperEfRepositoryBase<TDbContext, TEntity, Guid>, IDapperRepository<TEntity>
        where TEntity : class, IEntity
        where TDbContext : class

    {
        public DapperEfRepositoryBase(IActiveTransactionProvider activeTransactionProvider) : base(activeTransactionProvider)
        {
        }
    }
}
