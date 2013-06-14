using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Eleooo.Common
{
    public class JavaScriptObjectConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(string));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            reader.Read( );
            return reader.Value;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string v;
            if (value != null && !string.IsNullOrEmpty((v = value.ToString( ))))
            {

                if (System.Text.RegularExpressions.Regex.IsMatch(v, @"\[*\]"))
                {
                    writer.WriteRawValue(v);
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(v, @"\{*\}"))
                {
                    writer.WriteRawValue(v);
                }
                else
                {
                    writer.WriteStartConstructor(value.ToString( ));
                    writer.WriteEndConstructor( );
                }
            }
            else
                writer.WriteNull( );
        }
    }
}
