namespace ReinforcementLearning.Problems;

// ____________________________
// | 0      | 1      |(5)     |
// |        |                 |
// |___  ___|___  ___|________|
// | 4      | 3      | 2      |
// |                          |
// |___  ___|________|________|
public class SimpleRoomProblem : IProblem<int, int>
{
    private readonly Random _random = new();

    private readonly double[][] _rewards =
    {
        new double[] { -1, -1, -1, -1, 0, -1 },
        new double[] { -1, -1, -1, 0, -1, 100 },
        new double[] { -1, -1, -1, 0, -1, -1 },
        new double[] { -1, 0, 0, -1, 0, -1 },
        new double[] { 0, -1, -1, 0, -1, -1 },
        new double[] { -1, 0, -1, -1, -1, -1 }
    };

    public int[] States => new[] { 0, 1, 2, 3, 4, 5 };
    public int[] Actions => new[] { 0, 1, 2, 3, 4, 5 };

    public int GetInitialState() => 
        _random.Next(States.Length);

    public double GetReward(int currentState, int action) =>
        _rewards[currentState][action];

    public int[] GetValidActions(int currentState)
    {
        var validActions = new List<int>();
        for (var i = 0; i < _rewards[currentState].Length; i++)
        {
            if (_rewards[currentState][i] != -1)
                validActions.Add(i);
        }

        return validActions.ToArray();
    }

    public bool GoalStateIsReached(int currentState) =>
        currentState == 5;
}