﻿using Abp.ZeroCore.SampleApp.EntityFramework;
using System;

namespace Abp.Zero.TestData
{
    public class TestDataBuilder
    {
        private readonly SampleAppDbContext _context;
        private readonly Guid _tenantId;

        public TestDataBuilder(SampleAppDbContext context, Guid tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            new TestRolesBuilder(_context, _tenantId).Create();
            new TestOrganizationUnitsBuilder(_context, _tenantId).Create();

            _context.SaveChanges();
        }
    }
}
