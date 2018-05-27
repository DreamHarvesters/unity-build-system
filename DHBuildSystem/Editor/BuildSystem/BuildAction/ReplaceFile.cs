using System;
using System.IO;
using UnityEngine;

namespace DH.BuildSystem
{
    [CreateAssetMenu(menuName = "DH/Build/Actions/Replace File Action", fileName = "Replace File", order = 1)]
    public class ReplaceFile : ScriptableObject, IBuildAction, IRevertableBuildAction
    {
        [Tooltip("File to replace")]
        [SerializeField] private string destinationFile;
        
        [Tooltip("File that wanted to be replaced")]
        [SerializeField] private string sourceFile;

        [SerializeField] private bool willBeReverted;
        
        public bool WillBeReverted
        {
            get { return willBeReverted; }
            set { willBeReverted = value; }
        }
        
        public bool Revert()
        {
            try
            {
                File.Copy(destinationFile + "_temp", destinationFile, true);
                return true;
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                return false;
            }
        }

        public bool Execute()
        {
            try
            {
                if(WillBeReverted)
                    CacheDestinationFileForRevert();
                
                File.Copy(sourceFile, destinationFile, true);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                return false;
            }
        }

        void CacheDestinationFileForRevert()
        {
            File.Copy(destinationFile, destinationFile + "_temp");
        }
    }
}