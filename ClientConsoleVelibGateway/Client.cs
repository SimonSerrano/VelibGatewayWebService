using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsoleVelibGateway
{
    class Client
    {
        public Client() { }

        public void ExecuteCommand(string command)
        {
            Command commandObj = parseCommand(command);
            commandObj.Execute();
        }

        private Command parseCommand(string command)
        {
            string[] splits;
            string[] separators = { " " };
            Commands? commandName = null;

            splits = command.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            if(Enum.IsDefined(typeof(Commands), splits[0].ToUpper()))
            {
                commandName = (Commands)Enum.Parse(typeof(Commands), splits[0].ToUpper());
            }

            string[] args = new string[0];

            if (splits.Length > 0)
            {
                args = new string[splits.Length - 1];
            }
            
            for(int i = 0; i<args.Length; ++i)
            {
                args[i] = splits[i + 1];
            }

            return new Command(commandName, args);
            
            

        }
    }
}
