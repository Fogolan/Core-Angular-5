using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PartyPlanner.Data.Models;

namespace PartyPlanner.Api.Services.User.Services
{
    public class UserSerivce : IUserService
    {
        private readonly UserManager<UserIdentity> userManager;

        public UserSerivce(UserManager<UserIdentity> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<UserIdentity> GetUserIdentity(ClaimsPrincipal userClaims)
        {
            var userName = userClaims.Identities
                .First()
                .Claims.First().Value;
            var userIdentity = await userManager.FindByNameAsync(userName);

            return userIdentity;
        }
    }

}
