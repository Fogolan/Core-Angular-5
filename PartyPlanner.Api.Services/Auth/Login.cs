using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PartyPlanner.Api.Services.Auth.Models;
using PartyPlanner.Api.Services.Auth.Services;
using PartyPlanner.Api.Services.User.Models;
using PartyPlanner.Data.Models;

namespace PartyPlanner.Api.Services.Auth
{
  public static class Login
  {
    public class Query : IRequest<Credentials>
    {
      public string Username { get; set; }

      public string Password { get; set; }
    }

    public class Handler : AsyncRequestHandler<Query, Credentials>
    {
      private readonly UserManager<UserIdentity> _userManager;
      private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;

      public Handler(UserManager<UserIdentity> userManager, IJwtFactory jwtFactory, JwtIssuerOptions jwtOptions)
      {
        _userManager = userManager;
        _jwtFactory = jwtFactory;
          _jwtOptions = jwtOptions;
      }

      protected override async Task<Credentials> HandleCore(Query request)
      {
        var identity = await GetClaimsIdentity(request.Username, request.Password);
        var user = await _userManager.FindByNameAsync(request.Username);
        var role = await _userManager.GetRolesAsync(user);

        var result = new Credentials
        {
          Id = identity.Claims.Single(c => c.Type == "id").Value,
          Username = request.Username,
          Token = await _jwtFactory.GenerateEncodedToken(request.Username, identity),
          ExpiresIn = (int) _jwtOptions.ValidFor.TotalSeconds,
          Role = role.FirstOrDefault()
        };

        return result;
      }

      private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
      {
        if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
        {
          // get the user to verifty
          var userToVerify = await _userManager.FindByNameAsync(userName);

          if (userToVerify != null)
          {
            // check the credentials  
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
              return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
            }
          }
        }

        // Credentials are invalid, or account doesn't exist
        return await Task.FromResult<ClaimsIdentity>(null);
      }
    }
  }
}