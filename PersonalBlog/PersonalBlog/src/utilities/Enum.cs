using System.Text.Json.Serialization;

namespace PersonalBlog.src.utilities
{

    [JsonConverter(typeof(JsonStringEnumConverter))]

    public enum TypeUser
    {
        Commom,
        Administrator
    }
}
