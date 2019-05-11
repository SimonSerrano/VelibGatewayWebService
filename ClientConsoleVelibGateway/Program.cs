using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsoleVelibGateway
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();
            while (true)
            {
                client.ExecuteCommand(Console.ReadLine());
            }

        }
    }
}
