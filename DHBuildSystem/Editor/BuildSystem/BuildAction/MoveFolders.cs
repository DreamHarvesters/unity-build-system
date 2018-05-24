using UnityEngine;
using UnityEditor;
using System.Collections;

namespace BuildSystem
{
    [CreateAssetMenu(menuName = "DH/Build/Actions/Move Folders Action", fileName = "Move Folders", order = 1)]
    public class MoveFolders : ScriptableObject, IBuildAction
    {
        [SerializeField]
        private string[] foldersToMove;

        [SerializeField]
        private string targetFolder;

        [SerializeField]
        private bool createTargetFolder;

        public string[] FoldersToMove 
        {
            get { return foldersToMove; }
            set { foldersToMove = value; }
        }

        public string TargetFolder
        {
            get { return targetFolder; }
            set { targetFolder = value; }
        }

        public bool CreateTargetFolder
        {
            get { return createTargetFolder; }
            set { createTargetFolder = value; }
        }

        public bool Execute()
        {
            if (!System.IO.Directory.Exists(targetFolder) && createTargetFolder)
            {
                string folderName, parentPath;
                Utils.GetFolderAndParentPath(targetFolder, out parentPath, out folderName);

                AssetDatabase.CreateFolder(parentPath, folderName);

                AssetDatabase.Refresh();
            }

            for (int i = 0; i < foldersToMove.Length; i++)
            {
                try
                {
                    FileUtil.MoveFileOrDirectory(foldersToMove[i], targetFolder);
                }
                catch(System.Exception e)
                {
                    Debug.Log(e.Message);
                    return false;
                }
            }

            return true;
        }
	}

}
