using System.Text;

namespace ReinforcementLearning;

public class QLearningStats<TState, TAction>
{
    private TState InitialState { get; }
    public List<TAction> Actions { get; }

    public QLearningStats(TState initialState)
    {
        InitialState = initialState;
        Actions = new List<TAction>();
    }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"Agent needed {Actions.Count} steps to find the solution:");
        stringBuilder.AppendLine($"Actions: {InitialState} -> {string.Join(" -> ", Actions)}");
        return stringBuilder.ToString();
    }
}