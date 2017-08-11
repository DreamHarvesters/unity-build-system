using UnityEngine;
using System.Collections;

namespace BuildSystem
{
	public static class Utils
	{
		public static void GetFolderAndParentPath(string path, out string parentPath, out string folderName)
        {
            path = path.Replace("\\", "/");
            parentPath = path.Substring(0, path.LastIndexOf("/"));
            folderName = path.Substring(path.LastIndexOf("/") + 1);
        }
	}
}
