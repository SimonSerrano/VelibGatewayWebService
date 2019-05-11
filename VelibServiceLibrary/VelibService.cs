using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using VelibServiceLibrary.requests;

namespace VelibServiceLibrary
{
    
    public class VelibService : IVelibService
    {
        /// <inheritdoc />
        
        public int GetVelibsAvailableForStation(string city, int station_number)
        {
            VelibRequest request = new VelibRequest();
            return request.getAvalaibleBikes(city, station_number);
        }

        

        /// <inheritdoc />
        public IList<Station> GetVelibStationsInCity(string city)
        {
            VelibRequest request = new VelibRequest();
            return request.getStationsForCity(city);
        }

        /// <inheritdoc />
        public IList<String> GetCities()
        {
            VelibRequest request = new VelibRequest();
            return request.GetCities();
        }

        // <inheritdoc />
        public async Task<IList<string>> GetCitiesAsync()
        {
            VelibRequest request = new VelibRequest();
            IList<string> result = await request.GetCitiesAsync();
            return result;
        }

        // <inheritdoc />
        public async Task<IList<Station>> GetVelibStationsInCityAsync(string city)
        {
            VelibRequest request = new VelibRequest();
            IList<Station> stations = await request.getStationsForCityAsync(city);
            return stations;
        }

        // <inheritdoc />
        public async Task<int> GetVelibsAvailableForStationAsync(string city, int station_number)
        {
            VelibRequest request = new VelibRequest();
            int number_of_bikes = await request.getAvalaibleBikesAsync(city, station_number);
            return number_of_bikes;
        }
    }
}
