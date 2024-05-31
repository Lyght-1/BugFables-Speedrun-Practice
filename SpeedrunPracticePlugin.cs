using BepInEx;
using HarmonyLib;

namespace SpeedrunPractice
{
    [BepInPlugin("com.Lyght.BugFables.plugins.SpeedrunPractice", "Speedrun Practice", "5.0.6")]
    [BepInProcess("Bug Fables.exe")]
    public class SpeedrunPracticePlugin : BaseUnityPlugin
    {
        void Awake()
        {
            var harmony = new Harmony("com.Lyght.BugFables.harmony.SpeedrunPractice");
            harmony.PatchAll();
        }
    }
}
