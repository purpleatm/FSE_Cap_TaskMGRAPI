using System;
using Newtonsoft.Json;

namespace TaskManager.FSD.Core.ApiHelper.Resolver
{
    public class JsonConversionHelper
    {
        static readonly JsonSerializerSettings defaultSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            TypeNameHandling = TypeNameHandling.All,
            MissingMemberHandling = MissingMemberHandling.Error,
            TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Full
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
