using RobotsClientServerTest2.Core.Enums;
using RobotsClientServerTest2.Core.Models;

namespace RobotsClientServerTest2.Core.Extensions;

/// <summary>
/// Helper for working with <see cref="Direction" /> enum
/// </summary>
public static class DirectionHelper
{
    /// <summary>
    /// Gets robot's current position by old and current position
    /// </summary>
    /// <param name="oldPosition">Old robot's position</param>
    /// <param name="newPosition">Current robot's position</param>
    /// <returns>Current robot's direction, if unknown -> <see cref="Direction.None"/></returns>
    public static Direction GetDirection(Position oldPosition, Position newPosition)
    {
        if (oldPosition.X == newPosition.X)
        {
            if (newPosition.Y > oldPosition.Y)
            {
                return Direction.Forward;
            }

            if (newPosition.Y < oldPosition.Y)
            {
                return Direction.Backward;
            }
        }

        if (oldPosition.Y == newPosition.Y)
        {
            if (newPosition.X > oldPosition.X)
            {
                return Direction.Right;
            }

            if (newPosition.X < oldPosition.X)
            {
                return Direction.Left;
            }
        }

        return Direction.None;
    }

    /// <summary>
    /// Method for turning to left
    /// </summary>
    /// <param name="direction">Current direction</param>
    /// <returns>New direction</returns>
    public static Direction TurnLeft(Direction direction)
    {
        return direction switch
        {
            Direction.Left => Direction.Backward,
            Direction.Right => Direction.Forward,
            Direction.Forward => Direction.Left,
            Direction.Backward => Direction.Right,
            _ => Direction.None,
        };
    }

    /// <summary>
    /// Method for turning to right
    /// </summary>
    /// <param name="direction">Current direction</param>
    /// <returns>New direction</returns>
    public static Direction TurnRight(Direction direction)
    {
        return direction switch
        {
            Direction.Left => Direction.Forward,
            Direction.Right => Direction.Backward,
            Direction.Forward => Direction.Right,
            Direction.Backward => Direction.Left,
            _ => Direction.None,
        };
    }
}
