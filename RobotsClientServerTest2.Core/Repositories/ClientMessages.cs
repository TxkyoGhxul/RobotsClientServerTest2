namespace RobotsClientServerTest2.Core.Repositories;

/// <summary>
/// Contains client messages
/// </summary>
public static class ClientMessages
{
    /// <summary>
    /// Client username message
    /// </summary>
    public static (string message, int maxLength) CLIENT_USERNAME => ("<user name>\a\b", 20);

    /// <summary>
    /// Client key message
    /// </summary>
    public static (string message, int maxLength) CLIENT_KEY_ID => ("<Key ID>\a\b", 5);

    /// <summary>
    /// Client confirmation message
    /// </summary>
    public static (string message, int maxLength) CLIENT_CONFIRMATION => ("<16-bit number in decimal notation>\a\b", 7);

    /// <summary>
    /// Client's position message
    /// </summary>
    public static (string message, int maxLength) CLIENT_OK => ("OK <x> <y>\a\b", 12);

    /// <summary>
    /// Robot's rechanging message
    /// </summary>
    public static (string message, int maxLength) CLIENT_RECHARGING => ("RECHARGING\a\b", 12);

    /// <summary>
    /// Robot's full power message
    /// </summary>
    public static (string message, int maxLength) CLIENT_FULL_POWER => ("FULL POWER\a\b", 12);

    /// <summary>
    /// Client's secret message
    /// </summary>
    public static (string message, int maxLength) CLIENT_MESSAGE => ("<text>\a\b", 100);
}
