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
            public static void Postfix(LevelData __instance)
            {
                if (!Main.IsEnabled) return;

                __instance.levelSettings["author"] = Main.setting.levelAuthor;
                __instance.songSettings["volume"] = Main.setting.songVolume;
                __instance.cameraSettings["zoom"] = Main.setting.cameraZoom;
                __instance.cameraSettings["position"] = new Vector2(Main.setting.cameraPosX, Main.setting.cameraPosY);
                __instance.trackSettings["trackAnimation"] = Main.setting.trackAnimation;
                __instance.trackSettings["trackDisappearAnimation"] = Main.setting.trackDisappearAnimation;
                __instance.trackSettings["beatsAhead"] = Main.setting.trackBeatsAhead;
                __instance.trackSettings["beatsBehind"] = Main.setting.trackBeatsBehind;
            }
        }
    }
}
