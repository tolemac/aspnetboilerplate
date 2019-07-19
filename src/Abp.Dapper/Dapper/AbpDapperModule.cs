using Abp.Dapper.TypeHandlers;
using Abp.Dependency;
using Abp.Modules;
using Abp.Orm;
using Abp.Reflection.Extensions;
using Dapper;
using System;

namespace Abp.Dapper
{
    [DependsOn(typeof(AbpKernelModule))]
    public class AbpDapperModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactionScopeAvailable = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpDapperModule).GetAssembly());

            // Then you add follow line at where you app beigns to run.
            SqlMapper.AddTypeHandler<Guid>(new GuidTypeHandler());

            using (IScopedIocResolver scope = IocManager.CreateScope())
            {
                ISecondaryOrmRegistrar[] additionalOrmRegistrars = scope.ResolveAll<ISecondaryOrmRegistrar>();

                foreach (ISecondaryOrmRegistrar registrar in additionalOrmRegistrars)
                {
                    if (registrar.OrmContextKey == AbpConsts.Orms.EntityFramework)
                    {
                        registrar.RegisterRepositories(IocManager, EfBasedDapperAutoRepositoryTypes.Default);
                    }

                    if (registrar.OrmContextKey == AbpConsts.Orms.NHibernate)
                    {
                        registrar.RegisterRepositories(IocManager, NhBasedDapperAutoRepositoryTypes.Default);
                    }

                    if (registrar.OrmContextKey == AbpConsts.Orms.EntityFrameworkCore)
                    {
                        registrar.RegisterRepositories(IocManager, EfBasedDapperAutoRepositoryTypes.Default);
                    }
                }
            }
        }
    }
}
