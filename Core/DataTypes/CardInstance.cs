using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataTypes
{
    public class CardInstance: CardTemplate
    {
        #region Fields

        private CardTemplate mCardTemplate;

        #endregion Fields

        #region Properties

        public override int Id
        {
            get
            {
                return this.mCardTemplate.Id;
            }
            set
            {

            }
        }

        public override int Rarity
        {
            get
            {
                return this.mCardTemplate.Rarity;
            }
            set
            {

            }
        }

        public override int TopValue
        {
            get
            {
                return this.mCardTemplate.TopValue;
            }
            set
            {

            }
        }

        public override int RightValue
        {
            get
            {
                return this.mCardTemplate.RightValue;
            }
            set
            {

            }
        }

        public override int BottomValue
        {
            get
            {
                return this.mCardTemplate.BottomValue;
            }
            set
            {

            }
        }

        public override int LeftValue
        {
            get
            {
                return this.mCardTemplate.LeftValue;
            }
            set
            {

            }
        }

        public override string Name
        {
            get
            {
                return this.mCardTemplate.Name;
            }
            set
            {

            }
        }

        #endregion

        #region Constructor

        public CardInstance(CardTemplate pCardTemplate)
        {
            this.mCardTemplate = pCardTemplate;
        }

        #endregion

    }
}
