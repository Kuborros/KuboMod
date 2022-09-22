using BepInEx;
using HarmonyLib;
using HarmonyLib.Tools;
using System;
using System.IO;
using UnityEngine;

namespace KuboMod
{
    [BepInPlugin("com.kuborro.plugins.fp2.kubomod", PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("FP2.exe")]
    public class Plugin : BaseUnityPlugin
    {
        public static string ModsDirectory { get; } = Path.Combine(Paths.GameRootPath, "mods");
        private void Awake()
        {
            HarmonyFileLog.Enabled = true;
            var harmony = new Harmony("com.kuborro.plugins.fp2.kubomod");
            harmony.PatchAll(typeof(Patch));

        }
    }

    class Patch
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(FPHubNPC), nameof(FPHubNPC.OnActivation), MethodType.Normal)]
        static void Postfix(ref string ___NPCName, ref NPCDialog[] ___dialog, ref int[] ___sortedPriorityList, ref bool ___checkUnreadDialog)
        {
            //FileLog.Log(___NPCName);
            if (___NPCName == "Pommy")
            {
                ___NPCName = "Kubo";
                ___checkUnreadDialog = false;
                ___sortedPriorityList = new int[8] { 7, 6, 5, 4, 3, 2, 1, 0 };
                ___dialog = new NPCDialog[8] { new NPCDialog
            {
                description = "Intro",
                enableAtStoryFlag = 0,
                disableAtStoryFlag = 22,
                priority = 1,
                lines =
                    new NPCDialogLine[] { new NPCDialogLine{ text = "Hi, I’m Kubo. Im the captain of one of the airships you can see in the skies.", pose = "Pose1", options = new NPCDialogOption[0] },
                    new NPCDialogLine{ text = "The ship currently is being repainted back in <c=blue>Shang Tu</c>, so i took few days off to visit the <c=red>Battlesphere</c>.", pose = "Pose1", options = new NPCDialogOption[0]},
                    new NPCDialogLine{ text = "<j><s=0.75>Just between us two, do you think i have chances with that cute <c=orange>bat girl</c> next to us?</s></j>", pose = "Pose1" , options = new NPCDialogOption[0]}
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
                    new NPCDialogLine{ text = "Im not much of a fighter myself, outside my ship at least. I equipped her with <j>the latest in aerial combat technology~</j>", pose = "Pose1", options = new NPCDialogOption[0]},
                    new NPCDialogLine{ text = "<j><s=0.75>Maybe when the ship is done i could ask <c=orange>Maria</c> if she wants to join me on some nighttime flight?</s></j>", pose = "Pose1" , options = new NPCDialogOption[0]}
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
                    new NPCDialogLine{ text = "Well, i <b>was</b> before bunch of <c=brown>Sky Pirates</c> bombarded <c=blue>Shang Tu</c> dockyards.", pose = "Pose1", options = new NPCDialogOption[0]},
                    new NPCDialogLine{ text = "<j><s=0.75>It ruined all my plans for this month, and its not even the <c=green>first time this happened</c>.</s></j>", pose = "Pose1" , options = new NPCDialogOption[0]}
                }
            },
            new NPCDialog
            {
                description = "Mayor",
                enableAtStoryFlag = 22,
                disableAtStoryFlag = 0,
                priority = 4,
                lines =
                    new NPCDialogLine[] { new NPCDialogLine{ text = "I sure love having my ship grounded during <b> Major Historical Events </b>.", pose = "Pose1", options = new NPCDialogOption[0] },
                    new NPCDialogLine{ text = "There's only so much help i can offer without my ship afterall. But i did all i could anyways!", pose = "Pose1", options = new NPCDialogOption[0]},
                    new NPCDialogLine{ text = "<j><s=0.75>Im just glad me and <c=orange>Maria</c> got out of it all unharmed.</s></j>", pose = "Pose1" , options = new NPCDialogOption[0]}
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
                    new NPCDialogLine{ text = "From what i heard from <c=orange>Maria</c> one of the robots belonged to remnant of <c=green>Brevon's</c> forces. And the other one was driven by.. <j>you?</c>", pose = "Pose1", options = new NPCDialogOption[0]},
                    new NPCDialogLine{ text = "<j><s=0.75>Why didnt you tell me you have a cool battle robot...</s></j>", pose = "Pose1" , options = new NPCDialogOption[0]}
                }
            },
            new NPCDialog
            {
                description = "Volcano",
                enableAtStoryFlag = 36,
                disableAtStoryFlag = 39,
                priority = 6,
                lines =
                    new NPCDialogLine[] { new NPCDialogLine{ text = "Hi, I’m Kubo. Im the captain of one of the airships you can see in the skies.", pose = "Pose1", options = new NPCDialogOption[0] },
                    new NPCDialogLine{ text = "Well, i *was* before bunch of Sky Pirates bombarded Shang Mu dockyards...", pose = "Pose1", options = new NPCDialogOption[0]},
                    new NPCDialogLine{ text = "Anyhow, i heard Mayor Zao needs a new captain for his <c=red>'Amazing luxury pleasure cruiser 2.0'</c>, might as well apply for that. Its not like we will be chasing starships or something with it, <j>right?</j>", pose = "Pose1" , options = new NPCDialogOption[0]},
                    new NPCDialogLine{ text = "<j><s=0.75>And maybe i can take <c=orange>Maria</c> on a ride with that thing~</s></j>", pose = "Pose1" , options = new NPCDialogOption[0] }
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
                    new NPCDialogLine{ text = "<j><s=0.75>Apologies for not catching up to it - we reached the heights where engines choked due to lack of oxygen, any higher and we would start falling down like a rock.</s></j>", pose = "Pose1" , options = new NPCDialogOption[0]}
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
            }
        }

    }
}

