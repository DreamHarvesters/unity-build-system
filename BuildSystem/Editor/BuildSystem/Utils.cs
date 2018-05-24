using UnityEngine;
using System.Collections;

namespace BuildSystem
{
	public static class Utils
	{
        public static void GetFolderAndParentPath(string path, out string parentPath, out string folderName)
        {
            path = path.Replace("\\", "/");
            path = path.Replace(Application.dataPath, string.Empty);
            parentPath = path.Substring(0, path.LastIndexOf("/"));
            folderName = path.Substring(path.LastIndexOf("/") + 1);
        }

        public static string GetFileName(string path, bool removeExtension = true)
        {
            path = path.Replace("\\", "/");

            if (!path.Contains("."))
                throw new System.Exception("The provided path does not point to a file!!\n Provided path: " + path);

            string fileName = path.Substring(path.LastIndexOf("/") + 1);

            if (removeExtension)
                fileName = fileName.Remove(fileName.LastIndexOf("."));

            return fileName;
        }
	}
}
