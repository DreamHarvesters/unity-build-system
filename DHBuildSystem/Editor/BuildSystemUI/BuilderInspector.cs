using UnityEngine;
using System.Collections;
using UnityEditor;

namespace DH.BuildSystem
{
    [CustomEditor(typeof(Builder))]
    public class BuilderInspector : Editor
	{
        private Builder inspectedBuilder;

        private void OnEnable()
        {
            inspectedBuilder = (Builder)this.serializedObject.targetObject;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if(GUILayout.Button("Build"))
            {
                inspectedBuilder.Build(inspectedBuilder.GetConfiguration());
            }
        }
	}
}