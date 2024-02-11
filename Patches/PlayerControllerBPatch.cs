// @COPYRIGHT ZITROUILLE 2024

using GameNetcodeStuff;
using HarmonyLib;
using LCObjectiveMod.Objectives;
using LethalObjectiveMod;
using static UnityEngine.InputSystem.InputAction;

namespace LCObjectiveMod.Patches
{
    [HarmonyPatch(typeof(PlayerControllerB))]
    internal class PlayerControllerBPatch
    {
        //-----------------------------------------------------------------------------------------
        [HarmonyPatch(typeof(PlayerControllerB), "Awake")]
        [HarmonyPostfix]
        public static void AwakePatch(PlayerControllerB __instance)
        {
            _sName = __instance.name;
            _uClientId = __instance.actualClientId;
        }

        //-----------------------------------------------------------------------------------------
        [HarmonyPatch(typeof(PlayerControllerB), "Update")]
        [HarmonyPostfix]
        public static void UpdatePatch()
        {
            ObjectiveModBaseUnityPlugin.GetObjectiveManager().UpdateActiveObjective();
        }

        //-----------------------------------------------------------------------------------------
        [HarmonyPatch(typeof(PlayerControllerB), "Crouch_performed")]
        [HarmonyPrefix]
        public static void Crouch_performedPatch(PlayerControllerB __instance, ref CallbackContext context)
        {
            ObjectiveModBaseUnityPlugin.GetGameRoundManager().SetPlayerHasBeenCrouch(true);
        }

        //-----------------------------------------------------------------------------------------
        [HarmonyPatch(typeof(PlayerControllerB), "Jump_performed")]
        [HarmonyPrefix]
        public static void Jump_performedPatch(ref CallbackContext context)
        {
            ObjectiveModBaseUnityPlugin.GetGameRoundManager().SetPlayerHasJump(true);
        }

        //-----------------------------------------------------------------------------------------
        //                                   Getters
        //-----------------------------------------------------------------------------------------

        public static ulong GetClientId() => _uClientId;

        //-----------------------------------------------------------------------------------------
        //                                   Private members
        //-----------------------------------------------------------------------------------------

        private static string _sName = "Player";

        private static ulong _uClientId = 0;
    }
}
