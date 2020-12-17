using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketListern
{
    class Program
    {
        static void Main(string[] args)
        {
            Listeren.SetUp();
            Listeren.StartServer();
            Console.ReadLine();
        }
    }
}
