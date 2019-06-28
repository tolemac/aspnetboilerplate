using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Abp.Authorization.Users
{
    public class AbpUserConfirmation<TUser> : IUserConfirmation<TUser> where TUser : AbpUser<TUser>
    {
        public async Task<bool> IsConfirmedAsync(UserManager<TUser> manager, TUser user)
        {
            if (!await manager.IsEmailConfirmedAsync(user))
            {
                return false;
            }

            return true;
        }
    }
}
