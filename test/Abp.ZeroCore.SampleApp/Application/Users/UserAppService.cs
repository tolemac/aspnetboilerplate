using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.ZeroCore.SampleApp.Core;

namespace Abp.ZeroCore.SampleApp.Application.Users
{
    public class UserAppService : AsyncCrudAppService<User, UserDto>, IUserAppService
    {
        public UserAppService(IRepository<User> repository) 
            : base(repository)
        {
            
        }
    }
}