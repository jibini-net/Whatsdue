using Generated;
using Whatsdue.Extensions;
using Whatsdue.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IModelDbAdapter, ModelDbAdapter>();
builder.Services.AddScoped<IModelDbWrapper, ModelDbWrapper>();
builder.Services.AddWhatsdueBackend();

builder.Services.AddSwaggerGen((config) =>
{
    if (builder.Environment.IsDevelopment())
    {
        config.AddServer(new()
        {
            Description = "Local",
            Url = "https://localhost:7201"
        });
        config.AddServer(new()
        {
            Description = "Staging",
            Url = "https://whatsdue-dev.jibini.net"
        });
    } else
    {
        config.AddServer(new()
        {
            Description = "Production",
            Url = "https://whatsdue.today"
        });
    }

    config.SwaggerDoc("v1",
        new()
        {
            Title = "Whatsdue API",
            Version = "v1"
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
} else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");
app.UseSwagger((config) =>
{
    config.RouteTemplate = "api/docs/{documentName}/swagger.json";
});
app.UseSwaggerUI((config) =>
{
    config.RoutePrefix = "api/docs";
    config.SwaggerEndpoint("/api/docs/v1/swagger.json", "Whatsdue API v1");
});

app.Run();
