namespace RobotsClientServerTest2.Core.Models;

/// <summary>
/// Represents an instance of the key for authentication
/// </summary>
public class AuthenticationKey
{
    /// <summary>
    /// Key id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The key for server
    /// </summary>
    public ushort ServerKey { get; set; }

    /// <summary>
    /// The key for client
    /// </summary>
    public ushort ClientKey { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="id">Key id</param>
    /// <param name="serverKey">The key for server</param>
    /// <param name="clientKey">The key for client</param>
    public AuthenticationKey(int id, ushort serverKey, ushort clientKey)
    {
        Id = id;
        ServerKey = serverKey;
        ClientKey = clientKey;
    }
}
