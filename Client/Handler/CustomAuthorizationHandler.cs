using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace RentHome.Client.Handler
{
    public class CustomAuthorizationHandler : DelegatingHandler
    {
        private readonly ILocalStorageService localStorage;

        public CustomAuthorizationHandler(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await localStorage.GetItemAsync<string>("token");

            if (token !=null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer ", token);
            }

            return await base.SendAsync(request, cancellationToken);    
        }
    }
}
