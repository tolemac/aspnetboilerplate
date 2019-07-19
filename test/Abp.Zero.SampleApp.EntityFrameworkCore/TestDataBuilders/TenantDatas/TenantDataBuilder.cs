using System;

namespace Abp.Zero.SampleApp.EntityFrameworkCore.TestDataBuilders.TenantDatas
{
    public class TenantDataBuilder
    {
        private readonly AppDbContext _context;

        public TenantDataBuilder(AppDbContext context)
        {
            _context = context;
        }

        public void Build(Guid tenantId)
        {
            new TenantUserBuilder(_context).Build(tenantId);
        }
    }
}