﻿using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Extensions;
using Abp.MultiTenancy;

namespace Abp.AspNetCore.App.MultiTenancy
{
    public class TestTenantStore : ITenantStore
    {
        private readonly List<TenantInfo> _tenants = new List<TenantInfo>
        {
            new TenantInfo(GuidExtensions.Guid1, "Default"),
            new TenantInfo(GuidExtensions.Guid42, "acme"),
            new TenantInfo(GuidExtensions.Guid43, "vlsft")
        };

        public TenantInfo Find(Guid tenantId)
        {
            return _tenants.FirstOrDefault(t => t.Id == tenantId);
        }

        public TenantInfo Find(string tenancyName)
        {
            return _tenants.FirstOrDefault(t => t.TenancyName.ToLower() == tenancyName.ToLower());
        }
    }
}
