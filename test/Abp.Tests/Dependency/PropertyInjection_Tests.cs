using Abp.Application.Services;
using Abp.Extensions;
using Abp.Runtime.Session;
using Castle.MicroKernel.Registration;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Abp.Tests.Dependency
{
    public class PropertyInjection_Tests : TestBaseWithLocalIocManager
    {
        [Fact]
        public void Should_Inject_Session_For_ApplicationService()
        {
            var session = Substitute.For<IAbpSession>();
            session.TenantId.Returns(GuidExtensions.Guid1);
            session.UserId.Returns(GuidExtensions.Guid42);

            LocalIocManager.Register<MyApplicationService>();
            LocalIocManager.IocContainer.Register(
                Component.For<IAbpSession>().Instance(session)
                );

            var myAppService = LocalIocManager.Resolve<MyApplicationService>();
            myAppService.TestSession();
        }

        private class MyApplicationService : ApplicationService
        {
            public void TestSession()
            {
                AbpSession.ShouldNotBe(null);
                AbpSession.TenantId.ShouldBe(GuidExtensions.Guid1);
                AbpSession.UserId.ShouldBe(GuidExtensions.Guid42);
            }
        }
    }
}
