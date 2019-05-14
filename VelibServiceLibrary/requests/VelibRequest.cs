using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using VelibServiceLibrary.utils;

namespace VelibServiceLibrary.requests
{
    public class VelibRequest
    {
        private static readonly string API_KEY = "e561d2fbe2894d1a32eab3672038e981d98cda87";


        /// <summary>
        /// Lists all the stations for a given city, synchronous
        /// </summary>
        /// <param name="city">the city to get the stations from</param>
        /// <returns>the list of stations for the given city</returns>
        public IList<Station> getStationsForCity(string city)
        {
            increaseNumRequest();
            WebRequest request = WebRequest.Create("https://api.jcdecaux.com/vls/v1/stations?contract=" + city +
                                                   "&apiKey=" + API_KEY);
            StreamReader reader = null;
            WebResponse response = null;
            String result = "";
            DateTime start = DateTime.Now;
            try
            {
                response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                reader = new StreamReader(dataStream);
                result = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            IList<Station> res = ParseStations(city, result);
            DateTime end = DateTime.Now;
            computeMeanRequestTime(end.Millisecond - start.Millisecond);
            appendEvolutionTime(end.Millisecond - start.Millisecond);
            return res;
        }

        public Station GetStationData(int station_number, string contract_name)
        {
            increaseNumRequest();
            WebRequest request = WebRequest.Create("https://api.jcdecaux.com/vls/v1/stations/" + station_number +
                                                   "?contract=" + contract_name +
                                                   "&apiKey=" + API_KEY);
            StreamReader reader = null;
            WebResponse response = null;
            string result = "";
            DateTime start = DateTime.Now;
            try
            {
                response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                reader = new StreamReader(dataStream);
                result = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            DateTime end = DateTime.Now;
            Station res = ParseStation(result);
            computeMeanRequestTime(end.Millisecond - start.Millisecond);
            appendEvolutionTime(end.Millisecond - start.Millisecond);
            return res;
        }

        /// <summary>
        /// Lists all the stations for a given city, asynchronous
        /// </summary>
        /// <param name="city">the city to get the stations from</param>
        /// <returns>the list of stations for the given city</returns>
        public Task<IList<Station>> getStationsForCityAsync(string city)
        {
            return Task<IList<Station>>.Run(() => { return getStationsForCity(city); });
        }


        private IList<Station> ParseStations(string city, string data)
        {
            JArray array = null;
            IList<Station> res = new List<Station>();
            try
            {
                array = JArray.Parse(data);
            }
            catch (Exception e)
            {
                return res;
            }

            int size = array.Count();
            for (int i = 0; i < size; ++i)
            {
                string name = (string) ((JObject) array[i])["name"];
                int available_bikes = (int) ((JObject) array[i])["available_bikes"];
                int station_number = (int) ((JObject) array[i])["number"];
                JObject position = (JObject)((JObject)array[i])["position"];
                string station_status = (string) ((JObject) array[i])["status"];
                string contract_name = (string) ((JObject) array[i])["contract_name"];
                Console.WriteLine(name);
                Station station = new Station();
                station.Name = name;
                station.AvailableBikes = available_bikes;
                station.StationNumber = station_number;
                station.Status = station_status;
                station.ContractName = contract_name;
                station.Latitude = (float)(position)["lat"];
                station.Longitude = (float)(position)["lng"];
                res.Add(station);
            }

            return res;
        }

        private Station ParseStation(string data)
        {
            JObject obj = null;
            try
            {
                obj = JObject.Parse(data);
            }
            catch (Exception e)
            {
                return null;
            }

            string name = (string) obj["name"];
            int available_bikes = (int) obj["available_bikes"];
            int station_number = (int) obj["number"];
            string station_status = (string) obj["status"];
            string contract_name = (string) obj["contract_name"];
            Console.WriteLine(name);
            Station station = new Station();
            station.Name = name;
            station.AvailableBikes = available_bikes;
            station.StationNumber = station_number;
            station.Status = station_status;
            station.ContractName = contract_name;
            return station;
        }

        /// <summary>
        /// Get the list of cities from JCDecaux, synchronous
        /// </summary>
        /// <returns>The list of cities</returns>
        public IList<string> GetCities()
        {
            increaseNumRequest();
            WebRequest request = WebRequest.Create("https://api.jcdecaux.com/vls/v1/contracts?apiKey=" + API_KEY);
            StreamReader reader = null;
            WebResponse response = null;
            String result = "";
            DateTime start = DateTime.Now;
            try
            {
                response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                reader = new StreamReader(dataStream);
                result = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            IList<string> res = ParseCities(result);
            DateTime end = DateTime.Now;
            int second = (end.Millisecond - start.Millisecond);
            computeMeanRequestTime(second);
            appendEvolutionTime(second);
            return res;
        }

        /// <summary>
        /// Get the list of cities from JCDecaux, synchronous
        /// </summary>
        /// <returns>The list of cities</returns>
        public Task<IList<string>> GetCitiesAsync()
        {
            return Task<IList<string>>.Run(() => { return GetCities(); });
        }

        private IList<string> ParseCities(string result)
        {
            JArray array = null;
            IList<String> cities = new List<String>();
            try
            {
                array = JArray.Parse(result);
            }
            catch (Exception e)
            {
                return cities;
            }

            int size = array.Count();
            for (int i = 0; i < size; ++i)
            {
                cities.Add((string) ((JObject) array[i])["name"]);
            }

            return cities;
        }

        /// <summary>
        /// Give the available bikes for a given station in a given city, synchronous
        /// </summary>
        /// <param name="city">the city to get the sation from</param>
        /// <param name="station_number">the given station number which has available bikes</param>
        /// <returns>the number of available bikes at the given station in the given city</returns>
        public int getAvalaibleBikes(string city, int station_number)
        {
            increaseNumRequest();
            WebRequest request = WebRequest.Create("https://api.jcdecaux.com/vls/v1/stations/" + station_number +
                                                   "?contract=" + city + "&apiKey=" + API_KEY);
            StreamReader reader = null;
            WebResponse response = null;
            String result = "";
            DateTime start = DateTime.Now;
            try
            {
                response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                reader = new StreamReader(dataStream);
                result = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            int res = ParseAvailableBikes(result);
            DateTime end = DateTime.Now;
            computeMeanRequestTime(end.Millisecond - start.Millisecond);
            appendEvolutionTime(end.Millisecond - start.Millisecond);
            return res;
        }

        /// <summary>
        /// Give the available bikes for a given station in a given city, asynchronous
        /// </summary>
        /// <param name="city">the city to get the sation from</param>
        /// <param name="station_number">the given station number which has available bikes</param>
        /// <returns>the number of available bikes at the given station in the given city</returns>
        public Task<int> getAvalaibleBikesAsync(string city, int station_number)
        {
            return Task<int>.Run(() => { return getAvalaibleBikes(city, station_number); });
        }


        private int ParseAvailableBikes(string result)
        {
            JObject jobject = null;
            int available_bikes = 0;
            try
            {
                jobject = JObject.Parse(result);
            }
            catch (Exception e)
            {
                return -1;
            }

            available_bikes = (int) jobject["available_bikes"];
            return available_bikes;
        }

        private void increaseNumRequest()
        {
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + Constant.monitoring_dir))
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + Constant.monitoring_dir);
            int num_request =
                SaverLoader.ReadFromBinaryFile<int>(AppDomain.CurrentDomain.BaseDirectory + Constant.monitoring_dir + Constant.number_request_file);
            num_request += 1;
            SaverLoader.WriteToBinaryFile<int>(
                AppDomain.CurrentDomain.BaseDirectory + Constant.monitoring_dir + Constant.number_request_file, num_request);
        }

        private void computeMeanRequestTime(int seconds)
        {
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + Constant.monitoring_dir))
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + Constant.monitoring_dir);

