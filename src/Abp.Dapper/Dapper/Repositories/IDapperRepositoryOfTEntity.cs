using Abp.Domain.Entities;
using System;

namespace Abp.Dapper.Repositories
{
    public interface IDapperRepository<TEntity> : IDapperRepository<TEntity, Guid> where TEntity : class, IEntity<Guid>
    {
    }
}
