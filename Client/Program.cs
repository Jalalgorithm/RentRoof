using Blazored.LocalStorage;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RentHome.Client;
using RentHome.Client.Handler;
using RentHome.Client.Services.AccountServices;
using RentHome.Client.Services.AgentServices;
using RentHome.Client.Services.AppointmentServices;
using RentHome.Client.Services.HouseServices;
using RentHome.Client.Services.ModeServices;



var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp=> new HttpClient { BaseAddress= new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<CustomAuthorizationHandler>();

builder.Services.AddHttpClient<AccountService>( client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<CustomAuthorizationHandler>();

builder.Services.AddSweetAlert2();

builder.Services.AddScoped<AuthenticationStateProvider , CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IHouseService ,HouseService>();
builder.Services.AddScoped<IModeService,ModeService>();
builder.Services.AddScoped<IAccountService , AccountService>();
builder.Services.AddScoped<IAgentService, AgentService>();
builder.Services.AddScoped<IAppointmentService , AppointmentService>();
builder.Services.AddSingleton<BookingService>();


await builder.Build().RunAsync();
