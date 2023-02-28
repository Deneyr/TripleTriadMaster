using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Core.DataTypes
{
    [Serializable]
    public class CardLibrary
    {
        #region Fields

        [XmlArray("CardList")]
        [XmlArrayItem("Card")]
        public List<CardTemplate> CardList
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public CardLibrary()
        {
            this.CardList = new List<CardTemplate>();
        }

        #endregion

        #region

        public CardDeck CreateDeckFromCardNames(string[] lCardsName)
        {
            CardDeck lCardDeck = new CardDeck();

            foreach(string lCardName in lCardsName)
            {
                CardTemplate lCardTemplate = this.CardList.FirstOrDefault(pElem => pElem.Name.ToLower().Contains(lCardName.ToLower()));

                if(lCardTemplate != null)
                {
                    lCardDeck.AddCardToDeck(new CardInstance(lCardTemplate));
                }
            }

            return lCardDeck;
        }

        #endregion

    }
}
