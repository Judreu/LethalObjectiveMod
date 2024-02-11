// @COPYRIGHT ZITROUILLE 2024

using System.Linq;
using System.Xml.Linq;
using GameNetcodeStuff;
using HarmonyLib;
using LCObjectiveMod.Objectives;
using LethalObjectiveMod;
using UnityEngine;

namespace LCObjectiveMod.Patches
{
    [HarmonyPatch(typeof(StartOfRound))]
    internal class StartOfRoundPatch
    {
        //-----------------------------------------------------------------------------------------
        // Only called for the user who pressed button to start new round.
        // Retrieve objective manager and pick a random objective.
        // This objective will be set to all users in the lobby.
        //-----------------------------------------------------------------------------------------
        [HarmonyPatch("StartGame")]
        [HarmonyPostfix]
        private static void StartGamePatch(StartOfRound __instance)
        {
            ObjectiveModBaseUnityPlugin.GetGameRoundManager().Reset();
            ObjectiveManager objectiveManager = ObjectiveModBaseUnityPlugin.GetObjectiveManager();
            if (0 < objectiveManager.GetObjectives().Count)
            {
                objectiveManager.ResetObjectives();
                int nObjectiveType = (int)objectiveManager.GetObjectives()[0].GetObjectiveType();
                ObjectiveModBaseUnityPlugin.GetObjectiveTypeClientMessage().SendAllClients(nObjectiveType); 
            }
        }

        //-----------------------------------------------------------------------------------------
        // Callbed when doors are closed while the ship left the planet.
        // Update display of the objective to inform user if objective is succeeded or failed.
        // If the objective succeeded, add to the server additional credits based on the objective.
        // Because every player will call this method, the credit part is done only for the user
        // who starts the game and pick random objective from the list.
        //-----------------------------------------------------------------------------------------
        [HarmonyPatch("ShipHasLeft")]
        [HarmonyPostfix]
        private static void ShipHasLeftPatch()
        {
            IObjective objective = ObjectiveModBaseUnityPlugin.GetObjectiveManager().GetActiveObjective();
            if(null != objective)
            {
                ObjectiveModBaseUnityPlugin.GetObjectiveManager().UpdateActiveObjectiveUIDescription();
                if(objective.GetClientIdWhoStartObjective() == PlayerControllerBPatch.GetClientId())
                {
                    if (eObjectiveStatus.succeeded == objective.GetStatus())
                    {
                        Terminal terminal = Object.FindObjectOfType<Terminal>();
                        terminal.SyncGroupCreditsServerRpc(terminal.groupCredits+objective.GetCredit(), terminal.numberOfItemsInDropship);
                    }
                }        
            }
        }
    }
}
