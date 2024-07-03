using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RentHome.Client;
using RentHome.Client.Handler;
using RentHome.Client.Services.AccountServices;
using RentHome.Client.Services.HouseServices;
using RentHome.Client.Services.ModeServices;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp=> new HttpClient { BaseAddress= new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddTransient<CustomAuthorizationHandler>();

builder.Services.AddHttpClient<IAccountService, AccountService>("AccountServiceClient", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<CustomAuthorizationHandler>();

builder.Services.AddScoped<AuthenticationStateProvider , CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IHouseService ,HouseService>();
builder.Services.AddScoped<IModeService,ModeService>();
builder.Services.AddScoped<IAccountService , AccountService>();




await builder.Build().RunAsync();
