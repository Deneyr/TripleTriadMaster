using Core.DataTypes;
using Core.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class Program
    {
        static void Main(string[] args)
        {
            CardLibrary lCardLibrary = CardSerializer.DeserializeCardLibrary("CardListV1.xml");

            int nb1 = lCardLibrary.CardList.Where(pElem => pElem.Rarity == 1).Count();
            int nb2 = lCardLibrary.CardList.Where(pElem => pElem.Rarity == 2).Count();
            int nb3 = lCardLibrary.CardList.Where(pElem => pElem.Rarity == 3).Count();
            int nb4 = lCardLibrary.CardList.Where(pElem => pElem.Rarity == 4).Count();
            int nb5 = lCardLibrary.CardList.Where(pElem => pElem.Rarity == 5).Count();

            Console.WriteLine(nb1);
            Console.WriteLine(nb2);
            Console.WriteLine(nb3);
            Console.WriteLine(nb4);
            Console.WriteLine(nb5);
        }
    }
}
