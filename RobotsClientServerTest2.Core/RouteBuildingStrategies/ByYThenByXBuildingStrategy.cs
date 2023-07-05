using RobotsClientServerTest2.Core.Enums;
using RobotsClientServerTest2.Core.Models;
using RobotsClientServerTest2.Core.Repositories;
using RobotsClientServerTest2.Core.RouteBuildingStrategies.Base;

namespace RobotsClientServerTest2.Core.RouteBuildingStrategies;

/// <summary>
/// Class for building robot's route by X then by Y
/// </summary>
public class ByYThenByXBuildingStrategy : BaseRouteBuildingStrategy
{
    /// <inheritdoc />
    protected override Queue<string> GetRoute(Position oldPosition, Position newPosition, Direction currentRobotDirection, Direction directionToGoByX, Direction directionToGoByY)
    {
        if (directionToGoByY == Direction.None)
        {
            TurnToTheRightDirection(currentRobotDirection, directionToGoByY);

            if (oldPosition.Equals(newPosition))
            {
                BypassObstacle();
            }

            for (int i = Math.Abs(newPosition.X) - 2; i > 0; i--)
            {
                _responses.Enqueue(ServerMessages.SERVER_MOVE);
            }

            return _responses;
        }

        TurnToTheRightDirection(currentRobotDirection, directionToGoByY);

        for (int i = Math.Abs(newPosition.Y); i > 0; i--)
        {
            _responses.Enqueue(ServerMessages.SERVER_MOVE);
        }

        TurnToTheRightDirection(directionToGoByY, directionToGoByX);

        for (int i = Math.Abs(newPosition.X); i > 0; i--)
        {
            _responses.Enqueue(ServerMessages.SERVER_MOVE);
        }

        return _responses;
    }
}
