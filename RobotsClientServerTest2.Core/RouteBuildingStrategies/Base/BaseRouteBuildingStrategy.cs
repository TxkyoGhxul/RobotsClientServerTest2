using RobotsClientServerTest2.Core.Enums;
using RobotsClientServerTest2.Core.Models;
using RobotsClientServerTest2.Core.Repositories;

namespace RobotsClientServerTest2.Core.RouteBuildingStrategies.Base;

/// <summary>
/// Abstract class for building route for robot
/// </summary>
public abstract class BaseRouteBuildingStrategy : IRouteBuildingStrategy
{
    /// <summary>
    /// Robot's new route
    /// </summary>
    protected readonly Queue<string> _responses = new();

    ///<inheritdoc />
    public Queue<string> BuildRoute(Position oldPosition, Position newPosition,
        Direction currentRobotDirection)
    {
        var directionToGoByX = newPosition.X > 0 ? Direction.Left : Direction.Right;
        var directionToGoByY = newPosition.Y > 0 ? Direction.Backward : Direction.Forward;

        if (newPosition.X == 0)
        {
            directionToGoByX = Direction.None;
        }

        if (newPosition.Y == 0)
        {
            directionToGoByY = Direction.None;
        }

        return GetRoute(oldPosition, newPosition, currentRobotDirection, directionToGoByX, directionToGoByY);
    }

    /// <summary>
    /// Method that generates a new route for robot
    /// </summary>
    /// <param name="oldPosition">Old robot position</param>
    /// <param name="newPosition">New robot position</param>
    /// <param name="currentRobotDirection">Current robot direction</param>
    /// <param name="directionToGoByX">Direction to go by X</param>
    /// <param name="directionToGoByY">Direction to go by Y</param>
    /// <returns>New robot's route</returns>
    protected abstract Queue<string> GetRoute(Position oldPosition,
                                              Position newPosition,
                                              Direction currentRobotDirection,
                                              Direction directionToGoByX,
                                              Direction directionToGoByY);

    /// <summary>
    /// Method to bypass an obstacle
    /// </summary>
    protected void BypassObstacle()
    {
        _responses.Enqueue(ServerMessages.SERVER_TURN_RIGHT);
        _responses.Enqueue(ServerMessages.SERVER_MOVE);
        _responses.Enqueue(ServerMessages.SERVER_TURN_LEFT);
        _responses.Enqueue(ServerMessages.SERVER_MOVE);
        _responses.Enqueue(ServerMessages.SERVER_MOVE);
        _responses.Enqueue(ServerMessages.SERVER_TURN_LEFT);
        _responses.Enqueue(ServerMessages.SERVER_MOVE);
        _responses.Enqueue(ServerMessages.SERVER_TURN_RIGHT);
    }

    /// <summary>
    /// Method to turn to the right direction
    /// </summary>
    /// <param name="from">Current robot direction</param>
    /// <param name="to">Direction to get</param>
    protected void TurnToTheRightDirection(Direction from, Direction to)
    {
        if (from == to)
        {
            return;
        }

        if (Math.Abs(from - to) == 180)
        {
            _responses.Enqueue(ServerMessages.SERVER_TURN_RIGHT);
            _responses.Enqueue(ServerMessages.SERVER_TURN_RIGHT);

            return;
        }

        if (from - to == 90 || from - to == -270)
        {
            _responses.Enqueue(ServerMessages.SERVER_TURN_LEFT);
            return;
        }

        if (from - to == -90 || from - to == 270)
        {
            _responses.Enqueue(ServerMessages.SERVER_TURN_RIGHT);
            return;
        }
    }
}
