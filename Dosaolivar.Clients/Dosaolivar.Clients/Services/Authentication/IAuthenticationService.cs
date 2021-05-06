using SmartHotel.Clients.Core.Models;
using System.Threading.Tasks;


namespace SmartHotel.Clients.Core.Services.Authentication
{
    public interface IAuthenticationService
    {
        bool IsAuthenticated { get; }

        User AuthenticatedUser { get; }

        Task<bool> LoginAsync(string email, string password);

        Task<bool> UserIsAuthenticatedAndValidAsync();

        Task LogoutAsync();
    }
}
