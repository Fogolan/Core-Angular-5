using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using PartyPlanner.Api.Services.User.Models;
using PartyPlanner.Data.Models;

namespace PartyPlanner.Api.Services.Auth
{
  public static class Login
  {
    public class Query : IRequest<string>
    {
      public string Username { get; set; }

      public string Password { get; set; }
    }

    public class Handler : AsyncRequestHandler<Query, string>
    {
      private readonly UserManager<UserIdentity> _userManager;
      private readonly IJwtFactory _jwtFactory;
      private readonly JsonSerializerSettings _serializerSettings;
      private readonly JwtIssuerOptions _jwtOptions;

      public Handler(UserManager<UserIdentity> userManager, IJwtFactory jwtFactory, JsonSerializerSettings serializerSettings, JwtIssuerOptions jwtOptions)
      {
        _userManager = userManager;
        _jwtFactory = jwtFactory;
        _serializerSettings = serializerSettings;
        _jwtOptions = jwtOptions;
      }

      protected async override Task<string> HandleCore(Query request)
      {
        var identity = await GetClaimsIdentity(request.Username, request.Password);

        // Serialize and return the response
        var response = new
        {
          id = identity.Claims.Single(c => c.Type == "id").Value,
          auth_token = await _jwtFactory.GenerateEncodedToken(request.Username, identity),
          expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
        };

        var json = JsonConvert.SerializeObject(response, _serializerSettings);

        return json;
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