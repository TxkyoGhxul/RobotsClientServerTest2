using RobotsClientServerTest2.Core.Dialogs;
using RobotsClientServerTest2.Core.DialogStates.Base;
using RobotsClientServerTest2.Core.Enums;
using RobotsClientServerTest2.Core.Extensions;
using RobotsClientServerTest2.Core.Models;
using RobotsClientServerTest2.Core.Repositories;
using RobotsClientServerTest2.Core.RouteBuildingStrategies;
using RobotsClientServerTest2.Core.RouteBuildingStrategies.Base;

namespace RobotsClientServerTest2.Core.DialogStates;

/// <summary>
/// Gets OK and coords from client and returns the next robot move
/// </summary>
public class RobotMovingDialogState : IDialogState
{
    /// <inheritdoc />
    public int MaxLength => ClientMessages.CLIENT_OK.maxLength;

    private int _movesCount = 0;
    private string _lastResponse;
    private string _previousRequest;
    private DateTime _startOfCharging;
    private Direction _currentRobotDirection = Direction.None;
    private Position? _oldPosition = null;
    private IRouteBuildingStrategy _buildingStrategy = new ByXThenByYBuildingStrategy();

    /// <summary>
    /// Route of the robot to come in [0:0]
    /// </summary>
    private Queue<string> _responses = new Queue<string>();

    /// <inheritdoc />
    public string HandleRequest(string request, DialogServer dialog)
    {
        if (request == ClientMessages.CLIENT_RECHARGING.message[..^2])
        {
            if (_previousRequest != ClientMessages.CLIENT_RECHARGING.message[..^2])
            {
                _startOfCharging = DateTime.Now;
            }

            _previousRequest = request;

            return string.Empty;
        }

        if (request == ClientMessages.CLIENT_FULL_POWER.message[..^2])
        {
            if (_previousRequest != ClientMessages.CLIENT_RECHARGING.message[..^2])
            {
                return ServerMessages.SERVER_LOGIC_ERROR;
            }

            if (_startOfCharging.AddMilliseconds(1000 * TimeConstants.TIMEOUT_RECHARGING) < DateTime.Now)
            {
                return string.Empty;
            }

            if (!_responses.Any())
            {
                bool isXInt = int.TryParse(request.Split(' ')[^2], out int xPos);
                bool isYInt = int.TryParse(request.Split(' ')[^1], out int yPos);

                if (!isXInt || !isYInt)
                {
                    return ServerMessages.SERVER_SYNTAX_ERROR;
                }

                var newPos = new Position(xPos, yPos);

                _responses = _buildingStrategy.BuildRoute(_oldPosition.Value, newPos, _currentRobotDirection);
            }
            _lastResponse = _responses.Dequeue();

            return _lastResponse;
        }

        //Getting robot's current position
        bool isXInteger = int.TryParse(request.Split(' ')[^2], out int x);
        bool isYInteger = int.TryParse(request.Split(' ')[^1], out int y);

        if (!isXInteger || !isYInteger)
        {
            return ServerMessages.SERVER_SYNTAX_ERROR;
        }

        var newPosition = new Position(x, y);

        if (!_oldPosition.HasValue)
        {
            _movesCount++;
            _oldPosition = newPosition;
            _lastResponse = ServerMessages.SERVER_MOVE;

            return _lastResponse;
        }

        if (_movesCount == 1)
        {
            _currentRobotDirection = DirectionHelper.GetDirection(_oldPosition.Value, newPosition);

            _responses = _buildingStrategy.BuildRoute(_oldPosition.Value, newPosition, _currentRobotDirection);
        }

        if (_oldPosition.Value.Equals(newPosition) && _lastResponse == ServerMessages.SERVER_MOVE && _movesCount != 1)
        {
            _buildingStrategy = _buildingStrategy is ByXThenByYBuildingStrategy ?
                new ByYThenByXBuildingStrategy() : new ByXThenByYBuildingStrategy();

            _responses = _buildingStrategy.BuildRoute(_oldPosition.Value, newPosition, _currentRobotDirection);
        }

        if (newPosition.X == 0 && newPosition.Y == 0)
        {
            _movesCount++;

            dialog.SetNewState(new SecretMessageDialogState());

            return ServerMessages.SERVER_PICK_UP;
        }

        _currentRobotDirection = DirectionHelper.GetDirection(_oldPosition.Value, newPosition);
        
        if (!_responses.Any())
        {
            _responses = _buildingStrategy.BuildRoute(_oldPosition.Value, newPosition, _currentRobotDirection);
        }

        _movesCount++;
        _oldPosition = newPosition;
        _previousRequest = request;
        _lastResponse = _responses.Dequeue();

        return _lastResponse;
    }
}
