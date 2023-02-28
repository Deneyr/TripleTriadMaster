using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripleTriadMaster
{
    public class CardImageUtils
    {
        private Dictionary<string, string> mImageNameToPathDictionary;

        public CardImageUtils(string pCardImageHtml)
        {
            this.mImageNameToPathDictionary = new Dictionary<string, string>();

            using (StreamReader lStreamReader = new StreamReader(pCardImageHtml))
            {
                while (lStreamReader.Peek() >= 0)
                {
                    string lLine = string.Empty;
                    while (lStreamReader.Peek() >= 0 && (lLine = lStreamReader.ReadLine()).Contains("<li id=\"card") == false)
                    {
                    }

                    string lLineTotal = string.Empty;
                    while (lStreamReader.Peek() >= 0 && (lLine = lStreamReader.ReadLine()).Contains("</li>") == false)
                    {
                        lLineTotal += lLine + "\n";
                    }

                    string lName = string.Empty;
                    string lImagePath = string.Empty;
                    if(string.IsNullOrEmpty(lLineTotal) == false) { 
                        this.ParseCard(lLineTotal, out lName, out lImagePath);

                        if (string.IsNullOrEmpty(lName) == false && string.IsNullOrEmpty(lImagePath) == false)
                        {
                            this.mImageNameToPathDictionary.Add(lName, lImagePath);
                        }
                    }
                }
            }

            Console.WriteLine("dd");
        }

        private void ParseCard(string pLineTotal, out string pName, out string pImagePath)
        {
            string[] lLines = pLineTotal.Split('\n');

            string lLine = lLines.First(pElem => pElem.Contains("<img src=\""));
            string[] lSubLines = lLine.Split(new string[] { "fichiers/", "\" alt=\"\">" }, StringSplitOptions.RemoveEmptyEntries);
            pImagePath = "Cards @ ARR  Triple Triad - Final Fantasy XIV_fichiers" + Path.DirectorySeparatorChar + lSubLines[1];

            lLine = lLines.First(pElem => pElem.Contains("<div class=\"cardName\">"));
            lSubLines = lLine.Split(new string[] { "\">", "</a></div>" }, StringSplitOptions.RemoveEmptyEntries);
            pName = lSubLines[2].Replace("&amp;", "").Replace("amp;", "");
        }

        public string GetImagePathFromName(string pName)
        {
            if (this.mImageNameToPathDictionary.ContainsKey(pName))
            {
                return this.mImageNameToPathDictionary[pName];
            }
            return this.mImageNameToPathDictionary[pName.Replace(" Card", "")];
        }
    }
}
