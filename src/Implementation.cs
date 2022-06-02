using System.Collections.Generic;
using MelonLoader;
using UnityEngine;

namespace ChooseStartingLocation
{
    public class Implementation : MelonMod
    {
        public static Location startLocation = new Location();
        
        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
            Debug.Log($"[{Info.Name}] Version {Info.Version} loaded!");
            Settings.OnLoad();
        }
    }
}
