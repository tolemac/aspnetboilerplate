using Abp.Runtime.Caching;
using System;

namespace Abp.Domain.Entities.Caching
{
    public interface IEntityCache<TCacheItem> : IEntityCache<TCacheItem, Guid>
    {
    }

    public interface IEntityCache<TCacheItem, TPrimaryKey> : IEntityCacheBase<TCacheItem, TPrimaryKey>
    {
        ITypedCache<TPrimaryKey, TCacheItem> InternalCache { get; }
    }
}