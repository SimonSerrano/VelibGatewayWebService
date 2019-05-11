using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsoleVelibGateway
{

    enum Commands
    {
        [Description("Lists all the cities with velib stations")]
        VELIB_CITIES,
        [Description("Lists the velib stations for a given city")]
        VELIB_STATIONS,
        [Description("Gives the number of available bikes at a given station for a given city")]
        VELIB_STATION_BIKES,
        [Description("Lists all the available commands for the client")]
        HELP,
        [Description("Exits the program")]
        EXIT
    }

    class Command
    {
        private Commands? command;
        private string[] args;
        private VelibServiceReference.IVelibService velibService;
       

        private static readonly string AVAILABLE_COMMANDS = "List of commands : \n\t" + Commands.HELP + "\tLists all the available commands\n\t"
            + Commands.EXIT + "\tExits the program\n\t" + 
            Commands.VELIB_CITIES + "\tLists all the cities with velib stations\n\t" +
            Commands.VELIB_STATIONS + "\t[city]\tLists all the station for the given city\n\t" +
            Commands.VELIB_STATION_BIKES + "\t[city] [station]\t Gives the number of available bikes at a given station for a given city\n";

        public Command(Commands? command, string[] args)
        {
            this.command = command;
            this.args = args;
            this.velibService = new VelibServiceReference.VelibServiceClient();
            
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        public void Execute()
        {
            switch (command)
            {
                case Commands.HELP:
                    ListAvailableCommands();
                    break;
                case Commands.VELIB_STATIONS:
                    if (args.Length == 0)
                    {
                        Console.WriteLine("Number of arguments is too small, type 'help' to see proper usage of command");
                    }else {
                        Console.WriteLine("Requesting stations for city : " + args[0]);
                        RequestStations();
                    }
                    
                    break;
                case Commands.VELIB_STATION_BIKES:
                    if (args.Length == 1)
                    {
                        Console.WriteLine("Number of arguments is too small, type 'help' to see proper usage of command");
                    }else {
                        Console.WriteLine("Requesting number of available bikes for station : " + args[1] + "in city : " + args[0]);
                        RequestAvalaibleBikes();
                    }
                    
                    break;
                case Commands.EXIT:
                    ExitClient();
                    break;
                case Commands.VELIB_CITIES:
                    Console.WriteLine("Requesting cities :");
                    RequestCities();
                    break;
                default:
                    Console.WriteLine("This command is not available, type 'help' to list available commands.");
                    break;
            }
        }

        private void RequestCities()
        {
            IList<String> cities = this.velibService.GetCities();
            foreach(String city in cities)
            {
                Console.WriteLine(city);
            }
        }

        private void RequestStations()
        {
            IList<VelibServiceReference.Station> stations = this.velibService.GetVelibStationsInCity(args[0].ToLower());
            Console.WriteLine("Number of stations for " + args[0] + " : " + stations.Count);
            foreach(VelibServiceReference.Station station in stations){
                Console.WriteLine(station.Name + "\t" + station.AvailableBikes + "\t" + station.StationNumber);
            }
        }

        private void RequestAvalaibleBikes()
        {
            int number = this.velibService.GetVelibsAvailableForStation(args[0], int.Parse(args[1]));
            Console.WriteLine(number);
        }

        private void ExitClient()
        {
            System.Environment.Exit(0);
        }

        private void ListAvailableCommands()
        {
            Console.WriteLine(AVAILABLE_COMMANDS);
        }
    }
}
