namespace RobotsClientServerTest2.Core.Repositories;

/// <summary>
/// Contains server messages to client
/// </summary>
public static class ServerMessages
{
    /// <summary>
    /// A message with a confirmation code. 
    /// It can contain a maximum of 5 numbers and the ending sequence \a\b.
    /// </summary>
    /// <param name="serverConfirmationCode">16-bit number in decimal notation</param>
    /// <returns>Server confirmation message</returns>
    public static string SERVER_CONFIRMATION(ushort serverConfirmationCode) => $"{serverConfirmationCode}\a\b";

    /// <summary>
    /// Command to move forward one space
    /// </summary>
    public const string SERVER_MOVE = "102 MOVE\a\b";

    /// <summary>
    /// Command to turn left
    /// </summary>
    public const string SERVER_TURN_LEFT = "103 TURN LEFT\a\b";

    /// <summary>
    /// Command to turn right
    /// </summary>
    public const string SERVER_TURN_RIGHT = "104 TURN RIGHT\a\b";

    /// <summary>
    /// The command to retrieve the message
    /// </summary>
    public const string SERVER_PICK_UP = "105 GET MESSAGE\a\b";

    /// <summary>
    /// Command to end the connection after successfully picking up the message
    /// </summary>
    public const string SERVER_LOGOUT = "106 LOGOUT\a\b";

    /// <summary>
    /// Server request for Key ID for communication
    /// </summary>
    public const string SERVER_KEY_REQUEST = "107 KEY REQUEST\a\b";

    /// <summary>
    /// Positive confirmation
    /// </summary>
    public const string SERVER_OK = "200 OK\a\b";

    /// <summary>
    /// Authentication failed
    /// </summary>
    public const string SERVER_LOGIN_FAILED = "300 LOGIN FAILED\a\b";

    /// <summary>
    /// Incorrect message syntax
    /// </summary>
    public const string SERVER_SYNTAX_ERROR = "301 SYNTAX ERROR\a\b";

    /// <summary>
    /// A message sent in the wrong situation
    /// </summary>
    public const string SERVER_LOGIC_ERROR = "302 LOGIC ERROR\a\b";

    /// <summary>
    /// Key ID is not in the expected range
    /// </summary>
    public const string SERVER_KEY_OUT_OF_RANGE_ERROR = "303 KEY OUT OF RANGE\a\b";
}
