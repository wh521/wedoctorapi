using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

namespace MJ.Core.Helper
{
    public class JsonHelper
    {
        public static string ObjectToJson(object obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch (System.Exception)
            {
                return null;
            }
        }


        public static dynamic JsonToDynamic(string json)
        {
            try
            {
                return JsonToObject<dynamic>(json);
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public static T JsonToObject<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (System.Exception)
            {
                return default(T);
            }
        }

        public static IDictionary JsonToDictionary(string json)
        {
            try
            {
                return JsonToObject<Dictionary<string, object>>(json);
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}
