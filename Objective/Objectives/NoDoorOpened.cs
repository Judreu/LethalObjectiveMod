// @COPYRIGHT ZITROUILLE 2024

using LethalObjectiveMod;

namespace LCObjectiveMod.Objectives
{
    //---------------------------------------------------------------------------------------------
    internal class NoDoorOpened : IObjective
    {
        //-----------------------------------------------------------------------------------------
        public override void Initialize()
        {
            _sDescription = "Aucune porte ne doit être ouverte";
            _eObjectiveType = eObjectiveType.noDoorOpened;
            _eObjectiveStatus = eObjectiveStatus.running;
            _uClientIdWhoStartObjective = 0;
            _nCredit = 35;
        }

        //-----------------------------------------------------------------------------------------
        public override void Update()
        {
            if(eObjectiveStatus.failed != _eObjectiveStatus)
            {
                if (ObjectiveModBaseUnityPlugin.GetGameRoundManager().GetDoorHasBeenOpened())
                {
                    ObjectiveModBaseUnityPlugin.GetObjectiveFailureClientMessage().SendServer(1);
                }
            }
        }
    }
}
