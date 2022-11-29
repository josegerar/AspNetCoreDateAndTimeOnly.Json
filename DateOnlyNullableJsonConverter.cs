using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace AspNetCoreDateAndTimeOnly.Json;

public class DateOnlyNullableJsonConverter : JsonConverter<DateOnly?>
{
    public override DateOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (value == null)
        {
            return null;
        }
        try
        {
            return DateOnly.ParseExact(value, Constants.Format.DateOnly, CultureInfo.InvariantCulture);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public override void Write(Utf8JsonWriter writer, DateOnly? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value?.ToString(Constants.Format.DateOnly, CultureInfo.InvariantCulture));
    }
}