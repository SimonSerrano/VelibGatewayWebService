using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace VelibServiceLibrary
{
    [ServiceContract]
    public interface IVelibService
    {

        [OperationContract]
        IList<Station> GetVelibStationsInCity(string city);

        [OperationContract]
        int GetVelibsAvailableForStation(string city, int station_number);

        [OperationContract]
        IList<String> GetCities();


    }



    [DataContract]
    public class Station
    {
        string name = "station";
        int available_bikes = 0;
        int station_number = 0;



        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DataMember]
        public int AvailableBikes
        {
            get { return available_bikes; }
            set { available_bikes = value; }
        }

        [DataMember]
        public int StationNumber
        {
            get { return station_number; }
            set { station_number = value; }
        }
    }
}
