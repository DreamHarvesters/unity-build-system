using UnityEngine;
using System.Collections;

namespace BuildSystem
{
    [CreateAssetMenu(menuName = "Build/Actions/Move Folders Action", fileName = "Move Folders", order = 1)]
    public class MoveFolders : ScriptableObject, IBuildAction
    {
        [SerializeField]
        private string[] foldersToMove;

        [SerializeField]
        private string targetFolder;

        public bool Execute()
        {
            return true;
        }
	}

}
