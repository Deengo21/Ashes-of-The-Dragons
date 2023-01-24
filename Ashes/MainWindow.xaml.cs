using Ashes.Characters;
using Ashes.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using static Ashes.GameManager;
using static Ashes.MainWindow;
using static System.Net.Mime.MediaTypeNames;

namespace Ashes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Grid DynamicContent { get; set; } = new Grid();

        public const int BoardSize = 16;

        public const int BoardColumnSize = 70;
        public const int BoardRowSize = 70;
        private int playerTurn = 1;
        private Game game;
        public Layer<Character> characterLayer;
        public Layer<Resource> resourceLayer;
        List<Type> characterTypes = new List<Type>() 
        {
            typeof(Knight) ,
            typeof(Peasant) ,
            typeof(Sorcerer),
            typeof(Alchemist),
            typeof(Shaman),
            typeof(Bouncer),
            typeof(Dragon),
            typeof(Harlequin),
            typeof(Jester),
            typeof(Juggler),
            typeof(Merchant),
            typeof(Ninja),
            typeof(Raconteur),
            typeof(Ram),
            typeof(Saper),
            typeof(Sheikh),

        };
            

        
        
        public MainWindow()
        {
            InitializeComponent();
            
            game = new Game();
            


            MainScroll.Content = DynamicContent;
            for (int i = 0; i < BoardSize; i++)
            {
                DynamicContent.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(BoardColumnSize)
                });
                DynamicContent.RowDefinitions.Add(new RowDefinition
                {
                    Height = new GridLength(BoardRowSize)
                });
            }

           

            Layer<Field> fieldLayer = new Layer<Field>(BoardSize, BoardSize, new Dictionary<Type, int>
            {
                { typeof(Field), BoardSize * BoardSize }
            });
            List<int> resourcesCount = generateResourcesCount(BoardSize * BoardSize, 5);
            //Layer<Resource> resourceLayer = new Layer<Resource>(BoardSize, BoardSize, new Dictionary<Type, int >
            resourceLayer = new Layer<Resource>(BoardSize, BoardSize, new Dictionary<Type, int>
            {
                { typeof(Gold),resourcesCount[0] },
               { typeof(Steel),resourcesCount[1]},
                { typeof(LifeFountain),resourcesCount[2]},
                { typeof(Grain),resourcesCount[3]},
               { typeof(OilBarrel),resourcesCount[4]},


        }); ;

           
            characterLayer = new Layer<Character>(BoardSize, BoardSize, new Dictionary<Type, int>
            {
                
            });

            



            resourceLayer.Shuffle();
            generatePlayersCards(BoardSize, BoardSize);
            //characterLayer.Shuffle();
            //characterLayer.Serialize();
            //characterLayer.Deserialize();

            this.Closing += MainWindow_Closing;
        }



        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
        private void btnPlayCard_Click(object sender, RoutedEventArgs e)
        {
            if (sender is ComboBox cmb)
            {
                int index = Convert.ToInt32(cmb.Tag);
                Card selectedCard = (Card)cmb.SelectedItem;
                if (playerTurn == 1)
                {
                    playerTurn = 2;
                }
                else
                {
                    playerTurn = 1;
                }
            }
        }

        private List<int> generateResourcesCount(int boardSize, int resourcesCount)
        {
            List<int> resources = new List<int>();
            Random random = new Random();
            int boardSizeTemp = boardSize;
            for (int i = 0; i < resourcesCount - 1; i++)
            {

                int rInt = random.Next(0, boardSizeTemp);
                resources.Add(rInt);
                boardSizeTemp -= rInt;
            }
            resources.Add(boardSizeTemp);
            return resources;
        }
        void generatePlayersCards(int boardWidth, int boardHeight)
        {
            int charactersCount = 10;
            Random random = new Random();
            
            for (int i = 0; i < charactersCount; i++)
            {
                List<Type> cards = DrawCards();
               
                int choice = random.Next(0, cards.Count());
                Type card = cards[choice];
                int row = random.Next(0, boardHeight);
                int column = random.Next(0, boardWidth);
                characterLayer.AddCharacter(card, row, column);
                
            }

        }
        private List<Type> DrawCards()
        {
            return characterTypes;
            
            
        }
    }
}
