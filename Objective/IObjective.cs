// @COPYRIGHT ZITROUILLE 2024
using System;

namespace LCObjectiveMod.Objectives
{
    enum eObjectiveType
    {
        undefined,
        noCrouch,
        noDeathObjective,
        noDoorOpened,
        noJump,
        returnBodiesToShip
    }

    enum eObjectiveStatus
    {
        running,
        succeeded,
        failed
    }

    /**
     * Interface used to initialize and update in game objective.
     * To create a new objective for the game:
     *  1. Create a new class who extends IObjective interface.
     *  2. Add a new value in the eObjectiveType enum and set it inside the Initialize method.
     *  3. In the initialize method, set also a description and a default status.
     *  4. The update method should contains success and/or fail condition.
     *  4. Add the objective to the ObjectiveManager constructor. 
     */

    internal abstract class IObjective
    {
        //-----------------------------------------------------------------------------------------
        // Method called each time an objective is reset.
        // This should set by default the objective type, description and start status.
        //-----------------------------------------------------------------------------------------
        public abstract void Initialize();

        //-----------------------------------------------------------------------------------------
        // Called only if the objective is the active one.
        // Should contains success and/or fail condition for the objective.
        // Based on the conditions, update the status of the objective.
        // If the objective failed, you have to call:
        //  - ObjectiveModBaseUnityPlugin.GetObjectiveFailureClientMessage().SendServer(1);
        // If the objective was succesfull, you have to call:
        //  - ObjectiveModBaseUnityPlugin.GetObjectiveSuccessClientMessage().SendServer(1);
        // Do not update objective status in this method. This will be done automatically.
        //-----------------------------------------------------------------------------------------
        public abstract void Update();

        //-----------------------------------------------------------------------------------------
        //                                   Setters
        //-----------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------
        public void SetStatus(eObjectiveStatus ieObjectiveStatus)
        {
            _eObjectiveStatus = ieObjectiveStatus;
        }
        
        //-----------------------------------------------------------------------------------------
        public void SetClientIdWhoStartObjective(ulong iuClientIdWhoStartObjective)
        {
            _uClientIdWhoStartObjective = iuClientIdWhoStartObjective;
        }

        //-----------------------------------------------------------------------------------------
        //                                   Getters
        //-----------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------
        public string GetDescription() => _sDescription;  
        
        //-----------------------------------------------------------------------------------------
        public ulong GetClientIdWhoStartObjective() => _uClientIdWhoStartObjective;

        //-----------------------------------------------------------------------------------------
        public eObjectiveStatus GetStatus() => _eObjectiveStatus;

        //-----------------------------------------------------------------------------------------
        public eObjectiveType GetObjectiveType() => _eObjectiveType;

        //-----------------------------------------------------------------------------------------
        public int GetCredit() => _nCredit;

        //-----------------------------------------------------------------------------------------
        //                                   Private members
        //-----------------------------------------------------------------------------------------

        internal string _sDescription;

        internal ulong _uClientIdWhoStartObjective;

        internal eObjectiveStatus _eObjectiveStatus = eObjectiveStatus.running;

        internal eObjectiveType _eObjectiveType = eObjectiveType.undefined;

        internal int _nCredit = 0;
    }
}
