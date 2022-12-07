using System.Text.Json;
using System.Text.Json.Serialization;

namespace AspNetCoreDateAndTimeOnly.Json;

public static class JSONExtensions
{
    public static void AddDateAndTimeJsonConverters(this IList<JsonConverter> source)
    {
        source.Add(new DateOnlyJsonConverter());
        source.Add(new DateOnlyNullableJsonConverter());
        source.Add(new TimeOnlyJsonConverter());
        source.Add(new TimeOnlyNullableJsonConverter());
    }

    public static string ToJSON(this object request, Action<JsonSerializerOptions>? configure = null)
    {
        if (request.GetType().IsPrimitive
            || request.GetType() == typeof(string)
            || request.GetType() == typeof(DateTime)
            || request.GetType() == typeof(Guid)
            || request.GetType() == typeof(decimal))
        {
            return (string)(object)request.ToString()!;
        }
        JsonSerializerOptions defaultOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
#if NET6_0
        defaultOptions.Converters.AddDateAndTimeJsonConverters();
#endif
        configure?.Invoke(defaultOptions);
        return JsonSerializer.Serialize(request, defaultOptions);
    }

    public static bool TryToJSON(this object request, out string result)
    {
        try
        {
            result = request.ToJSON();
            return true;
        }
        catch (Exception) { }

        result = null!;
        return false;
    }

    public static T? GetJSON<T>(this string request, Action<JsonSerializerOptions>? configure = null)
    {
        if (string.IsNullOrEmpty(request))
        {
            return default;
        }

        if (typeof(T) == typeof(string))
        {
            return (T)(object)request;
        }
        if (typeof(T) == typeof(Guid))
        {
            return (T)(object)new Guid(request);
        }
        var settings = new JsonSerializerOptions(JsonSerializerDefaults.Web);
#if NET6_0
        settings.Converters.AddDateAndTimeJsonConverters();
#endif
        configure?.Invoke(settings);
        return JsonSerializer.Deserialize<T>(request, settings);
    }

    public static bool TryGetJSON<T>(this string request, out T result)
    {
        try
        {
            result = request.GetJSON<T>()!;
            return true;
        }
        catch (Exception) { }
        result = default!;
        return false;
    }

    public static IDictionary<string, object>? ToDictionary(this object source)
    {
        if (source.GetType() == typeof(string))
        {
            return ((string)source).GetJSON<IDictionary<string, object>>();
        }
        return source.ToJSON().GetJSON<IDictionary<string, object>>();
    }

    public static T? Clone<T>(this T source)
    {
        return source!.ToJSON().GetJSON<T>();
    }
}