using RobotsClientServerTest2.Core.Dialogs;
using RobotsClientServerTest2.Core.DialogStates.Base;
using RobotsClientServerTest2.Core.Repositories;

namespace RobotsClientServerTest2.Core.DialogStates;

/// <summary>
/// Gets clients confirmation code
/// </summary>
public class ClientConfirmationDialogState : IDialogState
{
    /// <inheritdoc />
    public int MaxLength => ClientMessages.CLIENT_CONFIRMATION.maxLength;

    /// <inheritdoc />
    public string HandleRequest(string request, DialogServer dialog)
    {
        bool isUShort = ushort.TryParse(request, out ushort clientsSentCode);

        if (!isUShort || IsContainsSpace(request, clientsSentCode))
        {
            return ServerMessages.SERVER_SYNTAX_ERROR;
        }

        var clientKey = AuthenticationKeysRepository
            .GetKeys()
            .FirstOrDefault(k => k.Id == AuthenticationKeysRepository.CURRENT_ID)?
            .ClientKey;

        ushort clientConfirmationCode = (ushort)((AuthenticationKeysRepository.CURRENT_HASH + clientKey) % 65536);

        if (clientsSentCode != clientConfirmationCode)
        {
            return ServerMessages.SERVER_LOGIN_FAILED;
        }

        dialog.SetNewState(new RobotMovingDialogState());

        return ServerMessages.SERVER_OK + ServerMessages.SERVER_MOVE;
    }

    private bool IsContainsSpace(string request, ushort clientsSentCode) =>
        clientsSentCode.ToString().Length != request.Length;
}
