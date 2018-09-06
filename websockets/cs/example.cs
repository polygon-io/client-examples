using System;
using WebSocket4Net;
using System.Security.Authentication;

namespace HelloWorld
{
    class Hello
    {
        WebSocket websocket;
        static void Main()
        {
            new Hello().Start();
        }
        public void Start(){
            this.websocket = new WebSocket("wss://socket.polygon.io/stocks", sslProtocols: SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls);
            this.websocket.Opened += websocket_Opened;
            this.websocket.Error += websocket_Error;
            this.websocket.Closed += websocket_Closed;
            this.websocket.MessageReceived += websocket_MessageReceived;
            this.websocket.Open();
            Console.ReadKey();
        }
        private void websocket_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("Connected!");
            this.websocket.Send("{\"action\":\"auth\",\"params\":\"YOUR_API_KEY\"}");
            this.websocket.Send("{\"action\":\"subscribe\",\"params\":\"T.AAPL\"}");
        }
        private void websocket_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            Console.WriteLine("WebSocket Error");
            Console.WriteLine(e.Exception.Message);
        }
        private void websocket_Closed(object sender, EventArgs e)
        {
            Console.WriteLine("Connection Closed...");
            // Add Reconnect logic... this.Start()
        }
        private void websocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
