using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace RentHome.Client.Handler
{
    public class CustomAuthHandler : AuthorizationMessageHandler
    {
        public CustomAuthHandler(IAccessTokenProvider provider, NavigationManager navigation) : base(provider, navigation)
        {

            ConfigureHandler(authorizedUrls: new[] { "https://localhost:7181" });
        }

        
    }
}
