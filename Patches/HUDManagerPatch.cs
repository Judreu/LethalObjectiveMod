// @COPYRIGHT ZITROUILLE 2024

using BepInEx.Logging;
using HarmonyLib;
using LethalObjectiveMod;
using TMPro;
using UnityEngine;

namespace LCObjectiveMod.Patches
{
    [HarmonyPatch(typeof(HUDManager))]
    internal class HUDManagerPatch
    {
        //-----------------------------------------------------------------------------------------
        [HarmonyPatch(typeof(HUDManager), "Start")]
        [HarmonyPostfix]
        private static void StartPatch(ref HUDManager __instance)
        {
            GameObject handsFullTextGameObject = GameObject.Find("Systems/UI/Canvas/IngamePlayerHUD/HandsFullText");
            _InitObjectiveTextComponent(ref __instance, handsFullTextGameObject);
        }

        //-----------------------------------------------------------------------------------------
        private static void _InitObjectiveTextComponent(ref HUDManager iInstance, GameObject iParentGameObject)
        {
            TextMeshProUGUI objectiveTextMeshProUGUI = ObjectiveModBaseUnityPlugin.GetObjectiveTextMeshProUGUI();
            if (!objectiveTextMeshProUGUI)
            {
                GameObject objectiveDisplayGameObject = new GameObject("ObjectiveDisplay");
                objectiveDisplayGameObject.transform.SetParent(iParentGameObject.transform, worldPositionStays: false);
       
                objectiveTextMeshProUGUI = objectiveDisplayGameObject.AddComponent<TextMeshProUGUI>();
                objectiveTextMeshProUGUI.font = iInstance.controlTipLines[0].font;
                objectiveTextMeshProUGUI.fontSize = 12f;
                objectiveTextMeshProUGUI.enabled = true;
                objectiveTextMeshProUGUI.alignment = TextAlignmentOptions.Center;
                objectiveTextMeshProUGUI.enableWordWrapping = false;
                ObjectiveModBaseUnityPlugin.SetObjectiveTextMeshProUGUI(objectiveTextMeshProUGUI);
                objectiveDisplayGameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -35f);
                ObjectiveModBaseUnityPlugin.GetObjectiveManager().UpdateActiveObjectiveUIDescription();
            }      
        }

        //-----------------------------------------------------------------------------------------
        [HarmonyPatch(typeof(HUDManager), "DisplayDaysLeft")]
        [HarmonyPrefix]
        private static void DisplayDaysLeftPatch(ref HUDManager __instance)
        {
            ObjectiveModBaseUnityPlugin.GetObjectiveManager().ResetObjectives();
        }
    }
}
