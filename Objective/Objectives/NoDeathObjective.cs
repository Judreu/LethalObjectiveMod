// @COPYRIGHT ZITROUILLE 2024

namespace LCObjectiveMod.Objectives
{
    //---------------------------------------------------------------------------------------------
    internal class NoDeathObjective : IObjective
    {
        //-----------------------------------------------------------------------------------------
        public override void Initialize()
        {
            _sDescription = "Aucun joueur ne doit mourir";
            _eObjectiveType = eObjectiveType.noDeathObjective;
            _eObjectiveStatus = eObjectiveStatus.running;
            _uClientIdWhoStartObjective = 0;
            _nCredit = 100;
        }

        //-----------------------------------------------------------------------------------------
        public override void Update()
        {
            
        }
    }
}
