using System;
using static System.Console;

namespace Bme121.Pa2
{
    // Procedural-programming implementation of the game Connect Four.
    // Note: Connect Four is (C) Hasbro, Inc.
    // This version is for educational use only.
    
    static partial class Program
    {
        // For random moves by AI players.
        static Random randomNumberGenerator = new Random( );
        
        // The game board and intuitive names for its size.
        // Each element of the game board is initially null.
        // In valid play, an element may become "O" or "X".
        static string[ , ] gameBoard = new string[ 6, 7 ];
        static readonly int gameRows = gameBoard.GetLength( 0 );
        static readonly int gameCols = gameBoard.GetLength( 1 );
        
        // The playing piece colors can be altered.
        static readonly ConsoleColor fgColor = ForegroundColor;
        static readonly ConsoleColor xColor = ConsoleColor.Cyan;
        static readonly ConsoleColor oColor = ConsoleColor.Magenta;
        
        // Save the two player's names and kinds (human or AI).
        static string xName, oName;
        static string xKind, oKind;
        
        // The symbol (O or X), name, and kind of the current player.
        static string currentPlayer;
        static string currentPlayerName;
        static string currentPlayerKind;
    
        // Play the game, largely by calling game methods.
        static void Main( )
        {
            WriteLine( );
            WriteLine( "BME 121 Connect Four! visit www.manthanshah.com" );
            WriteLine( );
            WriteLine( "The game of Connect Four is (C) Hasbro, Inc." );
            WriteLine( "This version is for educational use only." );
            WriteLine( );
            WriteLine( "Play by stacking your token in any column with available space." );
            WriteLine( "Win with four-in-a-row vertically, horizontally, or diagonally." );
            
            DrawGameBoard( );
            
            GetPlayerNames( );
            GetPlayerKinds( );
            GetFirstPlayer( );
            
            while( ! IsBoardFull( ) )
            {
                GetPlayerMove( );
                DrawGameBoard( );
                
                if( CurrentPlayerWins( ) )
                {
                    WriteLine( );
                    Write( $"{currentPlayerName} - " );
                    ColorWrite( currentPlayer );
                    WriteLine( " - wins!" );
                    WriteLine( );
                    return;
                }
                
                SwitchPlayers( );
            }
            
            WriteLine( );
            WriteLine( "Game is a draw!" );
            WriteLine( );
        }
        
        // Get the displayed names of the two players.
        static void GetPlayerNames( )
        {
            WriteLine( );
            Write( "Enter player O name: " );
            oName = ReadLine( );
            Write( "Enter player X name: " );
            xName = ReadLine( );
            
            while(oName == xName)
            {
                Write("Please enter another name for X: ");
                xName = ReadLine();
            }
        }
        
        // Get the kinds (human or ai) of the two players.
        static void GetPlayerKinds( )
        {
            WriteLine( );
            
            while( true )
            {
                Write( "Enter player O kind [human ai]: " );
                oKind = ReadLine( ).ToLower( );
                if( oKind == "human" ) break;
                if( oKind == "ai" ) break;
                WriteLine( "Must be one of 'human' or 'ai'." );
                WriteLine( "Please try again." );
            }
            
            while( true )
            {
                Write( "Enter player X kind [human ai]: " );
                xKind = ReadLine( ).ToLower( );
                if( xKind == "human" ) break;
                if( xKind == "ai" ) break;
                WriteLine( "Must be one of 'human' or 'ai'." );
                WriteLine( "Please try again." );
            }
        }
        
        // Get and set up the player who will play first.
        static void GetFirstPlayer( )
        {
            WriteLine( );
            
            while( true )
            {
                Write( "Enter first to play [O X]: " );
                currentPlayer = ReadLine( ).ToUpper( );
                if( currentPlayer == "O" ) break;
                if( currentPlayer == "X" ) break;
                WriteLine( "Must be one of 'O' or 'X'." );
                WriteLine( "Please try again." );
            }
            
            if( currentPlayer == "O" )
            {
                currentPlayerName = oName; 
                currentPlayerKind = oKind;
            }
            
            if( currentPlayer == "X" )
            {
                currentPlayerName = xName;
                currentPlayerKind = xKind;
            }
        }
        
        // Get and perform the desired move by the current player.
        static void GetPlayerMove( )
        {
            if( currentPlayerKind == "ai" ) 
            {
                WriteLine( );
                Write( $"{currentPlayerName} - " );
                ColorWrite( currentPlayer );
                Write( " - choose a column: " );
                int column = SelectRandomColumn( );
                System.Threading.Thread.Sleep( 1000 );
                Write( column );
                System.Threading.Thread.Sleep( 1000 );
                WriteLine( );
                PlayInColumn( column );
            }
            
            if( currentPlayerKind == "human" )
            {
                while( true )
                {
                    WriteLine( );
                    Write( $"{currentPlayerName} - " );
                    ColorWrite( currentPlayer );
                    Write( " - choose a column: " );
                    int column;
                    if( ! int.TryParse( ReadLine( ), out column ) || ! IsValidPlay( column ) )
                    {
                        WriteLine( "Not a valid column or column is full." );
                        WriteLine( "Please try again." );
                    }
                    else
                    {
                        PlayInColumn( column );
                        break;
                    }
                }
            }
        }
        
