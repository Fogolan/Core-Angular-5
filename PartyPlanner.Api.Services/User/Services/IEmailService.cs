using System.Threading.Tasks;

namespace PartyPlanner.Api.Services.User.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}