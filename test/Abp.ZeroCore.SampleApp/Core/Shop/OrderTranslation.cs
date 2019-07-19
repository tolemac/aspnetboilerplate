using Abp.Domain.Entities;
using System;

namespace Abp.ZeroCore.SampleApp.Core.Shop
{
    public class OrderTranslation : Entity, IEntityTranslation<Order>
    {
        public virtual string Name { get; set; }

        public virtual Order Core { get; set; }

        public virtual Guid CoreId { get; set; }

        public virtual string Language { get; set; }
    }
}