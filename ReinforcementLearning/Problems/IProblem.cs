namespace ReinforcementLearning.Problems;

public interface IProblem<TState, TAction> where TState : notnull where TAction : notnull
{
    TState[] States { get; }
    TAction[] Actions { get; }
    TState GetInitialState();
    double GetReward(TState currentState, TAction action);
    bool GoalStateIsReached(TState currentState);
}