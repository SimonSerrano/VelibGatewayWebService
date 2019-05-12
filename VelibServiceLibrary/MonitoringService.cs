using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VelibServiceLibrary
{
    class MonitoringService : IMonitoringService
    {
        private static readonly string mean_request_time_path = "/monitoring/mean_request_time";
        private static readonly string number_request_path = "/monitoring/num_request";

        public int MeanRequestTime()
        {
            return SaverLoader.ReadFromBinaryFile<Tuple<int, int>>(System.AppDomain.CurrentDomain + mean_request_time_path).Item2;
        }

        public int NumberOfRequest()
        {
            return SaverLoader.ReadFromBinaryFile<int>(System.AppDomain.CurrentDomain + number_request_path);
        }
    }
}
