using System.Text.Json.Serialization;

namespace Dinkle.Entities
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EntityType
    {
        Template,
        Report,
        Export
    }
}