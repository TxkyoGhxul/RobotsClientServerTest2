using RobotsClientServerTest2.Core.Dialogs;

namespace RobotsClientServerTest2.Core.DialogStates.Base;

/// <summary>
/// Represents server dialog state
/// </summary>
public interface IDialogState
{
    /// <summary>
    /// The max length of client's request
    /// </summary>
    int MaxLength { get; }

    /// <summary>
    /// Gets request from the client and returns server response
    /// </summary>
    /// <param name="request">Client request</param>
    /// <param name="dialog">The dialog with server</param>
    /// <returns>Server response</returns>
    string HandleRequest(string request, DialogServer dialog);
}
