using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BuildSystem
{
    public class BuildManager : IBuildManager
    {
        IBuilder[] builders;

        public BuildManager()
        {
            builders = GetBuilders("t:Builder");
        }

        public BuildManager(string builderFilter)
        {
            builders = GetBuilders(builderFilter);
        }

        public IBuilder[] Builders {get {return builders;}}

        public string Build(IBuilder builder)
        {
            return builder.Build(builder.GetConfiguration());
        }

        public IBuilder[] GetBuilders(string filter)
        {
            string[] assets = AssetDatabase.FindAssets(filter);

            IBuilder[] builders = new IBuilder[assets.Length];

            for (int i = 0; i < assets.Length; i++)
            {
                builders[i] = AssetDatabase.LoadAssetAtPath<Builder>(assets[i]);
            }

            return builders;
        }

        public IBuilder GetBuilder(string name)
        {
            string[] asset = AssetDatabase.FindAssets(name + " t:Builder");

            if (asset.Length == 0)
                throw new System.Exception("Builder could not be found: " + name);

            return AssetDatabase.LoadAssetAtPath<Builder>(AssetDatabase.GUIDToAssetPath(asset[0]));
        }
    }
}
