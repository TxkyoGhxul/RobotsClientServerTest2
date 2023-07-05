using RobotsClientServerTest2.Core.Dialogs;
using RobotsClientServerTest2.Core.DialogStates.Base;
using RobotsClientServerTest2.Core.Repositories;

namespace RobotsClientServerTest2.Core.DialogStates;

/// <summary>
/// Gets key id (from 0 to 4) and returns message with a confirmation code
/// </summary>
public class AuthKeyIdDialogState : IDialogState
{
    /// <inheritdoc />
    public int MaxLength => ClientMessages.CLIENT_KEY_ID.maxLength;

    /// <inheritdoc/>
    public string HandleRequest(string request, DialogServer dialog)
    {
        bool isInteger = int.TryParse(request, out int userSelectedId);

        if (!isInteger)
        {
            return ServerMessages.SERVER_SYNTAX_ERROR;
        }

        if (userSelectedId < 0 || userSelectedId > 4)
        {
            return ServerMessages.SERVER_KEY_OUT_OF_RANGE_ERROR;
        }

        var serverKey = AuthenticationKeysRepository
            .GetKeys()
            .FirstOrDefault(k => k.Id == userSelectedId)?
            .ServerKey;

        AuthenticationKeysRepository.CURRENT_ID = userSelectedId;

        ushort serverConfirmationCode = (ushort)((AuthenticationKeysRepository.CURRENT_HASH + serverKey) % 65536);

        dialog.SetNewState(new ClientConfirmationDialogState());

        return ServerMessages.SERVER_CONFIRMATION(serverConfirmationCode);
    }
}
