using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataTypes
{
    public class CardDeck: ICloneable
    {
        private List<Tuple<bool, CardTemplate>> mCardTemplates;

        public List<Tuple<bool, CardTemplate>> CardTemplates
        {
            get
            {
                return this.mCardTemplates;
            }
        }

        public CardDeck()
        {
            this.mCardTemplates = new List<Tuple<bool, CardTemplate>>();
        }

        public void AddCardToDeck(CardInstance pCardInstance)
        {
            this.mCardTemplates.Add(new Tuple<bool, CardTemplate>(false, pCardInstance));
        }

        public CardTemplate PickCard(int pCardIndex)
        {
            if(this.mCardTemplates.ElementAt(pCardIndex).Item1 == false)
            {
                this.mCardTemplates[pCardIndex] = new Tuple<bool, CardTemplate>(true, this.mCardTemplates.ElementAt(pCardIndex).Item2);

                return this.mCardTemplates[pCardIndex].Item2;
            }

            return null;
        }

        public CardTemplate ReturnCard(int pCardIndex)
        {
            if (this.mCardTemplates.ElementAt(pCardIndex).Item1 == true)
            {
                this.mCardTemplates[pCardIndex] = new Tuple<bool, CardTemplate>(false, this.mCardTemplates.ElementAt(pCardIndex).Item2);

                return this.mCardTemplates[pCardIndex].Item2;
            }

            return null;
        }

        public List<CardTemplate> GetCardsPlayable()
        {
            return this.mCardTemplates.Where(pElem => pElem.Item1 == false).Select(pElem => pElem.Item2).ToList();
        }

        public IEnumerable<int> GetAvailableCards()
        {
            List<int> lListIndex = new List<int>();

            int i = 0;
            foreach(Tuple<bool, CardTemplate> lCardTemplate in this.mCardTemplates)
            {
                if(lCardTemplate.Item1 == false)
                {
                    lListIndex.Add(i);
                }
                i++;
            }

            return lListIndex;
        }

        public void ResetDeck()
        {
            for(int i = 0; i < this.mCardTemplates.Count; i++ )
            {
                this.mCardTemplates[i] = new Tuple<bool, CardTemplate>(false, this.mCardTemplates.ElementAt(i).Item2);
            }
        }

        public object Clone()
        {
            CardDeck lNewCardDeck = new CardDeck();

            lNewCardDeck.mCardTemplates = new List<Tuple<bool, CardTemplate>>(this.mCardTemplates);

            /*foreach(Tuple<bool, CardTemplate> lTuple in this.mCardTemplates)
            {
                mCardTemplates.Add(new Tuple<bool, CardTemplate>(lTuple.Item1, lTuple.Item2));
            }*/

            return lNewCardDeck;
        }
    }
}
