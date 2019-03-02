using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Commands;
using Core.DataTypes;
using Core.Game;

namespace AI.MoveTree
{
    public class MoveLeaf : AMoveComponent
    {
        public MoveLeaf(MoveComposite pParent, ACommand pCommand) : base(pParent, pCommand)
        {
        }

        public override void ComputeFitnessAndClean(GameArea pGameArea)
        {
            int lPlointsPlayer = pGameArea.GetPointsPlayer(pGameArea.Player1);

            int lPlointsOtherPlayer = pGameArea.GetPointsPlayer(pGameArea.Player2);

            int lPointsDifference = lPlointsPlayer - lPlointsOtherPlayer;

            if (lPointsDifference > 0)
            {
                this.mFitnessPlayer1 = 3;
                this.mFitnessPlayer2 = 0;
            }
            else if (lPointsDifference < 0)
            {
                this.mFitnessPlayer1 = 0;
                this.mFitnessPlayer2 = 3;
            }
            else
            {
                this.mFitnessPlayer1 = 1;
                this.mFitnessPlayer2 = 1;
            }
        }

        public override AMoveComponent GetNextChild()
        {
            return null;
        }
    }
}
