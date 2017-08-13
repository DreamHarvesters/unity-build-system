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

        [SerializeField]
        private string scriptingSymbols;

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
        public string ScriptingSymbols { get { return scriptingSymbols; } set { scriptingSymbols = value; }}
        public BuildTargetGroup BuildTargetGroup
        {
            get
            {
                switch(buildTarget)
                {
                    case BuildTarget.StandaloneLinux:
                    case BuildTarget.StandaloneWindows:
                    case BuildTarget.StandaloneLinux64:
                    case BuildTarget.StandaloneOSXIntel:
                    case BuildTarget.StandaloneWindows64:
                    case BuildTarget.StandaloneOSXIntel64:
                    case BuildTarget.StandaloneOSXUniversal:
                    case BuildTarget.StandaloneLinuxUniversal:
                        return BuildTargetGroup.Standalone;
                    case BuildTarget.Android:
                        return BuildTargetGroup.Android;
                    case BuildTarget.iOS:
                        return BuildTargetGroup.iOS;
                    case BuildTarget.WebGL:
                        return BuildTargetGroup.WebGL;
                    default:
                        return BuildTargetGroup.Unknown;
                }
            }
        }
    }
}
