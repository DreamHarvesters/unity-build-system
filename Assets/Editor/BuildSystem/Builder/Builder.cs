using UnityEngine;
using System.Collections;
using UnityEditor;

namespace BuildSystem
{
    [CreateAssetMenu(menuName = "Build/Create Builder", fileName = "Builder", order = 1)]
    public class Builder : ScriptableObject, IBuilder
    {
        private IBuildAction[] onPreBuildActions;
        private IBuildAction[] onPostBuildActions;

        [SerializeField]
        private BuildConfiguration configuration;

        public string Name 
        {
            get { return name; }
        }

        public void SetPreBuildActions(IBuildAction[] actions)
        {
            onPreBuildActions = actions;
        }

		public void SetPostBuildActions(IBuildAction[] actions)
		{
            onPostBuildActions = actions;
		}

        public BuildConfiguration GetConfiguration()
        {
            return configuration;
        }

        public string Build(BuildConfiguration config)
        {
			bool actionsWorked = RunPreBuildActions(onPreBuildActions);

            if(!actionsWorked)
                Debug.LogError("Problem with pre build actions!!!");

            //NOTE: Should scripting symbols set before prebuild actions?
            //Store old scripting symbols before updating them with this configuration
			string prevSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(config.BuildTargetGroup);

            //Update scripting symbols with the configuration
			PlayerSettings.SetScriptingDefineSymbolsForGroup(config.BuildTargetGroup, config.ScriptingSymbols);

            config.PlatformConfiguration.SetupConfiguration();

            string error = BuildPipeline.BuildPlayer(config.BuildSceneList, config.TargetPath,
                                      config.BuildTarget, config.BuildOptions);

            actionsWorked = RunPostBuildActions(onPostBuildActions);

            if(!actionsWorked)
                Debug.LogError("Problem with post build actions!!!");

            //Revert scripting symbols to the value before this build process
			PlayerSettings.SetScriptingDefineSymbolsForGroup(config.BuildTargetGroup, prevSymbols);

			return error;
        }

        public bool RunPreBuildActions(IBuildAction[] actions)
        {
            bool workedWell = true;

            if(actions != null)
            {
				System.Array.ForEach<IBuildAction>(actions, delegate (IBuildAction action)
				{
					workedWell &= action.Execute();
				});    
            }

            return workedWell;
        }

        public bool RunPostBuildActions(IBuildAction[] actions)
        {
			bool workedWell = true;

			if (actions != null)
            {
                System.Array.ForEach<IBuildAction>(actions, delegate (IBuildAction action)
                {
                    workedWell &= action.Execute();
                });
			}

            return workedWell;
        }

    }
}
