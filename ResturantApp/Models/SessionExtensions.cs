using System.Text.Json;
using Microsoft.IdentityModel.Tokens;

namespace ResturantApp.Models
{
    public static class SessionExtensions
    {
        //save object to session
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.Set(key, JsonSerializer.Serialize(value));
        }

        // read object to session

        public static T Get<T>(this ISession session, string key)
        {
            var json = session.GetString(key);

            if (json.IsNullOrEmpty())
            {
                return default(T);
            }
            else
            {
                return JsonSerializer.Deserialize<T>(json);
            }
        }
    }
}

