// @COPYRIGHT ZITROUILLE 2024

namespace LCObjectiveMod.Objectives
{
    //---------------------------------------------------------------------------------------------
    internal class ReturnBodiesToShip : IObjective
    {
        //-----------------------------------------------------------------------------------------
        public override void Initialize()
        {
            _sDescription = "Ramener tous les cadavres dans le vaisseau";
            _eObjectiveType = eObjectiveType.returnBodiesToShip;
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
