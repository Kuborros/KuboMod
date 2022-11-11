using BepInEx;
using HarmonyLib;
using System.IO;
using System.Linq;
using UnityEngine;

namespace KuboMod
{
    [BepInPlugin("com.kuborro.plugins.fp2.kubomod", "KuboMod", "2.1.1")]
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

            //HarmonyFileLog.Enabled = true;
            var harmony = new Harmony("com.kuborro.plugins.fp2.kubomod");
            harmony.PatchAll(typeof(PatchPommy));
            harmony.PatchAll(typeof(PatchInstanceNPC));
            harmony.PatchAll(typeof(PatchNPCList));
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
                    FPStage.ValidateStageListPos(kuboObject.GetComponent<FPHubNPC>());
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
        class PatchNPCList
        {
            [HarmonyPostfix]
            [HarmonyPatch(typeof(FPSaveManager), nameof(FPSaveManager.LoadFromFile), MethodType.Normal)]
            static void Postfix(ref string[] ___npcNames)
            {
                if (FPSaveManager.gameMode == FPGameMode.CLASSIC) return;
                if (___npcNames == null) return;

                if (!(___npcNames.Contains("01 02 Kubo")))
                {
                    ___npcNames = ___npcNames.AddToArray("01 02 Kubo");
                }

                if (FPSaveManager.npcFlag.Length < ___npcNames.Length)
                    FPSaveManager.npcFlag = FPSaveManager.ExpandByteArray(FPSaveManager.npcFlag, ___npcNames.Length);
                if (FPSaveManager.npcDialogHistory.Length < ___npcNames.Length)
                    FPSaveManager.npcDialogHistory = FPSaveManager.ExpandNPCDialogHistory(FPSaveManager.npcDialogHistory, ___npcNames.Length);

                int id = FPSaveManager.GetNPCNumber("Kubo");
                if (FPSaveManager.npcDialogHistory[id].dialog.Length != 8)
                {
                    FPSaveManager.npcDialogHistory[id].dialog = new bool[8];
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

