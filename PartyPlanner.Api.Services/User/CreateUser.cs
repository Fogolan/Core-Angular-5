using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PartyPlanner.Api.Services.User.Services;
using PartyPlanner.Data.Entities;
using PartyPlanner.Data.Models;

namespace PartyPlanner.Api.Services.User
{
    public static class CreateUser
    {
        public class Command : IRequest<int>
        {
            public string Username { get; set; }

            public string Email { get; set; }

            public string Password { get; set; }

            public IUrlHelper UrlHelper { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command, int>
        {
            private readonly PartyPlannerContext partyPlannerContext;
            private readonly UserManager<UserIdentity> userManager;
            private readonly IEmailService emailService;

            public Handler(PartyPlannerContext partyPlannerContext, UserManager<UserIdentity> userManager, IEmailService emailService)
            {
                this.partyPlannerContext = partyPlannerContext;
                this.userManager = userManager;
                this.emailService = emailService;
            }

            protected override async Task<int> HandleCore(Command request)
            {
                var identity = await CreateUserIdentity(request);
                var user = new Data.Models.User
                {
                    Identity = identity
                };

                partyPlannerContext.Users.Add(user);

                await partyPlannerContext.SaveChangesAsync();

                return user.Id;
            }

            private async Task<UserIdentity> CreateUserIdentity(Command request)
            {
                var identity = new UserIdentity
                {
                    UserName = request.Username,
                    Email = request.Email
                };
                var result = await userManager.CreateAsync(identity, request.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(identity, "user");
                    await SendConfirmationMail(identity, request.UrlHelper);
                    return identity;
                }

                throw new ArgumentNullException();
            }

            private async Task SendConfirmationMail(UserIdentity userIdentity, IUrlHelper urlHelper)
            {
                var code = await userManager.GenerateEmailConfirmationTokenAsync(userIdentity);
                var callbackUrl = urlHelper.Action("ConfirmEmail", "User", new
                {
                    userId = userIdentity.Id,
                    code = code
                });
                await emailService.SendEmailAsync(userIdentity.Email, "Confirm your account",
                  $"{userIdentity.UserName}, подтвердите регистрацию, перейдя по ссылке: <a href='http://localhost:57083{callbackUrl}'>link</a>");
            }
        }
    }
}
