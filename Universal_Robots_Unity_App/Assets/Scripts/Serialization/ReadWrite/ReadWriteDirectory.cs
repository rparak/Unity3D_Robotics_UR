using System.IO;

namespace Treeka
{
    public static class ReadWriteDirectory
    {

        public static void Write(string fullPath)
        {
            if (fullPath == string.Empty) return;
            if (Exists(fullPath)) return;
            Directory.CreateDirectory(fullPath);
        }

        public static string[] Read(string directory)
        {
            //UnityEngine.Debug.Log($"Directory: {directory}");
            return Directory.GetFiles(directory);
        }


        public static bool Delete(string fullPath)
        {
            if (Exists(fullPath)) return false;

            Directory.Delete(fullPath);
            return true;
        }

        public static bool Exists(string fullPath)
        {
            return Directory.Exists(fullPath);
        }
    }
}


