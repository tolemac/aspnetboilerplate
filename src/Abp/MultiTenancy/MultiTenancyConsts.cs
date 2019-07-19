using Abp.Extensions;
using System;

namespace Abp.MultiTenancy
{
    public static class MultiTenancyConsts
    {
        /// <summary>
        /// Default tenant id: 1.
        /// </summary>
        public static readonly Guid DefaultTenantId = GuidExtensions.Guid1;
    }
}