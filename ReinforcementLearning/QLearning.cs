﻿using ReinforcementLearning.Problems;

namespace ReinforcementLearning;

public class QLearning
{
    private const double Gamma = 0.8;
    private readonly Random _random = new();
    private readonly IProblem _problem;

    private double[][] QTable { get; }

    public QLearning(IProblem problem)
    {
        _problem = problem;
        QTable = new double[problem.NumberOfStates][];
        for (var i = 0; i < problem.NumberOfStates; i++)
            QTable[i] = new double[problem.NumberOfStates];
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
        var states = QTable[currentState].Select((value, index) => new { Value = value, Index = index })
            .Where(state => state.Value != 0)
            .OrderByDescending(state => state.Value)
            .ToArray();
        if (!states.Any())
            return GetRandomAction(currentState);
        var max = states[0].Value;
        return states.Where(state => Math.Abs(state.Value - max) < 0.1).MinBy(_ => Guid.NewGuid())!.Index;
    }

    private void Learn(int currentState, int action)
    {
        double saReward = _problem.GetReward(currentState, action);
        var nsReward = QTable[action].Max();
        var qCurrentState = saReward + Gamma * nsReward;
        QTable[currentState][action] = qCurrentState;
    }
}