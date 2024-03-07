using CommunityToolkit.Maui;
using RestaurantAppClient.Common.Interfaces;
using RestaurantAppClient.Pages;
using RestaurantAppClient.Services;
using RestaurantAppClient.ViewModels;
using PopupService = RestaurantAppClient.Services.PopupService;

namespace RestaurantAppClient.Configuration
{
    public static class DIConfiguration
    {
        public static IServiceCollection AddViews(this IServiceCollection services)
        {
            services.RegisterPage<MainPage>(nameof(MainPage));
            services.RegisterPage<MenuPage, MenuPageViewModel>(nameof(MenuPage));
            services.RegisterPage<LoginPage, LoginViewModel>(nameof(LoginPage));
            services.RegisterPage<RegistrationPage, RegistrationViewModel>(nameof(RegistrationPage));
            services.RegisterPage<MenuItemsPage, MenuItemsViewModel>(nameof(MenuItemsPage));
            services.RegisterPage<MenuItemDetailsPage, MenuItemDetailsViewModel>(nameof(MenuItemDetailsPage));
            services.RegisterPage<CartPage, CartPageViewModel>(nameof(CartPage));
            services.RegisterPage<EmailConfirmedPage, EmailConfirmedViewModel>(nameof(EmailConfirmedPage));
            services.RegisterPage<CheckoutPage, CheckoutViewModel>(nameof(CheckoutPage));
            services.RegisterPage<EmailConfirmationPage>(nameof(EmailConfirmationPage));
            services.RegisterPage<OrdersPage, OrdersViewModel>(nameof(OrdersPage));

            services.AddTransient<LoadingPage>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IAlertService, AlertService>();
            services.AddSingleton<IClientFactory, ClientFactory>();
            services.AddSingleton<ITokenService, TokenService>();
            services.AddTransient<IPopupService, PopupService>();
            services.AddSingleton<ICartService, CartService>();
            services.AddSingleton<ILocationService, LocationService>();

            services.AddSingleton(Geolocation.Default);

            return services;
        }

        private static void RegisterPage<TPage>(this IServiceCollection services, string route)
        {
            services.AddTransient(typeof(TPage));

            Routing.RegisterRoute(route, typeof(TPage));
        }

        private static void RegisterPage<TPage, TViewModel>(this IServiceCollection services, string route)
        {
            services.AddTransient(typeof(TPage)).AddTransient(typeof(TViewModel));

            Routing.RegisterRoute(route, typeof(TPage));
        }
    }
}
