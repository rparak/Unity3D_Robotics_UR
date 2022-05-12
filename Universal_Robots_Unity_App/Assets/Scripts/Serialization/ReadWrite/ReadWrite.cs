using Newtonsoft.Json;
using System.IO;
using System;

namespace Treeka
{
    public static class ReadWrite
    {


        public static void Write <T> (T data, string filePath)
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);

            StreamWriter writer = new StreamWriter(filePath, append: false);
            writer.Write(json);
            writer.Close();
        }

        public static T Read <T>(string filePath)
        {
            StreamReader reader = new StreamReader(filePath);
            string json = reader.ReadToEnd();
            reader.Close();

            return JsonConvert.DeserializeObject<T>(json);
        }




        public static bool Delete(string filePath)
        {
            if (!Exists(filePath)) return false;

            File.Delete(filePath);
            return true;
        }

        public static bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}


