using Abp.MultiTenancy;
using Abp.Zero.SampleApp.Users;
using System;

namespace Abp.Zero.SampleApp.MultiTenancy
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