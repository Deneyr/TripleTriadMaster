using Core.Commands;
using Core.DataTypes;
using Core.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.MoveTree
{
    public abstract class AMoveComponent: IDisposable
    {
        protected GameAreaData mGameAreaData;

        private MoveComposite mParent;

        protected ACommand mCommand;

        protected float mFitnessPlayer1;
        protected float mFitnessPlayer2;

        public AMoveComponent(MoveComposite pParent, ACommand pCommand)
        {
            this.mParent = pParent;

            this.mGameAreaData = null;

            this.mCommand = pCommand;

            this.mFitnessPlayer1 = float.MinValue;
            this.mFitnessPlayer2 = float.MinValue;
        }

        public float FitnessPlayer1 { get => mFitnessPlayer1; set => mFitnessPlayer1 = value; }
        public float FitnessPlayer2 { get => mFitnessPlayer2; set => mFitnessPlayer2 = value; }
        public ACommand Command { get => mCommand; }
        public GameAreaData GameAreaData { get => mGameAreaData; set => mGameAreaData = value; }
        public MoveComposite Parent { get => mParent; }
        public virtual AMoveComponent BestChild
        {
            get
            {
                return null;
            }
        }

        public virtual void CreateChildren(GameArea pGameArea)
        {
            this.mGameAreaData = pGameArea.CreateSnapchot();
        }

        public abstract AMoveComponent GetNextChild();

        public abstract void ComputeFitnessAndClean(GameArea pGameArea);

        public virtual GameArea LoadGameAreaData(GameArea pGameArea)
        {
            pGameArea.SetFromSnapshot(this.mGameAreaData);

            return pGameArea;
        }

        public void Dispose()
        {
            this.mParent = null;
        }
    }
}
