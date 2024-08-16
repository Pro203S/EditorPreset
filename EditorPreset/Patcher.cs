using ADOFAI;
using HarmonyLib;
using System.Collections.Generic;

namespace EditorPreset
{
    public static class Patcher
    {
        [HarmonyPatch(typeof(LevelData), "Setup")]
        public static class LvlData_Setup
        {
            public static void Postfix(LevelData __instance)
            {
                if (!Main.IsEnabled) return;

                
                Main.Mod.Logger.Log("applying preset");

                __instance.cameraSettings["zoom"] = (float)350;

                Main.Mod.Logger.Log("applied preset");
            }
        }
    }
}
