using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace SocketListern
{
    class Listeren
    {
        public static List<string> receivedData = new List<string>();
        private static IPHostEntry _host;
        private static IPAddress _ipAddress;
        private static IPEndPoint _localEndPoint;
        private static Socket _listeren;
        private static string _folder;
        private static string _file;
        private static string _data;
        private static byte[] _bytes;


        public static void SetUp(int port = 11000, string nameHost = "localhost", string folder = "/PortParameters", string file = "/smsPort.txt")
        {
            if (folder[0] != '/' && file[0] != '/')
                 throw new Exception();

            Directory.CreateDirectory(folder);
            File.WriteAllText($"{folder}{file}", $"{port}-{nameHost}");
            _folder = folder;
            _file = file;
            _host = Dns.GetHostEntry(nameHost);
            _ipAddress = _host.AddressList[0];
            _localEndPoint = new IPEndPoint(_ipAddress, port);
            _listeren = new Socket(_ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _listeren.Bind(_localEndPoint);
            _listeren.Listen(10);
        }
        public static void StartServer()
        {
            Socket handler = _listeren.Accept();
            Task.Run(() =>
            {
                
                while (true)
                {
                    byte[] msg;
                    _bytes = new byte[1024];
                    int bytesRec = handler.Receive(_bytes);
                    _data = Encoding.ASCII.GetString(_bytes, 0, bytesRec);
                    if (string.IsNullOrEmpty(_data))
                    {
                        msg = Encoding.ASCII.GetBytes("2");
                        handler.Send(msg);
                        continue;
                    }
                    receivedData.Add(_data);
                    Console.WriteLine("Text received : {0}", _data);
                    msg = Encoding.ASCII.GetBytes("1");
                    handler.Send(msg);
                    _data = null;
                    Thread.Sleep(1000);
                }
            });
        }
    }
}
