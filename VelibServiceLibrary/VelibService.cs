using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using VelibServiceLibrary.requests;

namespace VelibServiceLibrary
{
   
    public class VelibService : IVelibService
    {
        /// <summary>
        /// Return the number of available bikes at a given station in a given city
        /// </summary>
        /// <param name="city">the city to get the station from</param>
        /// <param name="station_number">the station where to count the number of available bikes</param>
        /// <returns>the number of available bikes at the given station in the given city</returns>
        public int GetVelibsAvailableForStation(string city, int station_number)
        {
            VelibRequest request = new VelibRequest();
            return request.getAvalaibleBikes(city, station_number);
        }

        /// <summary>
        /// Lists all the stations in a given city
        /// </summary>
        /// <param name="city">the city from which to list the stations</param>
        /// <returns>the list of stations in the given city</returns>
        public IList<Station> GetVelibStationsInCity(string city)
        {
            VelibRequest request = new VelibRequest();
            return request.getStationsForCity(city);
        }

        public IList<String> GetCities()
        {
            VelibRequest request = new VelibRequest();
            return request.GetCities();
        }
    }
}
