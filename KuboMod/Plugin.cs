using BepInEx;
using HarmonyLib;
using System.IO;
using UnityEngine;

namespace KuboMod
{
    [BepInPlugin("com.kuborro.plugins.fp2.kubomod", "KuboMod", "2.0.0")]
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
                Logger.LogWarning("Failed to load AssetBundle!");
            }

            //HarmonyFileLog.Enabled = true;
            var harmony = new Harmony("com.kuborro.plugins.fp2.kubomod");
            harmony.PatchAll(typeof(PatchPommy));
            harmony.PatchAll(typeof(PatchInstanceNPC));
            harmony.PatchAll(typeof(PatchGetID));
            harmony.PatchAll(typeof(PatchTrains));
        }

        class PatchPommy
        {
            [HarmonyPostfix]
            [HarmonyPatch(typeof(FPHubNPC), nameof(FPHubNPC.OnActivation), MethodType.Normal)]
            static void Postfix(ref string ___NPCName, FPHubNPC __instance)
            {
                if (___NPCName == "Pommy")
                {
                    __instance.position = new Vector2(1652, -2456);
                    FileLog.Log(FPStage.ValidateStageListPos(kuboObject.GetComponent<FPHubNPC>()).ToString());
                }
            }   
        }

        class PatchInstanceNPC
        {
            [HarmonyPostfix]
            [HarmonyPatch(typeof(FPPlayer), "Start", MethodType.Normal)]
            static void Postfix()
            {
                if (FPStage.stageNameString == "Battlesphere Lobby")
                {
                    Object[] modKuboPre = moddedBundle.LoadAllAssets();
                    foreach (var mod in modKuboPre)
                    {
                        if (mod.GetType() == typeof(GameObject))
                        {
                            kuboObject = (GameObject)Instantiate(mod);
                            kuboObject.name = "NPC_Kubo_BSphere";                         
                        }
                    }
                }
            }
        }

        class PatchGetID
        {
            [HarmonyPrefix]
            [HarmonyPatch(typeof(FPSaveManager), nameof(FPSaveManager.GetNPCNumber), MethodType.Normal)]
            static bool Prefix(string name, ref int __result)
            {
                if (name == "Kubo")
                {
                    __result = FPSaveManager.GetNPCNumber("Pommy");
                    if (FPSaveManager.npcDialogHistory[__result].dialog.Length < 8)
                    {
                        FPSaveManager.npcDialogHistory[__result].dialog = new bool[8];
                    }
                    return false;
                }
                return true;
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
                        //FileLog.Log("Mods Path: " + texPath + "\\SpriteAtlasTexture_Passengers_256x256_fmt4.png");
                        //FileLog.Log("Texture file found: " + File.Exists(texPath + "\\SpriteAtlasTexture_Passengers_256x256_fmt4.png").ToString());
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

