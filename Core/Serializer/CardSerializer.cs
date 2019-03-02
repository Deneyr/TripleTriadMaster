using Core.DataTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Core.Serializer
{
    public static class CardSerializer
    {
        #region Methods

        public static void SerializeCardLibrary(CardLibrary pCardLibrary, string pPath)
        {
            XmlSerializer lXmlSerializer = new XmlSerializer(typeof(CardLibrary));
            using (StreamWriter lStreamWriter = new StreamWriter(pPath))
            {
                lXmlSerializer.Serialize(lStreamWriter, pCardLibrary);
            }
        }

        public static CardLibrary DeserializeCardLibrary(string pPath)
        {
            XmlSerializer lXmlSerializer = new XmlSerializer(typeof(CardLibrary));
            using (StreamReader lStreamReader = new StreamReader(pPath))
            {
                CardLibrary lCardLibrary = lXmlSerializer.Deserialize(lStreamReader) as CardLibrary;

                int lId = 0;
                foreach(CardTemplate cardTemplate in lCardLibrary.CardList)
                {
                    cardTemplate.Id = lId;
                    lId++;
                }

                return lCardLibrary;
            }
        }

        #endregion

    }
}
