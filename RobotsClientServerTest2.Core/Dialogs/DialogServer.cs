using RobotsClientServerTest2.Core.DialogStates;
using RobotsClientServerTest2.Core.DialogStates.Base;

namespace RobotsClientServerTest2.Core.Dialogs;

/// <summary>
/// Represents dialog with server
/// </summary>
public class DialogServer
{
    /// <summary>
    /// Current dialog state
    /// </summary>
    public IDialogState CurrentState { get; private set; }

    /// <summary>
    /// Constructor
    /// </summary>
    public DialogServer() => CurrentState = new StartDialogState();

    /// <summary>
    /// Method to set new dialog state
    /// </summary>
    /// <param name="newState">New dialog state</param>
    public void SetNewState(IDialogState newState) => CurrentState = newState;

    /// <summary>
    /// Gets request and returns server response
    /// </summary>
    /// <param name="request">Clients request with \a\b at the end</param>
    /// <returns>Server's response to the client</returns>
    public string GetServerResponse(string request)
    {
        var requestWithoutTerminators = request[..^2];

        return CurrentState.HandleRequest(requestWithoutTerminators, this);
    }
}
