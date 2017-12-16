As you can see, I attempted to complete the majority of the core logic for a full chessgame with all the piece types.

I used a combination of design patterns which I will explain below.

I wanted to decouple the actual chesspiece objects from their moveset calculations and I decided to go with a Strategy design pattern.
The reason for this is we don't want to instantiate new chess piece objects everytime we need to calculate where their possible move locations could be.
This is important for determining whether the next move will put the current player's king in check - we just need to mock the next move and reset the board back to the original state before performing the actual move.

Each core move strategy is based on a direction since half of the chesspieces in the game can move multiple tiles in a single direction, I figured I get maximum reuse out of that.

Since the pawn has such unique moves, I used an adapter pattern to explicitly define what the pawn move strategy does differently from other move strategies.

I wanted to keep track of which player's turn it is as well as other data like score, win/lose/draw, and for this I needed to use a Mediator design pattern.
The ChessGame object is the mediator that will manage whether a specific player has a turn available to take.

Since C# does not support Switch statements where each of the cases are typeof() evaluations, I needed to use a visitor design pattern.
If we attempted to .GetType().ToString() we would get a string akin to Chess.Domain.Pawn, which means that if the namespace for that class is ever changed, we would need to change the switch statement as well.
By using the visitor design pattern, the ChessBoard can 'visit' each chesspiece and get a hardcoded value for each type which is much  better suited for a switch statment. (see UpdateBoardState())

Initially I was thinking of going with a singleton pattern for the chessboard, but I decided against that since global static instances are rarely used in web apps, and is not a good OOP design pattern due to potential resource contention between multiple chessgames.
