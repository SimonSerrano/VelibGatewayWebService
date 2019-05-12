using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using VelibServiceLibrary.requests;
using VelibServiceLibrary.utils;

namespace VelibServiceLibrary
{
    public class VelibService : IVelibService
    {
        private Cache cache = new Cache();

        /// <inheritdoc />
        public int GetVelibsAvailableForStation(string city, int station_number)
        {
            VelibRequest request = new VelibRequest();
            return request.getAvalaibleBikes(city, station_number);
        }

        public Station GetStationData(int station_number, string contract_name)
        {
            VelibRequest request = new VelibRequest();
            return request.GetStationData(station_number, contract_name);
        }


        /// <inheritdoc />
        public IList<Station> GetVelibStationsInCity(string city)
        {
            IList<Station> res = cache.checkStations(city);
            if (res == null)
            {
                VelibRequest request = new VelibRequest();
                res = request.getStationsForCity(city);
                cache.cacheStations(city, res);
            }

            return res;
        }

        /// <inheritdoc />
        public IList<String> GetCities()
        {
            IList<String> result = cache.checkCities();
            if (result == null)
            {
                VelibRequest request = new VelibRequest();
                result = request.GetCities();
                cache.cacheCities(result);
            }

            return result;
        }

        // <inheritdoc />
        public async Task<IList<string>> GetCitiesAsync()
        {
            IList<String> result = cache.checkCities();
            if (result == null)
            {
                VelibRequest request = new VelibRequest();
                result = await request.GetCitiesAsync();
                cache.cacheCities(result);
            }

            return result;
        }

        // <inheritdoc />
        public async Task<IList<Station>> GetVelibStationsInCityAsync(string city)
        {
            IList<Station> res = cache.checkStations(city);
            if (res == null)
            {
                VelibRequest request = new VelibRequest();
                res = await request.getStationsForCityAsync(city);
                cache.cacheStations(city, res);
            }

            return res;
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