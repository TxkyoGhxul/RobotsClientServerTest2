using RobotsClientServerTest2.Core.Dialogs;
using RobotsClientServerTest2.Core.DialogStates.Base;
using RobotsClientServerTest2.Core.Repositories;

namespace RobotsClientServerTest2.Core.DialogStates;

/// <summary>
/// Gets the secret message 
/// </summary>
public class SecretMessageDialogState : IDialogState
{
    /// <inheritdoc />
    public int MaxLength => ClientMessages.CLIENT_MESSAGE.maxLength;

    /// <inheritdoc />
    public string HandleRequest(string request, DialogServer dialog) => ServerMessages.SERVER_LOGOUT;
}
