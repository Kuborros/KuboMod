using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using HarmonyLib.Tools;

namespace KuboMod
{
    [BepInPlugin("com.kuborro.plugins.fp2.kubomod", PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("FP2.exe")]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            HarmonyFileLog.Enabled = true;
            var harmony = new Harmony("com.kuborro.plugins.fp2.kubomod");
            harmony.PatchAll(typeof(Patch));
        }
    }
}  

    class Patch {
    [HarmonyPostfix]
    [HarmonyPatch(typeof(FPHubNPC),nameof(FPHubNPC.OnActivation),MethodType.Normal)]
    static void Postfix(ref string ___NPCName,ref NPCDialog[] ___dialog)
    {
        FileLog.Log(___NPCName);
        if (___NPCName == "Pommy")
        {
            ___NPCName = "Kubo";
            ___dialog = new NPCDialog[] { new NPCDialog
            {
                description = "Intro",
                lines = 
                    new NPCDialogLine[] { new NPCDialogLine{ text = "Hi, I’m Kubo. Im the captain of one of the airships you can see in the skies.", pose = "Pose1", options = new NPCDialogOption[0] },
                    new NPCDialogLine{ text = "Well, i *was* before bunch of Sky Pirates bombarded Shang Mu dockyards...", pose = "Pose1", options = new NPCDialogOption[0]},
                    new NPCDialogLine{ text = "Anyhow, i heard Mayor Zao needs a new captain for his 'Amazing luxury pleasure cruiser', might as well apply for that. Its not like we will be chasing starships or something with it, right?", pose = "Pose1" , options = new NPCDialogOption[0]}
                }
            }
            };

        }
    }
}

