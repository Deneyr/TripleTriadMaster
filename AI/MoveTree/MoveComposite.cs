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
    public class MoveComposite : AMoveComponent
    {
        protected List<AMoveComponent> mChildren;

        protected int mCrossIndex;

        protected AMoveComponent mBestChild;

        public MoveComposite(MoveComposite pParent, ACommand pCommand) : base(pParent, pCommand)
        {
            this.mChildren = null;

            this.mCrossIndex = 0;
        }

        public List<AMoveComponent> Children { get => mChildren; }
        public int CrossIndex { get => mCrossIndex; }
        public override AMoveComponent BestChild { get => mBestChild; }

        public override void CreateChildren(GameArea pGameArea)
        {
            if(this.Children != null)
            {
                return;
            }

            base.CreateChildren(pGameArea);

            this.mChildren = new List<AMoveComponent>();

            Player lPlayerTurn = this.mGameAreaData.PlayerTurn;

            IEnumerable<int> lAvailableCards = lPlayerTurn.CardDeck.GetAvailableCards();
            IEnumerable<Tuple<int, int>> lAvailableFrames = pGameArea.GetAvailableFrames();

            Player lPlayer = pGameArea.Player2;
            if (this.mGameAreaData.PlayerTurn == pGameArea.Player1)
            {
                lPlayer = pGameArea.Player1;
            }

            if (lAvailableFrames.Count() == 1)
            {
                foreach (int lCardIndex in lAvailableCards)
                {
                    ACommand lCommand = new PlayCardCommand(lPlayer, lCardIndex, lAvailableFrames.ElementAt(0).Item1, lAvailableFrames.ElementAt(0).Item2);

                    MoveLeaf lMoveLeaf = new MoveLeaf(this, lCommand);

                    this.mChildren.Add(lMoveLeaf);
                }
            }
            else
            {
                foreach (int lCardIndex in lAvailableCards)
                {
                    foreach (Tuple<int, int> lFrame in lAvailableFrames)
                    {
                        ACommand lCommand = new PlayCardCommand(lPlayer, lCardIndex, lFrame.Item1, lFrame.Item2);

                        MoveComposite lMoveComposite = new MoveComposite(this, lCommand);

                        this.mChildren.Add(lMoveComposite);
                    }
                }
            }
        }

        public override AMoveComponent GetNextChild()
        {
            if(this.mCrossIndex < this.mChildren.Count)
            {
                return this.mChildren.ElementAt(this.mCrossIndex++);
            }
            return null;
        }

        public override void ComputeFitnessAndClean(GameArea pGameArea)
        {
            AMoveComponent lBestComponent = null;
            float lTotalFitnessPlayer1 = 0;
            float lTotalFitnessPlayer2 = 0;
            foreach (AMoveComponent lChild in this.mChildren)
            {
                if (this.mGameAreaData.PlayerTurn == pGameArea.Player1)
                {
                    if (lBestComponent == null || lBestComponent.FitnessPlayer1 < lChild.FitnessPlayer1)
                    {
                        lBestComponent = lChild;
                    }
                }
                else
                {
                    if (lBestComponent == null || lBestComponent.FitnessPlayer2 < lChild.FitnessPlayer2)
                    {
                        lBestComponent = lChild;
                    }
                }

                lTotalFitnessPlayer1 += lChild.FitnessPlayer1;
                lTotalFitnessPlayer2 += lChild.FitnessPlayer2;
            }

            float lTotal = (lTotalFitnessPlayer2 + lTotalFitnessPlayer1);
            float lScalarPlayer1;
            float lScalarPlayer2;
            if (lTotal == 0)
            {
                lScalarPlayer1 = 0;
                lScalarPlayer2 = 0;
            }
            else
            {
                lScalarPlayer1 = lTotalFitnessPlayer1 / lTotal;
                lScalarPlayer2 = lTotalFitnessPlayer2 / lTotal;
            }

            this.mFitnessPlayer1 = lBestComponent.FitnessPlayer1 + 1 * lScalarPlayer1;
            this.mFitnessPlayer2 = lBestComponent.FitnessPlayer2 + 1 * lScalarPlayer2;

            foreach (AMoveComponent lChild in this.mChildren)
            {
                if(lChild != lBestComponent)
                {
                    lChild.Dispose();
                }
            }

            this.mChildren.Clear();

            this.mBestChild = lBestComponent;
        }
    }
}
