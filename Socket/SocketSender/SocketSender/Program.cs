using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketSender
{
    class Program
    {
        static void Main(string[] args)
        {
            Sender.SetUp();
            Sender.StartClient();
            Console.ReadLine();
        }
    }
}
