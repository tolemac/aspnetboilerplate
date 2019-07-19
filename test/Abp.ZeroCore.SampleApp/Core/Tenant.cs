using Abp.MultiTenancy;
using System;

namespace Abp.ZeroCore.SampleApp.Core
{
    public class Tenant : AbpTenant<User>
    {
        protected Tenant()
        {

        }

        public Tenant(Guid id, string tenancyName, string name)
            : base(id, tenancyName, name)
        {
        }
    }
}