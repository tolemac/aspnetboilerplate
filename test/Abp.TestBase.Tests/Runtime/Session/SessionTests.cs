using Abp.Configuration.Startup;
using Abp.Extensions;
using Abp.Runtime.Session;
using Shouldly;
using Xunit;

namespace Abp.TestBase.Tests.Runtime.Session
{
    public class SessionTests : AbpIntegratedTestBase<AbpKernelModule>
    {
        [Fact]
        public void Should_Be_Default_On_Startup()
        {
            Resolve<IMultiTenancyConfig>().IsEnabled = false;

            AbpSession.UserId.ShouldBe(null);
            AbpSession.TenantId.ShouldBe(GuidExtensions.Guid1);

            Resolve<IMultiTenancyConfig>().IsEnabled = true;

            AbpSession.UserId.ShouldBe(null);
            AbpSession.TenantId.ShouldBe(null);
        }

        [Fact]
        public void Can_Change_Session_Variables()
        {
            Resolve<IMultiTenancyConfig>().IsEnabled = true;

            AbpSession.UserId = GuidExtensions.Guid1;
            AbpSession.TenantId = GuidExtensions.Guid42;

            var resolvedAbpSession = LocalIocManager.Resolve<IAbpSession>();

            resolvedAbpSession.UserId.ShouldBe(GuidExtensions.Guid1);
            resolvedAbpSession.TenantId.ShouldBe(GuidExtensions.Guid42);

            Resolve<IMultiTenancyConfig>().IsEnabled = false;

            AbpSession.UserId.ShouldBe(GuidExtensions.Guid1);
            AbpSession.TenantId.ShouldBe(GuidExtensions.Guid1);
        }
    }
}
