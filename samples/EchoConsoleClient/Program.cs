using System;
using System.Threading.Tasks;
using WebSocketManager.Client;
using WebSocketManager.Common;

public class Program
{
    private static Connection _connection;
    public static void Main(string[] args)
    {
        StartConnectionAsync();

        _connection.On("receiveMessage", (arguments) =>
        {
            Console.WriteLine($"{arguments[0]} said: {arguments[1]}");
        });

        while (true)
        {
            string msg = Console.ReadLine();
            if (string.IsNullOrEmpty(msg))
                break;

            _connection.Invoke("SendMessage", new string[]{_connection.ConnectionId, msg});
        }
        StopConnectionAsync();
    }

    public static async Task StartConnectionAsync()
    {
        _connection = new Connection();
        await _connection.StartConnectionAsync("ws://localhost:65110/chat");
    }

    public static async Task StopConnectionAsync()
    {
        await _connection.StopConnectionAsync();
    }
}