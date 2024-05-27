using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NpgsqlTypes;

namespace CourseProj.Converters;

public class NpgsqlTsVectorConverter : JsonConverter<NpgsqlTsVector>
{
    public override void WriteJson(JsonWriter writer, NpgsqlTsVector value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString());
    }

    public override NpgsqlTsVector ReadJson(JsonReader reader, Type objectType, NpgsqlTsVector existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.StartObject)
        {
            JObject jsonObject = JObject.Load(reader);
            var values = jsonObject["$values"];

            // Проверяем, что это массив
            if (values is JArray valuesArray)
            {
                // Получаем первый элемент массива
                var firstValue = valuesArray.FirstOrDefault();

                // Проверяем, что первый элемент не пустой
                if (firstValue != null)
                {
                    var text = firstValue["text"].ToString();
                    var count = firstValue["count"].Value<int>();
                    return NpgsqlTsVector.Parse(text + count);
                }
            }

            
            return NpgsqlTsVector.Parse("text");
        }
        else if (reader.TokenType == JsonToken.String)
        {
            string valueString = (string)reader.Value;
            return NpgsqlTsVector.Parse(valueString);
        }

        throw new JsonSerializationException($"Unexpected token type '{reader.TokenType}' when deserializing NpgsqlTsVector.");
    }

    
}