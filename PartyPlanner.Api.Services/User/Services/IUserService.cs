using System.Security.Claims;
using System.Threading.Tasks;
using PartyPlanner.Data.Models;

namespace PartyPlanner.Api.Services.User.Services
{
    public interface IUserService
    {
        Task<UserIdentity> GetUserIdentity(ClaimsPrincipal userClaims);
    }
}
