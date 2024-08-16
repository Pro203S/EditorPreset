using HarmonyLib;
using System.Reflection;
using UnityEngine;
using UnityModManagerNet;
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
            entry.Logger.Log("Starting!");
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
            setting = UnityModManager.ModSettings.Load<Setting>(entry);
        }

        private static void OnGUI(ModEntry modEntry)
        {
            if (GUILayout.Button("go to main"))
            {
                ADOBase.GoToLevelSelect();
            }
        }

        private static void OnSaveGUI(ModEntry modEntry)
        {

        }
    }
}
