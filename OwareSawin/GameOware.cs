using System;

public class GameOware
{
    private const int Players = 2;
    private const int Columns = 6;
    private const int Rows = 2;

    private int[,] board = new int[Rows, Columns];
    private int[] points = new int[Players];
    private int currentPlayer = 0;
    private TableDisplay tableDisplay;

    public GameOware()
    {
        // Initialize the board with seeds
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                board[i, j] = 4;
            }
        }
        tableDisplay = new TableDisplay(board, points);
    }

    public void GameLoop()
    {
        while (!IsEndGame())
        {
            tableDisplay.ShowTable(currentPlayer);
            Console.Write("Choose a column (A-F): ");
            string input = Console.ReadLine().ToUpper();

            int column = input[0] - 'A';
            if (column < 0 || column >= Columns || board[currentPlayer, column] == 0)
            {
                Console.WriteLine("Invalid move. Try again.");
                continue;
            }

            int seeds = board[currentPlayer, column];
            board[currentPlayer, column] = 0;

            int lastRow = currentPlayer;
            int lastCol = column;

            while (seeds > 0)
            {
                if (lastRow == 0)
                {
                    // Top row: Go left (counterclockwise)
                    lastCol--;
                    if (lastCol < 0)
                    {
                        lastCol = 0;
                        lastRow = 1;
                    }
                }
                else
                {
                    // Bottom row: Go right (clockwise)
                    lastCol++;
                    if (lastCol >= Columns)
                    {
                        lastCol = Columns - 1;
                        lastRow = 0;
                    }
                }

                board[lastRow, lastCol]++;
                seeds--;
            }

            // Capture seeds if the last seed lands in the opponent's row and the cell has exactly 4 seeds
            if (lastRow != currentPlayer && board[lastRow, lastCol] == 4)
            {
                CaptureSeeds(currentPlayer, lastRow, lastCol);
            }

            SwitchPlayer();
        }

        tableDisplay.ShowTable(currentPlayer);
        Console.WriteLine($"Player 1 points: {points[0]}");
        Console.WriteLine($"Player 2 points: {points[1]}");
        Console.WriteLine(points[0] > points[1] ? "Player 1 wins!" : "Player 2 wins!");
    }

    private void CaptureSeeds(int player, int row, int col)
    {
        points[player] += board[row, col];
        board[row, col] = 0;
    }

    private bool IsEndGame()
    {
        if (points[0] >= 24 || points[1] >= 24)
        {
            return true;
        }

        bool player1Empty = true;
        bool player2Empty = true;
        for (int i = 0; i < Columns; i++)
        {
            if (board[0, i] > 0)
            {
                player1Empty = false;
            }
            if (board[1, i] > 0)
            {
                player2Empty = false;
            }
        }

        return player1Empty || player2Empty;
    }

    private void SwitchPlayer()
    {
        currentPlayer = 1 - currentPlayer;
    }
}