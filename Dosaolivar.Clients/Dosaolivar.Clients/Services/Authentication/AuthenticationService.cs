using System;
using System.Threading.Tasks;
using SmartHotel.Clients.Core.Extensions;
using SmartHotel.Clients.Core.Services.Request;
using System.Collections.Generic;


namespace SmartHotel.Clients.Core.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        readonly IBrowserCookiesService browserCookiesService;
        readonly IAvatarUrlProvider avatarProvider;
        readonly IRequestService requestService;
        //private User loginAnswer;
        public AuthenticationService(
        IBrowserCookiesService browserCookiesService,
            IAvatarUrlProvider avatarProvider)
        {
            this.requestService = requestService;
            this.browserCookiesService = browserCookiesService;
            this.avatarProvider = avatarProvider;
        }

        public bool IsAuthenticated => AppSettings.User != null;

        public Models.User AuthenticatedUser => AppSettings.User;


        public Task<bool> LoginAsync(string email, string password)
        {
            var user = new Models.User
            {
                username = email,
                password = password,
                token = "",
               /* firstName = email,
                LastName = string.Empty,
                AvatarUrl = avatarProvider.GetAvatarUrl(email),
                Token = email,
                LoggedInWithMicrosoftAccount = false*/
            };

            Console.WriteLine("LoginAsync");

            AppSettings.User = user;

            var builder = new UriBuilder(AppSettings.AuthURL);
            //builder.AppendToPath("Bookings");

            var uri = builder.ToString();
            Console.WriteLine("URL A ENVIAR " + uri);
            //loginAnswer = LoginRequest.LoginAPI("xxxx", "xxx", AppSettings.AuthURL);
            requestService.PostAsync<Models.User>(uri, user);
            //requestService.PostLogin<Models.User>("pepe","password",AppSettings.AuthURL);
            return Task.FromResult(true);
        }

      
        public async Task<bool> UserIsAuthenticatedAndValidAsync()
        {
            Console.WriteLine("UserIsAuthenticatedAndValidAsync");
            if (!IsAuthenticated)
            {
                return false;
            }
           
            else
            {
                var refreshSucceded = false;

                try
                {
                  /*  var tokenCache = App.AuthenticationClient.UserTokenCache;
                    var ar = await App.AuthenticationClient.AcquireTokenSilentAsync(
                        new string[] { AppSettings.B2cClientId },
                        AuthenticatedUser.Id,
                        $"{AppSettings.B2cAuthority}{AppSettings.B2cTenant}",
                        AppSettings.B2cPolicy,
                        true);
                    SaveAuthenticationResult(ar); */

                    refreshSucceded = true;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error with MSAL refresh attempt: {ex}");
                }

                return refreshSucceded;
            }
        }

        public async Task LogoutAsync()
        {
            AppSettings.RemoveUserData();
            await browserCookiesService.ClearCookiesAsync();
        }

     /*   void SaveAuthenticationResult(AuthenticationResult result)
        {
            var user = AuthenticationResultHelper.GetUserFromResult(result);
            //user.AvatarUrl = avatarProvider.GetAvatarUrl(user.username);
            AppSettings.User = user;
        }*/

        // Method para hacer login
        public Task<Models.User> PostAuthAsync(Models.User userToReturn, string token = "")
        {
            var builder = new UriBuilder(AppSettings.AuthURL);
            //builder.AppendToPath("Bookings");

            var uri = builder.ToString();

            return requestService.PostAsync<Models.User>(uri,userToReturn);
        }
    }
}
