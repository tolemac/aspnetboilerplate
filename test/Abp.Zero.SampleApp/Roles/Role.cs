using Abp.Authorization.Roles;
using Abp.Zero.SampleApp.Users;
using System;

namespace Abp.Zero.SampleApp.Roles
{
    public class Role : AbpRole<User>
    {
        public Role()
        {

        }

        public Role(Guid? tenantId, string name, string displayName)
            : base(tenantId, name, displayName)
        {

        }
    }
}