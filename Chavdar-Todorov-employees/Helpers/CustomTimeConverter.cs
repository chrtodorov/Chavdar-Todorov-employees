using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Chavdar_Todorov_employees.Helpers;

public class CustomDateTimeConverter : JsonConverter<DateTime>
{
    private readonly string[] _dateFormats = { "yyyy-MM-dd", "dd-MM-yyyy" };

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
            throw new JsonException("Unable to parse date.");
        
        var dateString = reader.GetString();
        if (DateTime.TryParseExact(dateString, _dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
        {
            return result;
        }

        throw new JsonException("Unable to parse date.");
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("yyyy-MM-dd")); // Serialize DateTime as "yyyy-MM-dd"
    }
}