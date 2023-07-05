using RobotsClientServerTest2.Core.Models;

namespace RobotsClientServerTest2.Core.Repositories;

/// <summary>
/// Contains authentication keys
/// </summary>
public static class AuthenticationKeysRepository
{
    /// <summary>
    /// Id choosen by client
    /// </summary>
    public static int CURRENT_ID;

    /// <summary>
    /// Client's hash to authenticate
    /// </summary>
    public static ushort CURRENT_HASH;

    /// <summary>
    /// Method to get all keys
    /// </summary>
    /// <returns>All keys</returns>
    public static IEnumerable<AuthenticationKey> GetKeys()
    {
        return new List<AuthenticationKey>()
        {
            new AuthenticationKey(0, 23019, 32037),
            new AuthenticationKey(1, 32037, 29295),
            new AuthenticationKey(2, 18789, 13603),
            new AuthenticationKey(3, 16443, 29533),
            new AuthenticationKey(4, 18189, 21952)
        };
    }
}
