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

    internal record ProblemDetails(
        string type,
        string title,
        int status,
        string detail,
        string instance);

    private async Task PropagateErrorAsync(HttpContent response)
    {
        var content = await response.ReadAsStringAsync();
        var problem = JsonSerializer.Deserialize<ProblemDetails>(content, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });

        if (string.IsNullOrEmpty(problem.detail))
        {
            throw new Exception(problem.title);
        } else
        {
            throw new Exception(string.Format("{0}: {1}",
                problem.title,
                problem.detail));
        }
    }

    public async Task ExecuteAsync(string path, object args)
    {
        var result = await http.PostAsJsonAsync(path, args);
        if (!result.IsSuccessStatusCode)
        {
            await PropagateErrorAsync(result.Content);
        }
    }

    public async Task<T> ExecuteAsync<T>(string path, object args)
    {
        var result = await http.PostAsJsonAsync(path, args);
        if (!result.IsSuccessStatusCode)
        {
            await PropagateErrorAsync(result.Content);
        }
        var content = await result.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });
    }
}