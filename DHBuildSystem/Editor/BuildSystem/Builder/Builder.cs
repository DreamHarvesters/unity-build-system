using UnityEngine;
using System.Collections;
using UnityEditor;

namespace BuildSystem
{
    [CreateAssetMenu(menuName = "DH/Build/Create Builder", fileName = "Builder", order = 1)]
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

            config.ApplyConfiguration();

            string error = BuildPipeline.BuildPlayer(config.BuildSceneList, config.TargetPath,
                                      config.BuildTarget, config.BuildOptions);

            config.RevertConfiguration();

            actionsWorked = RunPostBuildActions(onPostBuildActions);

            if(!actionsWorked)
                Debug.LogError("Problem with post build actions!!!");

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
