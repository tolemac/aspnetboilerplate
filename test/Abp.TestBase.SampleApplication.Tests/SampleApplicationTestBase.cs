using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Abp.Extensions;
using Abp.TestBase.SampleApplication.ContactLists;
using Abp.TestBase.SampleApplication.Crm;
using Abp.TestBase.SampleApplication.EntityFramework;
using Abp.TestBase.SampleApplication.Messages;
using Abp.TestBase.SampleApplication.People;
using Castle.MicroKernel.Registration;
using EntityFramework.DynamicFilters;

namespace Abp.TestBase.SampleApplication.Tests
{
    public abstract class SampleApplicationTestBase : AbpIntegratedTestBase<SampleApplicationTestModule>
    {
        protected SampleApplicationTestBase()
        {
            CreateInitialData();
        }

        protected override void PreInitialize()
        {
            base.PreInitialize();

            //Fake DbConnection using Effort!
            LocalIocManager.IocContainer.Register(
                Component.For<DbConnection>()
                    .UsingFactoryMethod(Effort.DbConnectionFactory.CreateTransient)
                    .LifestyleSingleton()
                );
        }

        protected override void PostInitialize()
        {
            //Commented out, since Effort.DbConnection does not provide Sql Text while interception time.
            //DbInterception.Add(Resolve<WithNoLockInterceptor>());
        }

        protected virtual void CreateInitialData()
        {
            UsingDbContext(
                context =>
                {
                    context.ContactLists.Add(
                        new ContactList
                        {
                            Id = GuidExtensions.Guid1,
                            TenantId = GuidExtensions.Guid1,
                            Name = "List of Tenant-1",
                            People = new List<Person>
                                     {
                                         new Person {Id = GuidExtensions.Guid1, Name = "halil", CreatorUserId = GuidExtensions.Guid42 },
                                         new Person {Id = GuidExtensions.Guid2, Name = "emre", IsDeleted = true}
                                     }
                        });

                    context.ContactLists.Add(
                        new ContactList
                        {
                            Id = GuidExtensions.Guid2,
                            TenantId = GuidExtensions.Guid2,
                            Name = "List of Tenant-2",
                            People = new List<Person>
                                     {
                                         new Person {Id = GuidExtensions.Guid3, Name = "asimov"},
                                     }
                        });

                    context.ContactLists.Add(
                        new ContactList
                        {
                            Id = GuidExtensions.Guid3,
                            TenantId = GuidExtensions.Guid3,
                            Name = "List-1 of Tenant-3",
                            People = new List<Person>
                                     {
                                         new Person {Id = GuidExtensions.Guid4, Name = "John Doe"},
                                     }
                        });

                    context.ContactLists.Add(
                        new ContactList
                        {
                            Id = GuidExtensions.Guid4,
                            TenantId = GuidExtensions.Guid3,
                            Name = "List-2 of Tenant-3",
                            People = new List<Person>()
                        });
                });

            UsingDbContext(
                context =>
                {
                    context.Messages.Add(
                        new Message
                        {
                            TenantId = null,
                            Text = "host-message-1"
                        });

                    context.Messages.Add(
                        new Message
                        {
                            TenantId = GuidExtensions.Guid1,
                            Text = "tenant-1-message-1"
                        });

                    context.Messages.Add(
                        new Message
                        {
                            TenantId = GuidExtensions.Guid1,
                            Text = "tenant-1-message-2"
                        });
                });

            UsingDbContext(
              context =>
              {
                  AddCompany(context,
                      GuidExtensions.Guid1,
                      "Volosoft",
                      "Turkey",
                      "Istanbul",
                      "Denizkoskler Mah. Avcilar",
                      "Halil",
                      "Gumuspala Mah. Avcilar",
                      "Ismail",
                      "Headquarter",
                      "Europe Headquarter");

                  AddCompany(context,
                      GuidExtensions.Guid2,
                      "Microsoft",
                      "USA",
                      "New York",
                      "Herkimer St, Brooklyn, NY",
                      "Neal",
                      "Vinegar Hill, Brooklyn, NY",
                      "Peter",
                      "Main Office",
                      "IT Office");
              });
        }

        private void AddCompany(SampleApplicationDbContext context, Guid id, string name, string country, string city, string address1, string modifier1, string address2, string modifier2, string branchName1, string branchName2)
        {
            var company = new Company
            {
                Id = id,
                Name = name,
                CreationTime = new DateTime(2017, 03, 16, 0, 0, 0, DateTimeKind.Utc),
                BillingAddress = new Address
                {
                    Country = country,
                    City = city,
                    FullAddress = address1,
                    CreationTime = new DateTime(2017, 03, 16, 0, 0, 0, DateTimeKind.Local),
                    LastModifier = new Modifier
                    {
                        Name = modifier1,
                        ModificationTime = new DateTime(2016, 03, 16, 0, 0, 0, DateTimeKind.Local)
                    }
                },
                ShippingAddress = new Address
                {
                    Country = country,
                    City = city,
                    FullAddress = address2,
                    CreationTime = new DateTime(2017, 03, 16, 0, 0, 0, DateTimeKind.Utc),
                    LastModifier = new Modifier
                    {
                        Name = modifier2,
                        ModificationTime = new DateTime(2017, 03, 16, 0, 0, 0, DateTimeKind.Utc)
                    }
                },
                Branches = new List<Branch>
                {
                    new Branch
                    {
                        Name = branchName1,
                        CreationTime = new DateTime(2017, 03, 16, 0, 0, 0, DateTimeKind.Local),
                    },
                    new Branch
                    {
                        Name = branchName2,
                        CreationTime = new DateTime(2017, 03, 16, 0, 0, 0, DateTimeKind.Utc)
                    }
                }
            };

            context.Companies.Add(company);
        }

        public void UsingDbContext(Action<SampleApplicationDbContext> action)
        {
            using (var context = LocalIocManager.Resolve<SampleApplicationDbContext>())
            {
                context.DisableAllFilters();
                action(context);
                context.SaveChanges();
            }
        }

        public T UsingDbContext<T>(Func<SampleApplicationDbContext, T> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<SampleApplicationDbContext>())
            {
                context.DisableAllFilters();
                result = func(context);
                context.SaveChanges();
            }

            return result;
        }

        public async Task<T> UsingDbContextAsync<T>(Func<SampleApplicationDbContext, Task<T>> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<SampleApplicationDbContext>())
            {
                context.DisableAllFilters();
                result = await func(context);
                context.SaveChanges();
            }

            return result;
        }
    }
}