            Tuple<int, int> time = SaverLoader.ReadFromBinaryFile<Tuple<int, int>>(
                AppDomain.CurrentDomain.BaseDirectory + Constant.monitoring_dir + Constant.mean_request_time_file);
            if (time == null)
            {
                time = new Tuple<int, int>(1, seconds);
            }
            else
            {
                time = new Tuple<int, int>(time.Item1 + 1, (time.Item2 + seconds) / time.Item1);
            }

            SaverLoader.WriteToBinaryFile<Tuple<int, int>>(
                AppDomain.CurrentDomain.BaseDirectory + Constant.monitoring_dir + Constant.mean_request_time_file, time);
        }

        private void appendEvolutionTime(int seconds)
        {
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + Constant.monitoring_dir))
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + Constant.monitoring_dir);

            IDictionary<DateTime, int> evolution = SaverLoader.ReadFromBinaryFile<IDictionary<DateTime, int>>(
                AppDomain.CurrentDomain.BaseDirectory + Constant.monitoring_dir + Constant.request_time_evolution_file);
            if(evolution == null)
            {
                evolution = new Dictionary<DateTime, int>();
                evolution.Add(DateTime.Now, seconds);
            }else
            {
                evolution.Add(DateTime.Now, seconds);
            }
            SaverLoader.WriteToBinaryFile <IDictionary<DateTime, int>>(
                AppDomain.CurrentDomain.BaseDirectory + Constant.monitoring_dir + Constant.request_time_evolution_file, evolution);
        }
    }
}