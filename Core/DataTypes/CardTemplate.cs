using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Core.DataTypes
{
    [Serializable]
    public class CardTemplate
    {

        #region Properties

        [XmlIgnore]
        public virtual int Id
        {
            get;
            set;
        }

        public virtual int Rarity
        {
            get;
            set;
        }

        public virtual int TopValue
        {
            get;
            set;
        }

        public virtual int RightValue
        {
            get;
            set;
        }

        public virtual int BottomValue
        {
            get;
            set;
        }

        public virtual int LeftValue
        {
            get;
            set;
        }

        public virtual string Name
        {
            get;
            set;
        }

        #endregion

    }
}
