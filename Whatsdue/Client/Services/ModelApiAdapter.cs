using Generated;
using System.Net.Http.Json;
using System.Text.Json;

namespace Whatsdue.Services;

public class ModelApiAdapter : IModelApiAdapter
{
    private readonly HttpClient http;
    public ModelApiAdapter(HttpClient http)
    {
        this.http = http;
    }

    public async Task ExecuteAsync(string path, object args)
    {
        var result = await http.PostAsJsonAsync(path, args);
        if (!result.IsSuccessStatusCode)
        {
            throw new Exception(await result.Content.ReadAsStringAsync());
        }
    }

    public async Task<T> ExecuteAsync<T>(string path, object args)
    {
        var result = await http.PostAsJsonAsync(path, args);
        if (!result.IsSuccessStatusCode)
        {
            throw new Exception(await result.Content.ReadAsStringAsync());
        }
        var content = await result.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });
    }
}