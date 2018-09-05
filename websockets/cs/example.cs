using System;
using PureWebSockets;
using System.Net.WebSockets;

namespace TestProject{
    class Program{
        private static PureWebSocket _ws;
        static void Main(string[] args){
            var socketOptions = new PureWebSocketOptions(){
                DebugMode = true, SendDelay = 100,
            };

            _ws = new PureWebSocket("wss://socket.polygon.io/forex", socketOptions);
            _ws.OnStateChanged += Ws_OnStateChanged;
            _ws.OnMessage += Ws_OnMessage;
            _ws.OnClosed += Ws_OnClosed;
            _ws.OnSendFailed += _ws_OnSendFailed;
            _ws.Connect();

            _ws.Send("{\"action\":\"auth\",\"params\":\"YOUR_API_KEY\"}");
            _ws.Send("{\"action\":\"subscribe\",\"params\":\"C.*\"}");

            Console.ReadLine();

        }
        private static void _ws_OnSendFailed(string data, Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{DateTime.Now} Send Failed: {ex.Message}");
            Console.ResetColor();
            Console.WriteLine("");
        }

        private static void Ws_OnClosed(WebSocketCloseStatus reason)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{DateTime.Now} Connection Closed: {reason}");
            Console.ResetColor();
            Console.WriteLine("");
            Console.ReadLine();
        }

        private static void Ws_OnMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{DateTime.Now} New message: {message}");
            Console.ResetColor();
            Console.WriteLine("");
        }

        private static void Ws_OnStateChanged(System.Net.WebSockets.WebSocketState newState, System.Net.WebSockets.WebSocketState prevState)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{DateTime.Now} Status changed from {prevState} to {newState}");
            Console.ResetColor();
            Console.WriteLine("");
        }
    }
}
