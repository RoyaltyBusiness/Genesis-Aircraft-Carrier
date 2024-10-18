using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VehicleFramework.VehicleTypes;
using System.Collections;
using VehicleFramework;
using GenesisCarrier;
using BepInEx;
using HarmonyLib;
using Nautilus.Utility;

namespace GenesisCarrier
{
    public static class Logger
    {
        public static void Log(string message)
        {
            UnityEngine.Debug.Log("[Genesis]:" + message);
        }
        public static void Output(string msg)
        {
            BasicText message = new BasicText(500, 0);
            message.ShowMessage(msg, 5);
        }
    }
    [BepInPlugin("com.royalty.subnautica.genesis.mod", "Genesis", "0.5.0")]
    [BepInDependency("com.mikjaw.subnautica.vehicleframework.mod")]
    [BepInDependency("com.snmodding.nautilus")]

    public class MainPatcher : BaseUnityPlugin
    {
        public void Start()
        {
            var harmony = new Harmony("com.royalty.subnautica.genesis.mod");
            harmony.PatchAll();
            UWE.CoroutineHost.StartCoroutine(Genesis.Register());
        }
    }
}