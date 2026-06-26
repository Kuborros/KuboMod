using BepInEx;
using HarmonyLib;
using System.IO;
using UnityEngine;

namespace KuboMod
{
    [BepInPlugin("com.kuborro.plugins.fp2.kubomod", "KuboMod", "3.0.1")]
    [BepInDependency("000.kuborro.libraries.fp2.fp2lib")]
    [BepInProcess("FP2.exe")]
    public class Plugin : BaseUnityPlugin
    {

        public static AssetBundle moddedBundle;
        public static GameObject kuboObject;
        private void Awake()
        {
            string assetPath = Path.Combine(Path.GetFullPath("."), "mod_overrides");
            moddedBundle = AssetBundle.LoadFromFile(Path.Combine(assetPath, "kubomod.assets"));
            if (moddedBundle == null)
            {
                Logger.LogError("Failed to load AssetBundle! Mod cannot work without it, exiting. Please reinstall it.");
                return;
            }

            kuboObject = moddedBundle.LoadAsset<GameObject>("NPC_Kubo");
            FP2Lib.NPC.NPCHandler.RegisterNPC("com.kuborros.kubo","Kubo","Battlesphere Lobby", kuboObject,1,2,8);

            var harmony = new Harmony("com.kuborro.plugins.fp2.kubomod");
            harmony.PatchAll(typeof(PatchPommy));
        }

        class PatchPommy
        {
            [HarmonyPostfix]
            [HarmonyPatch(typeof(FPHubNPC), nameof(FPHubNPC.OnActivation), MethodType.Normal)]
            static void Postfix(string ___NPCName,ref Vector2 ___start, FPHubNPC __instance)
            {
                if (___NPCName == "Pommy")
                {
                    __instance.position = new Vector2(3022, -2456);
                    ___start = new Vector2(3022, -2456);
                    __instance.idleTime = 255;
                }
            }   
        }
    }
}

