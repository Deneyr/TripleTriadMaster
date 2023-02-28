using Core.DataTypes;
using Core.Game;
using Core.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TripleTriadMaster
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameArea mGameArea;

        private CardImageUtils mCardImageUtils;

        private Dictionary<Image, string> mBitmapImageDictionary;

        public MainWindow()
        {
            this.mCardImageUtils = new CardImageUtils(@"Cards @ ARR  Triple Triad - Final Fantasy XIV.html");

            this.mGameArea = new GameArea();

            CardLibrary lCardLibrary = CardSerializer.DeserializeCardLibrary("CardListV1.xml");

            CardDeck lCardDeck1 = lCardLibrary.CreateDeckFromCardNames(new string[] {
                "Asahi", "Ysayle", "Kan-E", "Fordola", "Hilda"
            });
            CardDeck lCardDeck2 = lCardLibrary.CreateDeckFromCardNames(new string[] {
                "Titan", "Ifrit", "Nero", "Thancred", "Baha"
            });

            InitializeComponent();

            this.mBitmapImageDictionary = new Dictionary<Image, string>();
            this.mBitmapImageDictionary.Add(this.mImage00, string.Empty);
            this.mBitmapImageDictionary.Add(this.mImage01, string.Empty);
            this.mBitmapImageDictionary.Add(this.mImage02, string.Empty);

            this.mBitmapImageDictionary.Add(this.mImage10, string.Empty);
            this.mBitmapImageDictionary.Add(this.mImage11, string.Empty);
            this.mBitmapImageDictionary.Add(this.mImage12, string.Empty);

            this.mBitmapImageDictionary.Add(this.mImage20, string.Empty);
            this.mBitmapImageDictionary.Add(this.mImage21, string.Empty);
            this.mBitmapImageDictionary.Add(this.mImage22, string.Empty);

            this.mGameArea.PlayerTurnOn += this.OnPlayerTurnOn;
            this.mGameArea.GameFinished += this.OnGameEnd;

            this.InitializeSimulation(lCardDeck1, lCardDeck2, 0);

            this.mGameArea.PlayCard(this.mGameArea.Player1, 1, 0, 1);
            this.mGameArea.PlayCard(this.mGameArea.Player2, 2, 1, 1);
            this.mGameArea.PlayCard(this.mGameArea.Player1, 4, 1, 2);
        }

        public void InitializeSimulation(CardDeck pCardDeck1, CardDeck pCardDeck2, int pPlayerTurn)
        {
            Player lPlayer1 = new Player("Player1", pCardDeck1);
            Player lPlayer2 = new Player("Player2", pCardDeck2);

            switch (pPlayerTurn)
            {
                case 0:
                    this.mGameArea.InitializeGame(lPlayer1, lPlayer2, lPlayer1, false);
                    break;
                case 1:
                    this.mGameArea.InitializeGame(lPlayer1, lPlayer2, lPlayer2, false);
                    break;
            }
        }

        private void OnPlayerTurnOn(GameArea pGameArea)
        {
            this.UpdateGameArea(pGameArea);
        }

        private void OnGameEnd(GameArea pGameArea)
        {

        }

        private void UpdateGameArea(GameArea pGameArea)
        {
            int i = 0;
            foreach(Tuple<Player, CardTemplate> lGameSpaceRow in pGameArea.GameSpace)
            {

                Image lImage = this.mImage00;
                switch(i)
                {
                    case 0:
                        lImage = this.mImage00;
                        break;
                    case 1:
                        lImage = this.mImage01;
                        break;
                    case 2:
                        lImage = this.mImage02;
                        break;

                    case 3:
                        lImage = this.mImage10;
                        break;
                    case 4:
                        lImage = this.mImage11;
                        break;
                    case 5:
                        lImage = this.mImage12;
                        break;

                    case 6:
                        lImage = this.mImage20;
                        break;
                    case 7:
                        lImage = this.mImage21;
                        break;
                    case 8:
                        lImage = this.mImage22;
                        break;
                }

                if (lGameSpaceRow != null)
                {
                    if (lGameSpaceRow.Item1 == pGameArea.Player1)
                    {
                        (lImage.Parent as Border).Background = Brushes.Blue;
                    }
                    else
                    {
                        (lImage.Parent as Border).Background = Brushes.Red;
                    }

                    string lImagePath = this.mCardImageUtils.GetImagePathFromName("Tonberry");
                    lImagePath = this.mCardImageUtils.GetImagePathFromName(lGameSpaceRow.Item2.Name);

                    if (this.mBitmapImageDictionary[lImage].Equals(lImagePath) == false)
                    {
                        this.mBitmapImageDictionary[lImage] = lImagePath;

                        BitmapImage lBitmapImage = new BitmapImage(new Uri(System.IO.Path.GetFullPath(lImagePath)));

                        // BitmapImage.UriSource must be in a BeginInit/EndInit block
                        /*lBitmapImage.BeginInit();
                        lBitmapImage.UriSource = new Uri(lImagePath);
                        lBitmapImage.EndInit();*/

                        lImage.Source = lBitmapImage;
                    }
                }
                else
                {
                    (lImage.Parent as Border).Background = Brushes.Transparent;

                    this.mBitmapImageDictionary[lImage] = string.Empty;
                    lImage.Source = null;
                }

                i++;
            }
        }
    }
}
