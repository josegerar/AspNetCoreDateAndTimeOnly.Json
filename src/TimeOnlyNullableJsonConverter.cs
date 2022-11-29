using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace AspNetCoreDateAndTimeOnly.Json;

public class TimeOnlyNullableJsonConverter : JsonConverter<TimeOnly?>
{
    public override TimeOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (value == null)
        {
            return null;
        }
        try
        {
            return TimeOnly.ParseExact(value, Constants.Format.TimeOnly, CultureInfo.InvariantCulture);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value?.ToString(Constants.Format.TimeOnly, CultureInfo.InvariantCulture));
    }
}