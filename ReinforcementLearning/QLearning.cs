using ReinforcementLearning.Problems;

namespace ReinforcementLearning;

public class QLearning
{
    private const double Gamma = 0.8;
    private readonly Random _random = new();
    private readonly IProblem _problem;

    private double[][] QTable { get; } =
    {
        new double[] { 0, 0, 0, 0, 0, 0 },
        new double[] { 0, 0, 0, 0, 0, 0 },
        new double[] { 0, 0, 0, 0, 0, 0 },
        new double[] { 0, 0, 0, 0, 0, 0 },
        new double[] { 0, 0, 0, 0, 0, 0 },
        new double[] { 0, 0, 0, 0, 0, 0 }
    };

    public QLearning(IProblem problem)
    {
        _problem = problem;
    }

    public void TrainAgent(int numberOfIterations)
    {
        for (var i = 0; i < numberOfIterations; i++)
            RunEpisode(GetInitialState(_problem.NumberOfStates));
    }

    public object Run(int initialState)
    {
        var qLearningStats = new QLearningStats(initialState);
        var currentState = initialState;
        while (!_problem.GoalStateIsReached(currentState))
        {
            var action = GetBestAction(currentState);
            qLearningStats.Steps.Add(action);
            Learn(currentState, action);
            currentState = action;
        }

        return qLearningStats;
    }

    public double[][] GetNormalizedQTable()
    {
        var max = QTable.SelectMany(value => value).Max();
        return QTable.Select(row => row.Select(value => value / max).ToArray()).ToArray();
    }

    private int GetInitialState(object numberOfStates) =>
        _random.Next((int)numberOfStates);

    private void RunEpisode(int initialState)
    {
        var currentState = initialState;
        while (true)
        {
            currentState = TakeAction(currentState);
            if (_problem.GoalStateIsReached(currentState))
                break;
        }
    }

    private int TakeAction(int currentState)
    {
        var action = GetRandomAction(currentState);
        Learn(currentState, action);
        return action;
    }

    private int GetRandomAction(int currentState)
    {
        var validActions = _problem.GetValidActions(currentState);
        return validActions[_random.Next(0, validActions.Length)];
    }

    private int GetBestAction(int currentState)
    {
        var states = QTable[currentState].Select((x, i) => new { Value = x, Index = i })
            .Where(x => x.Value != 0)
            .OrderByDescending(x => x.Value)
            .Select(x => x.Index)
            .ToArray();
        return states.Any() ? states.First() : GetRandomAction(currentState);
    }

    private void Learn(int currentState, int action)
    {
        double saReward = _problem.GetReward(currentState, action);
        var nsReward = QTable[action].Max();
        var qCurrentState = saReward + Gamma * nsReward;
        QTable[currentState][action] = qCurrentState;
    }
}