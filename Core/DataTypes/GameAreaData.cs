using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataTypes
{
    public class GameAreaData
    {
        private CardDeck mCardDeckPlayer1;

        private CardDeck mCardDeckPlayer2;

        private Tuple<Player, CardTemplate>[,] mGameSpace;

        public Player PlayerTurn
        {
            get;
            set;
        }

        public CardDeck CardDeckPlayer1
        {
            get
            {
                return this.mCardDeckPlayer1.Clone() as CardDeck;
            }
            set
            {
                this.mCardDeckPlayer1 = value.Clone() as CardDeck;
            }
        }

        public CardDeck CardDeckPlayer2
        {
            get
            {
                return this.mCardDeckPlayer2.Clone() as CardDeck;
            }
            set
            {
                this.mCardDeckPlayer2 = value.Clone() as CardDeck;
            }
        }


        public Tuple<Player, CardTemplate>[,] GameSpace
        {
            get
            {
                return this.mGameSpace.Clone() as Tuple<Player, CardTemplate>[,];
            }
            set
            {
                this.mGameSpace = value.Clone() as Tuple<Player, CardTemplate>[,];
            }
        }

    }
}
