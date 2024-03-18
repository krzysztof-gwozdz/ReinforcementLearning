// using System.Drawing;
//
// namespace ReinforcementLearning.Problems;
//
// public class FindAppleProblem : IProblem<Point>
// {
//     private readonly Random _random = new();
//     public int NumberOfStates { get; }
//     public Point ApplePosition { get; }
//
//     public FindAppleProblem(int size)
//     {
//         NumberOfStates = size * size;
//         ApplePosition = new Point(_random.Next(size), _random.Next(size));
//     }
//
//     public double GetReward(Point currentState, Point action)
//     {
//         throw new NotImplementedException();
//     }
//
//     public Point[] GetValidActions(Point currentState)
//     {
//         throw new NotImplementedException();
//     }
//
//     public bool GoalStateIsReached(Point currentState) =>
//         currentState == ApplePosition;
// }