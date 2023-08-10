using System.Text;

namespace ReinforcementLearning;

public class QLearningStats
{
    private int InitialState { get; }
    public List<int> Steps { get; }

    public QLearningStats(int initialState)
    {
        InitialState = initialState;
        Steps = new List<int>();
    }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"Agent needed {Steps.Count} steps to find the solution:");
        stringBuilder.AppendLine($"Actions: {InitialState} -> {string.Join(" -> ", Steps)}");
        return stringBuilder.ToString();
    }
}