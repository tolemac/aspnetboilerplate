using System;
using Abp.Domain.Entities;
using Shouldly;
using Xunit;

namespace Abp.Tests.Domain.Entities
{
    public class EntityHelper_Tests
    {
        [Fact]
        public void GetPrimaryKeyType_Tests()
        {
            EntityHelper.GetPrimaryKeyType<Manager>().ShouldBe(typeof(Guid));
            EntityHelper.GetPrimaryKeyType(typeof(Manager)).ShouldBe(typeof(Guid));
            EntityHelper.GetPrimaryKeyType(typeof(TestEntityWithIntPk)).ShouldBe(typeof(int));
            EntityHelper.GetPrimaryKeyType(typeof(TestEntityWithLongPk)).ShouldBe(typeof(long));
        }

        private class TestEntityWithIntPk : Entity<int>
        {

        }
        private class TestEntityWithLongPk : Entity<long>
        {

        }
    }
}
