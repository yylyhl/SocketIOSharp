using EngineIOSharp.Common.Enum;
using Newtonsoft.Json.Linq;
using SocketIOSharp.Client;
using SocketIOSharp.Common;
using System;
using System.Threading;

namespace SocketIOSharp.Example.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(3000);
            var ExtraHeaders = new System.Collections.Generic.Dictionary<string, string>()
            {
                { "token", "tokenVal-" },
                { "matchId", "123456-" }
            };
            var client = new SocketIOClient(new SocketIOClientOption(EngineIOScheme.http, "localhost", 9001, ExtraHeaders: ExtraHeaders, Reconnection: false));
            InitEventHandlers(client);
            Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} 开始连接......");
            client.Connect();
            Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} 连接了 Input /exit to close connection.");

            string line;
            while (!(line = Console.ReadLine()).Equals("/exit"))
            {
                client.Emit("input", line);
                client.Emit("input array", line, line);
            }

            client.Close();

            Console.WriteLine("Press any key to continue...");
            Console.Read();
        }

        static void InitEventHandlers(SocketIOClient client)
        {
            client.On(SocketIOEvent.CONNECTION, () =>
            {
                Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} connected!");
            });

            client.On(SocketIOEvent.DISCONNECT, () =>
            {
                Console.WriteLine();
                Console.WriteLine("Disconnected!");
            });

            client.On("echo", (Data) =>
            {
                Console.WriteLine("Echo : " + (Data[0].Type == JTokenType.Bytes ? BitConverter.ToString(Data[0].ToObject<byte[]>()) : Data[0]));
            });

            client.On("echo array", (Data) =>
            {
                Console.WriteLine("Echo1 : " + Data[0]);
                Console.WriteLine("Echo2 : " + Data[1]);
            });
        }
    }
}
