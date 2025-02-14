using System;

public class Table
{
    private int[,] board;
    private int[] points;
    private const int Pots = 6;
    private const int Lines = 2;

    public Table(int[,] board, int[] points)
    {
        this.board = board;
        this.points = points;
    }

    public void ShowTable(int currentPlayer)
    {
        Console.Clear();
        Console.WriteLine("        A   B   C   D   E   F");
        Console.WriteLine("       -----------------------");
        for (int i = 0; i < Lines; i++)
        {
            if (i == 0)
            {
                Console.Write((currentPlayer == 0 ? "-> P1 " : "   P1 "));
            }
            else
            {
                Console.Write((currentPlayer == 1 ? "-> P2 " : "   P2 "));
            }

            for (int j = 0; j < Pots; j++)
            {
                Console.Write($"| {board[i, j]} ");
            }
            Console.WriteLine("|");
            Console.WriteLine("       -----------------------");
        }
        Console.WriteLine("        A   B   C   D   E   F");
        Console.WriteLine($"Player 1 Points: {points[0]}");
        Console.WriteLine($"Player 2 Points: {points[1]}");
    }
}