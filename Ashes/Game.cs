using Ashes;
using Ashes.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ashes.GameManager;

namespace Ashes
{
    public enum PlayerId
    {
        PLAYER_1 = 1,
        PLAYER_2 = 2,
    }

    public enum TurnType
    {
        PLACE_CARD = 1,
        GAME_MOVE = 2,
    }
    public enum ResourceType 
    {
        NONE = 0,
        GOLD = 1,
        GRAIN = 2,
        
    }
    public enum CardType
    {
        PEASANT = 1,
        KNIGHT = 2,
        SORCERER = 3,
        //TALKER = "TALKER",
        // MISTYK = "MISTYK",
        // SZEJK = "SZEJK",
        // MISTRZ = "MISTRZ",
        // HANDLARZ = "HANDLARZ",
        // HARLEQUIN = "HARLEQUIN",
        // JOKER = "JOKER",
        // TARAN = "TARAN",
        // SMOK = "SMOK",
        // KUGLARZ = "KUGLARZ",
        // SAPER = "SAPER",
        // WYKIDAJŁO = "WYKIDAJŁO",
    }

    public abstract class Turn
    {
        public abstract TurnType TURN_TYPE { get; }
    }

    public class PlayerPlacesCardTurn : Turn
    {
        //public override TurnType TURN_TYPE => TurnType.PLACE_CARD;
        public override TurnType TURN_TYPE { get { return TurnType.PLACE_CARD; } }
        public List<CardType> cards { get; set; } = Enumerable.Repeat(default(CardType), 5).ToList();
        public PlayerPlacesCardTurn()
        {
            for (int i = 0; i < cards.Count; i++)
            {
                cards[i] = (CardType)getRandomEnumValue(typeof(CardType));
            }
        }
      
        private static T getRandomEnumValue<T>()
        {
            var values = Enum.GetValues(typeof(T));
            var random = new Random();
            return (T)values.GetValue(random.Next(values.Length));
        }

        private static object getRandomEnumValue(Type type)
        {
            throw new NotImplementedException();
        }

        public PlayerPlacesCardTurn(PlayerId playerId)
        {
            this.playerId = playerId;
        }

        public readonly PlayerId playerId;
    }
    public class GameMakesMove : Turn
    {
        public override TurnType TURN_TYPE => TurnType.GAME_MOVE;

        public GameMakesMove()
        {
        }
        
        Turn[] turnsToExecute = Enumerable.Repeat(1, config.game.numberOfPlayerTurns)
    .SelectMany(x => new Turn[] { new PlayerPlacesCardTurn(PlayerId.PLAYER_1), new PlayerPlacesCardTurn(PlayerId.PLAYER_2) })
    .Concat(Enumerable.Repeat(1, config.game.numberOfGameTurns).Select(x => new GameMakesMove()))
    .ToArray();
    }

    

    public class GameManager
    {
        private readonly Turn[] turnsToExecute;
        private int currentTurn = 0;
        public GameManager(GameBoard gameBoard)
        {
            this.turnsToExecute = turnsToExecute;
            this.gameBoard = gameBoard;
        }

        private readonly GameBoard gameBoard;
        public void executeTurn()
        {
            var fieldsWithCard = gameBoard.getFields().Where(field => field.card != null);
            Console.WriteLine(fieldsWithCard);
            foreach (var field in fieldsWithCard)
            {
                field.card?.onAction(gameBoard);
            }
        }

        private void goToNextTurn()
        {
            this.currentTurn++;
        }
        public Turn getCurrentTurn()
        {
            return this.turnsToExecute[this.currentTurn];
        }

        public int getTurnNo()
        {
            return this.currentTurn;
        }

        public void placeCard(CardType cardType, Position position)
        {
            var currentTurn = (PlayerPlacesCardTurn)this.getCurrentTurn();

            this.gameBoard.placeCard(cardType, position, currentTurn.playerId);
            this.goToNextTurn();
        }
        private static T GetRandomEnumValue<T>()
        {
            var v = Enum.GetValues(typeof(T));
            return (T)v.GetValue(new Random().Next(v.Length));
        }

        public class GameBoard
        {
            public void placeCard(CardType cardType, Position position, PlayerId playerId)
            {
                throw new NotImplementedException();
            }
            public Field[] getFields()
            {
                throw new NotImplementedException();
            }
        }

        public class Field
        {
            public Card card { get; set; }
        }

        public class Card
        {
            public void onAction(GameBoard gameBoard)
            {
                throw new NotImplementedException();
            }
        }

        public struct Position
        {
        }

        public class config
        {
            public static Game game { get; set; }
        }

        public class Game
        {
            public int numberOfPlayerTurns { get; set; }
            public int numberOfGameTurns { get; set; }
        }
    }

    
}



