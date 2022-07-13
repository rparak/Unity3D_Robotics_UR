using Newtonsoft.Json;
using System.IO;
using System;
using UnityEngine;

namespace Treeka
{
    public static class ReadWrite
    {


        public static void Write <T> (T data, string filePath)
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            Write(json, filePath);
        }

        public static void Write(string data, string filePath)
        {
            filePath = Application.persistentDataPath + "/" + filePath;
            StreamWriter writer = new StreamWriter(filePath, append: false);
            writer.Write(data);
            writer.Close();
        }

        public static void WriteExact(string data, string filePath)
        {
            StreamWriter writer = new StreamWriter(filePath, append: false);
            writer.Write(data);
            writer.Close();
        }

        public static T Read <T>(string filePath)
        {
            string json = Read(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string Read(string filePath)
        {
            filePath = Application.persistentDataPath + "/" + filePath;
            StreamReader reader = new StreamReader(filePath);
            string json = reader.ReadToEnd();
            reader.Close();
            return json;
        }

        public static string ReadExact(string filePath)
        {
            StreamReader reader = new StreamReader(filePath);
            string json = reader.ReadToEnd();
            reader.Close();
            return json;
        }




        public static bool Delete(string filePath)
        {
            if (!Exists(filePath)) return false;

            File.Delete(filePath);
            return true;
        }

        public static bool Exists(string filePath)
        {
            filePath = Application.persistentDataPath + "/" + filePath;
            Debug.Log(filePath);
            return File.Exists(filePath);
        }
    }
}


