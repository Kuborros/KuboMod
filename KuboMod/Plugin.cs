using BepInEx;
using HarmonyLib;
using System.IO;
using UnityEngine;

namespace KuboMod
{
    [BepInPlugin("com.kuborro.plugins.fp2.kubomod", "KuboMod", "3.0.0")]
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
            harmony.PatchAll(typeof(PatchTrains));
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

        class PatchTrains
        {
            [HarmonyPostfix]
            [HarmonyPatch(typeof(MovingTrain), "Start", MethodType.Normal)]
            static void Postfix(MovingTrain __instance)
            {
                GameObject spTrain = __instance.gameObject;
                SpriteRenderer[] spriteRenderers = spTrain.GetComponentsInChildren<SpriteRenderer>();

                foreach (SpriteRenderer spriteRenderer in spriteRenderers)
                {
                    if (spriteRenderer.sprite.texture.name == "SpriteAtlasTexture-Passengers-256x256-fmt4")
                    {
                        string texPath = Path.Combine(Path.GetFullPath("."), "mod_overrides");
                        if (File.Exists(texPath + "\\SpriteAtlasTexture_Passengers_256x256_fmt4.png"))
                        {
                            spriteRenderer.sprite.texture.LoadImage(File.ReadAllBytes(texPath + "\\SpriteAtlasTexture_Passengers_256x256_fmt4.png"));
                        }
                    }
                }

            }
        }

    }
}

