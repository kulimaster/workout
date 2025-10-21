using Serilog.Events;
using Serilog.Formatting;
using System.Text;
using System.Text.Json;

namespace Workout.Infrastructure.Logging
{
    public class LogJsonFormatter : ITextFormatter
    {
        // Seznam klíčů, které chceme explicitně logovat
        private static readonly string[] KeysToInclude = new[]
        {
            "TraceId", "SpanId", "RequestId", "ActionId", "UserId", "RequestPath", "QueryParams"
        };

        public void Format(LogEvent logEvent, TextWriter output)
        {
            if (logEvent == null) throw new ArgumentNullException(nameof(logEvent));
            if (output == null) throw new ArgumentNullException(nameof(output));

            // Vytvoříme dictionary s hlavními hodnotami
            var dict = new Dictionary<string, object>
            {
                ["Timestamp"] = logEvent.Timestamp.ToString("o"), // ISO 8601
                ["Level"] = logEvent.Level.ToString(),
                ["Message"] = logEvent.RenderMessage(),
                ["SourceContext"] = logEvent.Properties.TryGetValue("SourceContext", out var sc) ? sc.ToString().Trim('"') : "<none>"
            };

            // Přidáme jen vybrané vlastnosti
            /*foreach (var key in KeysToInclude)
            {
                if (logEvent.Properties.TryGetValue(key, out var value))
                    dict[key] = value.ToString().Trim('"');
            }*/

            foreach (var key in logEvent.Properties.Keys)
            {
                logEvent.Properties.TryGetValue(key, out var value);
                if (value != null)
                    dict[key] = value.ToString().Trim('"');
            }
            
            

            // Přidáme exception, pokud existuje
            if (logEvent.Exception != null)
                dict["Exception"] = logEvent.Exception.ToString();

            // Zapíšeme JSON do TextWriteru pomocí System.Text.Json
            using var stream = new MemoryStream();
            using (var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = false }))
            {
                writer.WriteStartObject();
                foreach (var kv in dict)
                {
                    writer.WritePropertyName(kv.Key);
                    if (kv.Value is string s)
                        writer.WriteStringValue(s);
                    else
                        writer.WriteStringValue(kv.Value?.ToString() ?? string.Empty);
                }
                writer.WriteEndObject();
                writer.Flush();
            }

            // Převod do string a zápis do výstupu
            output.Write(Encoding.UTF8.GetString(stream.ToArray()));
            output.WriteLine();
        }
    }
}
