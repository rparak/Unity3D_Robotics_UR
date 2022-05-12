using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace Treeka
{
    [CreateAssetMenu(menuName = "Robot/Save File")]
    public class SaveFile : ScriptableObject
    {

        public new string name;
        public string savePath;
        public string directory;

        public Dictionary<string, string> data;


        public void Add<T>(string key, T value, bool safe = false)
        {
            if (data == null) ReadDisk();
            if (data.ContainsKey(key)) data[key] = JsonConvert.SerializeObject(value, Formatting.None);
            else data.Add(key, JsonConvert.SerializeObject(value, Formatting.None));
            if (safe) SaveDisk();
        }

        public T Get<T>(string key)
        {
            if (data.TryGetValue(key, out string value)) return JsonConvert.DeserializeObject<T>(value);
            else return default;
        }

        public bool Exists(string key)
        {
            if (data == null) ReadDisk();
            return data.ContainsKey(key);
        }


        public void SaveDisk()
        {
            ReadWriteDirectory.Write(directory);
            ReadWrite.Write(data, Application.persistentDataPath + savePath);
        }

        public void ReadDisk()
        {
            ReadWriteDirectory.Write(directory);
            if (ReadWrite.Exists(Application.persistentDataPath + savePath))
                data = ReadWrite.Read<Dictionary<string, string>>(Application.persistentDataPath + savePath);
            else data = new Dictionary<string, string>();
        }

        public void DeleteSaveOnDisk()
        {
            ReadWriteDirectory.Write(directory);
            ReadWrite.Delete(Application.persistentDataPath + savePath);
        }
    }
}


