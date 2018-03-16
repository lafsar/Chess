
I fixed up/changed some of the pawn unit tests (Especially the ones that weren't following chess rules), added Queen and King unit tests, and moved the board unit tests to another file.
For this app, I assumed that Black pieces start at the top (row 0-1) and White pieces are at the bottom of the board. That way, we can have legal pawn moves.
As per chess rules, White is assumed to go first.

I used a combination of design patterns which I will explain below.

I wanted to decouple the actual chesspiece objects from their moveset calculations so I decided to go with a Strategy design pattern.
The reason for this is we don't want to instantiate new chess piece objects everytime we need to calculate where their possible move locations could be.
This is important for determining whether the next move will put the current player's king in check - we just need to mock the next move and reset the board back to the original state before performing the actual move.

Each core move strategy is based on a direction since half of the chesspieces in the game can move multiple tiles in a single direction, I figured I get maximum re-use out of that. This is sort of a decorator pattern since the Knight, Queen and King are dynamically adding functionality to the abstract BaseMoveStrategy.

Since the pawn has such unique moves, I used an adapter pattern to explicitly define what the pawn move strategy does differently from other move strategies.

I wanted to keep track of which player's turn it is as well as other data like score, win/lose/draw, and for this I needed to use a Mediator design pattern.
The ChessGame object is the mediator that will manage whether a specific player has a turn available to take.

Since C# does not support Switch statements where each of the cases are typeof() evaluations, I needed to use a visitor design pattern.
If we attempted to .GetType().ToString() we would get a string akin to Chess.Domain.Pawn, which means that if the namespace for that class is ever changed, we would need to change the switch statement as well.
By using the visitor design pattern, the ChessBoard can 'visit' each chesspiece and get a hardcoded value for each type which is much better for a switch statment. (see UpdateBoardState())

For the abstract ChessPiece class, I used a sort of Template design pattern to control pawn-specific logic at the appropriate runtime. (BeforeMove, AfterMove)

Initially, I was thinking of going with a singleton pattern for the chessboard, but I decided against that since global static instances are rarely used in web apps, and it is not a good OOP design pattern in this case due to potential resource contention between multiple chessgames.

I would have opted for using HashSets for storing the tuples since we have no need for duplicates and ordering, but it is a pain to cast from IEnumerable to HashSet.
IEnumerables were used so I could take advantage of concise control flow and more condensed code via yield returns and lazy evaluation when it's needed.

-Luke Afsar
