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

    }
}
