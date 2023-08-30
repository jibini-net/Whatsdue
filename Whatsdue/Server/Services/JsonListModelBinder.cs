using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json;

namespace Whatsdue.Server.Services;

public class JsonListModelBinder : IModelBinder
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        await Task.CompletedTask;
        var json = bindingContext.ValueProvider.GetValue(bindingContext.FieldName).FirstValue;
        var parsed = JsonSerializer.Deserialize(json, bindingContext.ModelType, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });
        bindingContext.Result = ModelBindingResult.Success(parsed);
    }
}

//TODO Enable JSON parsing
[ModelBinder(BinderType = typeof(JsonListModelBinder))]
public class JsonList<T> : List<T>
{
}