using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VelibServiceLibrary
{
    public class MonitoringService : IMonitoringService
    {
        private static readonly string mean_request_time_path = "monitoring\\mean_request_time";
        private static readonly string number_request_path = "monitoring\\num_request";

        public int MeanRequestTime(string userIdHashed)
        {
            if (userIdHashed != "f1b9002267f7050b877321cf8810f79c229f47e3d8c84e1f15a36aad4b248d88")
            {
                Console.WriteLine("Try to access private data with bad key : " + userIdHashed);
                return -1;
            }

            Tuple<int, int> res =
                SaverLoader.ReadFromBinaryFile<Tuple<int, int>>(
                    AppDomain.CurrentDomain.BaseDirectory + mean_request_time_path);
            return res == null ? 0 : res.Item2;
        }

        public int NumberOfRequest(string userIdHashed)
        {
            if (userIdHashed != "f1b9002267f7050b877321cf8810f79c229f47e3d8c84e1f15a36aad4b248d88")
            {
                Console.WriteLine("Try to access private data with bad key : " + userIdHashed);
                return -1;
            }
            return SaverLoader.ReadFromBinaryFile<int>(AppDomain.CurrentDomain.BaseDirectory + number_request_path);
        }
    }
}