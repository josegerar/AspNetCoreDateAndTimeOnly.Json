using System.Collections;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace AspNetCoreDateAndTimeOnly.Json;

#if NET7_0_OR_GREATER
public class DynamicContractResolver : DefaultJsonTypeInfoResolver
{
    private readonly string[]? _ignoreSerializeAtributes;
    private readonly bool _ignoreNullOrEmpty;
    private readonly string[]? _properties;

    public DynamicContractResolver(string[]? properties = null,
        string[]? ignoreSerializeAtributes = null, bool ignoreNullOrEmpty = false)
    {
        _ignoreSerializeAtributes = ignoreSerializeAtributes;
        _ignoreNullOrEmpty = ignoreNullOrEmpty;
        _properties = properties;
    }

    public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
    {
        JsonTypeInfo typeInfo = base.GetTypeInfo(type, options);

        if (_properties != null)
        {
            foreach (JsonPropertyInfo property in typeInfo.Properties)
            {
                if (!_properties.Contains(property.Name))
                {
                    property.ShouldSerialize = (obj, value) => false;
                }
            }
        }
        if (_ignoreSerializeAtributes != null)
        {
            foreach (JsonPropertyInfo property in typeInfo.Properties)
            {
                if (_ignoreSerializeAtributes.Contains(property.Name))
                {
                    property.ShouldSerialize = (obj, value) => false;
                }
            }
        }
        if (_ignoreNullOrEmpty)
        {
            foreach (JsonPropertyInfo property in typeInfo.Properties)
            {
                if (property.ShouldSerialize != null) continue;
                property.ShouldSerialize = (obj, value) =>
                {
                    if (value == null)
                    {
                        return false;
                    }
                    if (value is string)
                    {
                        return !string.IsNullOrWhiteSpace(value as string);
                    }
                    else if (value is IEnumerable objects)
                    {
                        return objects.GetEnumerator().MoveNext();
                    }
                    return true;
                };
            }
        }

        return typeInfo;
    }
}

#endif