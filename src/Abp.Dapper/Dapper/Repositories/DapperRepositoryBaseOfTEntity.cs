using Abp.Data;
using Abp.Domain.Entities;
using System;

namespace Abp.Dapper.Repositories
{
    public class DapperRepositoryBase<TEntity> : DapperRepositoryBase<TEntity, Guid>, IDapperRepository<TEntity>
        where TEntity : class, IEntity
    {
        public DapperRepositoryBase(IActiveTransactionProvider activeTransactionProvider) : base(activeTransactionProvider)
        {
        }
    }
}
