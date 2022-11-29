using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AspNetCoreDateAndTimeOnly.Json;

public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateOnly.ParseExact(reader.GetString()!, Constants.Format.DateOnly, CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Constants.Format.DateOnly, CultureInfo.InvariantCulture));
    }
}