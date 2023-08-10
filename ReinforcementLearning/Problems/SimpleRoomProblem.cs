namespace ReinforcementLearning.Problems;

// ____________________________
// | 0      | 1      |(5)     |
// |        |                 |
// |___  ___|___  ___|________|
// | 4      | 3      | 2      |
// |                          |
// |___  ___|________|________|
public class SimpleRoomProblem : IProblem
{
    private readonly int[][] _rewards =
    {
        new[] { -1, -1, -1, -1, 0, -1 },
        new[] { -1, -1, -1, 0, -1, 100 },
        new[] { -1, -1, -1, 0, -1, -1 },
        new[] { -1, 0, 0, -1, 0, -1 },
        new[] { 0, -1, -1, 0, -1, -1 },
        new[] { -1, 0, -1, -1, -1, -1 }
    };

    public int NumberOfStates => _rewards.Length;

    public int GetReward(int currentState, int action) =>
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