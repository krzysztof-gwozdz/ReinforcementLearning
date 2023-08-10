using ReinforcementLearning.Problems;

namespace ReinforcementLearning;

public static class Program
{
    public static void Main()
    {
        var qLearning = new QLearning(new OpenSpaceProblem());
        Train(qLearning);
        Test(qLearning);
    }

    private static void Train(QLearning qLearning)
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
            foreach (var column in row)
            {
                if (column > 0.5)
                    Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{column:#00.00%} ");
                Console.ResetColor();
            }
            Console.WriteLine("]");
        }
    }

    private static void Test(QLearning qLearning)
    {
        do
        {
            Console.WriteLine("Enter initial state. Number 0-5. Press 'q' to exit.");
            if (!int.TryParse(Console.ReadLine(), out var initialState))
                break;
            var qLearningStats = qLearning.Run(initialState);
            Console.WriteLine(qLearningStats);
        } while (true);
    }
}