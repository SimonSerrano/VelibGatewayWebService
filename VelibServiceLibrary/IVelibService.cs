using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace VelibServiceLibrary
{
    [ServiceContract]
    public interface IVelibService
    {
        /// <summary>
        /// Lists all the stations in a given city, synchronous
        /// </summary>
        /// <param name="city">the city from which to list the stations</param>
        /// <returns>the list of stations in the given city</returns>
        [OperationContract]
        IList<Station> GetVelibStationsInCity(string city);

        /// <summary>
        /// Return the number of available bikes at a given station in a given city, synchronous
        /// </summary>
        /// <param name="city">the city to get the station from</param>
        /// <param name="station_number">the station where to count the number of available bikes</param>
        /// <returns>the number of available bikes at the given station in the given city</returns>
        [OperationContract]
        int GetVelibsAvailableForStation(string city, int station_number);

        /// <summary>
        /// Return the data of a station
        /// </summary>
        /// <param name="station_number">the city to get the station from</param>
        /// <param name="contract_name">the station where to count the number of available bikes</param>
        /// <returns>the number of available bikes at the given station in the given city</returns>
        [OperationContract]
        Station GetStationData(int station_number, string contract_name);

        /// <summary>
        /// Lists all the cities where JCDecaux is implemented, synchronous
        /// </summary>
        /// <returns>The list of cities</returns>
        [OperationContract]
        IList<String> GetCities();
    }


    [DataContract]
    public class Station
    {
        string name = "station";
        string status = "unknown";
        string contract_name = "unknown";
        int available_bikes = 0;
        int station_number = 0;


        [DataMember]
        public string Name
        {
            get => name;
            set => name = value;
        }

        [DataMember]
        public int AvailableBikes
        {
            get => available_bikes;
            set => available_bikes = value;
        }

        [DataMember]
        public int StationNumber
        {
            get => station_number;
            set => station_number = value;
        }

        [DataMember]
        public string Status
        {
            get => status;
            set => status = value;
        }

        [DataMember]
        public string ContractName
        {
            get => contract_name;
            set => contract_name = value;
        }
    }
}