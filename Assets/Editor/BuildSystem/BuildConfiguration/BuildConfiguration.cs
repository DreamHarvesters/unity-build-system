using System;
using UnityEngine;
using System.Collections;
using UnityEditor;

namespace BuildSystem
{
    [CreateAssetMenu(fileName = "Build Configuration", menuName = "Build/Create Build Configuration", order = 1)]
    public class BuildConfiguration : ScriptableObject, IBuildConfiguration
    {
        [SerializeField]
        private string companyName;

        [SerializeField]
        private string applicationIdentifier;

        [SerializeField]
        private string targetPath;

        //Allows setting scenes through the editor. Does not used in scripting, just for easy referencing.
        [SerializeField]
        private SceneAsset[] scenes;

        [SerializeField]
        private UnityEditor.BuildTarget buildTarget;

        //TODO: This field may be an array to be able to define multiple options.
        [SerializeField]
        private UnityEditor.BuildOptions[] buildOptions;

        [SerializeField]
        private string scriptingSymbols;

        private PlatformSpecificConfiguration platformConfig;
        private string prevScriptingSymbols;

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

        public BuildOptions BuildOptions
        {
            get
            {
                BuildOptions compiledOptions = BuildOptions.None;
                for (int i = 0; i < buildOptions.Length; i++)
                {
                    compiledOptions |= buildOptions[i];
                }
                
                return compiledOptions;
            }
        }
        public string ScriptingSymbols { get { return scriptingSymbols; } set { scriptingSymbols = value; }}
        public PlatformSpecificConfiguration PlatformConfiguration 
        { 
            get 
            {
                if (platformConfig == null)
                    platformConfig = ScriptableObject.CreateInstance<PlatformSpecificConfiguration>();

                return platformConfig; 
            }
        }
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
                    case BuildTarget.StandaloneOSX:
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

        public void ApplyConfiguration()
        {
            PlayerSettings.companyName = this.companyName;

            PlayerSettings.applicationIdentifier = this.applicationIdentifier;

			//NOTE: Should scripting symbols set before prebuild actions?
			//Store old scripting symbols before updating them with this configuration
            prevScriptingSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(this.BuildTargetGroup);

			//Update scripting symbols with the configuration
			PlayerSettings.SetScriptingDefineSymbolsForGroup(this.BuildTargetGroup, this.ScriptingSymbols);

            this.PlatformConfiguration.ApplyConfiguration();
        }

        public void RevertConfiguration()
        {
            PlayerSettings.SetScriptingDefineSymbolsForGroup(this.BuildTargetGroup, prevScriptingSymbols);

            this.PlatformConfiguration.RevertConfiguration();
        }

        public void SetBuildOptions(params BuildOptions[] buildOptions)
        {
            this.buildOptions = buildOptions;
        }
    }
}
