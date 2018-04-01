using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PartyPlanner.Data.Entities;
using PartyPlanner.Data.Models;

namespace PartyPlanner.Api.Services.User
{
    public static class CreateUser
    {
        public class Command : IRequest<int>
        {
            public string Username { get; set; }

            public string Password { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command, int>
        {
            private readonly PartyPlannerContext partyPlannerContext;
            private readonly UserManager<UserIdentity> userManager;

            public Handler(PartyPlannerContext partyPlannerContext, UserManager<UserIdentity> userManager)
            {
                this.partyPlannerContext = partyPlannerContext;
                this.userManager = userManager;
            }

            protected override async Task<int> HandleCore(Command request)
            {
                var identity = new UserIdentity
                {
                    UserName = request.Username
                };

                await userManager.CreateAsync(identity, request.Password);


                var user = new Data.Models.User
                {
                    Identity = identity
                };

                partyPlannerContext.Users.Add(user);

                await partyPlannerContext.SaveChangesAsync();

                return user.Id;
            }
        }
    }
}
