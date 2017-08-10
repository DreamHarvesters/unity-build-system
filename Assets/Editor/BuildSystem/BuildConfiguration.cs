using UnityEngine;
using System.Collections;
using UnityEditor;

namespace BuildSystem
{
    [CreateAssetMenu(fileName = "Build Configuration", menuName = "Build/Create Build Configuration", order = 1)]
    public class BuildConfiguration : ScriptableObject
    {
        [SerializeField]
        private string targetPath;

        //Allows setting scenes through the editor. Does not used in scripting, just for easy referencing.
        [SerializeField]
        private SceneAsset[] scenes;

        [SerializeField]
        private UnityEditor.BuildTarget buildTarget;

        [SerializeField]
        private UnityEditor.BuildOptions buildOptions;

        private EditorBuildSettingsScene[] buildScenes;
        public EditorBuildSettingsScene[] BuildSceneList 
        { 
            get 
            {
                buildScenes = new EditorBuildSettingsScene[scenes.Length];

                for (int i = 0; i < scenes.Length; i++)
                {
                    buildScenes[i] = new EditorBuildSettingsScene(AssetDatabase.GetAssetPath(scenes[i]), true);
                }

                return buildScenes;
            } 

            set 
            {
                scenes = new SceneAsset[value.Length];

                for (int i = 0; i < value.Length; i++)
                {
                    scenes[i] = AssetDatabase.LoadAssetAtPath<SceneAsset>(value[i].path);
                }
            }
        }

        public string TargetPath { get { return targetPath; } set { targetPath = value; }}
        public BuildTarget BuildTarget { get { return buildTarget; } set { buildTarget = value; }}
        public BuildOptions BuildOptions { get { return buildOptions; } set { buildOptions = value; }}
    }
}
