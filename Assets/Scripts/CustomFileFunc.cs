using System;
using System.IO;
using NUnit.Compatibility;
using UnityEngine;

namespace CustomFileFunc
{
    public class CustomFuncs
    {
        public static string GetFileByNameJson(string fileName, string addedFolderString = "")
        {
            return Application.persistentDataPath + Path.AltDirectorySeparatorChar + addedFolderString + Path.AltDirectorySeparatorChar + fileName + ".json";
        }
        public static string GetFileByNameFolder(string fileName, string addedFolderString = "")
        {
            return Application.persistentDataPath + Path.AltDirectorySeparatorChar + addedFolderString + Path.AltDirectorySeparatorChar + fileName;
        }
        public static bool CreateFolder(string folderName)
        {

            if (File.Exists(GetFileByNameFolder(folderName)))
            {
                return false;
            }
            File.Create(GetFileByNameFolder(folderName));
            return true;
        }
        public static bool CreateJsonFile(string folderName)
        {

            if (File.Exists(GetFileByNameJson(folderName)))
            {
                return false;
            }
            File.Create(GetFileByNameJson(folderName));
            return true;
        }
    }
}