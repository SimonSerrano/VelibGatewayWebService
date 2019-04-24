using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace VelibGatewayWebService.requests
{
    [Serializable]
    public class VelibRequest
    {
        private static readonly string API_KEY = "e561d2fbe2894d1a32eab3672038e981d98cda87";
    

        


        public IList<Station> getStationsForCity(string city)
        {
            WebRequest request = WebRequest.Create("https://api.jcdecaux.com/vls/v1/stations?contract=" + city + "&apiKey=" + API_KEY);
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
                response.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return ParseStation(city,result);

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
                Station station = new Station();
                station.Name = name;
                station.AvailableBikes = available_bikes;
                res.Add(new Station());
            }

            return res;
        }

        public int getAvalaibleBikes(string city, string station)
        {
            List<Station> stations = getStationsForCity(city) as List<Station>;
            Station stationObj = stations.Find(x => x.Name.Equals(station));
            return stationObj.AvailableBikes;
            
        }
    }
}