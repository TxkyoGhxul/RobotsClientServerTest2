using RobotsClientServerTest2.Core.Dialogs;
using RobotsClientServerTest2.Core.Repositories;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Timers;

int port = 8888;

var tcpListener = new TcpListener(IPAddress.Any, port);

try
{
    tcpListener.Start();

    Console.WriteLine("Сервер запущен. Ожидание подключений... ");

    while (true)
    {
        var tcpClient = await tcpListener.AcceptTcpClientAsync();

        Task.Run(async () => await ProcessClientAsync(tcpClient));
    }
}
finally
{
    tcpListener.Stop();
}

async Task ProcessClientAsync(TcpClient tcpClient)
{
    var dialog = new DialogServer();

    var stream = tcpClient.GetStream();

    var response = new List<byte>();
    int bytesRead = 10;

    string lastRequest = string.Empty;

    var timerFromResponseToRequest = new System.Timers.Timer();
    timerFromResponseToRequest.Interval = 1000;
    timerFromResponseToRequest.Elapsed += (object source, ElapsedEventArgs e) =>
    {
        tcpClient.Close();
        return;
    };

    while (true)
    {
        while ((bytesRead = stream.ReadByte()) != -1)
        {
            response.Add((byte)bytesRead);

            timerFromResponseToRequest.Stop();
            timerFromResponseToRequest.Start();

            if (response[^1] == 8 && response[^2] == 7)
            {
                break;
            }

            if (response.Count >= dialog.CurrentState.MaxLength)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(ServerMessages.SERVER_SYNTAX_ERROR);
                await stream.WriteAsync(buffer);
                tcpClient.Client.Disconnect(true);
                return;
            }
        }

        timerFromResponseToRequest.Stop();

        if (response[^1] != 8 || response[^2] != 7)
        {
            tcpClient.Client.Disconnect(true);
            break;
        }

        var request = Encoding.UTF8.GetString(response.ToArray());
        lastRequest = request;

        Console.WriteLine(request);

        if (request == ClientMessages.CLIENT_RECHARGING.message)
        {
            timerFromResponseToRequest.Stop();
            timerFromResponseToRequest.Interval = 1000 * TimeConstants.TIMEOUT_RECHARGING;
            timerFromResponseToRequest.Start();
        }

        //if (lastRequest == ClientMessages.CLIENT_RECHARGING.message &&
        //          (request != ClientMessages.CLIENT_FULL_POWER.message || 
        //          request != ClientMessages.CLIENT_RECHARGING.message))
        //      {
        //          byte[] responseBuffer = Encoding.UTF8.GetBytes(ServerMessages.SERVER_LOGIC_ERROR);
        //          await stream.WriteAsync(responseBuffer);
        //          tcpClient.Client.Disconnect(true);
        //          return;
        //      }

        if (request == ClientMessages.CLIENT_FULL_POWER.message)
        {
            timerFromResponseToRequest.Interval = 1000 * TimeConstants.TIMEOUT;
        }

        var serverResponse = dialog.GetServerResponse(request);

        // Отправить ответ клиенту
        Console.WriteLine(response);
        byte[] responseBuffer = Encoding.UTF8.GetBytes(serverResponse);
        await stream.WriteAsync(responseBuffer);

        timerFromResponseToRequest.Start();

        if (serverResponse is ServerMessages.SERVER_LOGOUT or
                              ServerMessages.SERVER_LOGIN_FAILED or
                              ServerMessages.SERVER_LOGIC_ERROR or
                              ServerMessages.SERVER_SYNTAX_ERROR or
                              ServerMessages.SERVER_KEY_OUT_OF_RANGE_ERROR)
        {
            tcpClient.Client.Disconnect(true);
            break;
        }

        response.Clear();
    }

    tcpClient.Close();
}