using Abp.Authorization.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.DataProtection;
using System;

namespace Abp.Zero.AspNetCore
{
    public class AspNetCoreUserTokenProviderAccessor : IUserTokenProviderAccessor
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;

        public AspNetCoreUserTokenProviderAccessor(IDataProtectionProvider dataProtectionProvider)
        {
            _dataProtectionProvider = dataProtectionProvider;
        }

        public IUserTokenProvider<TUser, Guid> GetUserTokenProviderOrNull<TUser>()
            where TUser : AbpUser<TUser>
        {
            return new DataProtectorUserTokenProvider<TUser>(_dataProtectionProvider.CreateProtector("ASP.NET Identity"));
        }
    }
}