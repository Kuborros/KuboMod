using BepInEx;
using HarmonyLib;
using System.IO;
using UnityEngine;

namespace KuboMod
{
    [BepInPlugin("com.kuborro.plugins.fp2.kubomod", PluginInfo.PLUGIN_NAME, "1.2.0")]
    [BepInProcess("FP2.exe")]
    public class Plugin : BaseUnityPlugin
    {


        private void Awake()
        {
            //HarmonyFileLog.Enabled = true;
            var harmony = new Harmony("com.kuborro.plugins.fp2.kubomod");
            harmony.PatchAll(typeof(Patch));
            harmony.PatchAll(typeof(Patch2));
            harmony.PatchAll(typeof(Patch3));
            harmony.PatchAll(typeof(Patch4));
        }

        class Patch
        {
            [HarmonyPostfix]
            [HarmonyPatch(typeof(FPHubNPC), nameof(FPHubNPC.OnActivation), MethodType.Normal)]
            static void Postfix(ref string ___NPCName, ref NPCDialog[] ___dialog, ref int[] ___sortedPriorityList)
            {
                if (___NPCName == "Pommy")
                {
                    ___NPCName = "Kubo";
                    ___sortedPriorityList = new int[8] { 7, 6, 5, 4, 3, 2, 1, 0 };
                    ___dialog = new NPCDialog[8] { new NPCDialog
            {
                description = "Intro",
                enableAtStoryFlag = 0,
                disableAtStoryFlag = 9,
                priority = 1,
                lines =
                    new NPCDialogLine[] { new NPCDialogLine{ text = "Hi, I’m Kubo. Im the captain of one of the airships you can see in the skies.", pose = "Pose1", options = new NPCDialogOption[0] },
                    new NPCDialogLine{ text = "The ship currently is being repainted back in <c=blue>Shang Tu</c>, so i took few days off to visit the <c=yellow>Battlesphere</c>.", pose = "Pose1", options = new NPCDialogOption[0]},
                    new NPCDialogLine{ text = "<i><s=0.75>Just between us two, do you think i have chances with that cute <c=orange>bat girl</c> next to us?</s></i>", pose = "Pose1" , options = new NPCDialogOption[0]}
                }
            },
            new NPCDialog
            {
                description = "Battlesphere",
                enableAtStoryFlag = 12,
                disableAtStoryFlag = 18,
                priority = 2,
                lines =
                    new NPCDialogLine[] { new NPCDialogLine{ text = "That's quite the show you pulled out back there!", pose = "Pose1", options = new NPCDialogOption[0] },
                    new NPCDialogLine{ text = "Im not much of a fighter myself, outside my ship at least. I equipped her with <w>the latest in aerial combat technology~</w>", pose = "Pose1", options = new NPCDialogOption[0]},
                    new NPCDialogLine{ text = "<i><s=0.75>Maybe when the ship is done i could ask <c=orange>Maria</c> if she wants to join me on some nighttime flight?</s></i>", pose = "Pose1" , options = new NPCDialogOption[0]}
                }
            },
            new NPCDialog
            {
                description = "Sigwada",
                enableAtStoryFlag = 9,
                disableAtStoryFlag = 0,
                priority = 3,
                lines =
                    new NPCDialogLine[] { new NPCDialogLine{ text = "Hi, I’m Kubo. Im the captain of one of the airships you can see in the skies.", pose = "Pose1", options = new NPCDialogOption[0] },
                    new NPCDialogLine{ text = "Well, i <b>was</b> before bunch of <c=gold>Sky Pirates</c> bombarded <c=blue>Shang Tu</c> dockyards.", pose = "Pose1", options = new NPCDialogOption[0]},
                    new NPCDialogLine{ text = "<i><s=0.75>It ruined all my plans for this month, and its not even the <c=green>first time this happened</c>.</s></i>", pose = "Pose1" , options = new NPCDialogOption[0]}
                }
            },
            new NPCDialog
            {
                description = "Mayor",
                enableAtStoryFlag = 22,
                disableAtStoryFlag = 0,
                priority = 4,
                lines =
                    new NPCDialogLine[] { new NPCDialogLine{ text = "I sure love having my ship grounded during <w>Major Historical Events</w>.", pose = "Pose1", options = new NPCDialogOption[0] },
                    new NPCDialogLine{ text = "There's only so much help i can offer without <i>my ship</i> afterall. <br> But i did all i could anyways!", pose = "Pose1", options = new NPCDialogOption[0]},
                    new NPCDialogLine{ text = "<i><s=0.75>Im just glad me and <c=orange>Maria</c> got out of it all unharmed.</s></i>", pose = "Pose1" , options = new NPCDialogOption[0]}
                }
            },
            new NPCDialog
            {
                description = "Jungle",
                enableAtStoryFlag = 30,
                disableAtStoryFlag = 0,
                priority = 5,
                lines =
                    new NPCDialogLine[] { new NPCDialogLine{ text = "First <c=brown>pirates</c>, then <c=green>giant battle robots</c>.", pose = "Pose1", options = new NPCDialogOption[0] },
                    new NPCDialogLine{ text = "From what i heard from <c=orange>Maria</c> one of the robots belonged to remnant of <c=green>Brevon's</c> forces. And the other one was driven by <i>you?</i></c>", pose = "Pose1", options = new NPCDialogOption[0]},
                    new NPCDialogLine{ text = "<i><s=0.75>Why didnt you tell me you have a cool battle robot...</s></i>", pose = "Pose1" , options = new NPCDialogOption[0]}
                }
            },
            new NPCDialog
            {
                description = "Volcano",
                enableAtStoryFlag = 36,
                disableAtStoryFlag = 39,
                priority = 6,
                lines =
                    new NPCDialogLine[] { new NPCDialogLine{ text = "That rainbow volcano must be quite a sight from up in the air. I should go there sometime.", pose = "Pose1", options = new NPCDialogOption[0] },
                    new NPCDialogLine{ text = "<s=1.25><j>If only i had a working airship!</j></s>", pose = "Pose1", options = new NPCDialogOption[0]},
                    new NPCDialogLine{ text = "Anyhow, i heard Mayor Zao needs a new captain for his <br><c=red>'Amazing Luxury Pleasure Cruiser 2.0'</c> or <w>something</w> like that... Might as well apply for it.<br> Its not like we will be chasing starships or something with it, <j>right?</j>", pose = "Pose1" , options = new NPCDialogOption[0]},
                    new NPCDialogLine{ text = "<i><s=0.75>And maybe i can take <c=orange>Maria</c> on a ride with that thing~</s></i>", pose = "Pose1" , options = new NPCDialogOption[0] }
                }
            },
            new NPCDialog
            {
                description = "Bakunawa",
                enableAtStoryFlag = 39,
                disableAtStoryFlag = 46,
                priority = 7,
                lines =
                    new NPCDialogLine[] { new NPCDialogLine{ text = "Hi! I got the job as Zao's captain!", pose = "Pose1", options = new NPCDialogOption[0] },
                    new NPCDialogLine{ text = "Firts task? We chased <j>an actual <c=lightblue>starship</c></j> while also rescuing you ladies!", pose = "Pose1", options = new NPCDialogOption[0]},
                    new NPCDialogLine{ text = "<i><s=0.75>Apologies for not catching up to it - we reached the heights where engines choked due to lack of oxygen, any higher and we would start falling down like a rock.</s></i>", pose = "Pose1" , options = new NPCDialogOption[0]}
                }
            },
            new NPCDialog
            {
                description = "Ending",
                enableAtStoryFlag = 46,
                disableAtStoryFlag = 0,
                priority = 8,
                lines =
                    new NPCDialogLine[] { new NPCDialogLine{ text = "Hi, I’m Kubo. Im the captain of one of the airships you can see in the skies~", pose = "Pose1", options = new NPCDialogOption[0] },
                    new NPCDialogLine{ text = "Insurance paid out, and my ship is almost back to a proper state. I will be back in the skies in no time!", pose = "Pose1", options = new NPCDialogOption[0]},
                    new NPCDialogLine{ text = "All is well that ends well, am i right?", pose = "Pose1", options = new NPCDialogOption[0]},
                    new NPCDialogLine{ text = "<j><s=0.75>I did it, i asked <c=orange>her</c> out and she agreed! We are going out on a date soon!</s></j>", pose = "Pose1" , options = new NPCDialogOption[0]}
                }
            }
            };
                    SpriteRenderer renderer = (SpriteRenderer)GameObject.Find("NPC_Pommy").GetComponent(typeof(SpriteRenderer));
                    Texture2D texture = renderer.sprite.texture;

                    string texPath = Path.Combine(Path.GetFullPath("."), "mod_overrides");
                    //FileLog.Log("Mods Path: " + texPath);
                    //FileLog.Log("Texture file found: " + File.Exists(texPath + "\\SpriteAtlasTexture_NPC_Pommy_512x256_fmt4.png").ToString());
                    if (File.Exists(texPath + "\\SpriteAtlasTexture_NPC_Pommy_512x256_fmt4.png"))
                    {
                        texture.LoadImage(File.ReadAllBytes(texPath + "\\SpriteAtlasTexture_NPC_Pommy_512x256_fmt4.png"));
                    }

                    int npcnumber = FPSaveManager.GetNPCNumber("Pommy");
                    int diaLenght = (___sortedPriorityList.Length);
                    if (FPSaveManager.npcDialogHistory[npcnumber].dialog.Length < diaLenght)
                    {
                        FPSaveManager.npcDialogHistory[npcnumber].dialog = new bool[diaLenght];
                    }
                }
            }
        }
        class Patch2
        {
            [HarmonyPrefix]
            [HarmonyPatch(typeof(FPSaveManager), nameof(FPSaveManager.GetNPCNumber), MethodType.Normal)]
            static bool Prefix(string name, ref int __result)
            {
                if (name == "Kubo")
                {
                    __result = FPSaveManager.GetNPCNumber("Pommy");
                    return false;
                }
                return true;
            }


        }
        class Patch3
        {
            [HarmonyPostfix]
            [HarmonyPatch(typeof(FPHubNPC), nameof(FPHubNPC.Talk), MethodType.Normal)]
            static void Postfix(string ___NPCName)
            {
                if (___NPCName == "Kubo")
                {
                    FPSaveManager.npcDialogHistory[FPSaveManager.GetNPCNumber("Pommy")].dialog[0] = true;
                }
            }

        }

        class Patch4 
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

