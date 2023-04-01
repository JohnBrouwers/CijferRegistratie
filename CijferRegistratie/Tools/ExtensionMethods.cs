using System.Text.Json; //migrate away from Newtonsoft ;)

namespace CijferRegistratie.Tools
{
    public static class ExtensionMethods
    {
        public static T Get<T>(this ISession session, string key) where T: new()
        {
            string? sessionData =  session.GetString(key);
            if (!string.IsNullOrEmpty(sessionData))
            {
                T? nullableT = JsonSerializer.Deserialize<T>(sessionData);
                if (nullableT != null)
                {
                    return nullableT;
                }
            }
            return new T();   
        }
        public static void Set<T>(this ISession session, string key, T sessionData)
        {
            session.SetString(key, JsonSerializer.Serialize<T>(sessionData));
        }
    }
}
