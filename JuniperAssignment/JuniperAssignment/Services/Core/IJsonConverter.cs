namespace JuniperAssignment.Services.Core
{
    public interface IJsonConverter
    {
        string Serialize(object value);
        T Deserialize<T>(string value);
    }
}