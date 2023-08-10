namespace ReinforcementLearning.Problems;

// _____________________________________
// | 0      | 1      | 2      | 3      |
// |                                   |
// |___  ___|___  ___|___  ___|___  ___|
// | 4      | 5      | 6      | 7      |
// |                                   |
// |___  ___|___  ___|___  ___|___  ___|
// | 8      | 9      | 10     | 11     |
// |                                   |
// |___  ___|___  ___|___  ___|___  ___|
public class OpenSpaceProblem : IProblem
{
    private readonly int[][] _rewards =
    {
        new[]
        {
            -1, 0, -1, -1,
            0, -1, -1, -1,
            1, -1, -1, -1
        },
        new[]
        {
            0, -1, 0, -1,
            -1, 0, -1, -1,
            1, -1, -1, -1
        },
        new[]
        {
            -1, 0, -1, 0,
            -1, -1, 0, -1,
            1, -1, -1, -1
        },
        new[]
        {
            -1, -1, 0, -1,
            -1, -1, -1, 100,
            1, -1, -1, -1
        },

        new[]
        {
            0, -1, -1, -1,
            -1, 0, -1, -1,
            0, -1, -1, -1
        },
        new[]
        {
            -1, 0, -1, -1,
            0, -1, 0, -1,
            -1, 0, -1, -1
        },
        new[]
        {
            -1, -1, 0, -1,
            -1, 0, -1, 100,
            -1, -1, 0, -1
        },
        new[]
        {
            -1, -1, -1, 0,
            -1, -1, 0, -1,
            -1, -1, -1, 0
        },


        new[]
        {
            -1, -1, -1, -1,
            0, -1, -1, -1,
            -1, 0, -1, -1
        },
        new[]
        {
            -1, -1, -1, -1,
            -1, 0, -1, -1,
            0, -1, 0, -1
        },
        new[]
        {
            -1, -1, -1, -1,
            -1, -1, 0, -1,
            -1, 0, -1, 0
        },
        new[]
        {
            -1, -1, -1, -1,
            -1, -1, -1, 100,
            -1, -1, 0, -1
        },
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
        currentState == 7;
}