using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AspNetCoreDateAndTimeOnly.Json;

public class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
{
    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return TimeOnly.ParseExact(reader.GetString()!, Constants.Format.TimeOnly, CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Constants.Format.TimeOnly, CultureInfo.InvariantCulture));
    }
}