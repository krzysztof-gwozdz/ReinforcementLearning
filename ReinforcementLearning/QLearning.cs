using ReinforcementLearning.Problems;

namespace ReinforcementLearning;

public class QLearning<TState, TAction> where TState : notnull where TAction : notnull
{
    private const double Gamma = 0.8;
    private readonly Random _random = new();
    private readonly IProblem<TState, TAction> _problem;

    private Dictionary<TState, Dictionary<TAction, double>> QTable { get; set; }

    public QLearning(IProblem<TState, TAction> problem)
    {
        _problem = problem;
        InitializeQTable();
    }

    private void InitializeQTable()
    {
        QTable = new Dictionary<TState, Dictionary<TAction, double>>();
        foreach (var state in _problem.States)
        {
            QTable[state] = new Dictionary<TAction, double>();
            foreach (var action in _problem.Actions)
                QTable[state][action] = 0;
        }
    }

    public void TrainAgent(int numberOfIterations)
    {
        for (var i = 0; i < numberOfIterations; i++)
            RunEpisode(_problem.GetInitialState());
    }

    public QLearningStats<TState, TAction> Run(TState initialState)
    {
        var qLearningStats = new QLearningStats<TState, TAction>(initialState);
        var currentState = initialState;
        while (!_problem.GoalStateIsReached(currentState))
        {
            var action = GetBestAction(currentState);
            qLearningStats.Actions.Add(action);
            Learn(currentState, action);
            currentState = action;
        }

        return qLearningStats;
    }

    public double[][] GetNormalizedQTable()
    {
        var max = QTable.SelectMany(state => state.Value).MaxBy(action => action.Value).Value;
        return QTable.Select(state => state.Value.Select(action => action.Value / max).ToArray()).ToArray();
    }

    private void RunEpisode(TState initialState)
    {
        var currentState = initialState;
        while (true)
        {
            currentState = ChooseAction(currentState);
            if (_problem.GoalStateIsReached(currentState))
                break;
        }
    }

    private TState ChooseAction(TState currentState)
    {
        var action = GetRandomAction();
        Learn(currentState, action);
        return action;
    }

    private TAction GetRandomAction() =>
        _problem.Actions[_random.Next(0, _problem.Actions.Length)];

    private TAction GetBestAction(TState currentState)
    {
        var actions = QTable[currentState]
            .OrderByDescending(state => state.Value)
            .ToArray();
        if (!actions.Any())
            return GetRandomAction();
        var max = actions[0].Value;
        return actions.Where(state => Math.Abs(state.Value - max) < 0.1).MinBy(_ => Guid.NewGuid()).Key;
    }

    private void Learn(TState currentState, TAction action)
    {
        var saReward = _problem.GetReward(currentState, action);
        var nsReward = QTable[currentState].MaxBy(state => state.Value).Value;
        var qCurrentState = saReward + Gamma * nsReward;
        QTable[currentState][action] = qCurrentState;
    }
}