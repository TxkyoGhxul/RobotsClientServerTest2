using RobotsClientServerTest2.Core.Dialogs;
using RobotsClientServerTest2.Core.DialogStates.Base;
using RobotsClientServerTest2.Core.Repositories;
using System.Text;

namespace RobotsClientServerTest2.Core.DialogStates;

/// <summary>
/// Gets name from client and asks for authentication id
/// </summary>
public class StartDialogState : IDialogState
{
    /// <inheritdoc />
	public int MaxLength => ClientMessages.CLIENT_USERNAME.maxLength;

    /// <inheritdoc/>
    public string HandleRequest(string request, DialogServer dialog)
    {
        if (IsContainsSeparators(request))
        {
            return ServerMessages.SERVER_SYNTAX_ERROR;
        }

        byte[] asciiBytes = Encoding.ASCII.GetBytes(request);

        ushort resultingHash = (ushort)(asciiBytes.Sum(c => c) * 1000 % 65536);
        AuthenticationKeysRepository.CURRENT_HASH = resultingHash;

        dialog.SetNewState(new AuthKeyIdDialogState());

        return ServerMessages.SERVER_KEY_REQUEST;
    }

    private bool IsContainsSeparators(string request)
    {
        for (int i = 0; i < request.Length - 1; i++)
        {
            if (request[i] == '\a' && request[i + 1] == '\b')
            {
                return true;
            }
        }

        return false;
    }
}