        // Detect whether the current player has won by looking for a vertical,
        // horizontal, or diagonal run of four of the current player's symbols.
        static bool CurrentPlayerWins( )
        {

            for( int row = 0; row < gameRows; row++)
            {
                for( int col = 0; col < gameCols; col++ )
                {
                    //horizontal
                    if( col <= 3 )
                    {
                        if( gameBoard[row,col] == currentPlayer && gameBoard[row,col+1] == currentPlayer && gameBoard[row,col+2] == currentPlayer && gameBoard[row,col+3] == currentPlayer ) 
                            return true;
                    }
                    
                    //vertical
                    if( row<=2 )
                    {
                        if( gameBoard[row,col] == currentPlayer && gameBoard[row+1,col] == currentPlayer && gameBoard[row+2,col] == currentPlayer && gameBoard[row+3,col] == currentPlayer ) 
                            return true;
                    }
                    
                    //diagnoal right up
                    if( row <= 2 && col <= 3 )
                    {
                        if( gameBoard[row,col] == currentPlayer && gameBoard[row+1,col+1] == currentPlayer && gameBoard[row+2,col+2] == currentPlayer && gameBoard[row+3,col+3] == currentPlayer ) 
                            return true;
                    }
                    
                    //diagonal left up
                    if(row <= 2 && col >= 3 )
                    {
                        if( gameBoard[row ,col] == currentPlayer && gameBoard[row+1,col-1] == currentPlayer && gameBoard[row+2,col-2] == currentPlayer && gameBoard[row+3,col-3] == currentPlayer ) 
                            return true;
                    }
                }
            }
            
            return false;
        }
        
        // Detect whether the game board is completely filled.
        static bool IsBoardFull( )
        {
            for( int row = 0; row < gameRows; row++ )
            {
                for( int col = 0; col < gameCols; col++ )
                {
                    if( gameBoard[ row, col ] == null) return false;
                }
            }
            
            return true;
        }
        
        // Detect whether given column is on the board and has space remaining.
        static bool IsValidPlay( int col )
        {
            if( col >= gameCols ) return false; 
            if( gameBoard[ gameRows-1, col ] == null )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        // Play current player's symbol on top of existing plays in the selected column.
        static void PlayInColumn(int col)
        {
            for ( int i = 0; i < gameRows; i++ )
            {
                if ( gameBoard[ i, col ] == null ) 
                {
                    gameBoard[ i, col ] = currentPlayer;
                    break;
                }
            }
        }
        
        // Select a column at random until a valid play is found.
        static int SelectRandomColumn( )
        {
            int colNum;
            while( true )
            {
                colNum = randomNumberGenerator.Next(0, 7);
                if( IsValidPlay( colNum ) ) break;
            }
            return colNum;
        }
        
        // Change the current player from player O to player X or vice versa.
        static void SwitchPlayers( )
        {
            if( currentPlayerName == oName )
            {
                currentPlayerName = xName;
                currentPlayerKind = xKind;
                currentPlayer = "X";
            }
            else
            {
                currentPlayerName = oName;
                currentPlayerKind = oKind;
                currentPlayer = "O";
            }
        }
        
        // Display the current game board on the console.
        // This version uses only ASCII characters for portability.
        static void DrawGameBoard( )
        {
            WriteLine( );
            for( int row = gameRows - 1; row >= 0; row -- )
            {
                Write( "   |" );
                for( int col = 0; col < gameCols; col ++ ) Write( "   |" ); 
                WriteLine( );
                Write( $"{row,2} |" );
                for( int col = 0; col < gameCols; col ++ ) 
                {
                    Write( " " );
                    ColorWrite( gameBoard[ row, col ] );
                    Write( " |" );
                }
                WriteLine( );
            }
            Write( "   |" );
            for( int col = 0; col < gameCols; col ++ ) Write( "___|" ); 
            WriteLine( );
            WriteLine( );
            Write( "    " );
            for( int col = 0; col < gameCols; col ++ ) Write( $"{col,2}  " ); 
            WriteLine( );
        }
        
        // Display O or X in their special color.
        static void ColorWrite( string symbol )
        {
            if( symbol == "O" ) ForegroundColor = oColor;
            if( symbol == "X" ) ForegroundColor = xColor;
            // Empty cells in the game board use null but
            // we still want them to display using one space.
            Write( $"{symbol,1}" );
            ForegroundColor = fgColor;
        }
    }  
}
    
