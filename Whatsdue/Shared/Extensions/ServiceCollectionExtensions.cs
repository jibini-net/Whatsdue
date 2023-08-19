using Generated;
using Microsoft.Extensions.DependencyInjection;
using Whatsdue.Services;

namespace Whatsdue.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddWhatsdueBackend(this IServiceCollection services)
    {
        services.AddAccountBackend<AccountService>();
    }

    public static void AddWhatsdueFrontend(this IServiceCollection services)
    {
        services.AddAccountFrontend();
    }
}
