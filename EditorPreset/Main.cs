using HarmonyLib;
using System.Reflection;
using UnityModManagerNet;

namespace EditorPreset
{
    public class Main
    {
        public static UnityModManager.ModEntry Mod;
        public static Harmony harmony;
        public static bool IsEnabled = false;

        public static void Startup(UnityModManager.ModEntry entry)
        {
            entry.Logger.Log("Starting!");
            Mod = entry;
            Mod.OnToggle = (modentry, value) =>
            {
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
        }

        private static void OnGUI(UnityModManager.ModEntry modEntry)
        {

        }

        private static void OnSaveGUI(UnityModManager.ModEntry modEntry)
        {

        }
    }
}
