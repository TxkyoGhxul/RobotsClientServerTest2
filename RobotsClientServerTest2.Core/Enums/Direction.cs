namespace RobotsClientServerTest2.Core.Enums;

/// <summary>
/// Represents robot's direction
/// </summary>
public enum Direction
{
    /// <summary>
    /// Direction is unknown
    /// </summary>
    None = -1,

    /// <summary>
    /// Robot is moving forward
    /// </summary>
    Forward = 0,

    /// <summary>
    /// Robot is moving backward
    /// </summary>
    Backward = 180,

    /// <summary>
    /// Robot is moving left
    /// </summary>
    Left = -90,

    /// <summary>
    /// Robot is moving right
    /// </summary>
    Right = 90
}