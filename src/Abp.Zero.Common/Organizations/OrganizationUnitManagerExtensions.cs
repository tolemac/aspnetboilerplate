using System;
using System.Collections.Generic;
using Abp.Threading;

namespace Abp.Organizations
{
    public static class OrganizationUnitManagerExtensions
    {
        public static string GetCode(this OrganizationUnitManager manager, Guid id)
        {
            return AsyncHelper.RunSync(() => manager.GetCodeAsync(id));
        }

        public static void Create(this OrganizationUnitManager manager, OrganizationUnit organizationUnit)
        {
            AsyncHelper.RunSync(() => manager.CreateAsync(organizationUnit));
        }

        public static void Update(this OrganizationUnitManager manager, OrganizationUnit organizationUnit)
        {
            AsyncHelper.RunSync(() => manager.UpdateAsync(organizationUnit));
        }

        public static void Delete(this OrganizationUnitManager manager, Guid id)
        {
            AsyncHelper.RunSync(() => manager.DeleteAsync(id));
        }

        public static string GetNextChildCode(this OrganizationUnitManager manager, Guid? parentId)
        {
            return AsyncHelper.RunSync(() => manager.GetNextChildCodeAsync(parentId));
        }

        public static OrganizationUnit GetLastChildOrNull(this OrganizationUnitManager manager, Guid? parentId)
        {
            return AsyncHelper.RunSync(() => manager.GetLastChildOrNullAsync(parentId));
        }

        public static void Move(this OrganizationUnitManager manager, Guid id, Guid? parentId)
        {
            AsyncHelper.RunSync(() => manager.MoveAsync(id, parentId));
        }

        public static List<OrganizationUnit> FindChildren(this OrganizationUnitManager manager, Guid? parentId, bool recursive = false)
        {
            return AsyncHelper.RunSync(() => manager.FindChildrenAsync(parentId, recursive));
        }
    }
}