using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NeverEndingJob.File
{
    public enum FolderPaths
    {
        Persistent,
        Cache
    }

    public class UnityFile
    {
        public static bool Exists(string fileName, FolderPaths path)
        {
            return System.IO.File.Exists(UnityFile.GetFolderPath(path) + "/" + fileName);
        }

        public static void RewriteFileContent(string fileName, object data, FolderPaths path)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = System.IO.File.Open(UnityFile.GetFolderPath(path) + "/" + fileName, FileMode.OpenOrCreate);

            bf.Serialize(file, data);
            file.Close();
        }

        public static object GetFileContent(string fileName, FolderPaths path)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = System.IO.File.Open(UnityFile.GetFolderPath(path) + "/" + fileName, FileMode.OpenOrCreate);

            var data = bf.Deserialize(file);
            file.Close();
            return data;
        }

        public static string GetFolderPath(FolderPaths path)
        {
            switch (path)
            {
                case FolderPaths.Persistent:
                    return UnityEngine.Application.persistentDataPath;
                case FolderPaths.Cache:
                    return UnityEngine.Application.temporaryCachePath;
                default:
                    return "";
            }
        }
    }
}