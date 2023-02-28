using AI.AIEngines;
using Core.Commands;
using Core.DataTypes;
using Core.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI
{
    class Program
    {
        static void Main(string[] args)
        {
            CardLibrary lCardLibrary = CardSerializer.DeserializeCardLibrary("CardListV1.xml");

            CardDeck lCardDeck1 = lCardLibrary.CreateDeckFromCardNames(new string[] {
                "Asahi", "Ysayle", "Kan-E", "Fordola", "Hilda"
            });
            CardDeck lCardDeck2 = lCardLibrary.CreateDeckFromCardNames(new string[] {
                "Titan", "Ifrit", "Nero", "Thancred", "Baha"
            });

            /*CardDeck lCardDeck1 = new CardDeck();
            CardDeck lCardDeck2 = new CardDeck();

            int i = 0;
            for(i = 0; i < 5; i++)
            {
                lCardDeck1.AddCardToDeck(new CardInstance(lCardLibrary.CardList.ElementAt(i)));
            }

            for (i = 5; i < 10; i++)
            {
                lCardDeck2.AddCardToDeck(new CardInstance(lCardLibrary.CardList.ElementAt(i)));
            }*/

            BrutForceAI lBrutForceAI = new BrutForceAI();

            Console.WriteLine(lCardDeck1.ToString());
            Console.WriteLine(lCardDeck2.ToString());
            Console.WriteLine("--------------------------------");

            lBrutForceAI.InitializeSimulation(lCardDeck2, lCardDeck1, 0);

            List<ACommand> lListCommand = lBrutForceAI.GetBestMoves();

            foreach(ACommand lCommand in lListCommand)
            {
                Console.WriteLine(lCommand.ToString());
            }
            Console.ReadKey();
        }
    }
}
