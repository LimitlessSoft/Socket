using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace SocketSender
{
    class Sender
    {
        public static List<Sms> messages = new List<Sms>() { new Sms() { Broj = "04324324", Tekst= "Frist message" }, new Sms() { Broj = "04324324", Tekst = "Seconde message" } };
        private static IPHostEntry _host;
        private static IPAddress _ipAddress;
        private static IPEndPoint _remoteEndPoint;
        private static Socket _sender;
        private static string _folder;
        private static string _file;

        private static Task _Connect()
        {
            return Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        _sender.Connect(_remoteEndPoint);
                        return;
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(1000);
                    }
                }
            });
        }
        public static void SetUp(string folder = "/PortParameters", string file = "/smsPort.txt")
        {
            if (!File.Exists($"{folder}{file}"))
                throw new Exception();

            _folder = folder;
            _file = file;
            string parameter = File.ReadAllText($"{_folder}{_file}");
            _host = Dns.GetHostEntry(parameter.Split('-')[1]);
            _ipAddress = _host.AddressList[0];
            _remoteEndPoint = new IPEndPoint(_ipAddress, Convert.ToInt32(parameter.Split('-')[0]));
            _sender = new Socket(_ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }
        
      
        public static void StartClient()
        {
            byte[] bytes = new byte[1024];
            Task.Run(() =>
            {
                _Connect().Wait();
                while (true)
                {
                    try
                    {
                      
                        if (messages.Count > 0)
                        {
                            byte[] msg = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(messages[0]));

                            int bytesSent = _sender.Send(msg);

                            int bytesRec = _sender.Receive(bytes);
                            string get = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                            if (get == "1")
                                messages.RemoveAt(0);

                            bytes = new byte[1024];
                            Thread.Sleep(1000);
                        }

                        Thread.Sleep(10000);
                    }
                    catch (Exception)
                    {

                    }

                }
            });
        }
    }
}
