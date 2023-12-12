using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Network
{
    public static class JsonHelper
    {
        public static T[] getJsonArray<T>(string json)
        {
            T[] res;
            try
            {
                res = JsonConvert.DeserializeObject<List<T>>(json).ToArray();
            }
            catch (Exception e)
            {
                Debug.Log("Couldn't parse the response to the class: \n" + e.Message);
                res = new T[] { };
            }

            return res;
        }

        public static T[] getJsonNested<T>(string json)
        {
            string newJson = "{ \"nested\": " + json + "}";
            Wrapper<T> wrapper;
            try
            {
                wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
            } catch (Exception e)
            {
                Debug.Log("Couldn't parse the response to the class: \n" + e.Message);
                return null;
            }
            return wrapper.array;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>
        /// T  - the deserialized object
        /// bool - true if the object was deserialized successfully, false if not
        /// </returns>
        public static (T, bool) TryDeserializeObject<T>(string json)
        {
            T res;
            try
            {
                res = JsonConvert.DeserializeObject<T>(json);
            } catch (Exception e)
            {
                Debug.Log("Couldn't parse the response to the class: \n" + e.Message);
                return (default, false);
            }

            return (res, true);
        }

        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] array;
            public T nested;
        }
    }
}