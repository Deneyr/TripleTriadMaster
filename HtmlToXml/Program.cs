using Core.DataTypes;
using Core.Serializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlToXml
{
    class Program
    {
        static void Main(string[] args)
        {

            /*for(long i = 0; i < 5000000000; i++)
            {
                int a = 5 + 5;
                if(a == 2)
                {
                    a = 5;
                }
                else
                {
                    a = 4;
                }
            }*/

            if (args.Length > 0)
            {
                foreach(string arg in args)
                {
                    if (File.Exists(arg))
                    {
                        CardLibrary lCardLibrary = new CardLibrary();

                        using (StreamReader lStreamReader = new StreamReader(arg))
                        {
                            while (lStreamReader.Peek() >= 0)
                            {
                                string lLineTotal = string.Empty;
                                string lLine;
                                while (lStreamReader.Peek() >= 0 && (lLine = lStreamReader.ReadLine()).Equals("</td></tr>") == false)
                                {
                                    lLineTotal += lLine + "\n";
                                }

                                CardTemplate lCardTemplate = ParseCardTemplate(lLineTotal);

                                lCardLibrary.CardList.Add(lCardTemplate);
                            }
                        }

                        CardSerializer.SerializeCardLibrary(lCardLibrary, Path.GetFileNameWithoutExtension(arg) + ".txt");
                    }
                }
            }
        }

        private static CardTemplate ParseCardTemplate(string lLineTotal)
        {
            CardTemplate lCardTemplate = new CardTemplate();

            string[] lLines = lLineTotal.Split('\n');

            string lLine = lLines.First(pElem => pElem.Contains("title="));
            string[] lSubLines = lLine.Split(new string[] { "\">", "</a>" }, StringSplitOptions.RemoveEmptyEntries);
            lCardTemplate.Name = lSubLines[1].Replace("&amp;", "").Replace("amp;", "");

            lLine = lLines.First(pElem => pElem.Contains("★"));
            lSubLines = lLine.Split(new string[] { "<td> ", "<br />" }, StringSplitOptions.RemoveEmptyEntries);
            lCardTemplate.Rarity = lSubLines[0].Count();

            int i = 0;
            foreach(string lStr in lLines)
            {
                lSubLines = lStr.Split(new string[] { "<td>", "</td>" }, StringSplitOptions.RemoveEmptyEntries);
                if(lSubLines.Length > 0)
                {
                    string lNumber = lSubLines[0];
                    int lResult;
                    if(int.TryParse(lNumber, out lResult))
                    {
                        switch (i)
                        {
                            case 0:
                                lCardTemplate.TopValue = lResult;
                                break;
                            case 1:
                                lCardTemplate.RightValue = lResult;
                                break;
                            case 2:
                                lCardTemplate.BottomValue = lResult;
                                break;
                            case 3:
                                lCardTemplate.LeftValue = lResult;
                                break;
                        }
                        i++;
                    }
                    else if (lNumber.Replace(" ", "").Equals("A"))
                    {
                        switch (i)
                        {
                            case 0:
                                lCardTemplate.TopValue = 10;
                                break;
                            case 1:
                                lCardTemplate.RightValue = 10;
                                break;
                            case 2:
                                lCardTemplate.BottomValue = 10;
                                break;
                            case 3:
                                lCardTemplate.LeftValue = 10;
                                break;
                        }
                        i++;
                    }
                }
            }

            return lCardTemplate;
        }
    }
}
