// <copyright file="IsolatedStorageHelper.cs" company="cematinlà.com">
//     Ce matin là. All rights reserved.
// </copyright>
// <author>Léo Davesne</author>

namespace TriathlonChecklist.Helper
{
    #region Usings

    using System.IO;
    using System.IO.IsolatedStorage;
    using System.Runtime.Serialization.Json;
    using System.Text;

    #endregion Usings

    /// <summary>
    /// IsolatedStorageHelper class
    /// </summary>
    public static class IsolatedStorageHelper
    {
        /// <summary>
        /// Get T object
        /// </summary>
        /// <typeparam name="T">The T parameter</typeparam>
        /// <param name="key">The key</param>
        /// <returns>One T</returns>
        public static T GetObject<T>(string key)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(key))
            {
                string serializedObject = IsolatedStorageSettings.ApplicationSettings[key].ToString();
                return Deserialize<T>(serializedObject);
            }

            return default(T);
        }

        /// <summary>
        /// Save object T
        /// </summary>
        /// <typeparam name="T">The T</typeparam>
        /// <param name="key">The key</param>
        /// <param name="objectToSave">The object to save</param>
        public static void SaveObject<T>(string key, T objectToSave)
        {
            string serializedObject = Serialize(objectToSave);
            IsolatedStorageSettings.ApplicationSettings[key] = serializedObject;
        }

        /// <summary>
        /// Delete an object
        /// </summary>
        /// <param name="key">The key</param>
        public static void DeleteObject(string key)
        {
            IsolatedStorageSettings.ApplicationSettings.Remove(key);
        }

        /// <summary>
        /// Serialize an object
        /// </summary>
        /// <param name="objectToSerialize">The object to serialize</param>
        /// <returns>A string</returns>
        private static string Serialize(object objectToSerialize)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(objectToSerialize.GetType());
                serializer.WriteObject(ms, objectToSerialize);
                ms.Position = 0;

                using (StreamReader reader = new StreamReader(ms))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Deserialize an object
        /// </summary>
        /// <typeparam name="T">The T</typeparam>
        /// <param name="jsonString">The JSON string</param>
        /// <returns>A T</returns>
        private static T Deserialize<T>(string jsonString)
        {
            using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonString)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(ms);
            }
        }
    }
}
