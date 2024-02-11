// @COPYRIGHT ZITROUILLE 2024

using GameNetcodeStuff;
using HarmonyLib;
using LethalObjectiveMod;

namespace LCObjectiveMod.Patches
{
    [HarmonyPatch(typeof(DoorLock))]
    internal class DoorLockPatch
    {
        //-----------------------------------------------------------------------------------------
        [HarmonyPostfix]
        [HarmonyPatch(typeof(DoorLock), "UnlockDoorSyncWithServer")]
        public static void UnlockDoorSyncWithServerPatch(DoorLock __instance)
        {
            ObjectiveModBaseUnityPlugin.GetGameRoundManager().SetDoorHasBeenOpened(true);
        }
    }
}
