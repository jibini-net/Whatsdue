using Generated;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Whatsdue.Client;
using Whatsdue.Extensions;
using Whatsdue.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped((sp) => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress.TrimEnd('/') + "/api/v1/")
});
builder.Services.AddScoped<IModelApiAdapter, ModelApiAdapter>();
builder.Services.AddWhatsdueFrontend();

await builder.Build().RunAsync();
