using RobotsClientServerTest2.Core.Repositories;
using System.Net.Sockets;
using System.Text;

int port = 8888;
string ip = "127.0.0.1";

using TcpClient tcpClient = new TcpClient();

//tcpClient.Client.SetSocketOption(SocketOptionLevel.Socket, 
//    SocketOptionName.ReceiveTimeout, TimeConstants.TIMEOUT * 1000);

await tcpClient.ConnectAsync(ip, port);
var stream = tcpClient.GetStream();

// буфер для входящих данных
var response = new List<byte>();
int bytesRead = 10; // для считывания байтов из потока

while (true)
{
    // считываем строку в массив байтов
    // при отправке добавляем маркер завершения сообщения - \n
    string request = Console.ReadLine();

    byte[] data = Encoding.UTF8.GetBytes(request + '\n');
    // отправляем данные
    await stream.WriteAsync(data);

    bool isRecharge = request == ClientMessages.CLIENT_RECHARGING.message;
    if (isRecharge) continue;

    // считываем данные до конечного символа
    while ((bytesRead = stream.ReadByte()) != '\n')
    {
        // добавляем в буфер
        response.Add((byte)bytesRead);
    }

    var serverResponse = Encoding.UTF8.GetString(response.ToArray());
    Console.WriteLine($"Client request: {request}. Server response: {serverResponse}");
    response.Clear();


    if (serverResponse + "\b" == ServerMessages.SERVER_OK)
    {
        while ((bytesRead = stream.ReadByte()) != '\b')
        {
            // добавляем в буфер
            response.Add((byte)bytesRead);
        }

        var serverResponse2 = Encoding.UTF8.GetString(response.ToArray());
        Console.WriteLine($"Client request: {request}. Server response: {serverResponse2}");
        response.Clear();

        Console.WriteLine("S: {0}", serverResponse2);
        continue;
    }

    if (serverResponse + "\b" is ServerMessages.SERVER_LOGOUT or
                          ServerMessages.SERVER_LOGIN_FAILED or
                          ServerMessages.SERVER_LOGIC_ERROR or
                          ServerMessages.SERVER_SYNTAX_ERROR)
    //or ServerMessages.SERVER_PICK_UP
    {
        Console.WriteLine("Пзда вылетаем");
        Console.ReadKey();
        Console.ReadLine();
        break;
    }


    // имитируем долговременную работу, чтобы одновременно несколько клиентов обрабатывались
    //await Task.Delay(500);
}

// отправляем маркер завершения подключения - END
await stream.WriteAsync(Encoding.UTF8.GetBytes("END\n"));