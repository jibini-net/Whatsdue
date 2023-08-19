namespace Generated;
using Microsoft.Extensions.DependencyInjection;
public static class AccountExtensions
{
    public static void AddAccountBackend<T>(this IServiceCollection services)
        where T : class, Account.IBackendService
    {
        services.AddScoped<Account.Repository>();
        services.AddScoped<Account.IBackendService, T>();
        services.AddScoped<Account.DbService>();
        services.AddScoped<Account.IService>((sp) => sp.GetRequiredService<Account.DbService>());
    }
    public static void AddAccountFrontend(this IServiceCollection services)
    {
        services.AddScoped<Account.ApiService>();
        services.AddScoped<Account.IService>((sp) => sp.GetRequiredService<Account.ApiService>());
    }
}
