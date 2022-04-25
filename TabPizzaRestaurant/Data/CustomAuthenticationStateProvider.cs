using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TabPizzaRestaurant.Models;

namespace TabPizzaRestaurant.Data
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ISessionStorageService _sessionStorageService;
        public CustomAuthenticationStateProvider(ISessionStorageService sessionStorage)
        {
            _sessionStorageService = sessionStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var name = await _sessionStorageService.GetItemAsync<string>("name");
            var email = await  _sessionStorageService.GetItemAsync<string>("email");
            var role = await _sessionStorageService.GetItemAsync<string>("role");
            var surname = await _sessionStorageService.GetItemAsync<string>("surname");
            ClaimsIdentity identity;

            if (email != null)
            {
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, name),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, role),
                    new Claim(ClaimTypes.Surname, surname)
                }, "apiauth_type");
            }
            else
            {
                identity = new ClaimsIdentity();
            }
            var user = new ClaimsPrincipal(identity);

            return await Task.FromResult(new AuthenticationState(user));
        }

        public void MarkUserAsAuthenticated(ClientFront user)
        {
            var identity = GetClaimsIdentity(user);

            var claimsPrincipal = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public void MarkUserAsLoggedOut()
        {
            _sessionStorageService.RemoveItemAsync("account_id");
            _sessionStorageService.RemoveItemAsync("email");
            _sessionStorageService.RemoveItemAsync("name");
            _sessionStorageService.RemoveItemAsync("surname");
            _sessionStorageService.RemoveItemAsync("login");
            _sessionStorageService.RemoveItemAsync("password");
            _sessionStorageService.RemoveItemAsync("phone");
            _sessionStorageService.RemoveItemAsync("address");
            _sessionStorageService.RemoveItemAsync("role");
            _sessionStorageService.RemoveItemAsync("points");
            _sessionStorageService.RemoveItemAsync("order_info");

            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        private ClaimsIdentity GetClaimsIdentity(ClientFront user)
        {
            var claimsIdentity = new ClaimsIdentity(new[]
{
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.EMail),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Surname, user.Surname)
            }, "apiauth_type");

            return claimsIdentity;
        }
    }
}
