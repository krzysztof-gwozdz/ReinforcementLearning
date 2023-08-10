namespace ReinforcementLearning.Problems;

public interface IProblem
{
    int NumberOfStates { get; }
    int GetReward(int currentState, int action);
    int[] GetValidActions(int currentState);
    bool GoalStateIsReached(int currentState);
}