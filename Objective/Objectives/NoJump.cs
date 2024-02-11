// @COPYRIGHT ZITROUILLE 2024

using LethalObjectiveMod;

namespace LCObjectiveMod.Objectives
{
    //---------------------------------------------------------------------------------------------
    internal class NoJump : IObjective
    {
        //-----------------------------------------------------------------------------------------
        public override void Initialize()
        {
            _sDescription = "Interdiction de sauter";
            _eObjectiveType = eObjectiveType.noJump;
            _eObjectiveStatus = eObjectiveStatus.running;
            _uClientIdWhoStartObjective = 0;
            _nCredit = 90;
        }

        //-----------------------------------------------------------------------------------------
        public override void Update()
        {
            if(eObjectiveStatus.failed != _eObjectiveStatus)
            {
                if (ObjectiveModBaseUnityPlugin.GetGameRoundManager().GetPlayerHasJump())
                {
                    ObjectiveModBaseUnityPlugin.GetObjectiveFailureClientMessage().SendServer(1);
                }
            }
        }
    }
}
