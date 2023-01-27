# Ashes-of-The-Dragons
The goal of the project is to create a strategy card game. The elements of the game will be fields and cards of different
characteristics. Each field is a potential place to lay the card. Cards are dealt on squares only in
initialization phase, i. e. at the beginning of the game. At a later stage, the players won't have any influence on the
the course of the game. The game is designed for two people. The goal of the game is to get as many gold units as possible.
Gold can be earned in many different ways, hence many different game strategies can ensure a win.
An inherent element of the game, however, is its random character. This randomness manifests itself on many levels, e. g.
the placement of different types of fields on the board is random.
The available types of fields are as follows:
• gold deposits
• fountains of life
• steel deposits
• cultivated fields
• oil fields
The board cosnsists of 256 squares (16x16).
Each card placed on a certain field brings out a certain number of units (depending on the type of
card) associated with the type of resource field. Resources can be used differently by available fields
cards.
The following properties can be associated with each card:
• the number of resource units it produces on a specific field resulting at initialization from the number of these cards
of its type on the fields
• random nickname
• mysterious power – depending on the type of card character
All mobile cards use the number of units of oil to move in proportion to the distance they traveled.
This also applies to teleportation. If a team does not have the right number of units of oil to move in
a given turn, it does not move.
During initialization, the cards are dealt by the players alternately in 7 turns. During each turn of the initialization phase, the player
decides which of 5 randomly generated cards should be set to an unused field (there are 5 new cards to choose from each round). Initialization phase turns do not generate
“activities” of cards or extract resources. The cards begin to fulfill their functions only in the second phase,
which is called the REM phase.
In the REM phase, the game comes to life and each card performs its assigned actions in the order of addition.
Then cards extract a certain number of units of the resource associated with the field on which they are located, then
if it is a moving unit, it moves to another field. In the REM phase, players have no real influence on
events that move the board. They can enjoy the gameplay presented by
visualization in WPF window. The REM phase should last about 256 rounds, after which the winner is selected i. e.
The player whose cards collected the most units of gold. If both players have the same number of units
the units of the resource extracted from the fountain of life will be decisive, if it does not determine the winner – count
will be the number of steel units. . . etc. 

Current Build - 0.5
-Added layers of a board (game field, resource layer and character layer)
-Added random generation of board
-Added random generation of 5 cards 
-Added random positioning of cards on board
-Added serialization and deserialization which allows to load progress
-Added basics of game logic
