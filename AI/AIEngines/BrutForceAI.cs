using AI.MoveTree;
using Core.Commands;
using Core.DataTypes;
using Core.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.AIEngines
{
    public class BrutForceAI
    {
        private GameArea mGameArea;

        public BrutForceAI()
        {
            this.mGameArea = new GameArea();
        }

        public void InitializeSimulation(CardDeck pCardDeck1, CardDeck pCardDeck2, int pPlayerTurn)
        {
            Player lPlayer1 = new Player("Player1", pCardDeck1);
            Player lPlayer2 = new Player("Player2", pCardDeck2);

            switch (pPlayerTurn)
            {
                case 0:
                    this.mGameArea.InitializeGame(lPlayer1, lPlayer2, lPlayer1, false);
                    break;
                case 1:
                    this.mGameArea.InitializeGame(lPlayer1, lPlayer2, lPlayer2, false);
                    break;
            }
        }

        public List<ACommand> GetBestMoves() 
        {
            MoveRoot lMoveRoot = new MoveRoot();

            lMoveRoot.GameAreaData = this.mGameArea.CreateSnapchot();

            AMoveComponent lCurrentComponent = lMoveRoot;
            AMoveComponent lNextComponent;

            lCurrentComponent.CreateChildren(this.mGameArea);

            // TO REMOVE

            /*ACommand lCommand = new PlayCardCommand(this.mGameArea.PlayerTurn, 1, 0, 0);
            lCommand.RunCommand();

            lCommand = new PlayCardCommand(this.mGameArea.PlayerTurn, 3, 2, 1);
            lCommand.RunCommand();

            lCommand = new PlayCardCommand(this.mGameArea.PlayerTurn, 2, 2, 0);
            lCommand.RunCommand();



            lCommand = new PlayCardCommand(this.mGameArea.PlayerTurn, 4, 1, 0);
            lCommand.RunCommand();

            lCommand = new PlayCardCommand(this.mGameArea.PlayerTurn, 0, 2, 2);
            lCommand.RunCommand();

            lMoveRoot.CreateChildren(this.mGameArea);*/

            // END 

            while ((lNextComponent = lCurrentComponent.GetNextChild()) != null || lCurrentComponent.Parent != null)
            {
                if(lNextComponent != null)
                {
                    lNextComponent.Command.RunCommand();

                    lNextComponent.CreateChildren(this.mGameArea);

                    lCurrentComponent = lNextComponent;
                }
                else
                {
                    lCurrentComponent.ComputeFitnessAndClean(this.mGameArea);

                    this.mGameArea.SetFromSnapshot(lCurrentComponent.Parent.GameAreaData);

                    lCurrentComponent = lCurrentComponent.Parent;
                }
            }

            lMoveRoot.ComputeFitnessAndClean(this.mGameArea);

            List<ACommand> lResult = new List<ACommand>();
            
            lCurrentComponent = lMoveRoot.BestChild;
            while(lCurrentComponent != null)
            {
                lResult.Add(lCurrentComponent.Command);
                lCurrentComponent = lCurrentComponent.BestChild;
            }

            return lResult;
        }

    }
}
