using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VelibServiceLibrary.utils;

namespace VelibServiceLibrary
{
    public class MonitoringService : IMonitoringService
    {

        public int MeanRequestTime(string userIdHashed)
        {
            if (userIdHashed != "f1b9002267f7050b877321cf8810f79c229f47e3d8c84e1f15a36aad4b248d88")
            {
                Console.WriteLine("Try to access private data with bad key : " + userIdHashed);
                return -1;
            }

            Tuple<int, int> res =
                SaverLoader.ReadFromBinaryFile<Tuple<int, int>>(
                    AppDomain.CurrentDomain.BaseDirectory + Constant.monitoring_dir + Constant.mean_request_time_file);
            return res == null ? 0 : res.Item2;
        }

        public int NumberOfRequest(string userIdHashed)
        {
            if (userIdHashed != "f1b9002267f7050b877321cf8810f79c229f47e3d8c84e1f15a36aad4b248d88")
            {
                Console.WriteLine("Try to access private data with bad key : " + userIdHashed);
                return -1;
            }
            return SaverLoader.ReadFromBinaryFile<int>(AppDomain.CurrentDomain.BaseDirectory + Constant.monitoring_dir + Constant.number_request_file);
        }

        public IDictionary<DateTime, int> RequestTimeEvolution(string userIdHashed)
        {
            if (userIdHashed != "f1b9002267f7050b877321cf8810f79c229f47e3d8c84e1f15a36aad4b248d88")
            {
                Console.WriteLine("Try to access private data with bad key : " + userIdHashed);
                return null;
            }
            
            return SaverLoader.ReadFromBinaryFile<IDictionary<DateTime, int>>(AppDomain.CurrentDomain.BaseDirectory + Constant.monitoring_dir + Constant.request_time_evolution_file);
        }
    }
}