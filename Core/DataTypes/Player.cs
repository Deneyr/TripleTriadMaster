using Core.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataTypes
{
    public class Player
    {
        private string mPlayerName;

        private CardDeck mCardDeck;

        private GameArea mGameArea;

        #region Events

        public event Action<GameArea> PlayerTurnOn;

        #endregion

        public string PlayerName
        {
            get
            {
                return this.mPlayerName;
            }
        }

        public CardDeck CardDeck
        {
            get
            {
                return this.mCardDeck;
            }

            set
            {
                this.mCardDeck = value;
            }
        }

        public GameArea GameArea
        {
            get
            {
                return this.mGameArea;
            }
            set
            {
                if(this.mGameArea != null)
                {
                    this.mGameArea.UnregisterPlayer(this);
                }

                this.mGameArea = value;

                if (this.mGameArea != null)
                {
                    this.mGameArea.RegisterPlayer(this);
                }
            }
        }

        public Player(string pPlayerName, CardDeck pCardDeck)
        {
            this.mPlayerName = pPlayerName;
            this.mCardDeck = pCardDeck;
            this.mGameArea = null;
        }

        public void PlayCard(int pCardIndex, int pYPosition, int pXPosition)
        {
            if(this.mGameArea != null
                && pCardIndex < this.CardDeck.CardTemplates.Count
                && this.CardDeck.CardTemplates[pCardIndex].Item1 == false)
            {
                this.mGameArea.PlayCard(this, pCardIndex, pYPosition, pXPosition);
            }
        }

        public void OnPlayerTurn(GameArea pGameArea)
        {
            if(pGameArea.PlayerTurn == this)
            {
                this.NotifyPlayerTurn(pGameArea);
            }
        }

        private void NotifyPlayerTurn(GameArea pGameArea)
        {
            this.PlayerTurnOn?.Invoke(pGameArea);
        }
    }
}
