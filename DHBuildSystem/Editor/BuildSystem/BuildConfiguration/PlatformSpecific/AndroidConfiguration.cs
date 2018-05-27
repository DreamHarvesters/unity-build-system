using UnityEngine;
using System.Collections;
using UnityEditor;

namespace DH.BuildSystem
{
    [CreateAssetMenu(fileName = "AndroidBuildConfiguration", menuName = "DH/Build/Platform Build Configuration/Android",
        order = 0)]
    public class AndroidConfiguration : BuildConfiguration
    {
        [SerializeField] private string keyAliasPassword;
        [SerializeField] private string keystorePassword;

        public override void ApplyConfiguration()
        {
            base.ApplyConfiguration();

            PlayerSettings.keyaliasPass = keyAliasPassword;

            PlayerSettings.keystorePass = keystorePassword;
        }
    }
}