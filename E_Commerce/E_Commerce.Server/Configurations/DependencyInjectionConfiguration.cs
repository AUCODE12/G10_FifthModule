using E_Commerce.Bll.Services;
using E_Commerce.Dal;
using E_Commerce.Repository.Services;

namespace E_Commerce.Server.Configurations;

public static class DependencyInjectionConfiguration
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ICardRepository, CardRepository>();

        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<ICartService, CartService>();

        services.AddScoped<ICartProductRepository, CartProductRepository>();

        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<IOrderRepository, OrderRepository>();

        services.AddScoped<ICustomerRepository, CustomerRepository>();

        services.AddScoped<IPaymentRepository, PaymentRepository>();
    }
}
