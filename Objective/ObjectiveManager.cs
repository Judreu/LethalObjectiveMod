// @COPYRIGHT ZITROUILLE 2024

using System.Collections.Generic;
using LethalObjectiveMod;
using TMPro;
using UnityEngine;

namespace LCObjectiveMod.Objectives
{
    internal class ObjectiveManager
    {
        //-----------------------------------------------------------------------------------------
        //                                   PUBLIC
        //-----------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------
        // This method should only contains list of objective available for user.
        //-----------------------------------------------------------------------------------------
        public ObjectiveManager()
        {
            _listObjectives.Add(new NoCrouch());
            _listObjectives.Add(new NoJump());
            _listObjectives.Add(new NoDoorOpened());
            //_listObjectives.Add(new NoDeathObjective());
            //_listObjectives.Add(new ReturnBodiesToShip());
        }

        //-----------------------------------------------------------------------------------------
        //                                   Getters
        //-----------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------
        public IObjective GetActiveObjective() => _activeObjective;

        //-----------------------------------------------------------------------------------------
        public List<IObjective> GetObjectives() => _listObjectives;

        //-----------------------------------------------------------------------------------------
        //                                   Setters
        //-----------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------
        public void SetActiveObjective(eObjectiveType ieObjectiveType, ulong iuClientIdWhoStartObjective)
        {
            ResetObjectives();
            if (eObjectiveType.undefined != ieObjectiveType)
            {
                foreach (IObjective objective in _listObjectives)
                {
                    if (ieObjectiveType == objective.GetObjectiveType())
                    {
                        _activeObjective = objective;
                        _activeObjective.SetClientIdWhoStartObjective(iuClientIdWhoStartObjective);
                        break;
                    }
                }
            }        
            UpdateActiveObjectiveUIDescription();
            SetActiveObjectiveStatus(eObjectiveStatus.succeeded);
        }

        //-----------------------------------------------------------------------------------------
        //                                   Tools
        //-----------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------
        public void SetActiveObjectiveStatus(eObjectiveStatus ieObjectiveStatus)
        {
            IObjective activeObjective = GetActiveObjective();
            if (activeObjective != null)
            {
                activeObjective.SetStatus(ieObjectiveStatus);
            }
        }

        //-----------------------------------------------------------------------------------------
        public void UpdateActiveObjectiveUIDescription()
        {
            // Display active objective description on the screen.
            IObjective activeObjective = GetActiveObjective();
            if (null != activeObjective)
            {
                if (eObjectiveStatus.running == activeObjective.GetStatus())
                {
                    ColorUtility.TryParseHtmlString("#dba502", out var color);
                    ObjectiveModBaseUnityPlugin.GetObjectiveTextMeshProUGUI().color = color;
                }
                else if (eObjectiveStatus.succeeded == activeObjective.GetStatus())
                {
                    ColorUtility.TryParseHtmlString("#2cc92c", out var color);
                    ObjectiveModBaseUnityPlugin.GetObjectiveTextMeshProUGUI().color = color;
                }
                else if (eObjectiveStatus.failed == activeObjective.GetStatus())
                {
                    ColorUtility.TryParseHtmlString("#c70000", out var color);
                    ObjectiveModBaseUnityPlugin.GetObjectiveTextMeshProUGUI().color = color;
                }
                ObjectiveModBaseUnityPlugin.GetObjectiveTextMeshProUGUI().text = $"Objectif: {activeObjective.GetDescription()} [{activeObjective.GetCredit()} crédits]";
            }
            else
            {
                TextMeshProUGUI objectiveTextMeshProUGUI = ObjectiveModBaseUnityPlugin.GetObjectiveTextMeshProUGUI();
                ColorUtility.TryParseHtmlString("#dba502", out var color);
                objectiveTextMeshProUGUI.color = color;
                objectiveTextMeshProUGUI.text = $"Objectif : Pas encore disponible";
            }
        }

        //-----------------------------------------------------------------------------------------
        public void UpdateActiveObjective()
        {
            if(null != _activeObjective)
            {
                _activeObjective.Update();
            }
        }

        //-----------------------------------------------------------------------------------------
        public void ResetObjectives()
        {
            foreach (IObjective objective in _listObjectives)
            {
                objective.Initialize();
            }
            _Shuffle(_listObjectives);
            _activeObjective = null;
            UpdateActiveObjectiveUIDescription();
        }

        //-----------------------------------------------------------------------------------------
        //                                   PRIVATE
        //-----------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------
        private static void _Shuffle<T>(IList<T> list)
        {
            System.Random rng = new System.Random(); // Considérez d'utiliser une instance plus globale si nécessaire
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        //-----------------------------------------------------------------------------------------
        //                                   Private members
        //-----------------------------------------------------------------------------------------

        private IObjective _activeObjective;

        private List<IObjective> _listObjectives = new List<IObjective>();
    }
}
