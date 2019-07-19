using Abp.Configuration.Startup;
using Abp.Extensions;
using Abp.Runtime.Session;
using Shouldly;
using Xunit;

namespace Abp.TestBase.SampleApplication.Tests.Session
{
    public class Session_Tests : SampleApplicationTestBase
    {
        private readonly IAbpSession _session;

        public Session_Tests()
        {
            Resolve<IMultiTenancyConfig>().IsEnabled = true;
            _session = Resolve<IAbpSession>();
        }

        [Fact]
        public void Session_Override_Test()
        {
            _session.UserId.ShouldBeNull();
            _session.TenantId.ShouldBeNull();

            using (_session.Use(GuidExtensions.Guid42, GuidExtensions.Guid999))
            {
                _session.TenantId.ShouldBe(GuidExtensions.Guid42);
                _session.UserId.ShouldBe(GuidExtensions.Guid999);

                using (_session.Use(null, GuidExtensions.Guid3))
                {
                    _session.TenantId.ShouldBeNull();
                    _session.UserId.ShouldBe(GuidExtensions.Guid3);
                }

                _session.TenantId.ShouldBe(GuidExtensions.Guid42);
                _session.UserId.ShouldBe(GuidExtensions.Guid999);
            }

            _session.UserId.ShouldBeNull();
            _session.TenantId.ShouldBeNull();
        }
    }
}
