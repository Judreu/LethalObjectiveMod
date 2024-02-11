// @COPYRIGHT ZITROUILLE 2024

using LethalObjectiveMod;

namespace LCObjectiveMod.Objectives
{
    //---------------------------------------------------------------------------------------------
    internal class NoCrouch : IObjective
    {
        //-----------------------------------------------------------------------------------------
        public override void Initialize()
        {
            _sDescription = "Interdiction de s'accroupir";
            _eObjectiveType = eObjectiveType.noCrouch;
            _eObjectiveStatus = eObjectiveStatus.running;
            _uClientIdWhoStartObjective = 0;
            _nCredit = 40;
        }

        //-----------------------------------------------------------------------------------------
        public override void Update()
        {
            if(eObjectiveStatus.failed != _eObjectiveStatus)
            {
                if (ObjectiveModBaseUnityPlugin.GetGameRoundManager().GetPlayerHasBeenCrouch())
                {
                    ObjectiveModBaseUnityPlugin.GetObjectiveFailureClientMessage().SendServer(1);
                }
            }
        }
    }
}
