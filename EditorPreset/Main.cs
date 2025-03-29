using HarmonyLib;
using System.Reflection;
using UnityEngine;
using static UnityModManagerNet.UnityModManager;

namespace EditorPreset
{
    public class Main
    {
        public static ModEntry Mod;
        public static Harmony harmony;
        public static bool IsEnabled = false;
        public static Setting setting;

        public static void Startup(ModEntry entry)
        {
            Mod = entry;
            Mod.OnToggle = (modentry, value) =>
            {
                entry.OnGUI = OnGUI;
                entry.OnSaveGUI = OnSaveGUI;
                IsEnabled = value;

                if (value)
                {
                    harmony = new Harmony(modentry.Info.Id);
                    harmony.PatchAll(Assembly.GetExecutingAssembly());
                    return true;
                }

                harmony.UnpatchAll(modentry.Info.Id);

                return true;
            };

            setting = new Setting();
            setting = ModSettings.Load<Setting>(entry);
        }

        private static void OnGUI(ModEntry modEntry)
        {
#if DEBUG
            if (GUILayout.Button("메인 화면으로 가기"))
            {
                ADOBase.GoToLevelSelect();
            }
#endif

            switch (ModSettingsGUI.CurrentScreen)
            {
                case "MainScreen": ModSettingsGUI.GUI(); break;
                case "SelectTrackAnimation": ModSettingsGUI.SelectAnimationType(ModSettingsGUI.AnimationType_Caption); break;
                case "SelectTrackAnimationD": ModSettingsGUI.SelectAnimationType(ModSettingsGUI.AnimationType_Caption, isDisappear: true); break;
                default: ModSettingsGUI.GUI(); break;
            }
        }

        private static void OnSaveGUI(ModEntry modEntry)
        {
            setting.Save(modEntry);
        }
    }
}
