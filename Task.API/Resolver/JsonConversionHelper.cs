using System;
using Newtonsoft.Json;

namespace Task.API.Resolver
{
    public class JsonConversionHelper
    {
        static readonly JsonSerializerSettings defaultSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            TypeNameHandling = TypeNameHandling.All,
            MissingMemberHandling = MissingMemberHandling.Error,
            TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full
        };

        public static bool TryParse<T>(object input, out T output)
        {
            try
            {
                output = JsonConvert.DeserializeObject<T>(input.ToString(), defaultSettings);
                return true;
            }
            catch (Exception)
            {
                output = default(T);
                return false;
            }
        }
    }
}
