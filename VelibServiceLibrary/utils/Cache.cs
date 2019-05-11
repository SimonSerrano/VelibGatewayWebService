using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VelibServiceLibrary.utils
{
    [Serializable]
    class Cache
    {

        private Tuple<DateTime, IList<string>> cachedCities;
        private IDictionary<string, Tuple<DateTime, IList<Station>>> cachedStations;
        private TimeSpan duration;


        public Cache()
        {
            cachedCities = null;
            cachedStations = new Dictionary<string, Tuple<DateTime, IList<Station>>>();
            duration = new TimeSpan(0, 2, 0);
        }


        /// <summary>
        /// Checks whether or not the cities are in cache
        /// </summary>
        /// <returns>the list of cities cached</returns>
        public IList<string> checkCities()
        {
            DateTime now = DateTime.Now;

            if(cachedCities == null)
            {
                return null;
            }

            if(duration>now - cachedCities.Item1)
            {
                return cachedCities.Item2;
            }else
            {
                cachedCities = null;
            }

            return null;
        }

        /// <summary>
        /// Cache the cities in the memory
        /// </summary>
        /// <param name="cities">List of cities to cache in server's memory</param>
        public void cacheCities(IList<string> cities)
        {
            cachedCities = new Tuple<DateTime, IList<string>>(DateTime.Now, cities);
        }

        /// <summary>
        /// Checks whether or not the stations are in the server's cache
        /// </summary>
        /// <param name="city">the city to check for the stations</param>
        /// <returns>the list of stations cached</returns>
        public IList<Station> checkStations(string city)
        {
            Tuple<DateTime, IList<Station>> res;
            DateTime now = DateTime.Now;

            if(!cachedStations.TryGetValue(city, out res))
            {
                return null;
            }

            if(duration > now - res.Item1)
            {
                return res.Item2;
            }
            else
            {
                cachedStations.Remove(city);
            }
            return null;
        }

        /// <summary>
        /// Adds a list of stations related to city in the server's cache
        /// </summary>
        /// <param name="city">the city where the sations are</param>
        /// <param name="stations">the stations to cache</param>
        public void cacheStations(string city, IList<Station> stations)
        {
            cachedStations.Add(city, new Tuple<DateTime, IList<Station>>(DateTime.Now, stations));
        }

    }
}
