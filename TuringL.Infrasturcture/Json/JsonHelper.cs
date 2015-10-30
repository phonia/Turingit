using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Utilities;
using Newtonsoft.Json.Linq;

namespace TuringL.Infrasturcture.Json
{
    public class JsonHelper
    {
        public static string SerializeObject(object obj)
        {
            //JsonConvert.DeserializeObject
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {}
            return "false:" + "json转化失败";
        }

        public static T DeserializeObject<T>(string json)
        {
            try
            {
                T t = JsonConvert.DeserializeObject<T>(json);
                return t;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        public static string GetProperty(string json,string propertyName)
        {
            try
            {
                return (JsonConvert.DeserializeObject(json) as JObject).Property(propertyName).ToString();
            }
            catch (Exception ex)
            { }
            return string.Empty;
        }
    }
}
