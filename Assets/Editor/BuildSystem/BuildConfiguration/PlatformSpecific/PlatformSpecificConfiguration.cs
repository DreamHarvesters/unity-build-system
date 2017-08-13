using UnityEngine;
using System.Collections;

namespace BuildSystem
{
    public class PlatformSpecificConfiguration : ScriptableObject
    {
        public virtual void SetupConfiguration(){}
    }
}