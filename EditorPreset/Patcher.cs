using ADOFAI;
using HarmonyLib;
using UnityEngine;

namespace EditorPreset
{
    public static class Patcher
    {
        [HarmonyPatch(typeof(LevelData), "Setup")]
        public static class LvlData_Setup
        {
            public static void Prefix(LevelData __instance)
            {
                Debug.Log("prefix");
            }
        }
    }
}
