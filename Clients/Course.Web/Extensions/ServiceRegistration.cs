using Course.Shared.Services.Abstractions;
using Course.Shared.Services.Concretes;
using Course.Web.Handler;
using Course.Web.Helpers;
using Course.Web.Models;
using Course.Web.Services.Abstractions;
using Course.Web.Services.Concretes;
using Course.Web.Validators.CourseValidators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Course.Web.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection service, IConfiguration configuration)
        {

            service.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            service.AddValidatorsFromAssemblyContaining(typeof(CreateCourseVMValidator));



            ServiceApiSettings serviceApiSettings = configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

            service.AddHttpClient<ICatalogService, CatalogService>(opts =>
            {
                opts.BaseAddress = new Uri(string.Concat(serviceApiSettings.GatewayBaseUri, serviceApiSettings.Catalog.Path));
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();


            service.AddHttpClient<IPhotoStockService, PhotoStockService>(opts =>
            {
                opts.BaseAddress = new Uri(string.Concat(serviceApiSettings.GatewayBaseUri, serviceApiSettings.PhotoStock.Path));
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            service.AddHttpClient<IBasketService, BasketService>(opts =>
            {
                opts.BaseAddress = new Uri(string.Concat(serviceApiSettings.GatewayBaseUri, serviceApiSettings.Basket.Path));
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            service.Configure<ServiceApiSettings>(configuration.GetSection("ServiceApiSettings"));
            service.Configure<ClientSettings>(configuration.GetSection("ClientSettings"));

            service.AddHttpContextAccessor();
            service.AddHttpClient<IIdentityService, IdentityService>();
            service.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();
            service.AddScoped<ResourceOwnerPasswordTokenHandler>();
            service.AddScoped<ClientCredentialTokenHandler>();
            service.AddScoped<ISharedIdentityService, SharedIdentityService>();
            service.AddSingleton<PhotoHelper>();
            service.AddAccessTokenManagement();

            service.AddHttpClient<IUserService, UserService>(opts =>
            {
                opts.BaseAddress = new Uri(serviceApiSettings.IdentityBaseUri);
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            service.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opts =>
            {
                opts.LoginPath = "/Auth/SignIn";
                opts.ExpireTimeSpan = TimeSpan.FromDays(60);
                opts.SlidingExpiration = true;
                opts.Cookie.Name = "webcookie";
            });



        }
    }
}
