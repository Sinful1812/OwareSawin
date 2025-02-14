using System;
namespace OwareNsvisEdition;
public class GameLoop
{
    private const int Players = 2;
    private const int Pots = 6;
    private const int Lines = 2;

    private int[,] board = new int[Lines, Pots];
    private int[] points = new int[Players];
    private int Playing = 0;
    private Table tableDisplay;

    public GameLoop()
    {
        for (int i = 0; i < Lines; i++)
        {
            for (int j = 0; j < Pots; j++)
            {
                board[i, j] = 4;
            }
        }
        tableDisplay = new Table(board, points);
    }

    public void Looper()
    {
        while (!EndGame())
        {
            tableDisplay.ShowTable(Playing);
            Console.Write("Select A-F: ");
            string input = Console.ReadLine().ToUpper();

            int pot = input[0] - 'A';
            if (pot < 0 || pot >= Pots || board[Playing, pot] == 0)
            {
                Console.WriteLine("No Seed Left.");
                continue;
            }

            int seeds = board[Playing, pot];
            board[Playing, pot] = 0;

            int lastLine = Playing;
            int lastPot = pot;

            while (seeds > 0)
            {
                if (lastLine == 0)
                {
                    lastPot--;
                    if (lastPot < 0)
                    {
                        lastPot = 0;
                        lastLine = 1;
                    }
                }
                else
                {
                    lastPot++;
                    if (lastPot >= Pots)
                    {
                        lastPot = Pots - 1;
                        lastLine = 0;
                    }
                }

                board[lastLine, lastPot]++;
                seeds--;
            }

            if (lastLine != Playing && board[lastLine, lastPot] == 4)
            {
                CaptureSeeds(Playing, lastLine, lastPot);
            }

            SwitchPlayer();
        }

        tableDisplay.ShowTable(Playing);
        Console.WriteLine($"Player 1 points: {points[0]}");
        Console.WriteLine($"Player 2 points: {points[1]}");
        Console.WriteLine(points[0] > points[1] ? "Player 1 wins!" : "Player 2 wins!");
    }

    private void CaptureSeeds(int player, int line, int pot)
    {
        points[player] += board[line, pot];
        board[line, pot] = 0;
    }

    private bool EndGame()
    {
        if (points[0] >= 24 || points[1] >= 24)
        {
            return true;
        }

        bool player1NoSeed = true;
        bool player2NoSeed = true;
        for (int i = 0; i < Pots; i++)
        {
            if (board[0, i] > 0)
            {
                player1NoSeed = false;
            }
            if (board[1, i] > 0)
            {
                player2NoSeed = false;
            }
        }

        return player1NoSeed || player2NoSeed;
    }

    private void SwitchPlayer()
    {
        Playing = 1 - Playing;
    }
}