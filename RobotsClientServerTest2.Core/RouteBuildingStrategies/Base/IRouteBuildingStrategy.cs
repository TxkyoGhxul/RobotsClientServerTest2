using RobotsClientServerTest2.Core.Enums;
using RobotsClientServerTest2.Core.Models;

namespace RobotsClientServerTest2.Core.RouteBuildingStrategies.Base;

/// <summary>
/// Interface to build route for robot
/// </summary>
public interface IRouteBuildingStrategy
{
    /// <summary>
    /// Method to get robots route
    /// </summary>
    /// <param name="oldPosition">Old robot position</param>
    /// <param name="newPosition">New robot position</param>
    /// <param name="currentRobotDirection">Current robot direction</param>
    /// <returns>Robot's route</returns>
    Queue<string> BuildRoute(Position oldPosition, Position newPosition, Direction currentRobotDirection);
}
