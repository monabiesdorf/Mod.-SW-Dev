class TicTacToe

    //Kommentar, test GitHub
{        
    private const int boardSize = 3;
    private char[,] board;
    private char currentPlayer;
    private bool isGameRunning;

    public TicTacToe()
    {

        board = new char[boardSize, boardSize];
        //first player is x
        currentPlayer = 'x';
        isGameRunning = true;
        CreateBoard();
    }

    private void CreateBoard()
    {
        for (int row = 0; row < boardSize; row++)
        {
            for (int col = 0; col < boardSize; col++)
            {
                board[row, col] = ' ';
            }
        }
    }

    private void DrawBoard()
    {
        // separate rows
        Console.WriteLine(" -------------");

        // separate fields in row with '|'
        for (int row = 0; row < boardSize; row++)
        {
            Console.Write(" | ");
            for (int col = 0; col < boardSize; col++)
            {
                Console.Write(board[row, col] + " | ");
            }
            Console.WriteLine();
            Console.WriteLine(" -------------");
        }
    }

    // If field is empty and is part of the board place symbol of current player
    private bool PlaceSymbol(int row, int col)
    {
        if (board[row, col] == ' ' && row >= 0 && row < boardSize && col >= 0 && col < boardSize)
        {
            board[row, col] = currentPlayer;
            return true;
        }
        return false;
    }

    private bool IsSymbolSameInDirection(int startRow, int startCol, int stepRow, int stepCol, int winConditionSize)
    {
        char symbol = board[startRow, startCol];
        return symbol != ' ' && Enumerable.Range(0, winConditionSize).All(i => board[startRow + i * stepRow, 
            startCol + i * stepCol] == symbol);
    }

    private bool CheckForWinInDirection(int startRow, int startCol, int stepRow, int stepCol, int winConditionSize)
    {
        for (int i = 0; i < boardSize; i++)
        {
            if (IsSymbolSameInDirection(startRow + i * stepRow, startCol + i * stepCol, 
                stepRow, stepCol, winConditionSize))
            {
                return true;
            }
        }
        return false;
    }

    // Methode to check for a win condition in the all directions
    private bool CheckForWinCondition()
    {
        return CheckForWinInDirection(0, 0, 0, 1, boardSize) || // Check columns
               CheckForWinInDirection(0, 0, 1, 0, boardSize) || // Check rows
               CheckForWinInDirection(0, 0, 1, 1, boardSize) || // Check diagonals
               CheckForWinInDirection(0, boardSize - 1, 1, -1, boardSize); // Check other diagonals
    }

    //second player is o
    private void SwitchPlayer()
    {

        currentPlayer = (currentPlayer == 'x') ? 'o' : 'x';
    }

    public void StartGame()
    {
        // write instruction how to play the game
        while (isGameRunning)
        {
            Console.Clear();
            DrawBoard();
            Console.WriteLine();
            Console.WriteLine("Player " + currentPlayer + ", enter row (0-2) and column (0-2) separated by space:");
            string[] input = Console.ReadLine().Split();

            if (!ValidateInput(input))
            {
                continue;
            }

            int row = int.Parse(input[0]);
            int col = int.Parse(input[1]);

            if (PlaceSymbol(row, col))
            {
                HandlePlayerMove();
            }
            else
            {
                Console.WriteLine("Invalid move. Try again!");
            }
        }
    }

    private bool ValidateInput(string[] input)
    {
        if (input.Length != 2 || !int.TryParse(input[0], out int row) || !int.TryParse(input[1], out int col))
        {
            Console.WriteLine("Invalid input. Please enter row and column values separated by space.");
            return false;
        }
        return true;
    }

    private void HandlePlayerMove()
    {
        // check if he wins: end game 
        if (CheckForWinCondition())
        {
            EndGame(currentPlayer + " wins!");
        }
        // check if there is any empty field: end game
        else if (!board.Cast<char>().Any(cell => cell == ' '))
        {
            EndGame("Nobody wins!");
        }
        // if there is no winner and any empty field: switch player
        else
        {
            SwitchPlayer();
        }
    }

    private void EndGame(string message)
    {
        Console.Clear();
        DrawBoard();
        Console.WriteLine(message);
        isGameRunning = false;
    }
}

class Program
{
    static void Main(string[] args)
    {
        TicTacToe game = new TicTacToe();
        game.StartGame();
    }
}
