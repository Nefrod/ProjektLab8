using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BibliotekaChemiczna
{
    public class ISubstancjaConverter : JsonConverter<ISubstancja>
    {
        public override ISubstancja? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Zdeserializuj do konkretnej klasy Substancja
            return JsonSerializer.Deserialize<Substancja>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, ISubstancja value, JsonSerializerOptions options)
        {
            // Serializacja konkretnej klasy Substancja
            JsonSerializer.Serialize(writer, (Substancja)value, options);
        }
    }
}
