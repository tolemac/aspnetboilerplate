﻿using System;
using System.Data.Entity;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Repositories;
using Abp.Tests;
using Castle.MicroKernel.Registration;
using Shouldly;
using Xunit;

namespace Abp.EntityFramework.Tests.Repositories
{
    public class EntityFrameworkGenericRepositoryRegistrar_Tests : TestBaseWithLocalIocManager
    {
        public EntityFrameworkGenericRepositoryRegistrar_Tests()
        {
            var fakeBaseDbContextProvider = NSubstitute.Substitute.For<IDbContextProvider<MyBaseDbContext>>();
            var fakeMainDbContextProvider = NSubstitute.Substitute.For<IDbContextProvider<MyMainDbContext>>();
            var fakeModuleDbContextProvider = NSubstitute.Substitute.For<IDbContextProvider<MyModuleDbContext>>();

            LocalIocManager.IocContainer.Register(
                Component.For<IDbContextProvider<MyBaseDbContext>>().Instance(fakeBaseDbContextProvider),
                Component.For<IDbContextProvider<MyMainDbContext>>().Instance(fakeMainDbContextProvider),
                Component.For<IDbContextProvider<MyModuleDbContext>>().Instance(fakeModuleDbContextProvider),
                Component.For<IDbContextEntityFinder>().ImplementedBy<EfDbContextEntityFinder>().LifestyleTransient(),
                Component.For<EfGenericRepositoryRegistrar>().LifestyleTransient()
                );

            using (var repositoryRegistrar = LocalIocManager.ResolveAsDisposable<EfGenericRepositoryRegistrar>())
            {
                repositoryRegistrar.Object.RegisterForDbContext(typeof(MyModuleDbContext), LocalIocManager, EfAutoRepositoryTypes.Default);
                repositoryRegistrar.Object.RegisterForDbContext(typeof(MyMainDbContext), LocalIocManager, EfAutoRepositoryTypes.Default);
            }
        }

        [Fact]
        public void Should_Resolve_Generic_Repositories()
        {
            //Entity 1 (with default PK)
            var entity1Repository = LocalIocManager.Resolve<IRepository<MyEntity1, Guid>>();
            entity1Repository.ShouldNotBe(null);
            (entity1Repository is EfRepositoryBase<MyBaseDbContext, MyEntity1, Guid>).ShouldBe(true);

            //Entity 1 (with specified PK)
            var entity1RepositoryWithPk = LocalIocManager.Resolve<IRepository<MyEntity1, Guid>>();
            entity1RepositoryWithPk.ShouldNotBe(null);
            (entity1RepositoryWithPk is EfRepositoryBase<MyBaseDbContext, MyEntity1, Guid>).ShouldBe(true);

            //Entity 1 (with specified Repository forIMyModuleRepository )
            var entity1RepositoryWithModuleInterface = LocalIocManager.Resolve<IMyModuleRepository<MyEntity1, Guid>>();
            entity1RepositoryWithModuleInterface.ShouldNotBe(null);
            (entity1RepositoryWithModuleInterface is MyModuleRepositoryBase<MyEntity1, Guid>).ShouldBe(true);
            (entity1RepositoryWithModuleInterface is EfRepositoryBase<MyModuleDbContext, MyEntity1, Guid>).ShouldBe(true);

            //Entity 1 (with specified Repository forIMyModuleRepository )
            var entity1RepositoryWithModuleInterfaceWithPk = LocalIocManager.Resolve<IMyModuleRepository<MyEntity1, Guid>>();
            entity1RepositoryWithModuleInterfaceWithPk.ShouldNotBe(null);
            (entity1RepositoryWithModuleInterfaceWithPk is MyModuleRepositoryBase<MyEntity1, Guid>).ShouldBe(true);
            (entity1RepositoryWithModuleInterfaceWithPk is EfRepositoryBase<MyModuleDbContext, MyEntity1, Guid>).ShouldBe(true);

            //Entity 2
            var entity2Repository = LocalIocManager.Resolve<IRepository<MyEntity2, long>>();
            (entity2Repository is EfRepositoryBase<MyMainDbContext, MyEntity2, long>).ShouldBe(true);
            entity2Repository.ShouldNotBe(null);

            //Entity 3
            var entity3Repository = LocalIocManager.Resolve<IMyModuleRepository<MyEntity3, int>>();
            (entity3Repository is EfRepositoryBase<MyModuleDbContext, MyEntity3, int>).ShouldBe(true);
            entity3Repository.ShouldNotBe(null);
        }

        public class MyMainDbContext : MyBaseDbContext
        {
            public virtual DbSet<MyEntity2> MyEntities2 { get; set; }

            public virtual DbSet<MyNonEntity> MyNonEntities { get; set; }
        }

        [AutoRepositoryTypes(
            typeof(IMyModuleRepository<>),
            typeof(IMyModuleRepository<,>),
            typeof(MyModuleRepositoryBase<>),
            typeof(MyModuleRepositoryBase<,>)
            )]
        public class MyModuleDbContext : MyBaseDbContext
        {
            public virtual DbSet<MyEntity3> MyEntities3 { get; set; }
        }

        public abstract class MyBaseDbContext : AbpDbContext
        {
            public virtual IDbSet<MyEntity1> MyEntities1 { get; set; }
        }

        public class MyEntity1 : Entity
        {

        }

        public class MyEntity2 : Entity<long>
        {

        }

        public class MyEntity3 : Entity<int>
        {

        }

        public class MyNonEntity
        {

        }

        public interface IMyModuleRepository<TEntity> : IRepository<TEntity>
            where TEntity : class, IEntity<Guid>
        {

        }

        public interface IMyModuleRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
            where TEntity : class, IEntity<TPrimaryKey>
        {

        }

        public class MyModuleRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<MyModuleDbContext, TEntity, TPrimaryKey>, IMyModuleRepository<TEntity, TPrimaryKey>
            where TEntity : class, IEntity<TPrimaryKey>
        {
            public MyModuleRepositoryBase(IDbContextProvider<MyModuleDbContext> dbContextProvider)
                : base(dbContextProvider)
            {
            }
        }

        public class MyModuleRepositoryBase<TEntity> : MyModuleRepositoryBase<TEntity, Guid>, IMyModuleRepository<TEntity>
            where TEntity : class, IEntity
        {
            public MyModuleRepositoryBase(IDbContextProvider<MyModuleDbContext> dbContextProvider)
                : base(dbContextProvider)
            {
            }
        }
    }
}
