﻿using ReinforcementLearning.Problems;

namespace ReinforcementLearning;

public static class Program
{
    public static void Main()
    {
        var qLearning = new QLearning<int, int>(new OpenSpaceProblem());
        Train(qLearning);
        Test(qLearning);
    }

    private static void Train(QLearning<int, int> qLearning)
    {
        Console.WriteLine("Training Agent...");
        qLearning.TrainAgent(1000);
        Console.WriteLine("Training is Done!");
        Console.WriteLine("Q table:");
        DisplayQTable(qLearning.GetNormalizedQTable());
        Console.WriteLine();
    }

    private static void DisplayQTable(double[][] table)
    {
        foreach (var row in table)
        {
            Console.Write("[ ");
            var max = row.Max();
            foreach (var column in row)
            {
                if (Math.Abs(column - max) < 0.01)
                    Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{column:#00.00%} ");
                Console.ResetColor();
            }

            Console.WriteLine("]");
        }
    }

    private static void Test(QLearning<int, int> qLearning)
    {
        do
        {
            Console.WriteLine("Enter initial state. Press 'q' to exit.");
            if (!int.TryParse(Console.ReadLine(), out var initialState))
                break;
            var qLearningStats = qLearning.Run(initialState);
            Console.WriteLine(qLearningStats);
        } while (true);
    }
}