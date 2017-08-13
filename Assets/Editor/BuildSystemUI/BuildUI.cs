using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BuildSystem
{
	public class BuildUI : EditorWindow
	{
        private BuildManager buildManager;		
		
		[MenuItem("DH/Build System")]
		public static void OpenWindow()
		{
			EditorWindow.GetWindow(typeof(BuildUI));
		}

        private void Awake()
        {
            buildManager = new BuildManager();
        }

        private void OnGUI()
		{
            EditorGUILayout.BeginVertical();

            for (int i = 0; i < buildManager.Builders.Length; i++)
            {
                EditorGUILayout.LabelField(buildManager.Builders[i].Name);
            }

            EditorGUILayout.EndVertical();
		}
	}
}