using Abp.Domain.Entities;
using Abp.Events.Bus.Entities;
using Abp.Extensions;
using Shouldly;
using Xunit;

namespace Abp.Tests.Events.Bus
{
    public class GenericInheritanceTest : EventBusTestBase
    {
        [Fact]
        public void Should_Trigger_For_Inherited_Generic_1()
        {
            var triggeredEvent = false;

            EventBus.Register<EntityChangedEventData<Person>>(
                eventData =>
                {
                    eventData.Entity.Id.ShouldBe(GuidExtensions.Guid42);
                    triggeredEvent = true;
                });

            EventBus.Trigger(new EntityUpdatedEventData<Person>(new Person { Id = GuidExtensions.Guid42 }));

            triggeredEvent.ShouldBe(true);
        }

        [Fact]
        public void Should_Trigger_For_Inherited_Generic_2()
        {
            var triggeredEvent = false;

            EventBus.Register<EntityChangedEventData<Person>>(
                eventData =>
                {
                    eventData.Entity.Id.ShouldBe(GuidExtensions.Guid42);
                    triggeredEvent = true;
                });

            EventBus.Trigger(new EntityChangedEventData<Student>(new Student { Id = GuidExtensions.Guid42 }));

            triggeredEvent.ShouldBe(true);
        }
        
        
        public class Person : Entity
        {
            
        }

        public class Student : Person
        {
            
        }
    }
}