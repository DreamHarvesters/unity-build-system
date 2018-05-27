using UnityEngine;
using System.Collections;

namespace DH.BuildSystem
{
    public class PlatformSpecificConfiguration : ScriptableObject, IBuildConfiguration
    {
        public virtual void ApplyConfiguration(){}
        public virtual void RevertConfiguration(){}
    }
}