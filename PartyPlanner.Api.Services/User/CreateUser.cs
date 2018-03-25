using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PartyPlanner.Api.Services.User.Models;
using PartyPlanner.Data.Entities;

namespace PartyPlanner.Api.Services.User
{
    public static class CreateUser
    {
        public class Command: IRequest<Guid>
        {
            public UserDto UserDto { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command, Guid>
        {
            private readonly PartyPlannerContext partyPlannerContext;
            private readonly IPasswordHasher<UserDto> hasher;

            public Handler(PartyPlannerContext partyPlannerContext)
            {
                this.partyPlannerContext = partyPlannerContext;
                this.hasher = new PasswordHasher<UserDto>();
            }

            protected override async Task<Guid> HandleCore(Command request)
            {
                var userDto = request.UserDto;
                var hashedPassword = hasher.HashPassword(userDto, userDto.Password);

                var user = await partyPlannerContext.Users.AddAsync(new Data.Models.User
                {
                    Id = Guid.NewGuid(),
                    Username = userDto.Username,
                    PasswordHash = hashedPassword
                });

                await partyPlannerContext.SaveChangesAsync();

                return user.Entity.Id;
            }
        }
    }
}
