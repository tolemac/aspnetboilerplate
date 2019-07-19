using Abp.Extensions;
using Shouldly;
using Xunit;

namespace Abp.Tests
{
    public class UserIdentifier_Tests
    {
        [Fact]
        public void GetHashCode_Test()
        {
            UserIdentifier.Parse($"{GuidExtensions.Guid5}@{GuidExtensions.Guid4}").GetHashCode().ShouldBe(UserIdentifier.Parse($"{GuidExtensions.Guid5}@{GuidExtensions.Guid4}").GetHashCode());

            UserIdentifier.Parse($"{GuidExtensions.Guid1}@{GuidExtensions.Guid1}").GetHashCode().ShouldNotBe(UserIdentifier.Parse($"{GuidExtensions.Guid2}@{GuidExtensions.Guid2}").GetHashCode());

            UserIdentifier.Parse($"{GuidExtensions.Guid1}@{GuidExtensions.Guid0}").GetHashCode().ShouldNotBe(UserIdentifier.Parse($"{GuidExtensions.Guid0}@{GuidExtensions.Guid1}").GetHashCode());

            UserIdentifier.Parse($"{GuidExtensions.Guid1}@{GuidExtensions.Guid2}").GetHashCode().ShouldNotBe(UserIdentifier.Parse($"{GuidExtensions.Guid2}@{GuidExtensions.Guid1}").GetHashCode());
        }
    }
}