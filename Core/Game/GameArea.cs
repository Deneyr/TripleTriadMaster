using Core.DataTypes;
using Core.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Game
{
    public class GameArea
    {
        private Player mPlayer1;

        private Player mPlayer2;

        private Player mPlayerTurn;

        private Player mPlayerStart;

        private Tuple<Player, CardTemplate>[,] mGameSpace;

        private bool mRegisterEvent;

        #region Events

        public event Action<GameArea> GameFinished;

        public event Action<GameArea> PlayerTurnOn;

        #endregion Events

        public Player PlayerTurn
        {
            get
            {
                return this.mPlayerTurn;
            }
        }

        public Player Player1 { get => mPlayer1; }
        public Player Player2 { get => mPlayer2; }

        public Tuple<Player, CardTemplate>[,] GameSpace { get => mGameSpace; }

        public GameArea()
        {
            this.mGameSpace = new Tuple<Player, CardTemplate>[3, 3];
            for (int i = 0 ; i < this.mGameSpace.GetLength(0); i++)
            {
                for (int j = 0; j < this.mGameSpace.GetLength(1); j++)
                {
                    this.mGameSpace[i, j] = null;
                }
            }
        }

        public void InitializeGame(Player pPlayer, Player pPlayer2, Player pPlayerTurn, bool pRegisterEvent = true)
        {
            this.mPlayer1 = pPlayer;

            this.mPlayer2 = pPlayer2;

            this.mPlayer1.CardDeck.ResetDeck();
            this.mPlayer2.CardDeck.ResetDeck();

            this.mRegisterEvent = pRegisterEvent;

            for (int i = 0; i < this.mGameSpace.GetLength(0); i++)
            {
                for (int j = 0; j < this.mGameSpace.GetLength(1); j++)
                {
                    this.mGameSpace[i, j] = null;
                }
            }

            this.mPlayerTurn = pPlayerTurn;
            this.mPlayerStart = this.mPlayerTurn;

            this.mPlayer1.GameArea = this;
            this.mPlayer2.GameArea = this;
        }

        public bool PlayCard(Player pPlayer, int pIndexCard, int pYPosition, int pXPosition)
        {
            if(pXPosition > 3 || pYPosition > 3 ||
                pXPosition < 0 || pYPosition < 0)
            {
                return false;
            }

            if (this.mGameSpace[pYPosition, pXPosition] != null)
            {
                return false;
            }

            if(this.mPlayerTurn != pPlayer)
            {
                return false;
            }

            CardTemplate lCardTemplate = this.PlayerTurn.CardDeck.PickCard(pIndexCard);
            if(lCardTemplate == null)
            {
                return false;
            }

            this.mGameSpace[pYPosition, pXPosition] = new Tuple<Player, CardTemplate>(this.mPlayerTurn, lCardTemplate);



            if(pYPosition - 1 >= 0)
            {
                if(this.mGameSpace[pYPosition - 1, pXPosition] != null
                    && this.mGameSpace[pYPosition - 1, pXPosition].Item1 != this.mPlayerTurn 
                    && this.mGameSpace[pYPosition - 1, pXPosition].Item2.BottomValue < this.mGameSpace[pYPosition, pXPosition].Item2.TopValue)
                {
                    this.mGameSpace[pYPosition - 1, pXPosition] = new Tuple<Player, CardTemplate>(this.mPlayerTurn, this.mGameSpace[pYPosition - 1, pXPosition].Item2);
                }
            }

            if (pYPosition + 1 < 3)
            {
                if (this.mGameSpace[pYPosition + 1, pXPosition] != null
                    && this.mGameSpace[pYPosition + 1, pXPosition].Item1 != this.mPlayerTurn
                    && this.mGameSpace[pYPosition + 1, pXPosition].Item2.TopValue < this.mGameSpace[pYPosition, pXPosition].Item2.BottomValue)
                {
                    this.mGameSpace[pYPosition + 1, pXPosition] = new Tuple<Player, CardTemplate>(this.mPlayerTurn, this.mGameSpace[pYPosition + 1, pXPosition].Item2);
                }
            }

            if (pXPosition - 1 >= 0)
            {
                if (this.mGameSpace[pYPosition, pXPosition - 1] != null
                    && this.mGameSpace[pYPosition, pXPosition - 1].Item1 != this.mPlayerTurn
                    && this.mGameSpace[pYPosition, pXPosition - 1].Item2.RightValue < this.mGameSpace[pYPosition, pXPosition].Item2.LeftValue)
                {
                    this.mGameSpace[pYPosition, pXPosition - 1] = new Tuple<Player, CardTemplate>(this.mPlayerTurn, this.mGameSpace[pYPosition, pXPosition - 1].Item2);
                }
            }

            if (pXPosition + 1 < 3)
            {
                if (this.mGameSpace[pYPosition, pXPosition + 1] != null
                    && this.mGameSpace[pYPosition, pXPosition + 1].Item1 != this.mPlayerTurn
                    && this.mGameSpace[pYPosition, pXPosition + 1].Item2.LeftValue < this.mGameSpace[pYPosition, pXPosition].Item2.RightValue)
                {
                    this.mGameSpace[pYPosition, pXPosition + 1] = new Tuple<Player, CardTemplate>(this.mPlayerTurn, this.mGameSpace[pYPosition, pXPosition + 1].Item2);
                }
            }

            bool lIsGameOver = true;
            for (int i = 0; i < this.mGameSpace.GetLength(0); i++)
            {
                for (int j = 0; j < this.mGameSpace.GetLength(1); j++)
                {
                    if(this.mGameSpace[i, j] == null)
                    {
                        lIsGameOver = false;
                    }
                }
            }

            if (this.mPlayerTurn == this.mPlayer1)
            {
                this.mPlayerTurn = this.mPlayer2;
            }
            else
            {
                this.mPlayerTurn = this.mPlayer1;
            }

            if (lIsGameOver)
            {
                this.UnregisterPlayer(this.mPlayer1);
                this.UnregisterPlayer(this.mPlayer2);

                this.NotifyGameFinished();
            }
            else
            {
                this.NotifyPlayerTurn();
            }

            return true;
        }

        public int GetPointsPlayer(Player pPlayer)
        {
            int nbPoints = 0;

            if (this.PlayerTurn != this.mPlayerStart)
            {
                if (this.mPlayerTurn == pPlayer)
                {
                    nbPoints++;
                }
            }

            for (int i = 0; i < this.mGameSpace.GetLength(0); i++)
            {
                for (int j = 0; j < this.mGameSpace.GetLength(1); j++)
                {
                    if (this.mGameSpace[i, j] != null && this.mGameSpace[i, j].Item1 == pPlayer)
                    {
                        nbPoints++;
                    }
                }
            }

            return nbPoints;
        }

        public IEnumerable<Tuple<int, int>> GetAvailableFrames()
        {
            List<Tuple<int, int>> lGetAvailableFrames = new List<Tuple<int, int>>();

            for (int i = 0; i < this.mGameSpace.GetLength(0); i++)
            {
                for (int j = 0; j < this.mGameSpace.GetLength(1); j++)
                {
                    if (this.mGameSpace[i, j] == null)
                    {
                        lGetAvailableFrames.Add(new Tuple<int, int>(i, j));
                    }
                }
            }

            return lGetAvailableFrames;
        }

        public int GetPointsOtherPlayer(Player pPlayer)
        {
            if(this.mPlayer1 == pPlayer)
            {
                return this.GetPointsPlayer(this.mPlayer2);
            }

            return this.GetPointsPlayer(this.mPlayer1);
        }

        public GameAreaData CreateSnapchot()
        {
            GameAreaData lSnapchot = new GameAreaData();

            lSnapchot.PlayerTurn = this.mPlayerTurn;

            lSnapchot.CardDeckPlayer1 = this.mPlayer1.CardDeck;
            lSnapchot.CardDeckPlayer2 = this.mPlayer2.CardDeck;

            lSnapchot.GameSpace = this.mGameSpace;

            return lSnapchot;
        }

        public void SetFromSnapshot(GameAreaData pGameAreaData)
        {
            this.mPlayerTurn = pGameAreaData.PlayerTurn;

            this.mPlayer1.CardDeck = pGameAreaData.CardDeckPlayer1;
            this.mPlayer2.CardDeck = pGameAreaData.CardDeckPlayer2;

            this.mGameSpace = pGameAreaData.GameSpace;
        }

        public void RegisterPlayer(Player pPlayer)
        {
            if (this.mRegisterEvent)
            {
                this.PlayerTurnOn -= pPlayer.OnPlayerTurn;
                this.PlayerTurnOn += pPlayer.OnPlayerTurn;
            }
        }

        public void UnregisterPlayer(Player pPlayer)
        {
            if (this.mRegisterEvent)
            {
                this.PlayerTurnOn -= pPlayer.OnPlayerTurn;
            }
        }

        private void NotifyPlayerTurn()
        {
            this.PlayerTurnOn?.Invoke(this);
        }

        private void NotifyGameFinished()
        {
            this.GameFinished?.Invoke(this);
        }

    }
}
