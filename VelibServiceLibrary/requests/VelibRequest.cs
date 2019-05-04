﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

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
            WebRequest request = WebRequest.Create("https://api.jcdecaux.com/vls/v1/stations?contract_name=" + city + "&apiKey=" + API_KEY);
            StreamReader reader = null;
            WebResponse response = null;
            String result = "";
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
            return ParseStation(city,result);

        }

        /// <summary>
        /// Lists all the stations for a given city, asynchronous
        /// </summary>
        /// <param name="city">the city to get the stations from</param>
        /// <returns>the list of stations for the given city</returns>
        public Task<IList<Station>> getStationsForCityAsync(string city)
        {
            return Task<IList<Station>>.Run(() =>
            {
                return getStationsForCity(city);
            });
        }

        private IList<Station> ParseStation(string city, string data)
        {
            JArray array = null;
            IList<Station> res = new List<Station>();
            try
            {
                array = JArray.Parse(data);
            }catch(Exception e)
            {
                return res;
            }

            int size = array.Count();
            for(int i=0; i<size; ++i)
            {
                string name = (string)((JObject)array[i])["name"];
                int available_bikes = (int)((JObject)array[i])["available_bikes"];
                int station_number = (int)((JObject)array[i])["number"];
                Console.WriteLine(name);
                Station station = new Station();
                station.Name = name;
                station.AvailableBikes = available_bikes;
                station.StationNumber = station_number;
                res.Add(station);
            }

            return res;
        }

        /// <summary>
        /// Get the list of cities from JCDecaux, synchronous
        /// </summary>
        /// <returns>The list of cities</returns>
        public IList<string> GetCities()
        {
            WebRequest request = WebRequest.Create("https://api.jcdecaux.com/vls/v1/contracts?apiKey=" + API_KEY);
            StreamReader reader = null;
            WebResponse response = null;
            String result = "";
            try
            {
                response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                reader = new StreamReader(dataStream);
                result = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
            }catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return ParseCities(result);
        }

        /// <summary>
        /// Get the list of cities from JCDecaux, synchronous
        /// </summary>
        /// <returns>The list of cities</returns>
        public Task<IList<string>> GetCitiesAsync()
        {
            return Task<IList<string>>.Run(() =>
            {
                return GetCities();
            });
        }

        private IList<string> ParseCities(string result)
        {
            JArray array = null;
            IList<String> cities = new List<String>();
            try
            {
                array = JArray.Parse(result);
            }catch (Exception e)
            {
                return cities;
            }
            int size = array.Count();
            for(int i=0; i<size; ++i)
            {
                cities.Add((string)((JObject)array[i])["name"]);
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
            WebRequest request = WebRequest.Create("https://api.jcdecaux.com/vls/v1/stations/" + station_number + "?contract=" + city + "&apiKey=" + API_KEY);
            StreamReader reader = null;
            WebResponse response = null;
            String result = "";
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
            return ParseAvailableBikes(result);
        }

        /// <summary>
        /// Give the available bikes for a given station in a given city, asynchronous
        /// </summary>
        /// <param name="city">the city to get the sation from</param>
        /// <param name="station_number">the given station number which has available bikes</param>
        /// <returns>the number of available bikes at the given station in the given city</returns>
        public Task<int> getAvalaibleBikesAsync(string city, int station_number)
        {
            return Task<int>.Run(() =>
            {
                return getAvalaibleBikes(city, station_number);
            });
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
            available_bikes = (int)jobject["available_bikes"];
            return available_bikes;
        }
    }
}