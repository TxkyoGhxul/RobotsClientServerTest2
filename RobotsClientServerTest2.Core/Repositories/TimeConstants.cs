namespace RobotsClientServerTest2.Core.Repositories;

/// <summary>
/// Contains time constants
/// </summary>
public class TimeConstants
{
    /// <summary>
    /// Both the server and the client wait for a response 
    /// from the counterparty during this interval.
    /// </summary>
    public const int TIMEOUT = 1;

    /// <summary>
    /// The time interval during which the robot must finish recharging.
    /// </summary>
    public const int TIMEOUT_RECHARGING = 5;
}
